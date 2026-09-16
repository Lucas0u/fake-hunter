using UnityEngine;

public class MusicaMenu : MonoBehaviour
{
    [Header("Áudio")]
    public AudioSource fonteMusica;       // AudioSource com Loop marcado
    public AudioClip musicaFundo;
    [Range(0f, 1f)] public float volume = 0.5f;

    void Start()
    {
        if (fonteMusica == null || musicaFundo == null) return;

        fonteMusica.clip = musicaFundo;
        fonteMusica.loop = true;
        fonteMusica.volume = volume;
        fonteMusica.Play();
    }
}
