using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text modalMessageText;

    public Canvas ModalCanvas
    {
        get => modalCanvas;
        set => modalCanvas = value;
    }
    [SerializeField]
    private Canvas modalCanvas;
    [SerializeField] 
    private GameObject modalWindowStyle;
    [SerializeField] 
    private ModalWindowManager modalWindowManager;
    
    // Continue Button
    public ButtonManager ContinueButton;
    // Raycast of all Views
    public GraphicRaycaster [] GraphicRaycasterViewers;
    private void Awake()
    {
        //modalWindowStyle.SetActive(true);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        GameManager.Instance.ShowModalMessage += ShowModalMessage;
        ContinueButton.onClick.AddListener(() =>
        {
            Continue();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ShowModalMessage(string message)
    {
        this.gameObject.SetActive(true);
        foreach (var viewer in GraphicRaycasterViewers)
        {
            viewer.enabled = false;
        }
        modalMessageText.text = message;
        //modalWindowManager.descriptionText = message;
        //modalCanvas.enabled = true;
    }

    public void Continue()
    {
        foreach (var viewer in GraphicRaycasterViewers)
        {
            viewer.enabled = true;
        }
        
        this.gameObject.SetActive(false);
    }
}
