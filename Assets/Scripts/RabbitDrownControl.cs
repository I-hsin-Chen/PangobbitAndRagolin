using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDrownControl : MonoBehaviour
{
    private PlayerControl rabbitControl;
    private WaterLevelControl waterCtrl;

    private GameObject rabbit;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rabbit = GameObject.Find("Rabbit");
        rabbitControl = rabbit.GetComponent<PlayerControl>();
        waterCtrl = GameObject.Find("Water").GetComponent<WaterLevelControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rabbit.transform.position.y < waterCtrl.getDrowningLevel() && !rabbitControl.isDrowning){
            rabbitControl.SetIsDrowning();
            StartCoroutine(ScheduleReload());
        }
    }

    public IEnumerator ScheduleReload(){
        yield return new WaitForSeconds(2.0f);
        // reload this scene
        gameManager.ChangeSceneTo(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
