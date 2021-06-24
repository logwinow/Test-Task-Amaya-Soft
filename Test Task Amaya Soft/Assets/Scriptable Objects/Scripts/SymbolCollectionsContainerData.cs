using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Symbol Collections Container.asset", menuName = "Symbol Collections Container")]
public class SymbolCollectionsContainerData : ScriptableObject
{
    [SerializeField] private SymbolCollectionData[] _symbolCollections;

    public SymbolCollectionData GetRandomCollection()
    {
        return _symbolCollections[Random.Range(0, _symbolCollections.Length)];
    }
}
