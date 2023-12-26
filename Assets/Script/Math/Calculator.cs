using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class Calculator : MonoBehaviour
{
    private OperationCreator operationCreator;
    private bool isFollowMouse = false; 
    private void Start()
    {
        operationCreator = GetComponent<OperationCreator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Operation") && isFollowMouse)
        {
            CheckProcess(collision.gameObject);
        }
    }

    private void CheckProcess(GameObject collision)
    {
        if (operationCreator.GetFirstOperator() == collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator() 
            && operationCreator.GetFirstOperator() == Operation.Multiply)
        {
            return;
        }
        else if (operationCreator.GetFirstOperator() == collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator()
            && operationCreator.GetFirstOperator() == Operation.Divide)
        {
            return;
        }
        else if (collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator() == Operation.Multiply
            && operationCreator.GetFirstOperator() == Operation.Divide)
        {
            return;
        }
        else if (collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator() == Operation.Divide
                           && operationCreator.GetFirstOperator() == Operation.Multiply)
        {
            return;
        }
        else
        {
            if (operationCreator.GetFirstOperator() == Operation.Multiply
              || operationCreator.GetFirstOperator() == Operation.Divide)
            {
                double result = Calculation(collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator()
                , collision.gameObject.GetComponent<OperationCreator>().GetSecondOperator()
                , collision.gameObject.GetComponent<OperationCreator>().GetNumber()
                , operationCreator.GetFirstOperator()
                , operationCreator.GetSecondOperator()
                , operationCreator.GetNumber());

                SetUp(collision.gameObject, result);
            }
            else
            {
                double result = Calculation(operationCreator.GetFirstOperator(),
                    operationCreator.GetSecondOperator(),
                    operationCreator.GetNumber(),
                    collision.gameObject.GetComponent<OperationCreator>().GetFirstOperator(),
                    collision.gameObject.GetComponent<OperationCreator>().GetSecondOperator(),
                    collision.gameObject.GetComponent<OperationCreator>().GetNumber());

                SetUp(collision.gameObject, result);
            }
        }
    }

    public void SetFollowMouse(bool isFollowMouse)
    {
        this.isFollowMouse = isFollowMouse;
    }

    private void SetUp(GameObject collision, double result)
    {
        double number = result;
        if(result < 0)
        {
            number = -result;
        }

        StartCoroutine(DestroyMaths(collision));
        OperationManager.instance.CreateOperation(
                        (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition),
                        OperationManager.instance.GetSign(Mathf.Sign((float)result)),
                        Operation.None,
                        number
                        );
    }

    private IEnumerator DestroyMaths(GameObject collision)
    {
        collision.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;    

        collision.transform.DOScale(0, .3f);
        this.transform.DOScale(0, .3f);

        collision.GetComponent<SpriteRenderer>().DOFade(0, .3f);
        this.GetComponent<SpriteRenderer>().DOFade(0, .3f);

        yield return new WaitForSeconds(.3f);
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }

    private double Calculation(Operation first1Operator, Operation first2Operator, double firstNumber
        , Operation second1Operator, Operation second2Operator, double secondNumber)
    {
        double result = 0;
        double totalFirst = 0;
        double totalSecond = 0;

        if(first2Operator == Operation.None)
        {
            totalFirst = firstNumber;
        }
        else if(first2Operator == Operation.Minus)
        {
            totalFirst = -firstNumber;
        }
        else if (first2Operator == Operation.Plus)
        {
            totalFirst = firstNumber;
        }

        if (second2Operator == Operation.None)
        {
            totalSecond = secondNumber;
        }
        else if (second2Operator == Operation.Minus)
        {
            totalSecond = -secondNumber;
        }
        else if (second2Operator == Operation.Plus)
        {
            totalSecond = secondNumber;
        }

        if(first1Operator == Operation.Plus)
        {
            result = totalFirst;
        }else if(first1Operator == Operation.Minus)
        {
            result = -totalFirst;
        }

        if(second1Operator == Operation.Plus)
        {
            result += totalSecond;
        }
        else if (second1Operator == Operation.Minus)
        {
            result -= totalSecond;
        }else if(second1Operator == Operation.Multiply)
        {
            result *= totalSecond;
        }
        else if (second1Operator == Operation.Divide)
        {
            result /= totalSecond;
        }
        OperationManager.instance.PlayOperationAnim(second1Operator);

        return result;
    }

}
