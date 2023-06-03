using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultScrenS : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

   public void Setup(int score)
    {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();

        //gameObject.SetActive(true);

        textmeshPro.SetText(score + "PUNKTOW");

        scoreUI.text = score.ToString();
        

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
