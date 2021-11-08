using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text problemText;                
    public Text[] answersTexts;             
    public Image remainingTimeDial;         
    private float remainingTimeDialRate;    
    public Text endText;
    public Image Blackscreen;
    float mainmenutime;


    float transition;
    public float BsSpeed = 0.04f;

    public static UI instance;
    void Awake()
    {
        instance = this;//Creating instance for accessing through other script
    }
    void Start()
    {
        {
            Blackscreen.gameObject.SetActive(false);
            remainingTimeDialRate = 1.0f / GameManager.instance.timePerProblem;//exp 8 seconds will divide dial 8 times so with Time.deltaTime that will just work as a clock
        }
    }
    void Update()
    {
        remainingTimeDial.fillAmount = remainingTimeDialRate * GameManager.instance.remainingTime;
    }

    public void SetProblemText(Problem problem)
    {
        string operatorText = "";
        switch (problem.operation)
        {
            case MathsOperation.Addition: operatorText = " + "; break;
            case MathsOperation.Subtraction: operatorText = " - "; break;
            case MathsOperation.Multiplication: operatorText = " x "; break;
            case MathsOperation.Division: operatorText = " ÷ "; break;
        }
        problemText.text = problem.firstNumber + operatorText + problem.secondNumber;
        for (int index = 0; index < answersTexts.Length; ++index)
        {
            answersTexts[index].text = problem.answers[index].ToString();
        }
    }
    public void SetEndText(bool win)
    {
        endText.gameObject.SetActive(true);
        if (win)
        {
            endText.text = "You Win!";
            endText.color = Color.green;
            Blackscreen.gameObject.SetActive(true);
            transition += BsSpeed;
            Blackscreen.color = new Color(0, 0, 0, transition);
            if (mainmenutime > 3f)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            endText.text = "Game Over!";
            endText.color = Color.red;
            Blackscreen.gameObject.SetActive(true);
            transition += BsSpeed;
            Blackscreen.color = new Color(0, 0, 0, transition);
            mainmenutime += Time.deltaTime;
            if (mainmenutime > 3f)
            {
                SceneManager.LoadScene(0);
            }
        }
        
        
    }
    
}
