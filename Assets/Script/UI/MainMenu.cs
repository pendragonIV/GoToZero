using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform gameLogo;
    [SerializeField]
    private Transform tutorPanel;
    [SerializeField]
    private Transform guideLine;
    [SerializeField]
    private Transform bg;
    [SerializeField]
    private Transform flash;


    private void Start()
    {
        tutorPanel.gameObject.SetActive(false);

        gameLogo.GetComponent<CanvasGroup>().alpha = 0f;
        gameLogo.GetComponent<CanvasGroup>().DOFade(1, 2f).SetUpdate(true);
        gameLogo.localScale = Vector3.zero;
        gameLogo.DOScale(1, 1.5f).SetEase(Ease.OutBack).SetUpdate(true);

        bg.DOShakePosition(10f, 100f, 0, 0, false, true).SetEase(Ease.InSine).SetLoops(-1);
        StartCoroutine(FlashEffect(flash));
    }

    private IEnumerator FlashEffect(Transform flash)
    {
        Vector3 flashPos = flash.GetComponent<RectTransform>().anchoredPosition;
        while (true)
        {
            flash.GetComponent<RectTransform>().DOAnchorPos(new Vector3(flashPos.x + 1600, flashPos.y, flashPos.z), 1f);
            yield return new WaitForSeconds(3f);
            flash.GetComponent<RectTransform>().anchoredPosition = flashPos;
        }
    }

    public void ShowTutorPanel()
    {
        tutorPanel.gameObject.SetActive(true);
        guideLine.gameObject.SetActive(true);
        FadeIn(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>());

    }

    public void HideTutorPanel()
    {
        StartCoroutine(FadeOut(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>()));

    }   

    private void FadeIn(CanvasGroup canvasGroup ,RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(-1200, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), .3f, false).SetEase(Ease.InOutBack).SetUpdate(true);
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(1200, 0), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);

        yield return new WaitForSecondsRealtime(.3f);
        guideLine.gameObject.SetActive(true);
        tutorPanel.gameObject.SetActive(false);

    }

}
