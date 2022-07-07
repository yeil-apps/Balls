using UnityEngine;

public class BallsManager : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Vector3 mainBallPosition;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballsParent;

    [SerializeField] private float speed;
    public float Speed { get => speed; private set => speed = value; }

    public float DeadZoneY = -2f;
    [SerializeField] public float FinishUiDelay = 4f;

    public delegate void SpeedChanged();
    public event SpeedChanged OnSpeedChanged;
    public void ChangeSpeed(float newSpeed)
    {
        Speed = newSpeed;
        OnSpeedChanged();
    }

    public void AddBalls(int count)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
                Instantiate(ballPrefab, spawnPosition, Quaternion.identity, ballsParent.transform);
        }
    }

    public void RemoveBalls(int count)
    {
        if (ballsParent.transform.childCount <= count)
            count = ballsParent.transform.childCount - 1;

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Destroy(ballsParent.transform.GetChild(i).gameObject);
            }
        }
    }

    public void MultiplyBalls(int count)
    {
        if (count > 1)
        {
            var countToAdd = ballsParent.transform.childCount * (count - 1);
            for (int i = 0; i < countToAdd; i++)
                Instantiate(ballPrefab, spawnPosition, Quaternion.identity, ballsParent.transform);
        }
    }

    public void DivideBalls(int count)
    {
        if (count > 0)
        {
            int remainder;
            int ballsCount = ballsParent.transform.childCount;

            if (ballsCount < count)
                remainder = 1;
            else remainder = ballsCount / count;
            
            int countToDelete = ballsCount - remainder;
            for (int i = 0; i < countToDelete; i++)
            {
                Destroy(ballsParent.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SetSpawnPos(Vector3 position)
    {
        spawnPosition = position;
    }

    public void CheckEndGame()
    {
        var ball = FindObjectOfType<Ball>();
        var bigBall = FindObjectOfType<BigBall>();
        if (ball == null && bigBall == null)
            FindObjectOfType<UI>().ShowFinishUIWithDelay();
    }
    public Vector3 GetMainBallPos()
    {
        if (ballsParent.transform.childCount == 0)
            return mainBallPosition;
        else
        {
            mainBallPosition = ballsParent.transform.GetChild(0).transform.position;
            return mainBallPosition;
        }
    }
}
