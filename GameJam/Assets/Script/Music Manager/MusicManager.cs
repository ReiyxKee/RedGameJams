using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MusicTrack
{
    public string AudioTitle; 
    public AudioClip clip; 
}

public class MusicManager : MonoBehaviour
{
    // Singleton instance
    private static MusicManager instance;
    public static MusicManager Instance { get { return instance; } }

    // Music audio source
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    // Music tracks playlist
    public MusicTrack[] musicTracks;
    private int currentTrackIndex = 0;

    // UI elements
    [Header("UI Elements")]
    public Button playButton;
    public Button pauseButton;
    public Button stopButton;
    public Button nextButton;
    public Slider volumeSlider;
    public Button toggleCanvasButton; 
    public Canvas musicCanvas;

    private AudioClip currentMusicClip;
    private bool isPaused = false;

    private void Awake()
    {
        // Singleton pattern: Ensure only one instance of MusicManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Add event listeners to the buttons
        playButton.onClick.AddListener(PlayMusic);
        pauseButton.onClick.AddListener(PauseMusic);
        stopButton.onClick.AddListener(StopMusic);
        volumeSlider.onValueChanged.AddListener(SetMusicVolume);
        nextButton.onClick.AddListener(PlayNextTrack);
        // Add listener to the toggle canvas button
        toggleCanvasButton.onClick.AddListener(ToggleMusicCanvas);

        musicSource.volume = 1.0f;

        if (musicTracks.Length > 0)
        {
            PlayNextTrack();
        }

        volumeSlider.value = musicSource.volume;
    }

    public void PlayNextTrack()
    {
        if (musicTracks.Length > 0)
        {
            StopMusic();
            currentMusicClip = musicTracks[currentTrackIndex].clip; // Store the current clip
            musicSource.clip = currentMusicClip;
            musicSource.Play();
            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        }
    }

    public void PauseMusic()
    {
        musicSource.Pause();
        isPaused = true;
    }

    public void StopMusic()
    {
        musicSource.Stop();
        isPaused = false;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void PlayMusic()
    {
        if (musicTracks.Length > 0)
        {
            if (isPaused)
            {
                musicSource.Play();
                isPaused = false;
            }
            else
            {
                StopMusic();
                PlayCurrentTrackFromStart();
            }
        }
    }

    private void PlayCurrentTrackFromStart()
    {
        if (currentMusicClip != null)
        {
            musicSource.clip = currentMusicClip;
            musicSource.Play();
        }
    }

    public void PlayNext()
    {
        PlayNextTrack();
    }

    private void ToggleMusicCanvas()
    {
        musicCanvas.gameObject.SetActive(!musicCanvas.gameObject.activeSelf);
    }
}
