using UnityEngine;

public class BigPin : MonoBehaviour
{
    private Score score;
    private bool isTouched = false;
    private Vector3 startPos;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (isTouched== false && Vector3.Distance(startPos,transform.position)>1)
        {
            score.AddScore();
            isTouched = true;
        }
    }
}
