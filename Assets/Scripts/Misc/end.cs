using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    public int sceneValue;

    void OnEnable()
    {
        Invoke("ending", 3);
    }

    void ending()
    {
        Debug.Log("Back to Menu");
        SceneManager.LoadScene(sceneBuildIndex: sceneValue);
    }
}
