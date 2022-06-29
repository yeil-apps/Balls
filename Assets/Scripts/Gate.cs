using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gate : MonoBehaviour
{
    public enum MathAction
    {
        Minus,
        Plus,
        Multiply,
        Divide
    }
    private Dictionary<MathAction, string> Symbols = new Dictionary<MathAction, string>()
    {
        {MathAction.Minus, "-" },
        {MathAction.Plus, "+" },
        {MathAction.Divide, "%" },
        {MathAction.Multiply, "x" }
    };

    [SerializeField] private List<Gate> linkedGates;
    [SerializeField] MathAction myAction;
    [SerializeField] private int number;

    private BallsManager ballsManager;
    public int Number { get => number; private set => number = value; }
    void Start()
    {
        ballsManager = GameObject.FindObjectOfType<BallsManager>();
        transform.gameObject.GetComponentInChildren<TMP_Text>().text = Symbols[myAction] + number.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            foreach (var gate in linkedGates)
                Destroy(gate);

            ballsManager.SetSpawnPos(transform.position);
            switch (myAction)
            {
                case MathAction.Plus:
                    ballsManager.AddBalls(Number);
                    break;
                case MathAction.Minus:
                    ballsManager.RemoveBalls(Number);
                    break;
                case MathAction.Divide:
                    ballsManager.DivideBalls(Number);
                    break;
                case MathAction.Multiply:
                    ballsManager.MultiplyBalls(Number);
                    break;
                default:
                    throw new ArgumentNullException(); 
            }
            Destroy(gameObject);
        }
    }
}
