using UnityEngine;
using System.IO;

public static class WavToAudioClip
{
    public static AudioClip ConvertToAudioClip(byte[] wavData)
    {
        // Create a new MemoryStream from the byte[] data
        MemoryStream stream = new MemoryStream(wavData);

        // Create a new BinaryReader from the MemoryStream
        BinaryReader reader = new BinaryReader(stream);

        // Read the WAV file header
        string riffHeader = new string(reader.ReadChars(4));
        int riffChunkSize = reader.ReadInt32();
        string waveHeader = new string(reader.ReadChars(4));
        string fmtHeader = new string(reader.ReadChars(4));
        int fmtChunkSize = reader.ReadInt32();
        int audioFormat = reader.ReadInt16();
        int numChannels = reader.ReadInt16();
        int sampleRate = reader.ReadInt32();
        int byteRate = reader.ReadInt32();
        int blockAlign = reader.ReadInt16();
        int bitsPerSample = reader.ReadInt16();

        // Read the data chunk header
        string dataHeader = new string(reader.ReadChars(4));
        int dataChunkSize = reader.ReadInt32();

        // Read the audio data
        byte[] audioData = reader.ReadBytes(dataChunkSize);

        // Create a new AudioClip
        AudioClip audioClip = AudioClip.Create("wav", audioData.Length / 2, numChannels, sampleRate, false);

        // Set the audio data
        audioClip.SetData(ConvertByteToFloat(audioData), 0);

        // Close the BinaryReader and MemoryStream
        reader.Close();
        stream.Close();

        // Return the AudioClip
        return audioClip;
    }

    private static float[] ConvertByteToFloat(byte[] byteArray)
    {
        float[] floatArr = new float[byteArray.Length / 2];
        for (int i = 0; i < floatArr.Length; i++)
        {
            floatArr[i] = (float)System.BitConverter.ToInt16(byteArray, i * 2) / 32768f;
        }
        return floatArr;
    }
}
