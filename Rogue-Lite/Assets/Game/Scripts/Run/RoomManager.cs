using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Transform[] _enemySpawnPositions;
    private GameObject[] _posibleEnemiesToSpawn;
    
    // Provisional:
    private int _minEnemies = 2;
    private int _maxEnemies = 4;

    void Start()
    {
        Audio.ActivateTrack(1);

        _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("Prefabs/Enemies");
        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var numEnemiesToSpawn = Random.Range(_minEnemies, _maxEnemies + 1);
        
        var enemySpawnPositionsList = _enemySpawnPositions.ToList();

        for (var i = 0; i < numEnemiesToSpawn; i++)
        {
            var randomIndex = Random.Range(0, enemySpawnPositionsList.Count);
            var randomPosition = enemySpawnPositionsList[randomIndex].position;

            Instantiate(SelectRandomEnemy(), randomPosition, Quaternion.identity);
            
            enemySpawnPositionsList.RemoveAt(randomIndex);
        }
    }

    private GameObject SelectRandomEnemy()
    {
        var randomIndex = Random.Range(0, _posibleEnemiesToSpawn.Length);
        return _posibleEnemiesToSpawn[randomIndex];
    }
    
    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            g.GetComponent<BoxCollider>().enabled = true;
        }
        Audio.DeactivateTrack(1);
    }
}
