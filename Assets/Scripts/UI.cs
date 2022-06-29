using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject finishUI;
    [SerializeField] private float finishDelay = 3f;

    [SerializeField] private Object nextLevel;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        finishUI.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel.name);
    }

    public void ShowFinishUIWithDelay()
    {
        StartCoroutine(ShowFinishUI());
    }

    private IEnumerator ShowFinishUI()
    {
        yield return new WaitForSeconds(finishDelay);
        finishUI.SetActive(true);
        scoreText.text = FindObjectOfType<Score>().ScoreCount.ToString();
    }

}
