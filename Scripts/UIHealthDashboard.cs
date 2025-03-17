using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using TMPro;
using UnityEngine;

public class UIHealthDashboard : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] string name;
    [SerializeField] float height;
    [SerializeField] int weight;
    [SerializeField] uint scorePoints;
    [SerializeField] uint heartRateBPM;
    [SerializeField] uint respiratoryRate;
    [SerializeField] string StressLevel;
    [SerializeField] List<MiXNotification> miXNotifications;

    [SerializeField] TMP_Text currentHeartRatesText;
    [SerializeField] HeartRateSimulation heartRateSimulation;
    [SerializeField] HVRAnalysisSystem hVRAnalysisSystem;

    [Header("VIEWS")] 
    public ModalWindowView ModalWindowView;
    public LoginView LoginView;
    public SignUpView SignUpView;
    public HealthDashboardView HealthDashboardView;
    public HealthDataGraphicView HealthDataGraphicView;
    public DialogSystem DialogSystemView;
    public MixBotSelectionMenuView MixBotSelectionMenuView;
    public MixBotRegainControlView MixBotRegainControlView;
    
    public SineWave SinWave
    {
        get => sineWave;
        set => sineWave = value;
    }
    private SineWave sineWave;
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe Events
        GameManager.Instance.ShowLoginInView += ShowLoginView;
        GameManager.Instance.ShowSignUpView += ShowSingUpView;
        GameManager.Instance.ShowHealthDataGraphicView += ShowHealthDataGraphicView;
        GameManager.Instance.ShowModalMessage += DisableSineWave;
        GameManager.Instance.ShowMixBotSelectionMenuView += ShowMixBotSelectionMenuView;
        GameManager.Instance.ShowMixBotRegainControlView += ShowMixBotRegainControlView;
        sineWave = GetComponent<SineWave>();
    }

    // Update is called once per frame
    void Update()
    {
        float trucatedCurrentHeartRates = Mathf.Floor(heartRateSimulation.HeartRateBPM);
        heartRateBPM = (uint)trucatedCurrentHeartRates;
        currentHeartRatesText.text = "Current Heart Rates: " + trucatedCurrentHeartRates.ToString();
        HealthDataGraphicView.StressLevel = hVRAnalysisSystem.StressLevel;
        HealthDataGraphicView.BreathingLevel = (int)hVRAnalysisSystem.RepirationRate;
    }

    public void ShowLoginView()
    {
        LoginView.gameObject.SetActive(true);
        SignUpView.gameObject.SetActive(false);
        MixBotSelectionMenuView.gameObject.SetActive(false);
        MixBotRegainControlView.gameObject.SetActive(false);
        HealthDataGraphicView.gameObject.SetActive(false);
        sineWave.LineRenderer.gameObject.SetActive(false);
    }
    
    public void ShowSingUpView()
    {
        SignUpView.gameObject.SetActive(true);
        LoginView.gameObject.SetActive(false);
        MixBotSelectionMenuView.gameObject.SetActive(false);
        MixBotRegainControlView.gameObject.SetActive(false);
        HealthDataGraphicView.gameObject.SetActive(false);
        sineWave.LineRenderer.gameObject.SetActive(false);
    }

    public void ShowHealthDataGraphicView()
    {
        LoginView.gameObject.SetActive(false);
        SignUpView.gameObject.SetActive(false);
        MixBotSelectionMenuView.gameObject.SetActive(false);
        MixBotRegainControlView.gameObject.SetActive(false);
        sineWave.LineRenderer.gameObject.SetActive(true);
        HealthDataGraphicView.gameObject.SetActive(true);
    }

    public void ShowMixBotSelectionMenuView()
    {
        HealthDataGraphicView.gameObject.SetActive(false);
        LoginView.gameObject.SetActive(false);
        SignUpView.gameObject.SetActive(false);
        sineWave.LineRenderer.gameObject.SetActive(false);
        MixBotSelectionMenuView.gameObject.SetActive(true);
    }
    
    public void ShowMixBotRegainControlView()
    {
        HealthDataGraphicView.gameObject.SetActive(false);
        LoginView.gameObject.SetActive(false);
        SignUpView.gameObject.SetActive(false);
        sineWave.LineRenderer.gameObject.SetActive(false);
        MixBotSelectionMenuView.gameObject.SetActive(false);
        MixBotRegainControlView.gameObject.SetActive(true);
    }

    public void DisableSineWave(string msg)
    {
        sineWave.LineRenderer.gameObject.SetActive(false);
    }
    
    //GameManager.Instance.APIManager.ExecuteHealthDataCoroutine();
}
