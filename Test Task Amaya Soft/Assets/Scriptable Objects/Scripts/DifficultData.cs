using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficult.asset", menuName = "Difficult")]
public class DifficultData : ScriptableObject
{
    [SerializeField] private Vector2Int _size;
    
    public Vector2Int Size => _size;
}
