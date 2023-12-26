
using TMPro;
using UnityEngine;

public class OperationCreator : MonoBehaviour
{
    [SerializeField]
    private Operation firstOperator;
    [SerializeField]
    private Operation secondOperator;
    [SerializeField]
    private double number;
    [SerializeField]
    private TMP_Text operationText;
    string operation = "";

    private void Start()
    {
        operationText.text = OperationCreation();   
    }

    public void SetOperation(Operation firstOperator, Operation secondOperator, double number)
    {
        this.firstOperator = firstOperator;
        this.secondOperator = secondOperator;
        this.number = number;
        operationText.text = OperationCreation();
    }

    private string OperationCreation()
    {
        operation = "";
        if(secondOperator != Operation.None)
        {
            operation += "("+ GetOperation(secondOperator) + number + ")";
        }
        else
        {
            operation += number;
        }   
        operation = GetOperation(firstOperator) + operation;
        return operation;
    }

    private string GetOperation(Operation operation)
    {
        switch(operation)
        {
            case Operation.Plus:
                return "+";
            case Operation.Minus:
                return "-";
            case Operation.Multiply:
                return "x";
            case Operation.Divide:
                return "/";
            default:
                return "";
        }
    }

    public string GetOperation()
    {
        return operation;
    }

    public double GetNumber()
    {
        return number;
    }

    public Operation GetFirstOperator()
    {
        return firstOperator;
    }

    public Operation GetSecondOperator()
    {
        return secondOperator;
    }
}
