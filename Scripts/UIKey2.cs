using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649 // Disable warnings for unassigned serialized fields

namespace ProcGenMusic
{
	// The UIKey2 class handles the behavior of a piano key in the user interface.
	// It inherits from NetworkBehaviour, enabling synchronized network interactions for multiplayer functionality.
	public class UIKey2 : NetworkBehaviour
	{
		// The Transform component of the key, initialized privately with a getter for access.
		public Transform Transform { get; private set; }

		// Reference to the Piano object to interact with music generation.
		public Piano mPiano;

		// A light component used to animate the light intensity when the key is pressed.
		[SerializeField] private Light sceneLight;

		// The maximum intensity the light will reach during animation.
		[SerializeField] private float maxIntensity = 1f;

		// Boolean to ensure only one animation runs at a time.
		private bool isAnimating = false;

		// Note index corresponding to this key, set in the editor via the Tooltip attribute.
		[SerializeField, Tooltip("Note index to which this key corresponds")]
		private int mNoteIndex;

		// Method triggered when the key is clicked by the user (Mouse Down event).
		private void OnMouseDown()
		{
			// If the music generator is currently playing or repeating, don't allow manual key presses.
			if (mPiano.mMusicGenerator.GeneratorState == GeneratorState.Playing ||
			    mPiano.mMusicGenerator.GeneratorState == GeneratorState.Repeating)
			{
				return;
			}

			// Get the instrument type and volume from the currently selected instrument in the MusicGenerator.
			var instrumentName = mPiano.mMusicGenerator.InstrumentSet.Data.Instruments[mPiano.instrumentIndex].InstrumentType;
			var volume = mPiano.mMusicGenerator.InstrumentSet.Data.Instruments[mPiano.instrumentIndex].Volume;

			// If the instrument is percussion, set the note index to 0, otherwise use the key's note index.
			var noteIndex = mPiano.mMusicGenerator.InstrumentSet.Data.Instruments[mPiano.instrumentIndex].IsPercussion ? 0 : mNoteIndex;

			// Start animating the light when the key is pressed.
			StartCoroutine(AnimateLightIntensity());

			// Call the RPC to play the corresponding audio clip for this key.
			RPC_PlayAudioClip(instrumentName, noteIndex, volume, mPiano.instrumentIndex);
		}

		// Remote Procedure Call (RPC) to play the audio clip for all clients in the network.
		[Rpc(RpcSources.All, RpcTargets.All)]
		private void RPC_PlayAudioClip(string instrumentName, int noteIndex, float volume, int instrumentIndex)
		{
			// Play the selected instrument's audio clip with the appropriate note index and volume.
			mPiano.mMusicGenerator.PlayAudioClip(mPiano.mMusicGenerator.InstrumentSet, instrumentName, noteIndex, volume, instrumentIndex);
		}

		// Coroutine to animate the light intensity when a key is pressed.
		private IEnumerator AnimateLightIntensity()
		{
			// Mark the animation as running to prevent overlapping animations.
			isAnimating = true;

			// Duration of the light fade in and fade out animation.
			float duration = 0.1f;
			float elapsedTime = 0f;

			// Gradually increase the light intensity over the duration (fade in).
			while (elapsedTime < duration)
			{
				sceneLight.intensity = Mathf.Lerp(0f, maxIntensity, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			// Ensure the intensity reaches exactly the maximum value.
			sceneLight.intensity = maxIntensity;

			// Optional wait for a brief moment (can be skipped if not needed).
			yield return null;

			// Reset elapsed time to fade out the light (decrease intensity).
			elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				sceneLight.intensity = Mathf.Lerp(maxIntensity, 0f, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			// Ensure the intensity is fully reset to 0 (light off).
			sceneLight.intensity = 0f;

			// Mark the animation as complete, allowing new key presses to trigger animations.
			isAnimating = false;
		}
	}
}
