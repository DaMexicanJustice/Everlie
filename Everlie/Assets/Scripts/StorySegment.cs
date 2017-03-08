using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySegment : ScriptableObject {
	public List<AudioClip> introAudioTracks = new List<AudioClip>();
	public AudioClip introMasterSoundClip;
	public GameObject introUIGraphics;

	public MiniGame myMiniGame;

	public List<AudioClip> outroAudioTracks = new List<AudioClip> ();
	public AudioClip outroMasterSoundClip;
	public GameObject outroUIGraphics;

	private AudioSource introAudioSource;

	public void Play(){
		foreach (AudioClip ac in introAudioTracks) {
			AudioSource audioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
			audioSource.clip = ac;
			audioSource.Play ();
		}

		introAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
		introAudioSource.clip = introMasterSoundClip;
		introAudioSource.Play ();
	}

	public void Update(){
		if (!introAudioSource.isPlaying) {
			
		}
	}
}