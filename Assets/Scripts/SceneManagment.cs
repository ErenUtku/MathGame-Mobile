using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public GameObject instruction;

    public void Start()
    {
        instruction.SetActive(false);
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenInstruction()
    {
        instruction.SetActive(true);
    }
    public void CloseInstruction()
    {
        instruction.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
