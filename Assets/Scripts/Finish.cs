using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject bigBall;
    [SerializeField] private int ballsPushPower = 50;
    [SerializeField] private int ballsCountPerSec = 5;
    private bool isEnd = false;
    void Start()
    {
        bigBall.SetActive(false);
    }
    void Update()
    {
        if (isEnd == false)
        {
            var balls = GameObject.FindObjectsOfType<Ball>();
            if (bigBall.activeSelf && balls.Length > 0)
            {
                var i = 0;
                foreach (var ball in balls)
                {
                    var forceVector = bigBall.transform.position - ball.transform.position;
                    ball.GetComponent<Rigidbody>().AddForce(forceVector * ballsPushPower, ForceMode.Impulse);
                    if (i > ballsCountPerSec)
                        break;
                    else
                        i++;
                }
            }
            else if (bigBall.activeSelf && balls.Length == 0)
            {
                isEnd = true;
                bigBall.GetComponent<BigBall>().ActiveArrow();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            bigBall.SetActive(true);
            GameObject.FindObjectOfType<BallsManager>().ChangeSpeed(0);
        }
    }
}
