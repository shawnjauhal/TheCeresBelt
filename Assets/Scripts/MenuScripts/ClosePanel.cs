using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    public Button closeButton;
    public GameObject controlsPanel;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = closeButton.GetComponent<Button>();
        btn.onClick.AddListener(Close);
        controlsPanel.SetActive(false);
    }

    // Closes the panel with game controls
    void Close() {
        controlsPanel.SetActive(false);
    }
}
