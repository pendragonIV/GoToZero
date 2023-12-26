using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationManager : MonoBehaviour
{
    public static OperationManager instance;
    [SerializeField] private GameObject operationPrefab;
    [SerializeField] private Transform operationAnim;
    [SerializeField] private Sprite[] operationSprites;
    [SerializeField] private Transform mathContainer;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this; 
        }
    }

    public void SetMathContainer(Transform container)
    {
        mathContainer = container;
    }

    public void CreateOperation(Vector3 position, Operation firstOperator, Operation secondOperator, double number)
    {
        if(number == 0)
        {
            CheckWin();
            return;
        }
        GameObject operation = Instantiate(operationPrefab, position, Quaternion.identity);
        operation.transform.SetParent(mathContainer);
        operation.GetComponent<OperationCreator>().SetOperation(firstOperator, secondOperator, number);
        MouseFollower.instance.SetCurrent(operation);
        CheckWin();
    }

    public Operation GetSign(float sign)
    {
        switch(sign)
        {
            case 1:
                return Operation.Plus;
            case -1:
            case 0:
                return Operation.Minus;
            default:
                return Operation.None;
        }
    }

    public void PlayOperationAnim(Operation operation)
    {
        operationAnim.GetComponent<SpriteRenderer>().color = Color.white;
        operationAnim.localScale = Vector3.one;

        operationAnim.gameObject.SetActive(true);
        operationAnim.DOScale(6, 1);
        operationAnim.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => operationAnim.gameObject.SetActive(false));
        operationAnim.GetComponent<SpriteRenderer>().sprite = operationSprites[(int)operation];
    }

    private void CheckWin()
    {
        if(mathContainer.childCount == 0)
        {
            GameManager.instance.Win();
        }
        else
        {
            int count = 0;
            foreach(Transform math in mathContainer)
            {
                if(math.GetComponent<Collider2D>().enabled)
                {
                    count++;
                }
            }
            if(count == 0)
            {
                GameManager.instance.Win();
            }
            else if(count == 1)
            {
                GameManager.instance.Lose();
            }
        }
    }
}
