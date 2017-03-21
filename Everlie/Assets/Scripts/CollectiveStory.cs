using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Everlie/Collective Story", fileName="Collective Story", order=0)]
public class CollectiveStory : ScriptableObject {

	public List<StorySegment> story = new List<StorySegment>();

	private int currentIndex = 0;

	public void Initiate(){
		story [currentIndex].Play ();
		story [currentIndex].OnBeganFade += OnReceivedFade;
	}

	public void Update(){
		story [currentIndex].Update ();
	}

	public void OnReceivedFade(){
		if (story.Count > currentIndex) {
			story [currentIndex + 1].Play ();
		}
	}
}