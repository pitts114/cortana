using UnityEngine;

public static class WavUtility
{
    public static AudioClip ToAudioClip(byte[] wavData)
    {
        // Determine if the system is little-endian
        bool littleEndian = System.BitConverter.IsLittleEndian;

        // Read the WAV file header
        int headerSize = 44;
        int channels = wavData[22];
        int sampleRate = System.BitConverter.ToInt32(wavData, 24);
        int dataSize = System.BitConverter.ToInt32(wavData, 40);
        int samples = dataSize / 2;

        // Create a new AudioClip
        AudioClip audioClip = AudioClip.Create("TempClip", samples, channels, sampleRate, false);

        // Copy the audio data into the AudioClip
        float[] audioData = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            int offset = i * 2 + headerSize;
            short sample = (short)(wavData[offset] | (wavData[offset + 1] << 8));
            if (littleEndian)
            {
                sample = (short)(sample << 8 | (sample >> 8 & 0xFF));
            }
            audioData[i] = sample / 32768f;
        }
        audioClip.SetData(audioData, 0);

        return audioClip;
    }
}
