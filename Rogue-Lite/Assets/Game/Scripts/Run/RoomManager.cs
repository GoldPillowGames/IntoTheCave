using System.Linq;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Transform[] _enemySpawnPositions;
    // [SerializeField] private GameObject[] _doors;
    private GameObject[] _posibleEnemiesToSpawn;
    
    // Provisional:
    private int _minEnemies = 2;
    private int _maxEnemies = 4;
    private int _numEnemies;
    
    void Start()
    {
        Audio.ActivateTrack(1);

        _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies");
        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        _numEnemies = Mathf.Min(Random.Range(_minEnemies, _maxEnemies + 1), _enemySpawnPositions.Length);
        
        var enemySpawnPositionsList = _enemySpawnPositions.ToList();

        for (var i = 0; i < _numEnemies; i++)
        {
            var randomIndex = Random.Range(0, enemySpawnPositionsList.Count);
            var randomPosition = enemySpawnPositionsList[randomIndex].position;
            var enemyToSpawn = SelectRandomEnemy();
            
            InstantiateEnemy(enemyToSpawn, randomPosition);
            
            enemySpawnPositionsList.RemoveAt(randomIndex);
        }
    }

    public void EnemyDied()
    {
        _numEnemies--;
        if (_numEnemies == 0)
        {
            FindObjectOfType<RunManager>().EndCurrentStage();
        }
    }
    
    private GameObject SelectRandomEnemy()
    {
        var randomIndex = Random.Range(0, _posibleEnemiesToSpawn.Length);
        return _posibleEnemiesToSpawn[randomIndex];
    }

    private void InstantiateEnemy(GameObject enemyToInstantiate, Vector3 position)
    {
        if (!Config.data.isOnline)
        {
            Instantiate(enemyToInstantiate, position, Quaternion.identity);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemies", enemyToInstantiate.name), position, Quaternion.identity);
        }
    }
    
    public void OpenDoors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (g.GetComponent<EventTrigger>())
            {
                if (g.GetComponent<EventTrigger>().canBeDeactivated)
                {
                    g.GetComponent<EventTrigger>().activated = false;
                    if(g.GetComponent<EventTrigger>().doorPosition == DoorPosition.TOP || g.GetComponent<EventTrigger>().doorPosition == DoorPosition.LEFT)
                    g.GetComponent<EventTrigger>().isOpen = true;
                }
                else
                {
                    g.GetComponent<EventTrigger>().activated = true;
                    g.GetComponent<EventTrigger>().isOpen = false;
                }
            }
            g.GetComponent<BoxCollider>().enabled = true;
        }
        Audio.DeactivateTrack(1);
    }
}
