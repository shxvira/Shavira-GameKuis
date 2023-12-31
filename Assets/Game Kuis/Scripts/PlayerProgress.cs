using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progressLevel;
    }

    [SerializeField]
    private string _filename = "progres.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // Simpan Starting Data saat objek Dictionary tidak ada saat dimuat
        if (progressData.progressLevel == null)
        {
            progressData.progressLevel = new();
            progressData.koin = 0;
            progressData.progressLevel.Add(_startingLevelPackName, 1);
        }

        // Informasi penyimpanan data
#if UNITY_EDITOR
        var directory = Application.dataPath + "/Temporary";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif

        string path = directory + "/" + _filename;

        // Membuat Directory Temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        // Membuat file baru
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        // Memuat data dari file menggunakan binari formatter
        var fileStream = File.Open(path, FileMode.Open);
        var formatter = new BinaryFormatter();

        formatter.Serialize(fileStream, progressData);

        //// Menyimpan data ke dalam file menggunakan binari writer
        //var writer = new BinaryWriter(fileStream);

        //writer.Write(progressData.koin);
        //foreach (var i in progressData.progressLevel)
        //{
        //    writer.Write(i.Key);
        //    writer.Write(i.Value);
        //}

        //// Putuskan aliran memori dengan file
        //writer.Dispose();
        //fileStream.Dispose();

        Debug.Log($"{_filename} Berhasil Disimpan");
    }

    public bool MuatProgress()
    {
        // Informasi penyimpanan data
#if UNITY_EDITOR
        var directory = Application.dataPath + "/Temporary";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif
        var path = directory + "/" + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);

            try
            {
                progressData.koin = reader.ReadInt32();
                if (progressData.progressLevel == null)
                    progressData.progressLevel = new();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progressData.progressLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                // Putuskan aliran memori dengan file
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                // Putuskan aliran memori dengan file
                reader.Dispose();
                fileStream.Dispose();

                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress\n{e.Message}");

                return false;
            }

            //// Memuat data dari file menggunakan binari formatter
            //var formatter = new BinaryFormatter();

            //progressData = (MainData)formatter.Deserialize(fileStream);

            // Putuskan aliran memori dengan file
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progressLevel.Count}");
        }
        catch (System.Exception e)
        {
            // Putuskan aliran memori dengan file
            fileStream.Dispose();

            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress\n{e.Message}");

            return false;
        }
       

        return true;
    }
}
