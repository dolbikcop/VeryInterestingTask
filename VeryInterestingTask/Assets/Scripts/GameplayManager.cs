using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private List<Player> allPlayers;
    
    private int round = 0;

    public int playerCount;

    public List<Player> FinishList;
    void Start()
    {
        InitGame(allPlayers.Count);
    }

    public void InitGame(int countPlayer)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell;
            if (transform.GetChild(i).TryGetComponent(out cell))
                Cell.AllCells.Add(cell);
        }
    }

    public void MakeMove(int score)
    {
        var pl = allPlayers.Where(x=>!x.isFinished).ToList()[round % playerCount];
        pl.Move(score);
        round++;
    }
}
