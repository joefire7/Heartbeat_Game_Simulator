using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HVRAnalysisSystem : MonoBehaviour
{
    [Header("Stress Level / Respiration Rate")]
    public string StressLevel;
    public double RepirationRate;
    [SerializeField]
    HeartRateSimulation heartRateSimulation;

    private List<double> heartRates = new List<double> { 100, 95, 102, 98, 101, 97 };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateHRVAnalysisSystem());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator UpdateHRVAnalysisSystem() 
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            heartRates = heartRateSimulation.BPMHIstory;

            // Convert heart rates to RR intervals
            List<double> rrIntervals = ConvertHRToRRIntervals(heartRates);

            if (rrIntervals.Count == 0)
            {
                Debug.LogError("RR intervals are empty. Ensure heart rates are provided correctly.");
                yield return null;
            }

            // Calculate HRV metrics
            double sdnn = CalculateSDNN(rrIntervals);
            double rmssd = CalculateRMSSD(rrIntervals);

            // Determine stress level
            string stressLevel = DetermineStressLevel(sdnn, rmssd);
            StressLevel = stressLevel;

            // Estimate respiration rate
            double respirationRate = EstimateRespirationRate(rrIntervals);
            RepirationRate = respirationRate;

            Debug.Log($"SDNN: {sdnn}");
            Debug.Log($"RMSSD: {rmssd}");
            Debug.Log($"Stress Level: {stressLevel}");
            Debug.Log($"Estimated Respiration Rate: {respirationRate} breaths per minute");
        }
    }

    // Convert heart rates (in bpm) to RR intervals (in seconds)
    public List<double> ConvertHRToRRIntervals(List<double> heartRates)
    {
        List<double> rrIntervals = new List<double>();
        foreach (var hr in heartRates)
        {
            if (hr > 0)
            {
                double rrInterval = 60.0 / hr;
                rrIntervals.Add(rrInterval);
            }
            else
            {
                Debug.LogError("Heart rate must be greater than 0.");
            }
        }
        Debug.Log($"RR Intervals: {string.Join(", ", rrIntervals)}");
        return rrIntervals;
    }

    // Calculate SDNN (Standard Deviation of NN intervals)
    public double CalculateSDNN(List<double> rrIntervals)
    {
        double mean = rrIntervals.Average();
        double sumOfSquaresOfDifferences = rrIntervals.Select(val => (val - mean) * (val - mean)).Sum();
        double sdnn = Mathf.Sqrt((float)(sumOfSquaresOfDifferences / rrIntervals.Count));
        return sdnn;
    }

    // Calculate RMSSD (Root Mean Square of Successive Differences)
    public double CalculateRMSSD(List<double> rrIntervals)
    {
        double sum = 0;
        for (int i = 1; i < rrIntervals.Count; i++)
        {
            double diff = rrIntervals[i] - rrIntervals[i - 1];
            sum += diff * diff;
        }
        double rmssd = Mathf.Sqrt((float)(sum / (rrIntervals.Count - 1)));
        return rmssd;
    }

    // Estimate respiration rate from RR intervals
    public double EstimateRespirationRate(List<double> rrIntervals)
    {
        double[] heartRate = rrIntervals.Select(rr => 60.0 / rr).ToArray();

        double samplingRate = 1.0 / rrIntervals.Average();
        var (a, b) = ButterworthBandpassFilter.DesignButterworthBandpassFilter(2, 0.1, 0.3, samplingRate);
        double[] filteredHR = ButterworthBandpassFilter.ApplyFilter(heartRate, a, b);

        List<int> peaks = DetectPeaks(filteredHR);

        if (peaks.Count < 2)
        {
            Debug.LogError("Not enough peaks detected to estimate respiration rate.");
            return 0;
        }

        double[] peakIntervals = new double[peaks.Count - 1];
        for (int i = 1; i < peaks.Count; i++)
        {
            peakIntervals[i - 1] = peaks[i] - peaks[i - 1];
        }
        double averageInterval = peakIntervals.Average();
        double respirationRate = 60.0 / averageInterval;

        return respirationRate;
    }

    // Detect peaks in the signal
    private List<int> DetectPeaks(double[] signal)
    {
        List<int> peaks = new List<int>();
        for (int i = 1; i < signal.Length - 1; i++)
        {
            if (signal[i] > signal[i - 1] && signal[i] > signal[i + 1])
            {
                peaks.Add(i);
            }
        }
        return peaks;
    }

    // Determine stress level based on HRV metrics
    public string DetermineStressLevel(double sdnn, double rmssd)
    {
        if (sdnn < 50 || rmssd < 30)
        {
            return "High Stress Level";
        }
        else
        {
            return "Low Stress Level";
        }
    }
}
