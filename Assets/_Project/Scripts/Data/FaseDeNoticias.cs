using UnityEngine;

[CreateAssetMenu(fileName = "NovaFase", menuName = "FakeHunter/FaseDeNoticias")]
public class FaseDeNoticias : ScriptableObject
{
    [Header("Notícias desta fase, na ordem que devem aparecer")]
    public Noticia[] noticias;
}