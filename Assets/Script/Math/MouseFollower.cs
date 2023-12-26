
using DG.Tweening;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public static MouseFollower instance;
    private GameObject current;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            current = CastRay();
            if (current != null)
            {
                PressAnim();
                current.GetComponent<Calculator>().SetFollowMouse(true);
                PlayPartical();
                PositionLimiter();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if(current != null)
            {
                PlayPartical();
                PositionLimiter();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(current != null)
            {
                DisablePartical();
                current.GetComponent<Calculator>().SetFollowMouse(false);
            }
        }
    }

    public GameObject GetHitObject()
    {
        return current;
    }

    private GameObject CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    public void SetCurrent(GameObject current)
    {
        this.current = current;
        current.GetComponent<Calculator>().SetFollowMouse(true);
        PressAnim();
    }

    private void PositionLimiter()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        Vector3 objPosition = Camera.main.ScreenToViewportPoint(mousePosition);
        objPosition.x = Mathf.Clamp(objPosition.x, 0.1f, 0.9f);
        objPosition.y = Mathf.Clamp(objPosition.y, 0.1f, 0.9f);

        current.transform.position = Camera.main.ViewportToWorldPoint(objPosition);
    }

    private void PlayPartical()
    {
        current.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void DisablePartical()
    {
        current.transform.GetChild(0).gameObject.SetActive(false);
    }   

    private void PressAnim()
    {
        if (current)
        {
            current.transform.DOScale(0.8f, 0.15f).OnComplete(() => current.transform.DOScale(1, 0.15f));
        }
    }
}
