using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PizzaFam.UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            ShowMain();
        }

        public Canvas MainCanvas;
        public Canvas LevelSelectCanvas;
        public Canvas OptionsCanvas;
        public Canvas AboutCanvas;

        public void ShowMain()
        {
            MainCanvas.enabled = true;
            LevelSelectCanvas.enabled = false;
            OptionsCanvas.enabled = false;
            AboutCanvas.enabled = false;
        }

        public void ShowLevelSelect()
        {
            MainCanvas.enabled = false;
            LevelSelectCanvas.enabled = true;
            OptionsCanvas.enabled = false;
            AboutCanvas.enabled = false;
        }

        public void ShowOptions()
        {
            MainCanvas.enabled = false;
            LevelSelectCanvas.enabled = false;
            OptionsCanvas.enabled = true;
            AboutCanvas.enabled = false;
        }

        public void ShowAbout()
        {
            MainCanvas.enabled = false;
            LevelSelectCanvas.enabled = false;
            OptionsCanvas.enabled = false;
            AboutCanvas.enabled = true;
        }



        public void NewGame() => SceneLoader.LoadCutscene(start: true);
        public void Quit() => Application.Quit();
    }
}