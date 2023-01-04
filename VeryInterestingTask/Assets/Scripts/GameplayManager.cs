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
    [SerializeField] private List<Player> allPlayers;
    [SerializeField] private GameObject playerPrefab;
    
    private int round = 0;

    public int playerCount;

    public List<Player> FinishList;

    public FinishEvent FinishGame;

    void Start()
    {
        InitializeGame(allPlayers.Count);
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
            var pl = Instantiate(playerPrefab, Cell.AllCells[0].position)
                .GetComponent<Player>();
            
            //initialize fiulds in Player
            pl.Initialize(i.ToString());
            allPlayers.Add(pl);
        }
    }

    public void MakeMove(int score)
    {
        var pl = allPlayers.Where(x=>!x.isFinished).ToList()[round % playerCount];
        pl.Move(score);
        round++;

        if (allPlayers.All(x => x.isFinished))
            FinishGame.Invoke(FinishList);
    }
}