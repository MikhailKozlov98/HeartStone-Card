using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCanvas : MonoBehaviour
{
    public int Health = 6;
    public int Attack = 6;
    public int Cost = 6;
    public Text HealthText;
    public Text AttackText;
    public Text CostText;
    [Space]
    public Text HealthChangeText;
    public Text AttackChangeText;
    public Text CostChangeText;

    internal bool _onTable = false;

    private CardCanvasManager _cardCanvasManager;
    private AbilityCardCanvasButton _abilityCardCanvasButton;

    private Action<int>[] _actionsArray;

    void Start()
    {
        SetParameterText();

        _cardCanvasManager = FindObjectOfType<CardCanvasManager>();
        _abilityCardCanvasButton = FindObjectOfType<AbilityCardCanvasButton>();

        _actionsArray = new Action<int>[3];

        _actionsArray[0] += ChangeCost;
        _actionsArray[1] += ChangeHealth;
        _actionsArray[2] += ChangeAttack;
    }

    public void SetOnTable()
    {
        _onTable = true;
    }

    public void SetParameterText()
    {
        HealthText.text = Health.ToString();
        AttackText.text = Attack.ToString();
        CostText.text = Cost.ToString();
    }

    public void ChangeParameter(int index)
    {
        int number = UnityEngine.Random.Range(-2, 10);
        _actionsArray[index].Invoke(number);
        //switch (index)
        //{
        //    case 0: // Стоимость
        //        ChangeCost(number);
        //        break;
        //    case 1: // Жизнь
        //        ChangeHealth(number);
        //        break;
        //    case 2: // Атака
        //        ChangeAttack(number);
        //        break;
        //    default:
        //        break;
        //}
    }

    private void ChangeCost(int value)
    {
        Cost += value;
        StartCounterAnimation(value, CostChangeText);
        if (Cost <= 0)
        {
            Cost = 0;
        }
    }
    private void ChangeHealth(int value)
    {
        Health += value;
        StartCounterAnimation(value, HealthChangeText);
        if (Health <= 0)
        {
            Die();
        }
    }
    private void ChangeAttack(int value)
    {
        Attack += value;
        StartCounterAnimation(value, AttackChangeText);
        if (Attack <= 0)
        {
            Attack = 0;
        }
    }

    public void Die()
    {
        _cardCanvasManager.RemoveFromCardsObjList(gameObject);
        _abilityCardCanvasButton.RemoveFromCardsList(this);

        _cardCanvasManager.PositionAndRotationCards();
        Destroy(gameObject);
    }

    public void StartCounterAnimation(int number, Text parameterText)
    {
        StartCoroutine(StartEffect(number, parameterText));
    }

    public IEnumerator StartEffect(int number, Text parameterText)
    {
        parameterText.gameObject.SetActive(true);

        Color32 color = Color.red;
        parameterText.text = number.ToString();

        if (number > 0)
        {
            color = Color.green;
            parameterText.text = "+" + number.ToString();
        }

        parameterText.color = new Color32(color.r, color.g, color.b, 255);

        for (float t = 255; t > 0F; t -= Time.deltaTime * 250F)
        {
            parameterText.color = new Color32(color.r, color.g, color.b, (byte)t);
            yield return null;
        }
    }
}
