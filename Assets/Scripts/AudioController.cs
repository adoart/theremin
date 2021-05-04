using UnityEngine;

public class AudioController : MonoBehaviour {
    [Range(65, 3000)] //Creates a slider in the inspector
    public float frequency1;

    private float _phase = 0;

    [Range(0, 1)] public float volume;
    public float sampleRate = 44100;
    [Range(0, 100000)] public float pitchDistanceMult;
    [Range(0, 100)] public float volumeDistanceMult;

    [Range(1.0f, 3000.0f)] public float pitchValue = 1.0f;
    AudioSource audioSource;
    int timeIndex = 0;
    
    [Space]
    [Range(0.1f, 2000)]
    public float pitchSensitivity;
    [Range(0, 50)]
    public float pitchMax;
    [Range(0, 10)]
    public float pitchMin;


    void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        // audioSource.Stop(); //avoids audiosource from starting to play automatically
        audioSource.volume = 0.5f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!audioSource.isPlaying) {
                timeIndex = 0; //resets timer before playing sound
                audioSource.Play();
            }
            else {
                audioSource.Stop();
            }
        }
    }

    private void OnGUI() {
        audioSource.volume = volume;
        // audioSource.pitch = pitchValue;
    }

    void OnAudioFilterRead(float[] data, int channels) {
        for (int i = 0; i < data.Length; i += channels) {
            _phase += 2 * Mathf.PI * frequency1 / sampleRate;

            data[i] = Mathf.Sin(_phase);

            if (_phase >= 2 * Mathf.PI) {
                _phase -= 2 * Mathf.PI;
            }
        }
    }


    public void SetVolume(float distance) {
        float audioSourceVolume = Mathf.Clamp(1 - (distance * volumeDistanceMult), 0, 1);
        // Debug.Log("volume[" + audioSourceVolume + "] distance[" + distance + "]");
        audioSource.volume = audioSourceVolume;
    }

    public void SetPitch(float distance) {
        // float frequency = 65.0f;//Mathf.Clamp(3000 - (distance * pitchDistanceMult), 65, 3000);
        // float pitch = Mathf.Clamp(3000 - (distance * pitchDistanceMult), 1, 3000);
        float pitch = Mathf.Exp(-distance * pitchSensitivity) * (pitchMax - pitchMin) + pitchMin;
        // Debug.Log("frequency[" + frequency + "] distance[" + distance + "]");
        Debug.Log("frequency[" + frequency1 * pitch + "] pitch[" + pitch + "] distance[" + distance + "]");
        // frequency1 = frequency;
        audioSource.pitch = pitch;
    }
}