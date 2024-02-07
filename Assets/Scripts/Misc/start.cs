using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class start : MonoBehaviour
{
    public int sceneValue;
    public Animator anim;

    private void Update()
    {
        if (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed)
        {
            anim.SetTrigger("anyKey");
            Invoke("starting", 1);
        }
    }

    void starting()
    {
        Debug.Log("Abberration Games Logo Played");
        SceneManager.LoadScene(sceneBuildIndex: sceneValue);
    }
}
