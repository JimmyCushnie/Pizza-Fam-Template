using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

namespace PizzaFam {
    public class CutscenePlayer : MonoBehaviour
    {
        public static bool StartingCutscene = true;

        private VideoPlayer video;
        private new AudioSource audio;

        public Canvas PauseMenu;

        void Start()
        {
            video = GetComponent<VideoPlayer>();
            audio = GetComponent<AudioSource>();

            video.url = Path.Combine(Application.streamingAssetsPath, StartingCutscene ? StartVideoName : EndVideoName);
            audio.clip = StartingCutscene ? StartAudio : EndAudio;
            video.Play();
            audio.Play();

            video.loopPointReached += OnVideoEnd;
        }

        public AudioClip StartAudio;
        public AudioClip EndAudio;

        public string StartVideoName = "start.mp4";
        public string EndVideoName = "end.mp4";

        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                TogglePaused();
            }
        }

        public void OnVideoEnd(VideoPlayer source)
        {
            if (StartingCutscene)
                SceneLoader.LoadLevel(1);
            else
                SceneLoader.LoadEndGame();
    }

        public void TogglePaused()
        {
            PauseMenu.enabled = !PauseMenu.enabled;

            if (PauseMenu.enabled) PausePlayer();
            else UnpausePlayer();
        }

        private void PausePlayer()
        {
            video.Pause();
            audio.Pause();
        }

        private void UnpausePlayer()
        {
            video.Play();
            audio.UnPause();
        }
    }
}