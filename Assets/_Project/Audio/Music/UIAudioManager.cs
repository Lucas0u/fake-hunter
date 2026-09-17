using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("Áudio")]
    public AudioSource fonteSom;          // AudioSource com Play On Awake desmarcado
    public AudioClip somClique;
    [Range(0f, 1f)] public float volume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarClique()
    {
        if (fonteSom == null || somClique == null) return;
        fonteSom.PlayOneShot(somClique, volume);
    }
}
