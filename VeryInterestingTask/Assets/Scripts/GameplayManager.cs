using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FinishEvent : UnityEvent<List<Player>> {}
public class GameplayManager : MonoBehaviour
{
    public List<Player> allPlayers = new List<Player>();
    private List<Cell> cells => Cell.AllCells;

    [SerializeField] private GameObject[] playerPrefab;
    
    private int round = 0;

    public List<Player> FinishList;

    public FinishEvent FinishGame;

    private void FixedUpdate() =>
        DiceSystem.CanMakeTurn = !allPlayers.Any(x => x.isMoving)
                                 && allPlayers.Any(x => !x.isFinished);

    public void InitializeGame(int countPlayer)
    {
        // добавляю ячейкам имена
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell;
            if (transform.GetChild(i).TryGetComponent(out cell))
            {
                Cell.AllCells.Add(cell);
                cell.labelText.text = i.ToString();
            }
        }
        // инициализирую игроков на ячейке 0

        for (int i = 0; i < countPlayer; i++)
        {
            var pl = Instantiate(playerPrefab[i], cells[0].position, Quaternion.identity)
                .GetComponent<Player>();
            
            cells[0].AddPlayer();
            
            pl.transform.SetParent(cells[0].transform);
            
            pl.Initialize(i.ToString());
            allPlayers.Add(pl);
        }
    }
    public void MakeMove(int score)
    {
        var list = allPlayers.Where(x=>!x.isFinished).ToList();

        var i = round % list.Count;
        var pl = list[i];
        
        pl.Move(score);
        
        if (cells[pl.currentCell].GetStatus == CellStatus.Positive)
            round--;
        else if (cells[pl.currentCell].GetStatus == CellStatus.Negative)
        {
            MakeMove(-3);
            round--;
        }
        
        round++;
        if (pl.isFinished) FinishList.Add(pl);

        if (allPlayers.All(x => x.isFinished))
        {
            FinishGame.Invoke(FinishList);
        }
    }
}