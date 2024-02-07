using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

public class InGameMenu : MonoBehaviour
{

    public GameObject menuCanvas;

    public bool settingsEnabled = false;

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !settingsEnabled)
            EnableSettings();
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && settingsEnabled)
            DisableSettings();
    }

    public void EnableSettings()
    {
        settingsEnabled = true;
        menuCanvas.SetActive(true);
        Debug.Log("Settings Enabled");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableSettings()
    {
        settingsEnabled = false;
        menuCanvas.SetActive(false);
        Debug.Log("Settings Disabled");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        int xPos = 0, yPos = 0;
        SetCursorPos(xPos, yPos);
    }
}
