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
    private List<Player> allPlayers = new List<Player>();
    private List<Cell> cells => Cell.AllCells;

    [SerializeField] private GameObject playerPrefab;
    
    private int round = 0;

    public int playerCount;

    public List<Player> FinishList;

    public FinishEvent FinishGame;

    void Start()
    {
        InitializeGame(playerCount);
    }

    public void InitializeGame(int countPlayer)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell;
            if (transform.GetChild(i).TryGetComponent(out cell))
            {
                Cell.AllCells.Add(cell);
                cell.labelText.text = i.ToString();
            }
        }

        for (int i = 0; i < countPlayer; i++)
        {
            var pl = Instantiate(playerPrefab, cells[0].position, Quaternion.identity)
                .GetComponent<Player>();
            
            cells[0].AddPlayer();
            
            pl.transform.SetParent(cells[0].transform);
            
            pl.Initialize(i.ToString());
            allPlayers.Add(pl);
        }
    }

    public void MakeMove(int score)
    {
        allPlayers = allPlayers.Where(x=>!x.isFinished).ToList();

        var i = round % allPlayers.Count;
        var pl = allPlayers[i];
        
        pl.Move(score);
        
        if (cells[pl.currentCell].GetStatus == CellStatus.Positive)
            round--;
        else if (cells[pl.currentCell].GetStatus == CellStatus.Negative)
            pl.Move(-3);
        
        round++;
        
        if (allPlayers.All(x => x.isFinished))
            FinishGame.Invoke(FinishList);
    }
}