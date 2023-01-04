using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CellStatus
{
    Simple, Negative, Positive
}
public class Cell : MonoBehaviour
{
    public static List<Cell> AllCells = new List<Cell>();
    
    public TextMeshProUGUI labelText;

    [SerializeField] private CellStatus cellStatus;

    public CellStatus GetStatus => cellStatus;

    private List<Transform> stayPoints = new();

    private int countPlayers;

    public Transform position => stayPoints[countPlayers%stayPoints.Count];

    void Start()
    {
        var sp = transform.GetChild(1);
        for (int i = 0; i < sp.childCount; i++)
            stayPoints.Add(sp.GetChild(i));
    }

    public void AddPlayer() => countPlayers++;
    public void RemovePlayer() => countPlayers--;
}
