using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameSceneScript : MonoBehaviour
{
    public string nextSceneName;
    public void nextScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
