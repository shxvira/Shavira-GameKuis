using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawban[] _pilihanJawaban = new UI_PoinJawban[0];

    private int _indexSoal = -1;

    private void Start()
    {
        if (!_playerProgress.MuatProgress())
        {
            _playerProgress.SimpanProgress();
        }
        

        NextLevel();
    }

    public void NextLevel()
    {
       // Soal index selanjutnya
       _indexSoal++;

       // Jika index melampaui soal terakhir, ulang dari awal
       if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            _indexSoal = 0;
        }

        // Ambil data pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //Set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}",
            soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
