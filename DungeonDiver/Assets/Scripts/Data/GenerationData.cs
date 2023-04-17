using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/RandomWalkData")]

public class GenerationData : ScriptableObject
{
    public int iterations = 32, walkLength = 16;
    public bool startRandomlyEachIteration = false;
}
