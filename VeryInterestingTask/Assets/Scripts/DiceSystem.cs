using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DiceSystem : MonoBehaviour
{
    [SerializeField] private GameObject dice;

    public TurnEvent StartTurn;
    private Rigidbody rb => dice.GetComponent<Rigidbody>();

    private int diceResult;
    private Vector3 startPosition;
    public static bool isMakeTurn;

    public static bool CanMakeTurn = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanMakeTurn)
        {
            ThrowDice();
        }
    }

    private void Start() => startPosition = dice.transform.position;

    void ThrowDice()
    {
        if (!isMakeTurn)
        {
            diceResult = 0;

            var dirX = Random.Range(0, 500);
            var dirY = Random.Range(0, 500);
            var dirZ = Random.Range(0, 500);
        
            dice.transform.SetPositionAndRotation(startPosition, 
                Quaternion.identity);
        
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
            isMakeTurn = true;   
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var velocity = rb.velocity;

        if (velocity == Vector3.zero)
        {
            switch (other.gameObject.name)
            {
                case "Side1":
                    diceResult = 1;
                    break;
                case "Side2":
                    diceResult = 2;
                    break;
                case "Side3":
                    diceResult = 3;
                    break;
                case "Side4":
                    diceResult = 4;
                    break;
                case "Side5":
                    diceResult = 5;
                    break;
                case "Side6":
                    diceResult = 6;
                    break;
            }

            if (isMakeTurn && diceResult != 0)
            {
                isMakeTurn = false;

                print(diceResult);
                StartTurn.Invoke(diceResult);
            }
        }
    }
}
[System.Serializable]
public class TurnEvent : UnityEvent<int> {}
