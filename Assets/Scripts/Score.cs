using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int ScoreCount { get; private set; }

    public void AddScore()
    {
        ScoreCount++;
    }
}
