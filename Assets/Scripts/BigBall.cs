using UnityEngine;

public class BigBall : MonoBehaviour
{
    [SerializeField] private float sizePointForBall = 0.05f;
    [SerializeField] private float pushPower = 50f;
    [SerializeField] private GameObject arrow;
    public int Size { get; private set; }
    private Rigidbody rigidBody;
    private void Start()
    {
        Size = 1;
        arrow.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Destroy(ball.transform.gameObject);
            Size++;
            transform.localScale = new Vector3(transform.localScale.x + sizePointForBall, transform.localScale.y + sizePointForBall, transform.localScale.z + sizePointForBall);
            transform.position = new Vector3(transform.position.x, transform.position.y + sizePointForBall/4, transform.position.z);
        }
    }

    public void ActiveArrow()
    {
        arrow.SetActive(true);
    }
    public void PushBallToTarget(Vector3 target)
    {
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        rigidBody.mass = Size;
        rigidBody.AddForce((target - transform.position).normalized * pushPower * Size, ForceMode.Impulse);
        arrow.SetActive(false);
        FindObjectOfType<UI>().ShowFinishUIWithDelay();
    }
}
