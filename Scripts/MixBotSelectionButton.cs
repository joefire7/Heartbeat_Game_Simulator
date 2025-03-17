using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class MixBotSelectionButton : MonoBehaviour
{
    public UIGradient Gradient;
    public void ChangeGradientColor(float value) => Gradient.Offset = value;
}
