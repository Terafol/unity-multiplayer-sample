using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

#pragma warning disable 0649

namespace ProcGenMusic
{
    public class DemoTest : NetworkBehaviour
{
    private MusicGenerator mMusicGenerator;
        [Networked, OnChangedRender(nameof(ModeChanged))]
        private Mode Mode { get; set; }

        private void Start()
        {
            // Initialization logic here if needed
            // Note: Ensure that NetworkBehaviours are fully initialized
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_ChangeMode(Mode value)
        {
                Mode = value;
        }

        private void ModeChanged()
        {
            // Ensure mMusicGenerator is properly initialized
            if (mMusicGenerator != null)
            {
                mMusicGenerator.ConfigurationData.Mode = Mode;
                Debug.LogWarning(mMusicGenerator.ConfigurationData.Mode);
            }
            else
            {
                Debug.LogError("mMusicGenerator is not assigned.");
            }
        }

        // Ensure to include a way to set mMusicGenerator if needed
        public void SetMusicGenerator(MusicGenerator musicGenerator)
        {
            mMusicGenerator = musicGenerator;
        }
    }

}

// [SerializeField] private MusicGenerator mMusicGenerator;
        // [SerializeField] private MusicGeneratorUIPanel mMusicGeneratorUIPanel;



        // private Transform mTransform;

        // // [Networked, OnChangedRender(nameof(TempoChanged))] private float Tempo { get; set; }
        // // [Networked, OnChangedRender(nameof(VolumeChanged))] private float Volume { get; set; }
        // // [Networked, OnChangedRender(nameof(KeyChanged))] private Key Key { get; set; }
        // // [Networked, OnChangedRender(nameof(ModeChanged))] private Mode Mode { get; set; }
