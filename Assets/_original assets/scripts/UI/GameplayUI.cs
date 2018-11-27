using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PizzaFam.UI
{
    public class GameplayUI : MonoBehaviour
    {
        public Canvas Pause;
        public Canvas GameOver;
        public TextMeshProUGUI LevelCounter;

        private static GameplayUI Instance;
        private void Awake()
        {
            Instance = this;
            Pause.enabled = false;
            GameOver.enabled = false;
        }

        private void Start()
        {
            LevelCounter.text = $"Level {SceneLoader.CurrentLevel}/{SceneLoader.LevelCount}";
        }

        private void Update()
        {
            if (Input.GetButtonDown("Pause") && !GameOver.enabled)
                TogglePaused();
        }

        public void TogglePaused()
        {
            Pause.enabled = !Pause.enabled;

            if (Pause.enabled) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        public void MainMenu()
        {
            SceneLoader.LoadMainMenu();
        }

        public void Retry()
        {
            SceneLoader.LoadLevel(SceneLoader.CurrentLevel);
        }



        public static void ShowGameOverCanvas()
        {
            Instance.GameOver.enabled = true;
        }
    }
}