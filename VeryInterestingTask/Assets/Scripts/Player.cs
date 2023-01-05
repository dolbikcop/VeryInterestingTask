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

    public PlayerInfo(string name, int score)
    {
        Name = name;
        Score = score;
        BonusPoints = 0;
        PenaltyPoints = 0;
    }
}
public class Player : MonoBehaviour
{
    private PlayerInfo Info;
    
    public string Name => Info.Name;
    public int Score => Info.Score;
    public int BonusPoints => Info.BonusPoints;
    public int PenaltyPoints => Info.PenaltyPoints;

    public bool isFinished = false;
    //public int FinishPlace = -1;
    
    [SerializeField]private TextMeshProUGUI nameLabel;
    [SerializeField]private int speed = 1;

    public void Initialize(string name = "Player", int score = 0)
    {
        nameLabel.text = name;
        Info = new PlayerInfo(name, score);
    }
    
    private List<Cell> cells => Cell.AllCells;
    public void Move(int score)
    {
        StopAllCoroutines();
        var c = cells[score + Score];
        
        cells[score].RemovePlayer();
        
        StartCoroutine(OneMove(c));
        
        if (c.GetStatus == CellStatus.Positive) Info.BonusPoints++;
        else if (c.GetStatus == CellStatus.Negative) Info.PenaltyPoints++;
        
        Info.Score = score + Score;
    }

    IEnumerator OneMove(Cell cell)
    {
        var target = cell.position;
        var dir = (target - transform.position).normalized;
        while ((target - transform.position).magnitude > 0.5)
        {
            transform.Translate(dir * Time.deltaTime * speed);
            yield return null;
        }
        transform.SetParent(cell.transform);
    }
}
