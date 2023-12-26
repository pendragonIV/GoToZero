using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Transform overlayPanel;
    [SerializeField]
    private Transform winPanel;
    [SerializeField]
    private Transform losePanel;
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private Transform[] bg;

    [SerializeField]
    private Transform nextButton;
    [SerializeField]
    private Transform replayButton;
    [SerializeField]
    private Text timeLeft;
    [SerializeField]
    private Text timeRemain;

    private void Start()
    {
        foreach (Transform b in bg)
        {
            b.DOShakePosition(10f, 100f, 0, 0, false, true).SetEase(Ease.InSine).SetLoops(-1);
        }
    }

    public void SetTime(float totalTime, float time)
    {
        int minute = (int)time / 60;
        int second = (int)time % 60;

        float remain = totalTime - time;
        int minuteRemain = (int)remain / 60;
        int secondRemain = (int)remain % 60;

        timeLeft.text = minute.ToString("00") + ":" + second.ToString("00");
        timeRemain.text = minuteRemain.ToString("00") + ":" + secondRemain.ToString("00");
    }

    public void ShowWinPanel()
    {
        overlayPanel.gameObject.SetActive(true);
        winPanel.gameObject.SetActive(true);
        FadeIn(overlayPanel.GetComponent<CanvasGroup>(), winPanel.GetComponent<RectTransform>());
        homeButton.interactable = false;
        nextButton.transform.DOScale(.9f, .5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ShowLosePanel()
    {
        overlayPanel.gameObject.SetActive(true);
        losePanel.gameObject.SetActive(true);
        FadeIn(overlayPanel.GetComponent<CanvasGroup>(), losePanel.GetComponent<RectTransform>());
        homeButton.interactable = false;
        replayButton.transform.DOScale(.9f, .5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void FadeIn(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);
        rectTransform.localScale = Vector3.zero;
        rectTransform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(true);
    }
}
