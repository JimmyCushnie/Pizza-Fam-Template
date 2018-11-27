using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PizzaFam.UI
{
    public class LevelSelect : MonoBehaviour
    {
        public void LoadLevel(int level) => SceneLoader.LoadLevel(level);

        private void Awake()
        {
            var UnlockedLevel = SceneLoader.HighestReachedLevel;

            Transform level = transform.Find("1");
            int i = 1;
            while(level != null)
            {
                level.GetComponent<Button>().interactable = i <= UnlockedLevel;
                i++;
                level = transform.Find(i.ToString());
            }
        }
    }
}