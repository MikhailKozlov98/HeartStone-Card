using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCardCanvasButton : MonoBehaviour
{
    public CardCanvas CurrentCardCanvas = null;

    /// <summary>
    /// ��� ����� �� �����
    /// </summary>
    private CardCanvas[] _allCardsCanvasArray;
    private List<CardCanvas> _handCardsCanvasList = new List<CardCanvas>();

    public void UseAbilityButton()
    {
        if (_handCardsCanvasList.Count == 0)
        {
            _allCardsCanvasArray = FindObjectsOfType<CardCanvas>();
            if (_allCardsCanvasArray.Length == 0)
            {
                return;
            }

            for (int i = 0; i < _allCardsCanvasArray.Length; i++)
            {
                if (!_allCardsCanvasArray[i]._onTable)
                {
                    _handCardsCanvasList.Add(_allCardsCanvasArray[i]);
                }
            }
        }

        if (!CurrentCardCanvas)
        {

            if (_handCardsCanvasList.Count != 0)
            {
                CurrentCardCanvas = _handCardsCanvasList[0];
            }
        }

        // �������� ������� ������ ����� ����� �����
        for (int i = 0; i < _handCardsCanvasList.Count; i++)
        {
            if (_handCardsCanvasList[i].transform.localPosition.x < CurrentCardCanvas.transform.localPosition.x)
            {
                CurrentCardCanvas = _handCardsCanvasList[i];
            }
        }

        if (CurrentCardCanvas)
        {
            int randomParameter = Random.Range(0, 3);
            CurrentCardCanvas.ChangeParameter(randomParameter);

            CurrentCardCanvas.SetParameterText();
            // �������� ����� �� ������ ����, ����� ���������� �� ���� ������
            _handCardsCanvasList.Remove(CurrentCardCanvas);

            CurrentCardCanvas = null;
        }
    }

    public void RemoveFromCardsList(CardCanvas cardCanvas)
    {
        if (_handCardsCanvasList.Contains(cardCanvas))
        {
            _handCardsCanvasList.Remove(cardCanvas);
        }
    }
}
