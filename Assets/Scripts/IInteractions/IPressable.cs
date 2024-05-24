using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPressable
{
    void Press();

    void StopPressing();
}
