using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System;

public class AudioRecorder : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject gameObject;
    private AudioClip audioClip;
    private bool isRecording = false;

    public void StartRecording()
    {
        audioClip = Microphone.Start(null, false, 10, 16000);
        isRecording = true;
        gameObject.GetComponent<Image>().color = Color.red;
    }

    public string StopRecording()
    {
        Microphone.End(null);
        isRecording = false;
        gameObject.GetComponent<Image>().color = Color.white;
        string filePath = SaveAudioClipToFile();
        return filePath;
    }

    public async void ToggleRecording()
    {
        if (isRecording)
        {
            string filePath = StopRecording();
            byte[] response = await SendFileAndGetResponse(filePath, "http://localhost:4567/");
            // save response to file
            string responseFilePath = Application.persistentDataPath + "/response.wav";
            SavWav.Save(responseFilePath, WavUtility.ToAudioClip(response));
            PlayAudio(response);
        }
        else
        {
            StartRecording();
        }
    }

    public void PlayAudio(byte[] response)
    {
        AudioClip audioClip = WavToAudioClip.ConvertToAudioClip(response);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public async Task<byte[]> SendFileAndGetResponse(string filePath, string url)
    {
        // mimic the command:
        // curl -X POST --data-binary @audio.wav http://desktop.home:8080/stt
        // but also tell server that request is sending octet-stream
        var client = new HttpClient();
        var fileStream = File.OpenRead(filePath);
        var content = new StreamContent(fileStream);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        var response = await client.PostAsync(url, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        else
        {
            throw new Exception($"Failed to send file: {response.StatusCode}");
        }
    }

    private string SaveAudioClipToFile()
    {
        string filePath = Application.persistentDataPath + "/audio.wav";
        SavWav.Save(filePath, audioClip);
        return filePath;
    }
}
