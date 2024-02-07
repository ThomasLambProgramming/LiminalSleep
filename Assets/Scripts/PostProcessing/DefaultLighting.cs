using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DayNight Setting", menuName = "DayNightSetting", order = 1)]
public class DefaultLighting : ScriptableObject
{
    public Gradient _ambientColour;
    public Gradient _directionalColour;
    public Gradient _fogColour;
}
