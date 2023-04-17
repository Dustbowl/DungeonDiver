using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EntityManager : MonoBehaviour
{
    [SerializeField]
    private EnemyPool spawnPool;
    public GameObject player;
    public GameObject exit;
    public Canvas canvas;
    private GameObject GetRandomEnemy()
    {
        return spawnPool.enemyPool[Random.Range(0, spawnPool.enemyPool.Length)];
    }

    public void SpawnPlayer(ref Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary)
    {
        List<Vector2Int> key = new List<Vector2Int>(roomDictionary.Keys);
        Vector2Int tmp = key[Random.Range(0, key.Count)];
        roomDictionary.Remove(tmp);
        Vector3Int position = (Vector3Int)tmp;
        position += Vector3Int.back;
        player.transform.position = position;
        //player.GetComponent<player_life>().SetupHealthBar(canvas);
    }

    public void SpawnEnemies(Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary)
    {
        ClearEnemies();
        HashSet<Vector3Int> enemySpawnPoints = new HashSet<Vector3Int>();
        foreach(HashSet<Vector2Int> room in roomDictionary.Values)
        {
            List<Vector2Int> positions = new List<Vector2Int>(room);
            int count = 0;
            while (count < 4)
            {
                Vector2Int randomPosition = positions[Random.Range(0, positions.Count)];
                if(room.Contains(randomPosition+Vector2Int.left) && 
                   room.Contains(randomPosition+Vector2Int.down) &&
                   room.Contains(randomPosition + Vector2Int.left + Vector2Int.down))
                {
                    positions.Remove(randomPosition);
                    enemySpawnPoints.Add((Vector3Int)randomPosition + Vector3Int.back);
                    count++;
                }
            }
        }
        foreach(Vector3Int spawnPoint in enemySpawnPoints)
        {
            Instantiate(GetRandomEnemy(), spawnPoint, Quaternion.identity);
        }
    }
    private void ClearEnemies()
    {
        GameObject[] expiredEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in expiredEnemies)
        {
            Destroy(enemy); //for actual use
            //DestroyImmediate(enemy); //for dev use;
        }
    }
    public void PlaceExit(Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary)
    {
        GameObject oldExit = GameObject.FindWithTag("Exit");
        Destroy(oldExit);
        List<Vector2Int> roomCenters = new List<Vector2Int>(roomDictionary.Keys);
        Vector3Int position = (Vector3Int)roomCenters[Random.Range(0, roomCenters.Count)] + Vector3Int.back;
        GameObject newExit = Instantiate(exit, position, Quaternion.identity);
        newExit.GetComponent<FloorExitScript>().ChangeMaxKillCount(roomDictionary.Count);
    }
}
