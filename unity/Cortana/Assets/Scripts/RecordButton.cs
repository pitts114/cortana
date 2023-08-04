using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordButton : MonoBehaviour
{
    public GameObject gameObject;
    public AudioRecorder audioRecorder;

    public RecordButton(GameObject gameObject, AudioRecorder audioRecorder)
    {
        this.gameObject = gameObject;
        this.audioRecorder = audioRecorder;

        audioRecorder.subscribe(this);
        Debug.Log("RecordButton.RecordButton");
    }

    public void handleClick()
    {
        Debug.Log("RecordButton.handleClick");
        audioRecorder.ToggleRecording();
    }

    public void onRecordingStarted()
    {
        Debug.Log("RecordButton.onRecordingStarted");
        gameObject.GetComponent<Image>().color = Color.red;
    }

    public void onRecordingStopped()
    {
        Debug.Log("RecordButton.onRecordingStopped");
        gameObject.GetComponent<Image>().color = Color.white;
    }
}
