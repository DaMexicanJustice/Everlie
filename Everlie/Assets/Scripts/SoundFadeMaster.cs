using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeMaster : Singleton<SoundFadeMaster> {

	struct AudioFadePair{
		AudioSource fromAudio;
		private float fromStartVolume;

		AudioSource toAudio;
		private float toStartVolume;

		public float fadeTime;
		public float progress;

		public AudioFadePair(AudioSource fromAudio, AudioSource toAudio, float fadeTime){
			this.fromAudio = fromAudio;
			fromStartVolume = fromAudio.volume;

			this.toAudio = toAudio;
			toStartVolume = toAudio.volume;
			toAudio.Play ();

			this.fadeTime = fadeTime;
			progress = 0f;
		}

		public void EvaluateFade(){
			float fraction = Mathf.Max (progress / fadeTime, 1);
			fromAudio.volume = fromStartVolume * (1f - fraction);
			toAudio.volume = toStartVolume * fraction;
		}

		public void Clear(){
			Destroy (fromAudio);
		}

		public bool Contains(AudioSource ac){
			if (fromAudio != ac && toAudio != ac) {
				return false;
			} else {
				return true;
			}
		}

		public void AddProgress(float deltaTime){
			progress += deltaTime;
		}
	}

	protected SoundFadeMaster()
	{
		// Since the Constructor is protected an instance can't be made of the Toolbox
	} 

	static bool applicationIsQuitting = false;

	private static List<AudioFadePair> fadePairs = new List<AudioFadePair> ();

	public static void FadeSound(AudioSource fromAudio, AudioSource toAudio, float fadeTime){
		foreach (AudioFadePair afp in fadePairs) {
			if (!afp.Contains (fromAudio)) {
				AudioFadePair newAudioFadePair = new AudioFadePair (fromAudio, toAudio, fadeTime);
				fadePairs.Add (newAudioFadePair);
			}
		}
	}

	void Update(){
		if (fadePairs != null) {
			if (fadePairs.Count > 0) {
				foreach (AudioFadePair afp in fadePairs) {
					if (afp.progress <= afp.fadeTime) {
						afp.AddProgress(Time.deltaTime);
						afp.EvaluateFade ();
					} else {
						afp.Clear ();
						fadePairs.Remove (afp);
					}
				}
			}
		}
	}
}