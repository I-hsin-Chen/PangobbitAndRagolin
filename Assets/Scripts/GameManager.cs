using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }
    }
}
