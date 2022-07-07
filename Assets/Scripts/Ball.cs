using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float magneticForce = 0.02f;
    [SerializeField] private int PushPowerZ = 50;
    [SerializeField] private int PushPowerY = 15;
    [SerializeField] private GameObject destroyEffect;

    private float speed;
    private Vector3 ballDirection = Vector3.forward;


    private TouchControll touchControll;
    private BallsManager ballsManager;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        touchControll = FindObjectOfType<TouchControll>();
        touchControll.OnDirectionChanged += changeDirection;

        ballsManager = FindObjectOfType<BallsManager>();
        ballsManager.OnSpeedChanged += changeSpeed;
        speed = ballsManager.Speed;
    }
   
    void FixedUpdate()
    {

        rigidBody.velocity = new Vector3(ballDirection.x * speed, rigidBody.velocity.y, ballDirection.z * speed);
        var magneticVector = ballsManager.GetMainBallPos();
        var distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(magneticVector.x, 0, magneticVector.z));
        if (magneticVector != null && Mathf.Abs(transform.position.y - magneticVector.y) < 1.2f && distance > 1.2f)
        {
            magneticVector = new Vector3(magneticVector.x - transform.position.x, 0, magneticVector.z - transform.position.z);
            rigidBody.AddForce(magneticVector.normalized * magneticForce, ForceMode.Acceleration);
        }

    }

    private void Update()
    {
        if (ballsManager.DeadZoneY > transform.position.y)
            Destroy(gameObject);
    }

    public void ChangeBallDirection()
    {
        ballDirection = Vector3.forward + touchControll.MoveDirection;
    }

    public void ChangeBallSpeed()
    {
        speed = ballsManager.Speed;
    }

    public void OnDestroy()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        touchControll.OnDirectionChanged -= changeDirection;
        ballsManager.OnSpeedChanged -= changeSpeed;
        ballsManager.CheckEndGame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(transform.gameObject);
        }
        else if (collision.gameObject.GetComponent<Pin>() != null)
        {
            var pinObj = collision.gameObject;
            Destroy(pinObj.GetComponent<Pin>());
            Destroy(transform.gameObject);
            Vector3 forceVector = collision.GetContact(0).normal;
            pinObj.GetComponent<Rigidbody>().AddForce(-forceVector * PushPowerZ + Vector3.up * PushPowerY, ForceMode.Impulse);
            Destroy(pinObj, 2f);
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        ballDirection = newDirection;
    }


    private void changeDirection() => ChangeBallDirection();
    private void changeSpeed() => ChangeBallSpeed();
}
