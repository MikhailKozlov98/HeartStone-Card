using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCardCanvasButton : MonoBehaviour
{
    public CardCanvas CurrentCardCanvas = null;

    /// <summary>
    /// Все карты из сцены
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

        // Выбираем текущей картой самую левую карту
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
            // Удаление карты из списка руки, чтобы сдвинуться на одну вправо
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
