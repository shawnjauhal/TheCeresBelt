using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scoreboards {
public class ScoreboardSubmission : MonoBehaviour
    {

        private ScoreboardSaveData scores;
        public Button submitButton;
        public Button goBackButton;
        public GameObject submissionPanel;
        public GameObject placementPanel;
        public InputField nameInput;
        public Text scoreDetails;
        public Text placement;
        private EntryData newEntry;
        private GameObject ship;
        private ShipMovement shipMovementScript;
        private string playerName;
        private float finalTime;
        private string formattedTime;

        private string SavePath => $"{Application.persistentDataPath}/scoreboard.json";
        // Start is called before the first frame update
        void Start()
        {
            submissionPanel.SetActive(false);
            placementPanel.SetActive(false);
            nameInput.onValidateInput += delegate (string s, int i, char c) {
                if ( s.Length >= 12) { return '\0'; }
                c = char.ToLower(c);
                return char.IsLetter(c) ? c : '\0';
            };
            ship = GameObject.FindGameObjectWithTag("Ship");
            shipMovementScript = ship.GetComponent<ShipMovement>();
            Button submitBtn = submitButton.GetComponent<Button>();
            submitBtn.onClick.AddListener(SubmitScore);
            Button goBack = goBackButton.GetComponent<Button>();
            goBack.onClick.AddListener(CancelSubmission);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return) && submissionPanel.activeSelf == true)
            {
                playerName = nameInput.text;
                finalTime = shipMovementScript.finalTime;
                formattedTime = shipMovementScript.formattedTime;
                newEntry = new EntryData(playerName, finalTime, formattedTime);
                scores.Add(newEntry);
                SaveScores();
                StartCoroutine(DisplayScore());
            }
        }

        ScoreboardSaveData LoadScoreboard() 
        {
            if (!File.Exists(SavePath)) {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            } else {
                using(StreamReader stream = new StreamReader(SavePath)) {
                    string json = stream.ReadToEnd();
                    return JsonUtility.FromJson<ScoreboardSaveData>(json);
                }
            }
        }

        private void CancelSubmission() {
            nameInput.text = "";
            submissionPanel.SetActive(false);
        }

        private void SaveScores()
        {
            File.Delete(SavePath);
            File.Create(SavePath).Dispose();
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scores, true);
                stream.Write(json);
            }
        }

        private void SubmitScore() {
            Debug.Log(shipMovementScript.finalTime);
            submissionPanel.SetActive(true);
            scores = LoadScoreboard();
            
        }

        IEnumerator DisplayScore() {
            placementPanel.SetActive(true);
            scoreDetails.text = playerName + "\n" + formattedTime;
            int place = scores.GetScores().IndexOf(newEntry) + 1;
            string suffix;
            if (place % 10 == 1 && place % 100 != 11) {
                suffix = "st";
            } else if (place % 10 == 2 && place % 100 != 12) {
                suffix = "nd";
            } else if (place % 10 == 3 && place % 100 != 13) {
                suffix = "rd";
            } else {
                suffix = "th";
            }
            placement.text = place + suffix;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("MainMenu");
        }

    }
}
