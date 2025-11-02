using System;
using TMPro;
using UnityEngine;

public class Mic : MonoBehaviour
{
    public TextMeshProUGUI _text;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    private void Start()
    {
        StartRecording();
    }

    private void Update()
    {

        if (recording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }

        //get volume
        if (recording)
        {
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);
            float sum = 0f;
            for (int i = 0; i < samples.Length; i++)
            {
                sum += samples[i] * samples[i];
            }

            float rms = Mathf.Sqrt(sum / samples.Length);
            float db = 20 * Mathf.Log10(rms / 0.1f);
            _text.text = "DB: " + db.ToString("F2");

        }
    }

    private void StartRecording() {
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
    }
    
    private void StopRecording() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        // bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
    }

    
}
