using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for DummyObject in DummyScene
// DummyScene is needed to set GameManager and AudioManager as DontDestroyOnLoad GameObjects
// This script simply load the MainScene

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // load next scene, 1 for MainScene currently
        Debug.Log("Load MainScene");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
}
