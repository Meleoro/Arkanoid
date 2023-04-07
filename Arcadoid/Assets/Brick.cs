using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public BrickManager refBrickManager;

    public void MoveBrick(Vector2 velocity)
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
    }

    public void DestroyBrick()
    {
        refBrickManager.brickList.Remove(this);
        
        Destroy(gameObject);
    }
}
