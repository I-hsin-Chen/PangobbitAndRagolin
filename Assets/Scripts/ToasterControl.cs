using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToasterControl : MonoBehaviour
{
    public Object toast;
    public GameObject door;
    private GameObject bondedToast;
    private PlayerControl playerCtrl;
    private CollisionState collisionState;

    public TMP_Text remainingToastText;
    public int cntJamToast { get; private set; } = 0;

    void Awake(){
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<PlayerControl>(out playerCtrl);
    }

    // Start is called before the first frame update
    void Start()
    {
        bondedToast = GameObject.Find("Toast");
        cntJamToast = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (bondedToast != null) bondedToast.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);

        if (Input.GetKeyDown(KeyCode.I) && bondedToast != null && playerCtrl.isRabbit) {
            bondedToast.GetComponent<BoxCollider2D>().enabled = true;
            bondedToast.GetComponent<ToastControl>().enabled = true;
            bondedToast.GetComponent<JamToastControl>().enabled = true;
            bondedToast.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2.0f, 2.0f), 12.0f);
            StartCoroutine(waitForNewToast());
        }
        remainingToastText.text = "Remaining Jam Toasts : " + (10 - cntJamToast).ToString();
    }

    private IEnumerator waitForNewToast(){
        bondedToast = null;
        yield return new WaitForSeconds(1.5f);
        GameObject newToast = GameObject.Instantiate(toast, Vector3.zero, Quaternion.identity) as GameObject;
        newToast.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
        bondedToast = newToast;
    }

    public void AddJamToast(){
        if (cntJamToast < 10) cntJamToast += 1;
        if(cntJamToast == 10)
            door.GetComponent<Animator>().enabled = true;
    }
}
