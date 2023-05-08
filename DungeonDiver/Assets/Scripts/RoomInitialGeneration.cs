using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomInitialGeneration : RandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 8)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary = new Dictionary <Vector2Int, HashSet<Vector2Int>>();
    public EntityManager entityManager;

    protected override void RunGeneration()
    {
        roomDictionary.Clear();
        CreateRooms();
        entityManager.SpawnPlayer(ref roomDictionary);
        entityManager.SpawnEnemies(roomDictionary);
        entityManager.SpawnItems(roomDictionary);
        entityManager.PlaceExit(roomDictionary);
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGeneration.BinarySpacePartition(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), 
            minRoomWidth, 
            minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRandomRooms(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapPainter.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapPainter);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector2Int nearest = FindNearestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(nearest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, nearest);
            currentRoomCenter = nearest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }
    private Vector2Int FindNearestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int nearest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach(var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                nearest = position;
            }
        }
        return nearest;
    }
    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach(var room in roomsList)
        {
            var roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));
            var roomFloor = RunRandomWalk(generationParameters, roomCenter);
            HashSet<Vector2Int> roomTemp = new HashSet<Vector2Int>();
            foreach(var position in roomFloor)
            {
                if(position.x >= (room.xMin + offset) && position.x <= (room.xMax - offset) &&
                    position.y >= (room.yMin + offset) && position.y <= (room.yMax - offset))
                {
                    roomTemp.Add(position);
                    floor.Add(position);
                }
            }
            roomDictionary.Add(roomCenter, roomTemp);
        }
        return floor;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
