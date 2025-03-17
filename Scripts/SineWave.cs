using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    [SerializeField]
    HeartRateSimulation heartRateSimulation;

    public int PointCount = 100; // number of points in the sine wave
    public float Amplitude = 1f; // Amplitude of the sine wave
    public float Frequency = 1f; // Frequency of the sine wave
    public float HeartRateBPM = 70; // Heart Rate in beats per minutes

    public LineRenderer LineRenderer;
    public float TimeOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer.positionCount = PointCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the frequency based on Heart Rate BPM
        Frequency = /*HeartRateBPM*/ heartRateSimulation.HeartRateBPM / 60f;

        // Generate the sine wave points 
        for(int i = 0; i < PointCount; i++) 
        {
            float x = i / (float)(PointCount - 1);
            float y = Amplitude * Mathf.Sin((x + TimeOffset) * Frequency * 2 * Mathf.PI);
            LineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }

        // Update time offset to animate the sine wave
        TimeOffset += Time.deltaTime;
    }

    // This method allows updating the heart rate dynamically
    public void UpdateSineWaveHeartRate(float newHeartRateBPM) 
    {
        HeartRateBPM = newHeartRateBPM;
    }
}
