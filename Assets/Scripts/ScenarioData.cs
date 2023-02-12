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
 
}
