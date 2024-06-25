using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 5.0f;
    private float moveHorizontal;
    private float moveVertical;
    
    public void MoveHorizontal(float horizontalValue)
    {
        moveHorizontal = horizontalValue;
        transform.Translate(Vector3.right * moveHorizontal * speed * Time.deltaTime);
    }

    public void MoveVertical(float moveVerticalValue)
    {
        moveVertical = moveVerticalValue;
        transform.Translate(Vector3.forward * moveVertical * speed * Time.deltaTime);
    }
}