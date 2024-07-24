using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pronouns : MonoBehaviour
{

    public string PlayerName;
    public string Pronouns1;
    public string Pronouns2;

    public TMP_InputField nameField;
    public TMP_InputField pronoun1Field;
    public TMP_InputField pronoun2Field;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void SubmitName()
    {
        if(string.IsNullOrEmpty(nameField.text) == false)
        {
            PlayerName = nameField.text;

            ProfileSystem.Set(ProfileSystem.Variable.PlayerName, PlayerName);
            Debug.Log("PLAYER NAME, " + ProfileSystem.Get<string>(ProfileSystem.Variable.PlayerName));
        }

    }

    public void SubmitPronoun1()
    {
        if (string.IsNullOrEmpty(pronoun1Field.text) == false)
        {
            Pronouns1 = pronoun1Field.text;

            ProfileSystem.Set(ProfileSystem.Variable.PlayerPronoun1, Pronouns1);
            Debug.Log("PLAYER PRONOUNS, " + ProfileSystem.Get<string>(ProfileSystem.Variable.PlayerPronoun1));
        }
    }

    public void SubmitPronoun2()
    {
        if (string.IsNullOrEmpty(pronoun2Field.text) == false)
        {
            Pronouns2 = pronoun2Field.text;

            ProfileSystem.Set(ProfileSystem.Variable.PlayerPronoun2, Pronouns2);
            Debug.Log("PLAYER PRONOUNS, " + ProfileSystem.Get<string>(ProfileSystem.Variable.PlayerPronoun2));
        }
    }

}
