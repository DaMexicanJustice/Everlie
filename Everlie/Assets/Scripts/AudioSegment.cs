using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Everlie/Audio Segment", fileName="Audio Segment", order=1)]
public class AudioSegment : StorySegment {

	public AudioSequence audioSegment;

	private AudioSource introAudioSource;

	[HideInInspector]
	public float segmentTimer;

    private GameObject graphicsObject;

	public float scaleModifier = 0.1f;

	public override void Play(){
		segmentTimer = 0f;

		introAudioSource = Camera.main.gameObject.AddComponent<AudioSource> ();
	    introAudioSource.volume = 1f;
		introAudioSource.clip = audioSegment.masterSoundClip;
		introAudioSource.time = startDelay;
		introAudioSource.Play ();
		introAudioSource.loop = false;

		audioSegment.segmentLength = audioSegment.masterSoundClip.length;

	    SoundFadeMaster.FadeSound(introAudioSource, 3f, false);

	    graphicsObject = Instantiate(audioSegment.UIGraphics, GameObject.FindGameObjectWithTag("GameController").transform);
	    graphicsObject.GetComponent<RectTransform>().offsetMax = Toolbox.Instance.rt.offsetMax;
	    graphicsObject.GetComponent<RectTransform>().offsetMin = Toolbox.Instance.rt.offsetMin;
	    graphicsObject.transform.localScale = Vector3.one;
	}

	public override void Update(){
		segmentTimer += Time.deltaTime;

		graphicsObject.transform.localScale = Vector3.one * (1 + ((segmentTimer / audioSegment.segmentLength) * scaleModifier));

	    if (segmentTimer >= audioSegment.segmentLength - 3)
	    {
			SoundFadeMaster.FadeSound(introAudioSource, 3, true);
			Toolbox.FindRequiredComponent<GameMaster> ().StartCoroutine (WaitForGraphicsFade ());
			OnSegmentCompleted ();
		}
	}

	public override void Clear(){
		Destroy (introAudioSource);
		Destroy (graphicsObject);
	}

	public override void Skip(){
		SoundFadeMaster.FadeSound(introAudioSource, 3, true);
		Toolbox.FindRequiredComponent<GameMaster> ().StartCoroutine (WaitForGraphicsFade ());
		OnSegmentCompleted ();
	}
}