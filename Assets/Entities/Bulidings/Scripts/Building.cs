using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] string buildingName = "Building";
    [Tooltip("The cost to construct this building.")]
    [SerializeField] int cost = 50;
    [Tooltip("Whether or not this building can be constructed on a platform.")]
    [SerializeField] bool isPlatformBuildable = false;
    [Tooltip("The parts that comprise the fully constructed building. The first part should be the main Structure.")]
    [SerializeField] GameObject[] parts;
    [Tooltip("Particles that play when the building is being constructed.")]
    [SerializeField] ParticleSystem[] constructionParticles;
    [SerializeField] AudioSource constructionAudio;
    [SerializeField] float constructionTime = 3f;
    [SerializeField] GameObject dismantleFX;

    bool isElevated = false;

    Player player;
    public string BuildingName { get { return buildingName; }}
    public int Cost { get { return cost; }}
    public bool IsElevated { get { return isElevated; }}

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();

    }

    void Start() {
        StartCoroutine(Construct());
    }

    public GameObject CreateBuilding(Tile tile) {
        RuntimeSpawns runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        Bank bank = FindObjectOfType<Bank>();
        
        if (CheckSiteCompatibility(tile) && WithdrawCost(bank)) {
            GameObject newBuilding = runtimeSpawns.SpawnObject(gameObject, tile.transform.position);
            newBuilding.GetComponent<Building>().SetIsElevated(tile.IsPlatform);
            return newBuilding;
        }
        return null;
    }

    IEnumerator Construct() {
        SetPartsActive(false);
        float timeElapsed = 0f;

        PlayConstructionFX();
        while (timeElapsed < constructionTime) {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetPartsActive(true);
        DisableConstructionFX();
    }
    
    void SetPartsActive(bool isActive) {
        foreach (GameObject part in parts) {
            part.SetActive(isActive);
        }
        if (isElevated) {   // hide ground structure if this building is elevated
            parts[0].SetActive(false);
        }
    }

    void PlayConstructionFX() {
        constructionAudio.Play();
        foreach (ParticleSystem particleSystem in constructionParticles) {
            var emission = particleSystem.emission;
            emission.enabled = true;
        }
    }

    void DisableConstructionFX() {
        foreach (ParticleSystem particleSystem in constructionParticles) {
            var emission = particleSystem.emission;
            emission.enabled = false;
        }     
    }

    public bool RefundBuilding() {
        if (DestroyBuilding()) {
            return DepositRefund(FindObjectOfType<Bank>());
        }
        return false;
    }

    public bool DestroyBuilding() {
        Tile[] tiles = GetComponentsInChildren<Tile>();
        // if this building has any child tiles and any of these tiles are in use
        // then this building is being used as a platform and cannot be destroyed
        foreach (Tile tile in tiles) {
            if (tile.IsOccupied) {
                Debug.Log("Cannot destroy a building being used as a platform.");
                return false;
            }        
        }
        // otherwise destroy the building
        SpawnDismantleFX();
        Destroy(gameObject);
        return true;
    }

    public bool CheckSiteCompatibility(Tile tile) {
        if (tile.IsBuildSite && !tile.IsOccupied) {
            if (!tile.IsPlatform || (tile.IsPlatform && isPlatformBuildable)) {
                return true;
            }
        }
        return false;
    }

    bool WithdrawCost(Bank bank) {
        if (null != bank && bank.Withdraw(cost)) {
            return true;
        }
        return false;
    }

    bool DepositRefund(Bank bank) {
        if (null != bank && bank.Deposit(cost/2)) {
            return true;
        }
        return false;
    }

    public void SetIsElevated(bool isElevated) {
        this.isElevated = isElevated;
    }

    public void SpawnDismantleFX() {
        RuntimeSpawns runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        runtimeSpawns.SpawnObject(dismantleFX, transform.position);
    }
}
