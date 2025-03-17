using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class MixBotSelectionMenuView : MonoBehaviour
{
    [SerializeField] 
    private ButtonManager[] buttonsManager;

    [SerializeField] 
    private ButtonManager nextButton;
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (var button  in buttonsManager)
        {
            button.onClick.AddListener(() =>
            {
                MixBotSelectionButton buttonComponent = button.GetComponent<MixBotSelectionButton>();
                if (buttonComponent != null)
                {
                    buttonComponent.ChangeGradientColor(1.0f);
                    nextButton.Interactable(true);
                }
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TriggerShowMixBotRegainControlView();
        });
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var button  in buttonsManager)
        {
            button.onClick.AddListener(() =>
            {
                MixBotSelectionButton buttonComponent = button.GetComponent<MixBotSelectionButton>();
                if (buttonComponent != null)
                {
                    buttonComponent.ChangeGradientColor(1.0f);
                    nextButton.Interactable(true);
                }
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TriggerShowMixBotRegainControlView();
        });
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
