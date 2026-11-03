using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemMusicGroup : MonoBehaviour
{
    public static SistemMusicGroup instance;
    public AudioClip[] DataSource;
    public AudioSource SourceEffect;
    public AudioSource SourceMusicBackground;

    private void Awake()
    {
       Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusic(scene.name);
    }
    private void ChangeMusic(string sceneName)
    {
        int targetIndex = -1;

        if (sceneName == "Profile" || sceneName == "MainMenu" || 
            sceneName == "Learning" || sceneName == "GameOver")
        {
            targetIndex = 6; 
        }
        else if (sceneName.Contains("Quiz")) 
        {
            targetIndex = 7; 
        }


        if (targetIndex != -1)
        {
            if (SourceMusicBackground.clip != DataSource[targetIndex])
            {
                SourceMusicBackground.clip = DataSource[targetIndex];
                SourceMusicBackground.Play();
            }
        }
    }

    public void SoundEffect(int IDMusic)
    {
        if (DataSource != null && IDMusic >= 0 && IDMusic < DataSource.Length)
        {
            SourceEffect.PlayOneShot(DataSource[IDMusic]);
        }
        else
        {
            Debug.LogWarning("ID Suara tidak valid atau DataSuara belum diisi!");
        }
    }
}
