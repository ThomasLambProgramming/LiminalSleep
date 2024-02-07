using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AberrationGames.ACN
{
    public class SceneControl : MonoBehaviour
    {
        public int sceneValue;

        public void changeScene()
        {
            SceneManager.LoadScene (sceneBuildIndex:sceneValue);
        }

        public void Exit()
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
