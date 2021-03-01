using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Color ColorA;
    public Color ColorB;

    private bool _occupied;

    private void Start()
    {
        ColorA = GetComponent<Image>().color;
    }

    public void OnEnter()
    {
        if (!_occupied)
        {
            GetComponent<Image>().color = ColorB;
            Pointer.Instance.SetActiveCell(this);
        }
    }
    public void OnExit()
    {
        GetComponent<Image>().color = ColorA;
        Pointer.Instance.SetActiveCell(null);
    }

    public void Occupy()
    {
        _occupied = true;
        GetComponent<Image>().color = ColorA;
    }

    public void UnOccupy()
    {
        _occupied = false;
    }
}
