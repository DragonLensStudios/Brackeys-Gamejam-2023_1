using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMusicPlayer : MonoBehaviour
{
    public List<string> musicList = new List<string>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(musicList.Count > 0)
        {
            if (scene.buildIndex > 0)
            {
                AudioManager.instance.StopMusic(musicList[scene.buildIndex - 1]);
            }
            AudioManager.instance.PlayMusic(musicList[scene.buildIndex]);
        }
    }
}
