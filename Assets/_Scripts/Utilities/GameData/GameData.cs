using System;
using System.Collections.Generic;
using UnityEngine;

// store data across the game
[Serializable]
public class GameData
{
    [SerializeField]
    public Dictionary<string, bool> IllustrationStats {get; private set;}
    [SerializeField]
    public Dictionary<string, bool> otherStats {get; private set;}
    [SerializeField]
    public Dictionary<string, bool> cardStats {get; private set;}
    [SerializeField]
    public Dictionary<string, bool> sceneCheckPoints {get; private set;}
    [SerializeField]
    public bool isGetReceptionKey;

    public GameData(){
        IllustrationStats = new Dictionary<string, bool>(){
            {"food", false},
            {"portrait_mya",false},
            {"portrait_rumii",false},
            {"portrait_rabbi",false},
            {"portrait_luna",false},
            {"hospital_entrance_mya", false},
            {"video_room_mya", false},
            {"cleaner_room_mya_poster", false},
            {"cleaner_room_gummy_poster", false},
            {"corridor_posters", false},
            {"general_ward_little_cat_poster", false},
            {"general_ward_mya_poster", false},
            {"gummy_tachie", false},  
        };

        otherStats = new Dictionary<string, bool>(){
            {"teahouse_password",false},
            {"watch_whole_poo_room_video",false},
            {"skip_poo_room_video", false},
            {"mya_ending",false},
            {"gummy_ending",false},
        };

        cardStats = new Dictionary<string, bool>(){
            {"teahouse_staffroom_card", false},
            {"cleaner_room_card", false},
            {"general_ward_card", false},
            {"hospital_entrance_card", false},
        };

        sceneCheckPoints = new Dictionary<string, bool>(){
            {"Prologue", false},
            {"Teahouse", false},
            {"Hospital_Entrance", false},
            {"Hospital_Poo_Room", false},
            {"Hospital_Ward", false},
        };

        isGetReceptionKey = false;
    }
}