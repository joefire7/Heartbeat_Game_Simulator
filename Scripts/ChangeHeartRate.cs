using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeartRate : MonoBehaviour
{
    private HeartRateSimulation heartRateSimulation;
    public float ChangeInterval = 5f; // Time in seconds between heart rate changes
    public float MinHeartRateBPM = 60f;
    public float MaxHeartRateBPM = 100f;

    public float ElapsedTime = 0f;
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        if(heartRateSimulation == null) heartRateSimulation = GetComponent<HeartRateSimulation>();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= ChangeInterval)
        {
            // Change heart rate to a random value within the specified range
            float newHeartRate = (float)(random.NextDouble() * (MaxHeartRateBPM - MinHeartRateBPM) + MinHeartRateBPM);
            heartRateSimulation.UpdateHeartRate(newHeartRate);

            // Reset elapsed time
            ElapsedTime = 0f;
        }
    }
}
