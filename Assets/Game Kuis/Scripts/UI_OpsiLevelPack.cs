using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<UI_OpsiLevelPack, LevelPackKuis, bool> EventSaatKlik;

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [Space, Header("Properti Pengunci Level Pack")]
    [SerializeField]
    private TextMeshProUGUI _labelTerkunci = null;

    [SerializeField]
    private TextMeshProUGUI _labelHarga = null;

    [SerializeField]
    private bool _terkunci = false;

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
        EventSaatKlik?.Invoke(this, _levelPack,_terkunci);
    }

    public void KunciLevelPack()
    {
        _terkunci = true;
        _labelTerkunci.gameObject.SetActive(true);
        _labelHarga.transform.parent.gameObject.SetActive(true);
        _labelHarga.text = $"{_levelPack.Harga}";
    }

    public void BukaLevelPack()
    {
        _terkunci = false;
        _labelTerkunci.gameObject.SetActive(false);
        _labelHarga.transform.parent.gameObject.SetActive(false);
    }
}
