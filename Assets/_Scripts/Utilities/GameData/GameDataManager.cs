using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

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
        try {
            string savePath = Application.persistentDataPath + "/" + folderName;
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + folderName);
                GLogger.Log("file does not exist and is created: " + savePath);
                
            }
            else{
                GLogger.Log("file exist: " + savePath);
            }
        }
        catch {
            GLogger.Log("cannot create save folder, user temp save data only");
        }

    }

    // save when gameData has updated
    public void SaveGame()
    {
        try {
            saveFilePath = Application.persistentDataPath + "/" + folderName + "/save" + ".data";
            FileStream dataStream = new FileStream(saveFilePath, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, gameData);
            dataStream.Close();
        }
        catch {
            GLogger.LogError("cannot save game, can only use temp save data in this game");
        }
    }

    //load on start
    public GameData LoadGame()
    {
        try {
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
                GLogger.LogWarning("Save file not found in: " + saveFilePath + " - use new data instead");
                return new GameData();
            }
        }
        catch{
            GLogger.LogError("cannot load game, can only use new data in each load new game");
            return new GameData();
        }

    }

    public void UnlockCard(string _cardName){
        GLogger.Log("set card: " + _cardName);
        gameData.cardStats[_cardName] = true;
        SaveGame();
    }
    
    public void UnlockIllustration(string _illustrationName){
        GLogger.Log("set illustration: " + _illustrationName);
        gameData.IllustrationStats[_illustrationName] = true;
        SaveGame();
    }

    public void UnlockScene(string _sceneName){
        GLogger.Log("set unlock scene: " + _sceneName);
        gameData.IllustrationStats[_sceneName] = true;
        SaveGame();
    }

    public void UnlockOther(string _otherName){
        GLogger.Log("unlock other: " + _otherName);
        gameData.otherStats[_otherName] = true;
        SaveGame();
    }

    public void LogWholeSaving(){
        string outputString = "";
        outputString += "isGetReceptionKey: " + gameData.isGetReceptionKey;
        outputString += " ---illustration--- ";
        foreach(KeyValuePair<string, bool> entry in gameData.IllustrationStats){
            outputString +=  entry.Key + ": " + entry.Value + " - ";
        }
        outputString += " ---other--- ";
        foreach(KeyValuePair<string, bool> entry in gameData.otherStats){
            outputString +=  entry.Key + ": " + entry.Value + " - ";
        }
        outputString += " ---card--- ";
        foreach(KeyValuePair<string, bool> entry in gameData.cardStats){
            outputString +=  entry.Key + ": " + entry.Value + " - ";
        }
        outputString += " ---sceneCheckPoints--- ";
        foreach(KeyValuePair<string, bool> entry in gameData.sceneCheckPoints){
            outputString +=  entry.Key + ": " + entry.Value + " - ";
        }
        outputString += " ------ ";
        GLogger.Log("LogSaveFile: " + outputString);
    }
}
