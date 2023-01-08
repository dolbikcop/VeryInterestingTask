using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnterPlayerName : MonoBehaviour
{
    [SerializeField] private GameplayManager game;
    [SerializeField] private GameObject enterNamePrefab;

    private List<InputField> names = new List<InputField>();

    public void CreatePlayerUI(int plCount)
    {
        for(int i = 0; i < plCount; i++)
            names.Add(Instantiate(enterNamePrefab, transform).GetComponent<InputField>());
    }

    public void ApplyNameToPlayer()
    {
        for (int i = 0; i < game.allPlayers.Count; i++)
        {
            game.allPlayers[i].SetName(names[i].text);
            print(names[i].text);            
        }
    }
}
