using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProcGenMusic
{
    // The Piano class inherits from NetworkBehaviour, which means it supports networking features (synchronization across clients).
    public class Piano : NetworkBehaviour
    {
        // UI elements for selecting instruments (buttons for moving up and down the list of instruments).
        [SerializeField] private DemoOnClick mInstrumentUp;
        [SerializeField] private DemoOnClick mInstrumentDown;

        // Reference to the MusicGenerator that holds the instrument data and other music-related settings.
        public MusicGenerator mMusicGenerator;

        // Text fields in the UI to display the currently selected instrument and its index.
        [SerializeField] public TMP_Text Instrument;
        [SerializeField] public TMP_Text SelectedInstrument;

        // Demo parameters, such as color palettes, used for setting up the UI.
        [SerializeField] public DemoParameters mDemoParameters;

        // Tracks the index of the currently selected instrument.
        public int instrumentIndex = 0;

        // This method is called when the object is spawned in the network.
        public override void Spawned()
        {
            // Initialize the "Instrument Up" button, set its color and subscribe to the OnSelected event.
            mInstrumentUp.Initialize(mDemoParameters.mColorPalette.ColorFields[1].Color, mDemoParameters);
            mInstrumentUp.OnSelected += OnInstrumentUpSelected;

            // Initialize the "Instrument Down" button, set its color and subscribe to the OnSelected event.
            mInstrumentDown.Initialize(mDemoParameters.mColorPalette.ColorFields[2].Color, mDemoParameters);
            mInstrumentDown.OnSelected += OnInstrumentDownSelected;

            // Set the initial text for the selected instrument index and its type.
            SelectedInstrument.text = $"{instrumentIndex + 1}";
            Instrument.text = mMusicGenerator.InstrumentSet.Data.Instruments[0].InstrumentType;
        }

        // Method triggered when the "Instrument Up" button is pressed.
        public void OnInstrumentUpSelected()
        {
            // Increment the instrument index and wrap it around if it exceeds the list length.
            instrumentIndex = (instrumentIndex + 1) % mMusicGenerator.InstrumentSet.Data.Instruments.Count;

            // Update the UI text to show the new instrument's index and its type.
            SelectedInstrument.text = $"{instrumentIndex + 1}";
            Instrument.text = mMusicGenerator.InstrumentSet.Data.Instruments[instrumentIndex].InstrumentType;
        }

        // Method triggered when the "Instrument Down" button is pressed.
        public void OnInstrumentDownSelected()
        {
            // Decrement the instrument index, and if it's below 0, wrap it around to the last instrument in the list.
            instrumentIndex = (instrumentIndex - 1) < 0 ? mMusicGenerator.InstrumentSet.Data.Instruments.Count - 1 : instrumentIndex - 1;

            // Update the UI text to show the new instrument's index and its type.
            SelectedInstrument.text = $"{instrumentIndex + 1}";
            Instrument.text = mMusicGenerator.InstrumentSet.Data.Instruments[instrumentIndex].InstrumentType;
        }
    }
}
