using System;
using System.Collections;
using System.Collections.Generic;
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
    private PlayerInfo Info = new PlayerInfo("Player", 0);
    
    public string Name => Info.Name;
    public int Score => Info.Score;
    public int BonusPoints => Info.BonusPoints;
    public int PenaltyPoints => Info.PenaltyPoints;

    public bool isFinished = false;
    //public int FinishPlace = -1;
    
    private List<Cell> cells => Cell.AllCells;
    public void Move(int score)
    {
        StopAllCoroutines();
        throw new NotImplementedException();
    }
}
