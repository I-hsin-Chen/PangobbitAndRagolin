using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirControl : MonoBehaviour
{
    private FacingDirection FaceDir;
    // Start is called before the first frame update
    void Start()
    {
        FaceDir = GetComponent<FacingDirection>();
        FaceDir.SetDirection(-1);
    }

}
