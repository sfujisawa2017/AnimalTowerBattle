using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 targetPos;
    bool movedBefore = false;

    // 移動速さ
    const float MOVE_SPEED = 0.05f;
    // 動物からのYオフセット
    const float OFFSET_FROM_ANIMAL = 6.0f;

    public void StartMove(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        movedBefore = true;
    }

    public bool IsMoving()
    {
        return movedBefore;
    }

    private void Start()
    {
        targetPos = transform.position + new Vector3(0,-OFFSET_FROM_ANIMAL,0);
    }

    private void Update()
    {
        movedBefore = false;

        if (transform.position.y < targetPos.y + OFFSET_FROM_ANIMAL)
        {
            float move = Mathf.Min(targetPos.y + OFFSET_FROM_ANIMAL - transform.position.y, MOVE_SPEED);

            transform.position += new Vector3(0, move, 0);

            movedBefore = true;
        }
    }
}
