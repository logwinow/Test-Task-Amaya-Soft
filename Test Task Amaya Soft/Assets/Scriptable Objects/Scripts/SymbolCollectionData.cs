using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolsCollection.asset", menuName = "SymbolsCollection")]
public class SymbolCollectionData : ScriptableObject
{
    [SerializeField] private Symbol[] _symbols;

    public void GetDifferentSymbols(Symbol[] symbols)
    {
        List<Symbol> copySymbols = new List<Symbol>(_symbols);
        Symbol randomSymbol;

        for (int i = 0; i < symbols.Length; i++)
        {
            randomSymbol = GetRandomSymbol(copySymbols);
            copySymbols.Remove(randomSymbol);
            symbols[i] = randomSymbol;
        }
    }

    private Symbol GetRandomSymbol(List<Symbol> symbols)
    {
        return symbols[Random.Range(0, symbols.Count)];
    }
}

[System.Serializable]
public class Symbol
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _originRotate = 0;
    [SerializeField] private string _name;

    public Sprite Sprite => _sprite;
    public float OriginRotate => _originRotate;
    public string Name => _name;
}
