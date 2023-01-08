using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinalBoardScript : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    public void Initialize(List<Player> fList)
    {
        print("init" + fList.Count);
        for (int i = 0; i < fList.Count; i++)
        {
            var row = Instantiate(rowPrefab, transform);
            row.GetComponent<UIRowScript>().Initialize(i+1, fList[i]);
        }
    }
}
