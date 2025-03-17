using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

}

[System.Serializable]
public class UserData
{
    public string name;
    public string email;
    public string password;

    public UserData(string name, string email, string password)
    {
        this.name = name;
        this.email = email;
        this.password = password;
    }
}


[System.Serializable]
public class SignInData
{
    public string email;
    public string password;

    public SignInData(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

// Define a class for the sign-in response
[System.Serializable]
public class SignInResponse
{
    public string token;
}

[System.Serializable]
public class ErrorResponse
{
    public string message;
}

// Class to represent the health data being sent
[System.Serializable]
public class HealthData
{
    public string fullName;
    public int height;
    public int weight;
    public string heartRateGraph;
    public int[] heartRateHistory;
    public int[] stressLevelHistory;
    public int[] breathingLevelHistory;
    public int[] healthScores;
}

[System.Serializable]
public class HealthDataArrayWrapper
{
    public HealthData[] healthDataArray;
}


[System.Serializable]
public class AuthResponse
{
    public string token;
    public User user;

    public AuthResponse(string token, User user)
    {
        this.token = token;
        this.user = user;
    }
}

[System.Serializable]
public class User
{
    public string id;
    public string name;
    public string email;

    public User(string id, string name, string email)
    {
        this.id = id;
        this.name = name;
        this.email = email;
    }
}
