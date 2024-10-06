#region

using UnityEngine;

#endregion

namespace Beautify.Universal
{
    [ExecuteInEditMode]
    public class LUTBlending : MonoBehaviour
    {
        public Texture2D LUT1, LUT2;

        [Range(0, 1)] public float LUT1Intensity = 1f;

        [Range(0, 1)] public float LUT2Intensity = 1f;

        [Range(0, 1)] public float phase;

        public Shader lerpShader;
        Material lerpMat;

        float oldPhase = -1;
        RenderTexture rt;

        void LateUpdate()
        {
            UpdateBeautifyLUT();
        }

        void OnEnable()
        {
            UpdateBeautifyLUT();
        }

        void OnDestroy()
        {
            if (rt != null)
            {
                rt.Release();
            }
        }

        void OnValidate()
        {
            oldPhase = -1;
            UpdateBeautifyLUT();
        }

        void UpdateBeautifyLUT()
        {
            if (oldPhase == phase || LUT1 == null || LUT2 == null || lerpShader == null)
            {
                return;
            }

            oldPhase = phase;

            if (rt == null)
            {
                rt = new RenderTexture(LUT1.width, LUT1.height, 0, RenderTextureFormat.ARGB32,
                    RenderTextureReadWrite.Linear);
                rt.filterMode = FilterMode.Point;
            }

            if (lerpMat == null)
            {
                lerpMat = new Material(lerpShader);
            }

            lerpMat.SetTexture(ShaderParams.LUT2, LUT2);
            lerpMat.SetFloat(ShaderParams.Phase, phase);
            Graphics.Blit(LUT1, rt, lerpMat);
            BeautifySettings.settings.lut.Override(true);
            float intensity = Mathf.Lerp(LUT1Intensity, LUT2Intensity, phase);
            BeautifySettings.settings.lutIntensity.Override(intensity);
            BeautifySettings.settings.lutTexture.Override(rt);
        }

        static class ShaderParams
        {
            public static readonly int LUT2 = Shader.PropertyToID("_LUT2");
            public static readonly int Phase = Shader.PropertyToID("_Phase");
        }
    }
}