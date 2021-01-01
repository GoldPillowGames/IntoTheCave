using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int health = 100;
    public int damage = 18; // Por implementar
    public float movementSpeed = 1;
    public int goldPerEnemy; // Por implementar
    public int gold = 0; // Por implementar
    public float healthSteal = 0.0f; // Por implementar
    // public float attackSpeed = 1.0f;
    public bool hasHalbert = false; // Por implementar (que no aparezca otra arma después)
    public bool hasRapier = false; // Por implementar (que no aparezca otra arma después)
    public bool hasSpecialSkillOverpowered = false; // Por implementar
    public bool hasMainAttackOverpowered = false; // Por implementar
    public float luck = 1; // Por implementar
    public bool enemiesExplodes = false; // Por implementar
    public float agility = 1; // Por implementar
    public bool survivesToLetalAttack = false;
    public float lessInitialLifeForEnemies = 0; // Por implementar
    public float heal = 20; // Por implementar
    public float push = 1.0f; // Por implementar
    public bool spawnGrenadeWhenRolls = false; // Por implementar
    public float enemiesThreshold = 0.0f; // Por implementar
    public bool canRoll = true;
}
