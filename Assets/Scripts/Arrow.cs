using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] private float rotationSpeed= 100f;
    [SerializeField] private Transform targetToPush;

    private float arrowRotationSpeed = 0f;
    private TouchControll touchControll;
    void Start()
    {
        touchControll = FindObjectOfType<TouchControll>();
        touchControll.OnDirectionChanged += changeArrowDirection;
        touchControll.OnDragEnd += startPushBall;
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * arrowRotationSpeed);
    }


    public void ChangeArrowDirection()
    {
        if (transform.gameObject.activeSelf)
        {
            if (touchControll.MoveDirection == Vector3.right)
            {
                arrowRotationSpeed = -Mathf.Abs(rotationSpeed);
            }
            else if (touchControll.MoveDirection == Vector3.left)
            {
                arrowRotationSpeed = Mathf.Abs(rotationSpeed);
            }
        }
    }

    public void StartPushBall()
    {
        if (transform.gameObject.activeSelf)
        {
            arrowRotationSpeed = 0;
            touchControll.OnDirectionChanged -= changeArrowDirection;
            touchControll.OnDragEnd -= startPushBall;
            FindObjectOfType<BigBall>().PushBallToTarget(targetToPush.position);
        }

    }

    private void startPushBall() => StartPushBall();
    private void changeArrowDirection() => ChangeArrowDirection();
}
