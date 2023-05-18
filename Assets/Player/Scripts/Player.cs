using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Bank))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreKeeper))]
[RequireComponent(typeof(Builder))]
public class Player : MonoBehaviour
{
    [SerializeField] Faction playerFaction;
    [SerializeField] Vector2Int headquartersCoordinates = new Vector2Int(20, 17);

    readonly public static string playerTag = "Player";

    ScoreKeeper scoreKeeper;

    public Faction PlayerFaction { get { return playerFaction; } }
    public ScoreKeeper ScoreKeeper { get { return scoreKeeper; } }
    public Vector2Int HeadquartersCoordinates { get { return headquartersCoordinates; } }

    void Awake() {
        FactionSettings settings = FindObjectOfType<FactionSettings>();

        if (null != settings)
        {
            Faction settingsFaction = FindObjectOfType<FactionSettings>().PlayerFaction;
            if (null != settingsFaction)
            {
                playerFaction = settingsFaction;
            }
        }
        scoreKeeper = GetComponent<ScoreKeeper>();
    }

    private void Start()
    {
        // create the player's headquarters visuals
        GridManager gridManager = FindObjectOfType<GridManager>();
        GameObject headquarters = GameObject.Instantiate(playerFaction.PlayerHeadquarters, transform);
        headquarters.transform.localPosition = gridManager.GetPositionFromCoordinates(headquartersCoordinates);
    }

}
