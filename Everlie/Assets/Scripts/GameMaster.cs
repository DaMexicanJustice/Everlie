using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public delegate void voidEvent ();
	public voidEvent OnStorySegmentEnded;

	private CollectiveStory story;
	private int index = 0;

	void Awake () {
		Toolbox.RegisterComponent<GameMaster> (this);

		story = (CollectiveStory) Resources.Load ("StorySegments");
		story.Initiate();
	}

	void Update(){
		story.Update ();
	}
}