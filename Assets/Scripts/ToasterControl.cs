using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterControl : MonoBehaviour
{
    public Object toast;
    private GameObject bondedToast;
    private PlayerControl playerCtrl;
    private CollisionState collisionState;

    void Awake(){
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<PlayerControl>(out playerCtrl);
    }

    // Start is called before the first frame update
    void Start()
    {
        bondedToast = GameObject.Find("Toast");
    }

    // Update is called once per frame
    void Update()
    {
        if (bondedToast != null) bondedToast.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);

        if (Input.GetKeyDown(KeyCode.I) && bondedToast != null && playerCtrl.isRabbit) {
            bondedToast.GetComponent<BoxCollider2D>().enabled = true;
            bondedToast.GetComponent<ToastControl>().enabled = true;
            bondedToast.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2.0f, 2.0f), 12.0f);
            StartCoroutine(waitForNewToast());
        }
    }

    private IEnumerator waitForNewToast(){
        bondedToast = null;
        yield return new WaitForSeconds(1.5f);
        GameObject newToast = GameObject.Instantiate(toast, Vector3.zero, Quaternion.identity) as GameObject;
        newToast.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
        bondedToast = newToast;
    }
}
