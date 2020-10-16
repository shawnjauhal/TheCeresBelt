using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenControlsPanel : MonoBehaviour
{
    public Button controlsButton;
    public GameObject controlsPanel;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = controlsButton.GetComponent<Button>();
        btn.onClick.AddListener(Open);
    }
    
    // Opens the panel with the controls to play the game
    void Open() {
        controlsPanel.SetActive(true);
    }
}
