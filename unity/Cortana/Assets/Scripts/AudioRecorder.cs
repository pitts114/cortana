using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioRecorder : MonoBehaviour
{
    public GameObject gameObject;
    private AudioClip audioClip;
    private bool isRecording = false;

    public void StartRecording()
    {
        audioClip = Microphone.Start(null, false, 10, 44100);
        isRecording = true;
        gameObject.GetComponent<Image>().color = Color.red;
    }

    public void StopRecording()
    {
        Microphone.End(null);
        isRecording = false;
        SaveAudioClipToFile();
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void ToggleRecording()
    {
        if (isRecording)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }

    private void SaveAudioClipToFile()
    {
        string filePath = Application.persistentDataPath + "/audio.wav";
        SavWav.Save(filePath, audioClip);
    }
}
