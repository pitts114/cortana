using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioRecorder : MonoBehaviour
{

    private AudioClip audioClip;
    private bool isRecording = false;
    private List<RecordButton> subscribers = new List<RecordButton>();

    public void subscribe(RecordButton recordButton)
    {
        Debug.Log("AudioRecorder.subscribe");
        subscribers.Add(recordButton);
        Debug.Log("subscribers.Count: " + subscribers.Count);
    }

    public void StartRecording()
    {
       //  audioClip = Microphone.Start(null, false, 10, 44100);
        Debug.Log("AudioRecorder.StartRecording");
        isRecording = true;
        subscribers.ForEach(subscriber => subscriber.onRecordingStarted());
    }

    public void StopRecording()
    {
        Debug.Log("AudioRecorder.StopRecording");
        // Microphone.End(null);
        isRecording = false;
        // SaveAudioClipToFile();
        subscribers.ForEach(subscriber => subscriber.onRecordingStopped());
    }

    public void ToggleRecording()
    {
        Debug.Log("AudioRecorder.ToggleRecording");
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
