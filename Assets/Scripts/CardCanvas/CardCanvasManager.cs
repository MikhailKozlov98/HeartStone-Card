using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCanvasManager : MonoBehaviour
{
    public GameObject CardCanvasPrefab;
    public Transform ParentOfCardCanvas;
    public Button AbilityButton;
    public int MinCardNumber = 4;
    public int MaxCardNumber = 6;

    /// <summary>
    /// Список объектов карт
    /// </summary>
    private List<GameObject> _allCardsCanvasObj = new List<GameObject>();

    void Start()
    {
        int random = Random.Range(MinCardNumber, MaxCardNumber);
        for (int i = 0; i < random; i++)
        {
            GameObject newCardObj = Instantiate(CardCanvasPrefab);

            newCardObj.transform.SetParent(ParentOfCardCanvas);
            newCardObj.transform.localScale = new Vector3(1F, 1F, 1F);

            _allCardsCanvasObj.Add(newCardObj);
        }
        PositionAndRotationCards();
    }

    public void PositionAndRotationCards()
    {
        float yMultiplier1 = -20F;
        float yMultiplier2 = -350F;

        float xMultiplier1 = 100F;

        if (_allCardsCanvasObj.Count <= 0)
        {
            return;
        }

        int center = Mathf.FloorToInt(_allCardsCanvasObj.Count / 2);
        float yPosition = yMultiplier1 * center + yMultiplier2;

        float n = 1F - _allCardsCanvasObj.Count % 2; // НЕчётно - n = 0
        float xPosition = xMultiplier1 * (center - 0.5F * n);

        float maxAngle = _allCardsCanvasObj.Count * 9F;
        maxAngle = Mathf.Clamp(maxAngle, 0F, 45F);
        float angle = maxAngle / Mathf.Max(center, 0.001F);

        for (int i = 0; i < center; i++)
        {
            StartCoroutine(MoveCard(_allCardsCanvasObj[i], -xPosition, yPosition));
            StartCoroutine(MoveCard(_allCardsCanvasObj[_allCardsCanvasObj.Count - 1 - i], xPosition, yPosition));
            xPosition -= xMultiplier1;
            yPosition += -yMultiplier1;

            _allCardsCanvasObj[i].transform.eulerAngles = new Vector3(0F, 0F, angle);
            _allCardsCanvasObj[_allCardsCanvasObj.Count - 1 - i].transform.eulerAngles = new Vector3(0F, 0F, -angle);
            angle /= 2;
        }
        _allCardsCanvasObj[center].transform.localPosition = new Vector3(xPosition * -n, yMultiplier1 + yMultiplier2, 0F);
        _allCardsCanvasObj[center].transform.eulerAngles = new Vector3(0F, 0F, angle * -n);
    }

    public void RemoveFromCardsObjList(GameObject cardObj)
    {
        if (_allCardsCanvasObj.Contains(cardObj))
        {
            _allCardsCanvasObj.Remove(cardObj);
        }
    }

    public IEnumerator MoveCard(GameObject cardObj, float xPosition, float yPosition)
    {
        AbilityButton.interactable = false;
        for (float t = 0; t < 0.3F; t += Time.deltaTime)
        {
            cardObj.transform.localPosition = Vector3.Lerp(cardObj.transform.localPosition, new Vector3(xPosition, yPosition, 0F), Time.deltaTime * 50F);
            yield return null;
        }
        AbilityButton.interactable = true;
    }
}
