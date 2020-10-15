using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public Button startButton;
    void Start() {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void StartGame() {
        SceneManager.LoadScene("MainGame");
    }
}

