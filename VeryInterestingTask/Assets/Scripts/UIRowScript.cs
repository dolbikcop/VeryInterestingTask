using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRowScript : MonoBehaviour
{
    [SerializeField] private Text number;
    [SerializeField] private Text playerName;
    [SerializeField] private Text score;
    [SerializeField] private Text bonus;
    [SerializeField] private Text penalty;
    
    public void Initialize(int number, Player p)
    {
        print(p.Name);
        this.number.text = number.ToString();
        
        playerName.text = p.Name;
        score.text = p.MoveCount.ToString();
        bonus.text = p.BonusPoints.ToString();
        penalty.text = p.PenaltyPoints.ToString();
    }
}
