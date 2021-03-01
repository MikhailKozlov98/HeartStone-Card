using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    public Image[] Images;
    public RawImage RawImage;
    public Cell CurrentCell;
    public RectTransform RectTransform;

    private Color _startColor = Color.white;
    private Color _activeColor = Color.green;
    private Transform _parentOfCanvasCard;

    private CardCanvasManager _cardCanvasManager;
    private AbilityCardCanvasButton _abilityCardCanvasButton;

    private void Start()
    {
        _parentOfCanvasCard = RectTransform.parent;
        //_startColor = Images[0].color;

        _cardCanvasManager = FindObjectOfType<CardCanvasManager>();
        _abilityCardCanvasButton = FindObjectOfType<AbilityCardCanvasButton>();
    }

    public void BeginDrag()
    {
        if (CurrentCell)
        {
            CurrentCell.UnOccupy();
        }
    }

    public void OnDrag()
    {
        Images[0].color = _activeColor;

        RectTransform.SetParent(_parentOfCanvasCard);

        RectTransform.position = Input.mousePosition;

        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].raycastTarget = false;
        }
        RawImage.raycastTarget = false;

        RectTransform.rotation = Quaternion.identity;
    }

    public void EndDrag()
    {
        Images[0].color = _startColor;

        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].raycastTarget = true;
        }

        RawImage.raycastTarget = true;

        if (Pointer.Instance.ActiveCell)
        {
            SetToCell(Pointer.Instance.ActiveCell);
        }
        else
        {
            SetToCell(CurrentCell);
        }

    }

    public void SetToCell(Cell cell)
    {
        if (cell)
        {
            CurrentCell = cell;
            cell.Occupy();
            RectTransform.SetParent(cell.transform);
            RectTransform.localPosition = Vector3.zero;

            GetComponent<CardCanvas>().SetOnTable();
            _cardCanvasManager.RemoveFromCardsObjList(gameObject);
            _abilityCardCanvasButton.RemoveFromCardsList(gameObject.GetComponent<CardCanvas>());
        }
        _cardCanvasManager.PositionAndRotationCards();
    }
}
