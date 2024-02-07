using UnityEngine;

namespace CoreFunctions
{
    /// <summary>
    /// This library is for commonly used functions that allow for simpler writing of code
    /// Mainly for designers to understand what each function does and
    /// for Thomas to make cleaner code
    /// </summary>
    public class FunctionLibrary
    {
        public static void PerformLerp(
            ref float _timer, 
            out bool _finished, 
            bool _lerpIn = true, 
            float _lerpSpeedIn = 2f, 
            float _lerpSpeedOut = 2f)
        {
            _finished = false;
            if (_lerpIn)
            {
                _timer += Time.deltaTime * _lerpSpeedIn;

                if (_timer >= 1)
                {
                    _finished = true;
                    _timer = 1;
                }
            }
            else
            {
                _timer -= Time.deltaTime * _lerpSpeedOut;

                if (_timer <= 0)
                {
                    _timer = 0;
                    _finished = true;
                }
            }
        }
    }
}
