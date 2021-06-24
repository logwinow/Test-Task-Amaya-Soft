using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, IRecyclable
{
    [SerializeField] private SymbolCollectionsContainerData _symbolCollectionsContainerData;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private FindHint _findHint;

    private List<Cell> _cells = new List<Cell>();
    private List<Symbol> _closedSymbols = new List<Symbol>();

    private void OnDestroy()
    {
        GameManager.Instance.onRecycle.RemoveListener(OnRecycle);
    }

    public void BuildLevel(DifficultData difficultData, bool playAppearance = false)
    {
        ClearLevel();

        SetCells(difficultData, playAppearance);
        
        var rightCell = SetRightCell();
        
        _findHint.Show(rightCell.Symbol.Name, playAppearance);
    }

    private void SetCells(DifficultData difficultData, bool playAppearance)
    {
        var symbolCollection = _symbolCollectionsContainerData.GetRandomCollection();
        Vector2 leftUpperCorner = transform.position - new Vector3(
            x: _cellPrefab.Size.x * difficultData.Size.x / 2,
            _cellPrefab.Size.y * difficultData.Size.y / 2
        ) + _cellPrefab.Size.x / 2f * Vector3.right;
        Symbol[] symbols = new Symbol[difficultData.Size.x * difficultData.Size.y];
        Cell cell;
        
        symbolCollection.GetDifferentSymbols(symbols);
        
        for (int i = 0; i < difficultData.Size.y; i++)
        {
            for (int j = 0; j < difficultData.Size.x; j++)
            {
                cell = Instantiate(_cellPrefab);
                
                cell.Set(symbols[i * difficultData.Size.x + j], playAppearance);
                cell.transform.position = leftUpperCorner +
                                          new Vector2(
                                              j * _cellPrefab.Size.x,
                                              i * _cellPrefab.Size.y
                                          );
                
                _cells.Add(cell);
            }
        }
    }

    private Cell SetRightCell()
    {
        List<Cell> cellsWithNotParticipatedSymbols = new List<Cell>(_cells);
        foreach (var s in _closedSymbols)
        {
            foreach (var c in cellsWithNotParticipatedSymbols)
            {
                if (c.Symbol == s)
                {
                    cellsWithNotParticipatedSymbols.Remove(c);
                    break;
                }
            }
        }

        var rightCell = cellsWithNotParticipatedSymbols[Random.Range(0, cellsWithNotParticipatedSymbols.Count)];
        rightCell.SetAsRight(() => GameManager.Instance.GoToNextLevel());
        
        _closedSymbols.Add(rightCell.Symbol);

        return rightCell;
    }

    private void ClearLevel()
    {
        _cells.ForEach(c => Destroy(c.gameObject));

        _cells.Clear();
    }
    
    public void OnRecycle()
    {    
        _closedSymbols.Clear();
    }
}