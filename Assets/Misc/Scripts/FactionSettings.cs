using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSettings : MonoBehaviour
{

    [SerializeField] Faction playerFaction;
    [SerializeField] Faction enemyFaction;

    [SerializeField] List<Faction> factions;

    System.Random random = new System.Random();

    readonly Dictionary<string, Faction> factionsByName = new ();

    public Faction PlayerFaction { get { return playerFaction; } }
    public Faction EnemyFaction { get { return enemyFaction; } }
    public List<Faction> Factions { get { return factions; } }

    private void Awake()
    {
        foreach (var faction in Factions)
        {
            factionsByName.Add(faction.FactionName, faction);
        }
        playerFaction = factions[0];
    }

    public void SetPlayerFaction(string factionName)
    {
        SetPlayerFaction(factionsByName[factionName]);

    }

    void SetPlayerFaction(Faction playerFaction)
    {
        this.playerFaction = playerFaction;
    }

    public int GetFactionIndex(Faction faction)
    {
        return factions.IndexOf(faction);
    }

    public void SetRandomEnemyFaction()
    {
        enemyFaction = factions[random.Next(0, factions.Count)];
    }

}
