using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    public TMP_InputField EmailInputField
    {
        get => emailInputField;
        set => emailInputField = value;
    }
    [SerializeField] 
    private TMP_InputField emailInputField;

    public TMP_InputField PasswordInputField
    {
        get => passwordInputField;
        set => passwordInputField = value;
    }
    
    [SerializeField] 
    private TMP_InputField passwordInputField;
    
    [SerializeField] 
    private ButtonManager signInButton;
    
    [SerializeField]
    private ButtonManager signUpButton;
    // Start is called before the first frame update
    void Start()
    {
        signUpButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TriggerShowSignUpView();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignIn()
    {
        bool emailInputField = (!string.IsNullOrEmpty((this.emailInputField.text)));
        bool passwordInputField = (!string.IsNullOrEmpty((this.passwordInputField.text)));
        if (!emailInputField && !passwordInputField)
        {
            Debug.Log("Please fill all fields, from Sing In");
            GameManager.Instance.TriggerShowModalMessage("Please fill all fields, from Sing In");
        }
        else
        {
            GameManager.Instance.TriggerSignIn(this.emailInputField.text, this.passwordInputField.text);
        }
        
    }
}
