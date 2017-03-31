using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName= "Everlie/", fileName="New MiniGame")]
public abstract class MiniGame : ScriptableObject {
	public List<AudioClip> audioTracks = new List<AudioClip>();
	public AudioClip masterSoundClip;
	public GameObject UIGraphics;

	public AudioSource minigameAudioSource;

	public void Initialize(){
		minigameAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
		minigameAudioSource.clip = masterSoundClip;
		minigameAudioSource.Stop ();
		minigameAudioSource.loop = true;
	}

	public void Play(){
		Instantiate (UIGraphics, UIGraphics.transform.position, UIGraphics.transform.rotation);
	}

	public abstract float Update ();
}