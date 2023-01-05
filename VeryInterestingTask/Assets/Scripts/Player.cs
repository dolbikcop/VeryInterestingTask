using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
struct PlayerInfo
{
    public string Name;
    public int Score;
    public int BonusPoints;
    public int PenaltyPoints;

    public PlayerInfo(string name)
    {
        Name = name;
        Score = 0;
        BonusPoints = 0;
        PenaltyPoints = 0;
    }
}
public class Player : MonoBehaviour
{
    private PlayerInfo Info;
    
    public string Name => Info.Name;
    public int currentCell => Info.Score;
    public int BonusPoints => Info.BonusPoints;
    public int PenaltyPoints => Info.PenaltyPoints;

    public bool isFinished = false;
    
    [SerializeField]private TextMeshProUGUI nameLabel;
    [SerializeField]private int speed = 1;
    private bool isMoving = false;
    
    public void Initialize(string name = "Player")
    {
        nameLabel.text = name;
        Info = new PlayerInfo(name);
    }
    
    private List<Cell> cells => Cell.AllCells;
    public void Move(int score)
    {
        cells[currentCell].RemovePlayer();

        Info.Score = score + currentCell;
        
        isFinished = currentCell >= cells.Count - 1;

        Info.Score = Math.Clamp(currentCell, 0, cells.Count - 1);
        
        var c = cells[currentCell];
        
        StartCoroutine(OneMove(c));
        
        if (c.GetStatus == CellStatus.Positive) Info.BonusPoints++;
        else if (c.GetStatus == CellStatus.Negative) Info.PenaltyPoints++;
    }

    IEnumerator OneMove(Cell cell)
    {
        yield return new WaitWhile(() => isMoving);

        isMoving = true;
        
        var target = cell.position;
        var dir = (target - transform.position).normalized;
        while ((target - transform.position).magnitude > 0.05)
        {
            transform.Translate(dir * Time.deltaTime * speed);
            yield return null;
        }
        
        transform.SetParent(cell.transform);
        
        cell.AddPlayer();

        isMoving = false;
    }
}
