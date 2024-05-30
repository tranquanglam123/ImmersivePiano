using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System.Collections;
using UnityEngine;

public class SynthesizerManager : MonoBehaviour
{

    public GameObject gameNoteObj;

    private Playback _playback;
    private OutputDevice _outputDevice;

    // Start is called before the first frame update
    void Start()
    {
        var midiFile = MidiFile.Read("Assets/HangEight_SylvielmnaZjsch.mid");
        _outputDevice = OutputDevice.GetByIndex(0);
        //_playback = midiFile.GetPlayback(_outputDevice, new MidiClockSettings
        //{
        //    CreateTickGeneratorCallback = interval => new RegularPrecisionTickGenerator(interval)
        //});

        _playback.NotesPlaybackFinished += Test;
        _playback.InterruptNotesOnStop = true;
        StartCoroutine(StartMusic());


    }

    private void Test(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        foreach (Note item in notesList)
        {
            Debug.Log(item);
        }
    }

    private IEnumerator StartMusic()
    {
        _playback.Start();
        while (_playback.IsRunning)
        {

            yield return null;

        }
        _playback.Dispose();

    }

    private void OnApplicationQuit()
    {
        _playback.Stop();
        _playback.Dispose();
    }
}