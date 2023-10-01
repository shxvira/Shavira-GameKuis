using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<LevelPackKuis> EventSaatKlik;

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    private void Start()
    {
        if (_levelPack != null)
            SetLevelPack(_levelPack);

        // Subscribe events
        _tombol.onClick.AddListener(SaatKlik);
    }

    private void OnDestroy()
    {
        // Unsubscribe event
        _tombol.onClick.RemoveListener(SaatKlik);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    private void SaatKlik()
    {
        // Debug.Log("KLIK!!!");
        EventSaatKlik?.Invoke(_levelPack);
    }
}
