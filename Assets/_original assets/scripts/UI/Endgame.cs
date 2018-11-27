using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PizzaFam.UI
{
    public class Endgame : MonoBehaviour
    {
        public void GoToMainMenu() => SceneLoader.LoadMainMenu();
    }
}