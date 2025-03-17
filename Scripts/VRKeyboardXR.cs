using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class VRKeyboardXR : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable inputFieldInteractable;
    public TMPro.TMP_InputField inputField;

    private void OnEnable()
    {
        inputFieldInteractable.selectEntered.AddListener(OnInputFieldSelected);
    }

    private void OnDisable()
    {
        inputFieldInteractable.selectEntered.RemoveListener(OnInputFieldSelected);
    }

    private void OnInputFieldSelected(SelectEnterEventArgs args)
    {
        ShowVirtualKeyboard();
    }

    private void ShowVirtualKeyboard()
    {
        // Show the Oculus virtual keyboard
        //OVRManager.instance.ShowVirtualKeyboard();
    }

    private void Update()
    {
        // Update the input field text with the virtual keyboard text
//        if (OVRManager.instance.virtualKeyboard.Visible)
  //      {
    //        inputField.text = OVRManager.instance.virtualKeyboard.GetText();
      //  }
    }
}
