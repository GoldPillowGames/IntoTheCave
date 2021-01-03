using System.Linq;
using UnityEngine;
using Photon.Pun;
using System.IO;

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

        _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies");
        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var numEnemiesToSpawn = Mathf.Min(Random.Range(_minEnemies, _maxEnemies + 1), _enemySpawnPositions.Length);
        
        var enemySpawnPositionsList = _enemySpawnPositions.ToList();

        for (var i = 0; i < numEnemiesToSpawn; i++)
        {
            var randomIndex = Random.Range(0, enemySpawnPositionsList.Count);
            var randomPosition = enemySpawnPositionsList[randomIndex].position;
            if (!Config.data.isOnline)
            {
                Instantiate(SelectRandomEnemy(), randomPosition, Quaternion.identity);
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemies", SelectRandomEnemy().name), randomPosition, Quaternion.identity);
            }
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
