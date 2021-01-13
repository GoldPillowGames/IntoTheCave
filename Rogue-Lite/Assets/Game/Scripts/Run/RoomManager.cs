using System.Linq;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Transform[] _enemySpawnPositions;
    [SerializeField] private bool isBossRoom;
    // [SerializeField] private GameObject[] _doors;
    private GameObject[] _posibleEnemiesToSpawn;
    private string pathName;
    
    // Provisional:
    private int _minEnemies = 2;
    private int _maxEnemies = 4;
    private int _numEnemies;
    
    void Start()
    {
        Audio.ActivateTrack(1);

        RunManager runManager = FindObjectOfType<RunManager>();
        int currentStage = runManager.currentStage;
        if (isBossRoom)
        {
            pathName = "Bosses";
            currentStage--;
        }
        else
        {
            switch (currentStage)
            {
                case 1:
                    pathName = "Enemies";
                    break;
                case 2:
                    pathName = "Enemies2";
                    break;
                case 3:
                    pathName = "Enemies3";
                    break;
                default:
                    pathName = "Enemies";
                    break;
            }
            
        }
        
        switch (currentStage)
        {
            case 1:
                if (isBossRoom)
                {
                    _posibleEnemiesToSpawn = new GameObject[1]
                        {Resources.Load<GameObject>("PhotonPrefabs/Bosses/Roquita")};
                }
                else
                {
                    _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies");
                }
                break;
            case 2:
                if (isBossRoom)
                {
                    _posibleEnemiesToSpawn = new GameObject[1]
                        {Resources.Load<GameObject>("PhotonPrefabs/Bosses/Pinchitos")}; // Se llama en el primer boss.
                }
                else
                {
                    _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies2");
                }
                break;
            case 3:
                if (isBossRoom)
                {
                    _posibleEnemiesToSpawn = new GameObject[1]
                        {Resources.Load<GameObject>("PhotonPrefabs/Bosses/Litos")}; // Se llama en el segundo boss.
                }
                else
                {
                    _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies3");
                }
                break;
            default:
                _posibleEnemiesToSpawn = Resources.LoadAll<GameObject>("PhotonPrefabs/Enemies");
                break;
        }
        
        
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
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", pathName, enemyToInstantiate.name), position, Quaternion.identity);
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
