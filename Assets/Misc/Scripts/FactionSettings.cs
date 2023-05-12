using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSettings : MonoBehaviour
{

    [SerializeField] Faction playerFaction;
    [SerializeField] List<Faction> factions;

    readonly Dictionary<string, Faction> factionsByName = new ();

    public Faction PlayerFaction { get { return playerFaction; } }
    public List<Faction> Factions { get { return factions; } }

    private void Awake()
    {
        foreach (var faction in Factions)
        {
            factionsByName.Add(faction.FactionName, faction);
        }
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

}
