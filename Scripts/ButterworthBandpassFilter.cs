using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterworthBandpassFilter : MonoBehaviour
{
    public static (double[] a, double[] b) DesignButterworthBandpassFilter(int order, double lowCut, double highCut, double samplingRate)
    {
        double nyquist = 0.5 * samplingRate;
        double low = lowCut / nyquist;
        double high = highCut / nyquist;

        // Design coefficients
        var (a, b) = ButterworthBandpassCoefficients(order, low, high);

        return (a, b);
    }

    private static (double[] a, double[] b) ButterworthBandpassCoefficients(int order, double low, double high)
    {
        int n = 2 * order;
        double[] a = new double[n + 1];
        double[] b = new double[n + 1];

        // Dummy coefficients, replace with actual Butterworth design
        a[0] = 1.0;
        b[0] = 1.0;

        return (a, b);
    }

    public static double[] ApplyFilter(double[] signal, double[] a, double[] b)
    {
        double[] filteredSignal = new double[signal.Length];
        double[] temp = new double[a.Length];

        for (int i = 0; i < signal.Length; i++)
        {
            filteredSignal[i] = b[0] * signal[i];

            for (int j = 1; j < a.Length; j++)
            {
                if (i - j >= 0)
                {
                    filteredSignal[i] += b[j] * signal[i - j] - a[j] * temp[i - j];
                }
            }

            temp[i % a.Length] = filteredSignal[i];
        }

        return filteredSignal;
    }
}
