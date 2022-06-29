using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 7, -7);
    private float deltaTimeMultiply = 10f;
    private BallsManager ballsManager;

    private void Start()
    {
        ballsManager = FindObjectOfType<BallsManager>();
    }
    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(offset.x, offset.y, ballsManager.GetMainBallPos().z + offset.z);
        if (transform.position != newPosition)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * deltaTimeMultiply);
        }
    }
}
