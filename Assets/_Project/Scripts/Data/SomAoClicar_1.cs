using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SomAoClicar : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (UIAudioManager.Instance != null)
                UIAudioManager.Instance.TocarClique();
        });
    }
}
