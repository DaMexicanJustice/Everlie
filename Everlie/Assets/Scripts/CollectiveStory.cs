using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveStory : ScriptableObject {
	public List<StorySegment> story = new List<StorySegment>();

	private int currentIndex = 0;

	public void Initiate(){
		story [currentIndex].Play ();
	}

	public void Update(){
		story [currentIndex].Update ();
	}
}