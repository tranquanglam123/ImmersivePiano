using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Models;
using UnityEngine;

public static class Music
{
    private static string[] _twinkleLittleStar = new string[28]
    {
        "C4", "C4", "G4", "G4","A4", "A4", "G4", "F4", "F4", "E4", "E4", "D4", "D4", "C4",
        "G4", "G4", "F4", "F4", "E4", "E4", "D4", "G4", "G4", "F4", "F4", "E4", "E4", "D4", 
    };
    
    private static string[] _maryHadALittleLamb = new string[26]
    {
        "E4", "D4", "C4", "D4","E4", "E4", "E4", "D4", "D4", "D4", "E4", "E4", "E4",
        "E4", "D4", "C4", "D4", "E4", "E4", "E4", "E4", "D4", "D4", "E4", "D4", "C4"
    };
    
    private static string[] _jingleBells = new string[49]
    {
        "E4", "E4", "E4", "E4","E4", "E4", "E4", "G4", "C4", "D4", "E4",
        "F4", "F4", "F4", "F4", "F4", "E4", "E4", "E4", "E4", "D4", "D4", "E4", "D4", "G4", 
        "E4", "E4", "E4", "E4","E4", "E4", "E4", "G4", "C4", "D4", "E4",
        "F4", "F4", "F4", "F4", "F4", "E4", "E4", "E4", "G4", "G4", "F4", "D4", "C4", 
    };

    public static string[] GetNotes(Songs songNumber)
    {
        switch (songNumber)
        {
            case Songs.TwinkleLittleStar:
                return _twinkleLittleStar;
            case Songs.MaryHadALittleLamb:
                return _maryHadALittleLamb;
            case Songs.JingleBells:
                return _jingleBells;
            default:
                return new string[0];
        }
    }
}
public enum Songs
    {
        TwinkleLittleStar,
        MaryHadALittleLamb,
        JingleBells
    }