using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health = 100;
    public float damage = 18;
    public float movementSpeed = 1;
    public int goldPerEnemy;
    public int gold = 0;
    public float healthSteal = 0.0f;
    // public float attackSpeed = 1.0f;
    public bool hasHalbert = false;
    public bool hasRapier = false;
    public bool hasSpecialSkillOverpowered = false;
    public bool hasMainAttackOverpowered = false;
    public float luck = 1;
    public bool enemiesExplodes = false;
    public float agility = 1;
    public bool survivesToLetalAttack = false;
    public float lessInitialLifeForEnemies = 0;
    public float heal = 20;
    public float push = 1.0f;
    public bool spawnGrenadeWhenRolls = false;
    public float enemiesThreshold = 0.0f;
    public bool canRoll = true;
}
