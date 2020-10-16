using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    public Button button;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(Open);
    }
    
    // Opens the panel with the controls to play the game
    void Open() {
        panel.SetActive(true);
    }
}
