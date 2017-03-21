using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Everlie/Story Segment", fileName="Story Segment", order=1)]
public class StorySegment : ScriptableObject {

	public enum StorySegmentState{
		intro,
		minigame,
		outro,
		done
	}

	[HideInInspector]
	public StorySegmentState currentState = StorySegmentState.intro;

	public AudioSequence intro;
	public MiniGame myMiniGame;
	public AudioSequence outro;

	private AudioSource introAudioSource;
	private AudioSource outroAudioSource;

	public delegate void voidEvent ();
	public voidEvent OnBeganFade;

	[HideInInspector]
	public float segmentTimer;

	public void Play(){
		#region introAudio
		foreach (AudioClip ac in intro.audioTracks) {
			AudioSource audioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
			audioSource.clip = ac;
			audioSource.Play ();
		}

		introAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
		introAudioSource.clip = intro.masterSoundClip;
		introAudioSource.Play ();
		introAudioSource.loop = false;

		intro.segmentLength = intro.masterSoundClip.length;
		#endregion

		myMiniGame.Initialize ();

		#region outroAudio
		foreach (AudioClip ac in outro.audioTracks) {
			AudioSource audioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
			audioSource.clip = ac;
			audioSource.Play ();
			audioSource.loop = false;
		}

		outroAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
		outroAudioSource.clip = outro.masterSoundClip;
		outroAudioSource.Play ();
		outroAudioSource.loop = false;

		outro.segmentLength = outro.masterSoundClip.length;
		#endregion
	}

	public void Update(){
		switch(currentState){
		case StorySegmentState.intro:
			Intro ();
			break;
		case StorySegmentState.minigame:
			Minigame ();
			break;
		case StorySegmentState.outro:
			Outro ();
			break;
		case StorySegmentState.done:
			Done ();
			break;
		}
	}

	public void Intro(){
		segmentTimer += Time.deltaTime;

		if (segmentTimer > intro.segmentLength - intro.fadeOutTime) {
			if (introAudioSource != null) {
				SoundFadeMaster.FadeSound (introAudioSource, myMiniGame.minigameAudioSource, intro.fadeOutTime);
			} else {
				currentState = StorySegmentState.minigame;
				myMiniGame.Play ();
				segmentTimer = 0f;
			}
		}
	}

	public void Minigame(){
		myMiniGame.Update ();
	}

	public void Outro(){
		segmentTimer += Time.deltaTime;

		if (segmentTimer > outro.segmentLength - outro.fadeOutTime) {
			currentState = StorySegmentState.done;
		}
	}

	public void Done(){
		
	}
}