using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// this class exists among the whole game for saving and loading data
public class GameDataManager : MonoBehaviour
{
    public GameData gameData;
    public static GameDataManager instance;
    private string saveFilePath;
    private string folderName = "save";

    private void Awake(){
        CreateFolder();
        gameData = LoadGame();
    }

    //create folder for once only if there is none in the path
    private void CreateFolder()
    {
        string savePath = Application.persistentDataPath + "/" + folderName;
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + folderName);
            Debug.Log("file does not exist and is created: " + savePath);
            
        }
        else{
            Debug.Log("file exist: " + savePath);
        }
    }

    // save when gameData has updated
    public void SaveGame()
    {
        saveFilePath = Application.persistentDataPath + "/" + folderName + "/save" + ".data";
        FileStream dataStream = new FileStream(saveFilePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, gameData);
        dataStream.Close();
    }

    //load on start
    public GameData LoadGame()
    {
        saveFilePath = Application.persistentDataPath + "/" + folderName + "/save" + ".data";
        if (File.Exists(saveFilePath))
        {
            FileStream dataStream = new FileStream(saveFilePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            GameData saveData = converter.Deserialize(dataStream) as GameData;
            dataStream.Close();
            return saveData;
        }
        else
        {
            // File does not exist
            Debug.LogWarning("Save file not found in: " + saveFilePath);
            return new GameData();
        }
    }

    public void UnlockCard(string _cardName){
        Debug.Log(_cardName);
        gameData.cardStats[_cardName] = true;
        SaveGame();
    }
    
    public void UnlockIllustration(string _illustrationName){
        Debug.Log(_illustrationName);
        gameData.illustrationStats[_illustrationName] = true;
        SaveGame();
    }
}
