using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected GenerationData generationParameters;

    //static room generation
    protected override void RunGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(generationParameters, startPosition);
        tilemapPainter.Clear();
        tilemapPainter.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapPainter);
    }
    //room generation algorithm
    protected HashSet<Vector2Int> RunRandomWalk(GenerationData generationParameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i=0; i<generationParameters.iterations; i++)
        {
            var path = ProceduralGeneration.SimpleRandomWalk(currentPosition, generationParameters.walkLength);
            floorPositions.UnionWith(path);
            if(generationParameters.startRandomlyEachIteration) { currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count)); }
        }
        return floorPositions;
    }
}
