<<<<<<< HEAD
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallsPositionRotation
{
    public Vector3 position;
    public Quaternion rotation;
}
[CreateAssetMenu(menuName = "Data/New Scenario")]
public class ScenarioData : ScriptableObject
{
    public GameObject WallePrefab;
    public List<WallsPositionRotation> Walls; 
 
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "New Scénario")]
public class ScenarioData : ScriptableObject
{
    public Vector3[] FirstWalls;

>>>>>>> 00381690acf35c19081922c1a1edf84c2c8f560c
}
