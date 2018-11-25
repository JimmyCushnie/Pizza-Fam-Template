using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CommandTerminalPlus;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.sceneCount == 1) // so you can have this scene open while working on a level or whatnot
            LoadMainMenu();
    }

    public static int HighestReachedLevel
    {
        get => GameData.Get("HighestReachedLevel", 0);
        private set => GameData.Set("HighestReachedLevel", value);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public static void LoadLevel(int level)
    {
        if (level > HighestReachedLevel) HighestReachedLevel = level;

        SceneManager.LoadScene("InEveryLevel");
        SceneManager.LoadScene("level" + level, LoadSceneMode.Additive);
        SceneManager.LoadScene("GameplayUI", LoadSceneMode.Additive);
    }

    public static void LoadCutscene(bool start)
    {
        throw new System.NotImplementedException();
    }



    [RegisterCommand(Name = "Load.MainMenu", MaxArgCount = 0)]
    private static void CommandLoadMM(CommandArg[] args) => LoadMainMenu();

    [RegisterCommand(Name = "Load.Level", MinArgCount = 1, MaxArgCount = 1)]
    private static void CommandLoadLevel(CommandArg[] args) => LoadLevel(args[0].Int);

    [RegisterCommand(Name = "Load.Cutscene", MinArgCount = 1, MaxArgCount = 1)]
    private static void CommandLoadCutscene(CommandArg[] args) => LoadCutscene(args[0].Bool);
}
