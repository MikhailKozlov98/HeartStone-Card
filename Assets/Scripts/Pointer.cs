using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public static Pointer Instance;

    public Cell ActiveCell;

    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveCell(Cell cell)
    {
        ActiveCell = cell;
    }
}
