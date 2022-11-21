using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingDirection : MonoBehaviour
{
    public enum Direction {
        Left, Right
    }
    [SerializeField]
    private Direction initialDirection;
    private Direction direction;
    public bool isFacingAwayFromInitialDirection => direction != initialDirection;
    private void Awake() {
        direction = initialDirection;
    }
    // public void SetDirection(float xAxis){
    //     if(xAxis > 0) direction = Direction.Right;
    //     else if(xAxis < 0) direction = Direction.Left;
    // }

    public void SetDirection(int x){
        if (x == 1) direction = Direction.Right;
        else direction = Direction.Left;
    }

    public float GetDirection(){
        if (isFacingAwayFromInitialDirection) return -1.0f;
        else return 1.0f;
    }
}
