using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bank))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreKeeper))]
[RequireComponent(typeof(Builder))]
public class Player : MonoBehaviour
{

    readonly public static string playerTag = "Player";

}
