using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Problem[] problems;      
    public int curProblem;          
    public float timePerProblem;    
    public float remainingTime;     
    public Player player;
    public AudioClip CorrectSound;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    public static GameManager instance;

   
   
    void Awake()
    {
        instance = this;  // set instance to this script.(singleton) Accessing purposes(GameManager.instance)
    }
    void Start()
    {
        SetProblem(0);
    }
    void Win()
    {
        AudioSource.PlayClipAtPoint(WinSound, Camera.main.transform.position, 0.1f); 
        UI.instance.SetEndText(true);
    }
    void Lose()
    {   
        AudioSource.PlayClipAtPoint(LoseSound, Camera.main.transform.position, 0.1f);
        UI.instance.SetEndText(false);
    }
    void SetProblem(int problem)
    {
       
        curProblem = problem;
        UI.instance.SetProblemText(problems[curProblem]);
        remainingTime = timePerProblem;
        
    }


    // player enters the correct tube
    void CorrectAnswer()
    {
        if (problems.Length - 1 == curProblem)
        { 
            Win();
            
        }
        else
        {
            AudioSource.PlayClipAtPoint(CorrectSound, Camera.main.transform.position, 0.5f);
            SetProblem(curProblem + 1);
        }
    }
    //player enters the incorrect tube
    void IncorrectAnswer()
    {
        player.Stun();
    }
    public void OnPlayerEnterTube(int tube)
    {
        if (tube == problems[curProblem].correctTube)
        {
            CorrectAnswer();

            player.transform.position = new Vector2(-0.05f, -3.34f);
        }
        else
            IncorrectAnswer();
    }
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0.0f)
        {
            Lose();
        }
    }

}
