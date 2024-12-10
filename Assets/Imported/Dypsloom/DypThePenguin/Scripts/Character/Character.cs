#region

using System;
using System.Collections;
using System.Threading.Tasks;
using Dypsloom.DypThePenguin.Scripts.Damage;
using Dypsloom.DypThePenguin.Scripts.Items;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    #region

    using CharacterController = CharacterController;

    #endregion

    /// <summary>
    ///     The character controller.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [HideInInspector, Tooltip("Singleton instance of the character controller.")]
        public static Character Instance;

        [SerializeField, Tooltip("Reference to the prompt UI.")]
        public GameObject promptUI;

        public CanvasGroup promptGroup;

        [Tooltip("The character speed in units/second."), SerializeField]
        protected float m_Speed = 1f;
        
        [Tooltip("The character sprinting speed in units/second."), SerializeField]
        protected float m_SprintSpeed = 2f;

        [Tooltip("The gravity."), SerializeField]
        protected float m_Gravity = 1f;

        [Tooltip("Grounded error correction time."), SerializeField]
        protected float m_AdditionalGroundedTime = 0.5f;

        [Tooltip("Grounded Raycast length."), SerializeField]
        protected float m_GroundRaycastLength = 0.3f;

        [Tooltip("The character jump force."), SerializeField]
        protected float m_JumpForce = 1f;

        [Tooltip("The gravity modifier while pressing the jump button."), SerializeField]
        protected float m_JumpFallOff = 1f;

        [Tooltip("The character speed in units/second."), SerializeField]
        protected Transform m_SpawnTransform;

        [Tooltip("The delay between death and respawn."), SerializeField]
        protected float m_RespawnDelay = 3f;

        [Tooltip("The character speed in units/second."), SerializeField]
        protected float m_PushPower = 2.0f;

        [Tooltip("The transform where the projectiles thrown will spawn From."), SerializeField]
        protected Transform m_ProjectilesSpawnPoint;

        [Tooltip("Death Effect."), SerializeField]
        protected GameObject m_DeathEffects;

        public MovementMode m_MovementMode = MovementMode.Free;
        public Animator m_Animator;

        protected UnityEngine.Camera m_Camera;
        protected ICharacterAnimator m_CharacterAnimator;
        protected CharacterController m_CharacterController;
        protected IDamageable m_CharacterDamageable;
        protected ICharacterInput m_CharacterInput;

        protected ICharacterMover m_CharacterMover;
        protected CharacterRotator m_CharacterRotator;
        protected Task m_DeathTask;
        float m_GroundedTimer;
        protected Inventory m_Inventory;
        protected bool m_IsDead;

        protected Rigidbody m_Rigidbody;

        public float Speed
        {
            get { return m_Speed; }
        }
        
        public float SprintSpeed
        {
            get { return m_SprintSpeed; }
        }

        public float JumpForce
        {
            get { return m_JumpForce; }
        }

        public float JumpFallOff
        {
            get { return m_JumpFallOff; }
        }

        public float Gravity
        {
            get { return m_Gravity; }
        }

        public UnityEngine.Camera CharacterCamera
        {
            get { return m_Camera; }
        }

        public Transform ProjectilesSpawnPoint
        {
            get { return m_ProjectilesSpawnPoint; }
        }

        public Rigidbody Rigidbody
        {
            get { return m_Rigidbody; }
        }

        public Animator Animator
        {
            get { return m_Animator; }
        }

        public CharacterController CharacterController
        {
            get { return m_CharacterController; }
        }

        public ICharacterInput CharacterInput
        {
            get { return m_CharacterInput; }
        }

        public IDamageable CharacterDamageable
        {
            get { return m_CharacterDamageable; }
        }

        public ICharacterMover CharacterMover
        {
            get { return m_CharacterMover; }
        }

        public ICharacterAnimator CharacterAnimator
        {
            get { return m_CharacterAnimator; }
        }

        public Inventory Inventory
        {
            get { return m_Inventory; }
        }

        public bool IsDead
        {
            get { return m_IsDead; }
        }

        public bool IsGrounded
        {
            get { return m_GroundedTimer >= Time.time; }
            set { m_GroundedTimer = value ? Time.time + m_AdditionalGroundedTime : 0; }
        }

        /// <summary>
        ///     Initialize all the properties.
        /// </summary>
        protected virtual void Awake()
        {
            Instance = this;
            m_Camera = UnityEngine.Camera.main;

            m_Rigidbody = GetComponent<Rigidbody>();
            m_CharacterController = GetComponent<CharacterController>();
            m_Inventory = GetComponent<Inventory>();
            m_CharacterDamageable = GetComponent<IDamageable>();

            AssignCharacterControllers();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        ///     Tick all the properties which needs to update every frame.
        /// </summary>
        protected virtual void Update()
        {
            if (m_CharacterController.isGrounded)
            {
                IsGrounded = true;
            }
            else if (
                Physics.Raycast(transform.position + transform.up * 0.5f, -1f * transform.up, out RaycastHit hit,
                    m_GroundRaycastLength + 0.5f, int.MaxValue, QueryTriggerInteraction.Ignore))
            {
                if (m_CharacterMover.IsJumping == false)
                {
                    IsGrounded = true;
                }
            }

            m_CharacterMover.Tick();
            m_CharacterRotator.Tick();
            m_CharacterAnimator.Tick();
        }

        /// <summary>
        ///     Move objects when the character controller hits a collider.
        /// </summary>
        /// <param name="hit">The controller collider hit object.</param>
        protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
            {
                return;
            }

            Vector3 force;
            // We use gravity and weight to push things down, we use
            // our velocity and push power to push things other directions
            if (hit.moveDirection.y < -0.3)
            {
                force = (new Vector3(0.1f, -0.5f, 0.1f) + hit.controller.velocity) * m_Rigidbody.mass;
            }
            else
            {
                force = m_CharacterMover.CharacterInputMovement * m_PushPower;
            }

            // Apply the push
            body.AddForceAtPosition(force, hit.point);
        }

        public event Action OnDie;

        /// <summary>
        ///     Assign the controllers for your character.
        /// </summary>
        protected virtual void AssignCharacterControllers()
        {
            m_CharacterMover = new CharacterMover(this);
            m_CharacterRotator = new CharacterRotator(this);
            m_CharacterAnimator = new CharacterAnimator(this);
            m_CharacterInput = new CharacterInput(this);
        }

        /// <summary>
        ///     Make the character die.
        /// </summary>
        public virtual void Die()
        {
            if (m_IsDead || m_DeathTask != null)
            {
                return;
            }

            m_DeathTask = ScheduleDeathRespawn();
        }

        /// <summary>
        ///     Respawn
        /// </summary>
        /// <returns>Return the asynchronous task.</returns>
        protected virtual async Task ScheduleDeathRespawn()
        {
            CharacterAnimator.Die(true);
            m_IsDead = true;

            await Task.Delay(1100);
            m_DeathEffects?.SetActive(true);

            await Task.Delay((int)(m_RespawnDelay * 1000f) - 1600);
            gameObject.SetActive(false);
            OnDie?.Invoke();

            await Task.Delay(500);
            m_DeathTask = null;
            Respawn();
        }

        /// <summary>
        ///     Respawn the character.
        /// </summary>
        protected virtual void Respawn()
        {
            m_DeathEffects?.SetActive(false);
            CharacterAnimator.Die(false);
            m_CharacterDamageable.Heal(int.MaxValue);
            transform.position = m_SpawnTransform != null ? m_SpawnTransform.position : new Vector3(0, 1, 0);
            gameObject.SetActive(true);
            m_IsDead = false;
        }
        
        public IEnumerator LateTeleport(Transform position)
        {
            m_CharacterController.enabled = false;
            m_CharacterController.transform.SetPositionAndRotation(position.position, position.rotation);
            yield return new WaitForEndOfFrame();
            m_CharacterController.enabled = true;
        }
    }

    public enum MovementMode
    {
        RailZ,
        RailX,
        Free,
        Decorating
    }
}