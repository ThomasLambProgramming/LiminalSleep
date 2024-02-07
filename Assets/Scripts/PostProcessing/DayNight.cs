using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DayNight : MonoBehaviour
{
    [SerializeField] private Transform _lightContainer = null;
    [SerializeField] private Light _directionalLight = null;
    [SerializeField] private DefaultLighting _preset = null;
    [SerializeField] private Transform[] _parentLights = null;

    [SerializeField, Range(0f, 24f)] private float _timeOfDay = 0;

    [SerializeField, Range(0f, 20f)] private float _daySpeed = 1;
    [SerializeField, Range(0f, 20f)] private float _nightSpeed = 1;

    [SerializeField, Range(0f, 24f)] private float _nightTimeStart = 17.6f;
    [SerializeField, Range(0f, 24f)] private float _nightTimeEnd = 3.9f;

    [SerializeField, Range(0f, 24f)] private float _lightOn = 17.5f;
    [SerializeField, Range(0f, 24f)] private float _lightOff = 6.5f;

    public void Update()
    {
        if (_directionalLight == null || _preset == null)
            return;

        //Will get reference from time system but for example will just calculate it on its own
        if (_timeOfDay < 24 && Application.isPlaying)
        {
            if (_timeOfDay < _nightTimeEnd || _timeOfDay >= _nightTimeStart)
            {
                _timeOfDay += _nightSpeed * Time.deltaTime;
            }
            else
                _timeOfDay += _daySpeed * Time.deltaTime;
            _timeOfDay %= 24f;
        }
        UpdateLighting(_timeOfDay / 24f);

        if (_timeOfDay > _lightOff && _timeOfDay < _lightOn)
        {
            if (_parentLights != null)
            {
                foreach (Transform childLight in _parentLights)
                {
                    childLight.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform childLight in _parentLights)
            {
                childLight.gameObject.SetActive(true);
            }
        }
    }

    //We have the percent so the ambient can be found as the percent through the day (and for lerping that is why it is /24f
    private void UpdateLighting(float a_percent)
    {
        RenderSettings.ambientLight = _preset._ambientColour.Evaluate(a_percent);
        _directionalLight.color = _preset._directionalColour.Evaluate(a_percent);

        RenderSettings.fogColor = _preset._fogColour.Evaluate(a_percent);
        _directionalLight.transform.localRotation = Quaternion.Euler(a_percent * 360f - 90f, 170f, 0);
    }
}
