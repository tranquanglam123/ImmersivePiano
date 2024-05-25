using System;
using System.Collections;
using System.Threading;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TestScript : MonoBehaviour
{
    private sealed class ThreadTickGenerator 
    {
        public event EventHandler TickGenerated;

        private Thread _thread;

        public void TryStart()
        {
            if (_thread != null)
                return;

            _thread = new Thread(() =>
            {
                var stopwatch = new System.Diagnostics.Stopwatch();
                var lastMs = 0L;

                stopwatch.Start();

                while (true)
                {
                    var elapsedMs = stopwatch.ElapsedMilliseconds;
                    if (elapsedMs - lastMs >= 1)
                    {
                        TickGenerated?.Invoke(this, EventArgs.Empty);
                        lastMs = elapsedMs;
                    }
                }
            });

            _thread.Start();
        }

        public void Dispose()
        {
        }
    }

    public GameObject gameNoteObj;

    private Playback _playback;
    private OutputDevice _outputDevice;


    // Start is called before the first frame update
    void Start()
    {
        var midiFile = MidiFile.Read("Assets/StreamingAssets/MIDI/Yiruma - Rivers Flow In You.mid");
        
        _outputDevice = OutputDevice.GetByIndex(0);
        //_playback = midiFile.GetPlayback(_outputDevice, new MidiClockSettings
        //{
        //    CreateTickGeneratorCallback = _ => new ThreadTickGenerator()
        //});
        _playback = midiFile.GetPlayback(_outputDevice, new PlaybackSettings
        {
            
        });

        _playback.NotesPlaybackFinished += Test;
        _playback.InterruptNotesOnStop = true;
        StartCoroutine(StartMusic());
    }

    private void Test(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        //var Notes = sender.GetNotes();
        //var tempoMap = midiFile.GetTempoMap();
        foreach (var note in notesList)
        {
            //GameObject nObj = Instantiate(gameNoteObj);
            Debug.Log("Note name " + note.NoteName + ", Note number " + note.NoteNumber + ", Note channel " + note.Channel);
            //nObj.GetComponent<GameNoteObj>().InitGameNote(note.TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 100000.0f, note.NoteNumber, note.LengthAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / 100000f * NoteWidth, note.Channel);
        }
        //foreach (Note item in notesList)
        //{
        //    Debug.Log(item);
        //}
        //foreach (var note in notesList)
        //{
        //    GameObject nObj = Instantiate(gameNoteObj);
        //    nObj.GetComponent<GameNote>().InitGameNote(note.Time / 1000f * NoteWidth, note.NoteNumber, note.Length / 1000f * NoteWidth, note.Channel);

        //}
        //foreach (var note in notesList)
        //{
        //    GameObject nobj = Instantiate(gameNoteObj);
        //    nobj.GetComponent<GameNoteObj>().InitGameNote(note.Time / 1000f, note.NoteNumber, note.Length / 1000f , note.Channel);

        //}
    }
    private void TestGetNotes(MidiFile midifile)
    {
        var notelist = midifile.GetNotes();
        foreach (var note in notelist)
        {
            GameObject noteobj = Instantiate(gameNoteObj);
            noteobj.GetComponent<GameNoteObj>().InitGameNote(note.Time / 1000f, note.NoteNumber, note.Length / 1000f, note.Channel);
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