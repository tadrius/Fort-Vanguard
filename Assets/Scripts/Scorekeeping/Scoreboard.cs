using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{

    // a sorted list of int score values by string username
    SortedList<int, string> scores = new SortedList<int, string>(new HighToLowIntComparer());

    void Awake() {
        // keep scoreboard as a singleton (destroy this instance if there are 2 or more instances)
        if (2 <= FindObjectsOfType<Scoreboard>().Length) {
            Destroy(this.gameObject);
        } else {
            // if this is an existing scoreboard, keep it alive.
            DontDestroyOnLoad(this.gameObject);
        }

        // add an initial score of 0
        AddScoreEntry(0);
    }

    public KeyValuePair<int, string> GetHighScore() {
        return new KeyValuePair<int, string>(scores.Keys[0], scores.Values[0]);
    }

    void LogScoreboard() {
        if (0 == scores.Count) {
            Debug.Log("No scores found.");
        }
        foreach(KeyValuePair<int, string> entry in scores) {
            Debug.Log($"{entry.Value} : {entry.Key}");
        }
    }

    public void AddScoreEntry(int score) {
        AddScoreEntry(score, "");
    }

    public void AddScoreEntry(int score, string username) {
        scores.Add(score, username);
    }

    private class HighToLowIntComparer : IComparer<int> {
        public int Compare(int x, int y) {
            return y.CompareTo(x);
        }
    }
}
