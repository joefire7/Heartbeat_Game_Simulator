using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

public class SignUpView : MonoBehaviour
{
    [SerializeField]
    private InputField fullNameInputField;
    [SerializeField]
    private InputField emailInputField;
    
    [SerializeField]
    private InputField userNameInputField; 
    
    [SerializeField]
    private InputField passwordInputField;

    [SerializeField] 
    private InputField heightInputField;
    
    [SerializeField]
    private InputField wightInputField;
    
    [SerializeField]
    private ButtonManager signUpButton;
    // Start is called before the first frame update
    void Start()
    {
        // signUpButton.onClick.AddListener(() =>
        // {
        //     // bool fullnameField = (!string.IsNullOrEmpty(fullNameInputField.text));
        //     // bool emailField = (!string.IsNullOrEmpty(emailInputField.text));
        //     // bool passworldField = (!string.IsNullOrEmpty(passwordInputField.text));
        //     // if (!fullnameField && !emailField && !passworldField)
        //     // {
        //     //     Debug.Log("Please fill all fields");
        //     //     // Popup Window To tell the user about fill the fields
        //     //     GameManager.Instance.TriggerShowModalMessage("Please fill all fields");
        //     // }
        //     // else
        //     // {
        //     //     GameManager.Instance.TriggerSignUp(fullNameInputField.text, emailInputField.text, userNameInputField.text);
        //     // }
        //     //
        // });
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignUp()
    {
        bool fullnameField = (!string.IsNullOrEmpty(fullNameInputField.text));
        bool emailField = (!string.IsNullOrEmpty(emailInputField.text));
        bool passworldField = (!string.IsNullOrEmpty(passwordInputField.text));
        if (!fullnameField && !emailField && !passworldField)
        {
            Debug.Log("Please fill all fields");
            // Popup Window To tell the user about fill the fields
            GameManager.Instance.TriggerShowModalMessage("Please fill all fields");
        }
        else
        {
            GameManager.Instance.TriggerSignUp(fullNameInputField.text, emailInputField.text, passwordInputField.text);
        }
    }

}
