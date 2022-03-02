using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScreen : MonoBehaviour
{

    public Button play;
    public Button quit;

    public void playClick()
    {

        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

    }

    public void quitClick()
    {

        Application.Quit();

    }

}