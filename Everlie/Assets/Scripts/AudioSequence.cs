using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSequence {
	public List<AudioClip> audioTracks;
	public AudioClip masterSoundClip;
	public GameObject UIGraphics;

	[HideInInspector]
	public float segmentLength;
	public float fadeOutTime;
}