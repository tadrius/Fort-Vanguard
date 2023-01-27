using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    
    public int Score { get { return score; }}

    public bool AddToScore(int points) {
        score += points;
        return true;
    }
}
