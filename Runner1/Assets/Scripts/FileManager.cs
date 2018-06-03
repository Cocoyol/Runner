
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManager : MonoBehaviour {

    //private string _filePath;
    public string filePath { get; set; }

    private void Awake() {
        filePath = Application.persistentDataPath + "/gamedata.dat";
    }

    public int LoadGameData(ref GameData gData) {
        Debug.Log(filePath);
        int fileStatus = 1;
        if(File.Exists(filePath)) {
            try {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(filePath, FileMode.Open);

                gData = bf.Deserialize(stream) as GameData;

                stream.Close();
                fileStatus = 0;
            } catch {
                fileStatus = 2;
            }
        }

        return fileStatus;
    }

    public void SaveGameData(ref GameData gData) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        bf.Serialize(stream, gData);

        stream.Close();
    }
}
