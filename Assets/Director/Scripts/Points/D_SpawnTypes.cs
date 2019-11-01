using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_SpawnTypes : MonoBehaviour
{
    public static System.Array getTypes()
    {
        return spawnTypes.GetValues(typeof(spawnTypes));
    }
}

public enum spawnTypes
{
    Health,
    Item,
    Obstacle,
    Enemy,
    Hazard
};
