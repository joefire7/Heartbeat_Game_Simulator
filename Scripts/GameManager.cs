using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIHealthDashboard UIHealthDashboard;
    public HVRAnalysisSystem HVRAnalysisSystem;
    public HeartRateSimulation HeartRateSimulation;
    public ChangeHeartRate ChangeHeartRate;
    public SineWave SineWave;
    public MiXBotAI MixBotAI;
    public APIManager APIManager;

    public event Action<string, string, string> OnSignUpRequested; // Declare the event
    public event Action<string, string> OnSignInRequested;
    public event Action ShowLoginInView;
    public event Action ShowSignUpView;
    public event Action ShowHealthDataGraphicView;
    public event Action ShowMixBotSelectionMenuView;
    public event Action ShowMixBotRegainControlView;
    public event Action<string> ShowModalMessage;
    public ModalWindowView ModalWindowView;
    
    private static GameManager _instance;
    private static readonly object _lock = new object();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<GameManager>();

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<GameManager>();
                            singleton.name = typeof(GameManager).ToString() + " (Singleton)";

                            DontDestroyOnLoad(singleton);
                        }
                    }
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    
    // Method to trigger the sign-up event
    public void TriggerSignUp(string fullname, string email, string password)
    {
        // Safely invoke the event if there are subscribers
        OnSignUpRequested?.Invoke(fullname, email, password);
    }
    
    // Method to trigger the sign-on event
    public void TriggerSignIn(string email, string password)
    {
        // Safely invoke the event if there are subscribers
        OnSignInRequested?.Invoke(email, password);
    }

    public void TriggerShowModalMessage(string message)
    {
        ShowModalMessage?.Invoke(message);
    }

    public void TriggerShowLoginView()
    {
        ShowLoginInView?.Invoke();
    }

    public void TriggerShowSignUpView()
    {
        ShowSignUpView?.Invoke();
    }

    public void TriggerShowHealthDataGraphicView()
    {
        ShowHealthDataGraphicView?.Invoke();
    }

    public void TriggerShowMixBotSelectionMenuView()
    {
        ShowMixBotSelectionMenuView?.Invoke();
    }
    
    public void TriggerShowMixBotRegainControlView()
    {
        ShowMixBotRegainControlView?.Invoke();
    }
}
