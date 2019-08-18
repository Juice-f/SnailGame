using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] string gameScene;
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
