using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// store data across the game
[Serializable]
public class GameData
{
    [SerializeField]
    public Dictionary<string, bool> illustrationStats = new Dictionary<string, bool>(){
        {"portrait_mya",false},
        {"portrait_rumii",false},
        {"portrait_rabbi",false},
        {"portrait_luna",false},
        {"gummy_poster", false},
        {"little_cat_poster", false},
        {"gummy_2D_are", false}
    };

    [SerializeField]
    public Dictionary<string, bool> otherStats = new Dictionary<string, bool>(){
        {"teahouse_password",false},
        {"watch_poo_room_video",false},
        {"mya_ending",false},
        {"gummy_ending",false},
    };

    [SerializeField]
    public Dictionary<string, bool> cardStats = new Dictionary<string, bool>(){
        {"teahouse_staffroom_card", false},
        {"cleaner_room_card", false},
    };

    public bool isPooRoomVideoForcelyStopped = false;
    public bool isGetReceptionKey = false;

}
