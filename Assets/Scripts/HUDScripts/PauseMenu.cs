using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        Button resumebtn = resumeButton.GetComponent<Button>();
        pausePanel.SetActive(false);
        resumebtn.onClick.AddListener(Resume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            if (pausePanel.activeSelf == false) {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            } else {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    void Resume() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }


}
