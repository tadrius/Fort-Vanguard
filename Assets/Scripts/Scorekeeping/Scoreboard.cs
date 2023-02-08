using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{

    // a list of int score values
    List<int> scores = new List<int>();
    HighScoreComparer comparer = new HighScoreComparer();

    void Awake() {
        // keep as a singleton (destroy this instance if there are 2 or more instances)
        if (2 <= FindObjectsOfType<Scoreboard>().Length) {
            Destroy(this.gameObject);
        } else {
            // if this is an existing singleton, keep it alive.
            DontDestroyOnLoad(this.gameObject);
        }

        // add an initial score of 0
        AddScore(0);
    }

    public int GetHighScore() {
        return scores[0];
    }

    void LogScoreboard() {
        Debug.Log("Scoreboard:");
        if (0 == scores.Count) {
            Debug.Log("Empty.");
        }
        foreach(int score in scores) {
            Debug.Log($"{score}");
        }
    }

    public void AddScore(int score) {
        scores.Add(score);
        scores.Sort(comparer);
    }

    private class HighScoreComparer : IComparer<int> {
        public int Compare(int x, int y) {
            return y.CompareTo(x);
        }
    }
}
