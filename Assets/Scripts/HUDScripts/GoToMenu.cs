using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public Button menuButton;
    void Start() {
        Button btn = menuButton.GetComponent<Button>();
        btn.onClick.AddListener(Menu);
    }

    void Menu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
