﻿using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LibretroWrapper.Wrapper wrapper;

    public Renderer Display;
    public string CoreName = "snes9x_libretro.so";
    public string RomName = "Chrono Trigger (USA).sfc";

    private void Start()
    {
        Application.targetFrameRate = 60;
        LoadRom(Application.streamingAssetsPath + "/" + RomName);
    }

    private void Update()
    {
        if (wrapper != null)
        {
            wrapper.Update();
        }

        if (LibretroWrapper.tex != null)
        {
            Display.material.mainTexture = LibretroWrapper.tex;
        }

        // debug input
        //if (Input.GetButton("B")) Debug.Log("B");
        //if (Input.GetButton("Y")) Debug.Log("Y");
        //if (Input.GetButton("SELECT")) Debug.Log("SELECT");
        //if (Input.GetButton("START")) Debug.Log("START");
        //if (Input.GetAxisRaw("DpadX") >= 1.0f) Debug.Log("UP");
        //if (Input.GetAxisRaw("DpadX") <= -1.0f) Debug.Log("DOWN");
        //if (Input.GetAxisRaw("DpadY") >= 1.0f) Debug.Log("RIGHT");
        //if (Input.GetAxisRaw("DpadY") <= -1.0f) Debug.Log("LEFT");
        //if (Input.GetButton("A")) Debug.Log("A");
        //if (Input.GetButton("X")) Debug.Log("X");
        //if (Input.GetButton("L")) Debug.Log("L");
        //if (Input.GetButton("R")) Debug.Log("R");
    }

    public void LoadRom(string path)
    {
#if !UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_WEBGL
        // Doesn't work on Android because you can't do File.Exists in StreamingAssets folder.
        // Should figure out a different way to perform check later.
        // If the file doesn't exist the application gets stuck in a loop.
        if (!File.Exists(path))
        {
            Debug.LogError(path + " not found.");
            return;
        }
#endif
        Display.material.color = Color.white;

        wrapper = new LibretroWrapper.Wrapper(Application.streamingAssetsPath + "/" + CoreName);

        wrapper.Init();
        wrapper.LoadGame(path);
    }
}