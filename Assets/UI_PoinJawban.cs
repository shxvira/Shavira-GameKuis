using TMPro;
using UnityEngine;

public class UI_PoinJawban : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _teksJawaban = null;

    [SerializeField]
    private bool _adalahBenar = false;

    public void PilihJawaban()
    {
        _tempatPesan.Pesan = $"Jawaban Anda Adalah {_teksJawaban.text} ({_adalahBenar})";
        //Debug.Log($"Jawaban Anda adalah {_teksJawaban.text} ({_adalahBenar})");
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar)
    {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}
