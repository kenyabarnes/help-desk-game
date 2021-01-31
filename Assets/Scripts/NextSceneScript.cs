using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    public string nextSceneName;
    public void nextScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    void Update() {
        if (Input.anyKeyDown) {
            nextScene(nextSceneName);
        }
    }
}
