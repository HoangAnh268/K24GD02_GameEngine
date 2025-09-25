using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip menuMusic;       
    public AudioClip gameMusic;

    private bool isMusicOn = true;
    private bool isSFXOn = true;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //Lấy lại âm thanh từ playerPrefab
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;

        // Lấy lại trạng thái on/off
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSFXOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        musicSource.mute = !isMusicOn;
        sfxSource.mute = !isSFXOn;
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || !isMusicOn) return;

        // Nếu đang chạy nhạc này rồi thì không phát lại
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.Stop();          // Tắt nhạc cũ
        musicSource.clip = clip;     // Gán nhạc mới
        musicSource.loop = true;
        musicSource.Play();
    }    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    // Nhạc riêng cho gameplay
    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }
    public void PlaySFX(AudioClip clip)
    {
        if(clip == null || !isSFXOn) return;
        sfxSource.PlayOneShot(clip);
    }
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }    
    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        sfxSource.mute = !isSFXOn;
        PlayerPrefs.SetInt("SFXon", isSFXOn ? 1 : 0);
        PlayerPrefs.Save();
    }    
}
