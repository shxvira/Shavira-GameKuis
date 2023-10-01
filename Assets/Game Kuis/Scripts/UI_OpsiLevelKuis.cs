using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelKuis : MonoBehaviour
{
    public static event System.Action<int> EventSaatKlik;

    [SerializeField]
    private Button _tombolLevel = null;
    
    [SerializeField]
    private TextMeshProUGUI _levelName = null;

    [SerializeField]
    private LevelSoalKuis _levelKuis = null;

    private void Start()
    {
        if (_levelKuis != null)
            SetLevelKuis(_levelKuis, _levelKuis.levelPackIndex);

        // Subscribe event
        _tombolLevel.onClick.AddListener(SaatKlik);
    }

    private void OnDestroy()
    {
        // Unsubsribe event
        _tombolLevel.onClick.RemoveListener(SaatKlik);
    }

    public void SetLevelKuis(LevelSoalKuis levelKuis, int index)
    {
        _levelName.text = levelKuis.name;
        _levelKuis = levelKuis;

        _levelKuis.levelPackIndex = index;
    }

    private void SaatKlik()
    {
        EventSaatKlik?.Invoke(_levelKuis.levelPackIndex);
    }
}
