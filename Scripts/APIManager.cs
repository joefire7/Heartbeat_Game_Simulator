using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public string GetToken
    {
        get => token;
    }
    [SerializeField]
    string token;
    
    [SerializeField]
    string userId;

    [SerializeField]
    string name;

    [SerializeField]
    string email;
    
    // https://mix-test-backend-ae152106c283.herokuapp.com/
    // protected string signUpUrl = "http://localhost:3000/api/mixSignUp";
    // protected string signInUrl = "http://localhost:3000/api/mixSignIn"; 
    // protected string setHealthDataUrl = "http://localhost:3000/api/mixSetHealthDataByUserId/";
    // protected string getHealthDataUrl = "http://localhost:3000/api/mixGetHealthDataByUserId/";
    
    protected string signUpUrl = "https://mix-test-backend-ae152106c283.herokuapp.com/api/mixSignUp";
    protected string signInUrl = "https://mix-test-backend-ae152106c283.herokuapp.com/api/mixSignIn"; 
    protected string setHealthDataUrl = "https://mix-test-backend-ae152106c283.herokuapp.com/api/mixSetHealthDataByUserId/";
    protected string getHealthDataUrl = "https://mix-test-backend-ae152106c283.herokuapp.com/api/mixGetHealthDataByUserId/";
    // Start is called before the first frame update
    void Start()
    {
        //SignUp("Steven Quiles", "steven@gmail.com", "puertorico1991");
        
        // Subscribe the SignUp and SignIn Events
        GameManager.Instance.OnSignUpRequested += SignUp;
        GameManager.Instance.OnSignInRequested += SignIn;
        //SignIn("crouchrayder7@gmail.com", "zelda1991");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ExecuteHealthDataCoroutine()
    {
         // Run the coroutine to get health data by user ID
            StartCoroutine(GetHealthDataByUserIdCoroutine(userId));
    }

    public void ExecuteSingInCoroutine()
    {
        //SignIn("crouchrayder7@gmail.com", "zelda1991");
        GameManager.Instance.UIHealthDashboard.LoginView.EmailInputField.text = "crouchrayder7@gmail.com";
        GameManager.Instance.UIHealthDashboard.LoginView.PasswordInputField.text = "zelda1991";
        GameManager.Instance.UIHealthDashboard.LoginView.SignIn();
    }
    public void ExecuteSingInEmptyField()
    {
        //SignIn("crouchrayder7@gmail.com", "zelda1991");
        GameManager.Instance.UIHealthDashboard.LoginView.EmailInputField.text = "";
        GameManager.Instance.UIHealthDashboard.LoginView.PasswordInputField.text = "";
        GameManager.Instance.UIHealthDashboard.LoginView.SignIn();
    }
    public void ExecuteSingUpCoroutine()
    {
        SignUp("Elizabeth", "Elizabeth@gmail.com", "dioslove1");
    }
    
    public void ExecuteCloseModalWindowCoroutine()
    {
        //GameManager.Instance.ModalWindowView.ModalCanvas.enabled = false;
        GameManager.Instance.ModalWindowView.gameObject.SetActive(false);
    }

    public void SignUp(string name, string email, string password)
    {
        StartCoroutine(SignUpCoroutine(name, email, password));
    }

    public IEnumerator SignUpCoroutine(string name, string email, string password)
    {
        // Create a JSON object with the user data
        UserData userData = new UserData(name, email, password);

        // Convert the JSON object to JSON string
        string jsonString = JsonUtility.ToJson(userData);
        Debug.Log(jsonString);

        // Create a UnityWebRequest for the POST request method
        UnityWebRequest www = new UnityWebRequest(signUpUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            // handle success response
            Debug.Log("Sing up successful: " + www.downloadHandler.text);
            GameManager.Instance.TriggerShowModalMessage("Sing up successful");

        }else 
        {
            // handle error response
            Debug.LogError("Sign up failed: " + www.error);
            GameManager.Instance.TriggerShowModalMessage("Sign up failed");

            // if there is additional error information in the response
            if(!string.IsNullOrEmpty(www.downloadHandler.text))
            {
                try 
                {
                    // Parse the error response JSON if available
                    var errorResonse = JsonUtility.FromJson<ErrorResponse>(www.downloadHandler.text);
                    Debug.LogError("Error Message: " + errorResonse.message);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing error response: " + ex.Message);
                }
            }
        }
    }

    public void SignIn(string email, string password)
    {
        StartCoroutine(SignInCoroutine(email, password));
    }

    public IEnumerator SignInCoroutine(string email, string password)
    {
        // Create a JSON object with the user data
        SignInData signInData = new SignInData(email, password);

        // Convert the JSON object to a JSON string
        string jsonString = JsonUtility.ToJson(signInData);
        Debug.Log("Sending JSON: " + jsonString);

        // Create a UnityWebRequest for the POST request method
        UnityWebRequest www = new UnityWebRequest(signInUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Handle success response
            Debug.Log("Sign in successful: " + www.downloadHandler.text);
            GameManager.Instance.TriggerShowModalMessage("Sign In successful");
            GameManager.Instance.TriggerShowHealthDataGraphicView();
            // Optionally, extract the token from the response
            var signInResponse = JsonUtility.FromJson<SignInResponse>(www.downloadHandler.text);
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(www.downloadHandler.text);
            Debug.Log("Token: " + signInResponse.token);
            Debug.Log("AuthResponse:" + authResponse.user);
            Debug.Log("Email:" + authResponse.user.email);
            // Store the token for future use
            PlayerPrefs.SetString("jwt_token", signInResponse.token);

             token = authResponse.token;
             userId = authResponse.user.id;
             name = authResponse.user.name;
             this.email = authResponse.user.email;

              // Example call to SetHealthData
        SetHealthDataByUserId(userId, new HealthData 
        { 
            fullName = name, 
            height = 180, 
            weight = 85, 
            heartRateGraph = "path/to/graph.png",
            heartRateHistory = new int[] { 70, 72, 68, 75 },
            stressLevelHistory = new int[] { 3, 4, 2, 3 },
            breathingLevelHistory = new int[] { 15, 16, 14, 17 },
            healthScores = new int[] { 80, 82, 85, 88 }
        });
        
        ExecuteHealthDataCoroutine();

        }
        else
        {
            // Handle error response
            Debug.LogError("Sign in failed: " + www.error);
            GameManager.Instance.TriggerShowModalMessage("Sign In failed: " + www.error);
            // If there is additional error information in the response
            if (!string.IsNullOrEmpty(www.downloadHandler.text))
            {
                try
                {
                    var errorResponse = JsonUtility.FromJson<ErrorResponse>(www.downloadHandler.text);
                    Debug.LogError("Error Message: " + errorResponse.message);
                    GameManager.Instance.TriggerShowModalMessage("Login Error Message: " + errorResponse.message);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing error response: " + ex.Message);
                }
            }
        }
    }


     public void SetHealthDataByUserId(string userId, HealthData healthData)
    {
        StartCoroutine(SetHealthDataCoroutine(userId, healthData));
    }

    public IEnumerator SetHealthDataCoroutine(string userId, HealthData healthData)
    {
        string url = setHealthDataUrl + userId;

        // Convert the HealthData object to a JSON string
        string jsonString = JsonUtility.ToJson(healthData);
        Debug.Log("Sending JSON: " + jsonString);

        // Create a UnityWebRequest for the POST request
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Health data successfully set: " + www.downloadHandler.text);
        }
        else
        {
            HandleError(www);
        }
    }

    
    public IEnumerator GetHealthDataByUserIdCoroutine(string userId)
{
    string url = getHealthDataUrl + userId;

    // Create a UnityWebRequest for the GET request
    UnityWebRequest www = UnityWebRequest.Get(url);
    www.SetRequestHeader("Content-Type", "application/json");

    // Send the request and wait for the response
    yield return www.SendWebRequest();

    if (www.result == UnityWebRequest.Result.Success)
    {
        // Parse the response JSON to a HealthData object or other appropriate class
        string jsonResponse = www.downloadHandler.text;
        Debug.Log("Received health data: " + jsonResponse);

        // Assuming you have a HealthData class to deserialize the JSON into
        //HealthData healthData = JsonUtility.FromJson<HealthData>(jsonResponse);
        // Wrap the array in an object so JsonUtility can handle it
        string wrappedJson = "{\"healthDataArray\":" + jsonResponse + "}";
        HealthDataArrayWrapper wrapper = JsonUtility.FromJson<HealthDataArrayWrapper>(wrappedJson);
        HealthData[] healthDataArray = wrapper.healthDataArray;
        Debug.Log(healthDataArray[0].fullName);
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.FullName = healthDataArray[0].fullName;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.Height = healthDataArray[0].height;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.Weight = healthDataArray[0].weight;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.HeartRateGraph = healthDataArray[0].heartRateGraph;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.HeartRateHistory = healthDataArray[0].heartRateHistory;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.StressLevelHistory = healthDataArray[0].stressLevelHistory;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.BreathingLevelHistory = healthDataArray[0].breathingLevelHistory;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.HealthScores = healthDataArray[0].healthScores;
        GameManager.Instance.UIHealthDashboard.HealthDataGraphicView.SetHealthDataGraphicView();
        // Handle the received health data as needed
    }
    else
    {
        HandleError(www);
    }
}


    private void HandleError(UnityWebRequest www)
    {
         Debug.LogError("Error fetching health data");
         Debug.LogError("Error Code: " + www.responseCode);
         Debug.LogError("Error Message: " + www.error);

            // Check if there is any additional information in the response body
        if (!string.IsNullOrEmpty(www.downloadHandler.text))
        {
            Debug.LogError("Response: " + www.downloadHandler.text);
        }

        if (!string.IsNullOrEmpty(www.downloadHandler.text))
        {
            try
            {
                var errorResponse = JsonUtility.FromJson<ErrorResponse>(www.downloadHandler.text);
                Debug.LogError("Error Message: " + errorResponse.message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing error response: " + ex.Message);
            }
        }
    }


}
