using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [System.Serializable]
    public struct DataSoal
    {
        public string pertanyaan;
        public Sprite petunjukJawaban;

        public string[] pilihanJawaban;
        public bool[] adalahBenar;
    }

    [SerializeField]
    private DataSoal[] _soalSoal = new DataSoal[0];

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawban[] _pilihanJawaban = new UI_PoinJawban[0];

    private int _indexSoal = -1;

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {
       // Soal index selanjutnya
       _indexSoal++;

       // Jika index melampaui soal terakhir, ulang dari awal
       if (_indexSoal >= _soalSoal.Length)
        {
            _indexSoal = 0;
        }

        // Ambil data pertanyaan
        DataSoal soal = _soalSoal[_indexSoal];

        //Set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}",
            soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawban poin = _pilihanJawaban[i];
            poin.SetJawaban(soal.pilihanJawaban[i], soal.adalahBenar[i]);
        }
    }
}
