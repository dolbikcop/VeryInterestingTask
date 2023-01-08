using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

struct PlayerInfo
{
    public string Name;
    public int Score;
    public int BonusPoints;
    public int PenaltyPoints;
    public int MoveCount;

    public PlayerInfo(string name)
    {
        Name = name;
        Score = 0;
        BonusPoints = 0;
        PenaltyPoints = 0;
        MoveCount = 0;
    }
}
public class Player : MonoBehaviour
{
    private PlayerInfo Info;
    
    public string Name => Info.Name;
    public int currentCell => Info.Score;
    public int BonusPoints => Info.BonusPoints;
    public int PenaltyPoints => Info.PenaltyPoints;
    public int MoveCount => Info.MoveCount;

    public bool isFinished = false;
    
    [SerializeField]private TextMeshProUGUI nameLabel;
    
    public void Initialize(string name = "Player")
    {
        nameLabel.text = name;
        Info = new PlayerInfo(name);
        NMAgent = GetComponent<NavMeshAgent>();
    }
    
    private List<Cell> cells => Cell.AllCells;
    public void Move(int score)
    {
        Info.MoveCount++;
        
        Info.Score = score + currentCell;

        Info.Score = Math.Clamp(currentCell, 0, cells.Count - 1);

        var c = cells[currentCell];
        
        cellToMove.Enqueue(c);
        
        isFinished = currentCell == cells.Count - 1;

        if (!isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(OneMove());   
        }
        
        if (c.GetStatus == CellStatus.Positive) Info.BonusPoints++;
        else if (c.GetStatus == CellStatus.Negative) Info.PenaltyPoints++;
    }

    private Queue<Cell> cellToMove = new Queue<Cell>();
    public bool isMoving = false;
    private NavMeshAgent NMAgent;

    IEnumerator OneMove()
    {
        isMoving = true;
        while (cellToMove.Count != 0)
        {
            var cell = cellToMove.Dequeue();

            NMAgent.SetDestination(cell.position);
            
            yield return new WaitWhile(() => (cell.position - transform.position).magnitude > 0.5f);
            print(cell);
            cell.AddPlayer();
            transform.SetParent(cell.transform);
        }
        isMoving = false;
    }

    public void EnterName(string name)
    {
        Info.Name = name;
    }
}
