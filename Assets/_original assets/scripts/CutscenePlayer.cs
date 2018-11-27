using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using UnityEngine.UI;

namespace PizzaFam
{
    public class CutscenePlayer : MonoBehaviour
    {
        public static bool StartingCutscene = true;

        private VideoPlayer video;
        private new AudioSource audio;

        public Canvas PauseMenu;

        private void Awake()
        {
            SubtitleToggle.isOn = Subtitles.Enabled;
            PauseMenu.enabled = false;
        }

        void Start()
        {
            video = GetComponent<VideoPlayer>();
            audio = GetComponent<AudioSource>();

            video.url = Path.Combine(Application.streamingAssetsPath, StartingCutscene ? StartVideoName : EndVideoName);
            audio.clip = StartingCutscene ? StartAudio : EndAudio;
            video.loopPointReached += OnVideoEnd;

            string penis = Resources.Load<TextAsset>("subtitles").text;
            string name = StartingCutscene ? "start scene" : "end scene";
            var node = SUCC.DataConverter.DataStructureFromPENIS(penis).Item2[name];
            foreach(var child in node.ChildNodes)
            {
                var heck = (SUCC.KeyNode)child;
                SubtitleData.Add(float.Parse(heck.Key), heck.Value);
            }
            SubtitleTimes = new List<float>(SubtitleData.Keys);
            SubtitleTimes.Sort();

            StartCoroutine(PrepareThenPlay());
        }

        IEnumerator PrepareThenPlay()
        {
            video.Prepare();
            while (!video.isPrepared)
                yield return new WaitForEndOfFrame();

            video.Play();
            audio.Play();
        }

        public AudioClip StartAudio;
        public AudioClip EndAudio;

        public string StartVideoName = "start.mp4";
        public string EndVideoName = "end.mp4";

        private Dictionary<float, string> SubtitleData = new Dictionary<float, string>();
        private List<float> SubtitleTimes;
        private int nextTimeIndex = 0;

        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
                TogglePaused();

            if (nextTimeIndex >= SubtitleTimes.Count) return;
            if (video.time >= SubtitleTimes[nextTimeIndex])
            {
                Subtitles.Say(SubtitleData[SubtitleTimes[nextTimeIndex]], 10000000);
                nextTimeIndex++;
            }
        }

        public void OnVideoEnd(VideoPlayer source)
        {
            if (StartingCutscene)
                SceneLoader.LoadLevel(1);
            else
                SceneLoader.LoadEndGame();
        }

        public Toggle SubtitleToggle;
        public void ToggleSubtitles()
        {
            Subtitles.Enabled = SubtitleToggle.isOn;
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