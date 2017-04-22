using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySegment : ScriptableObject {
    public virtual void Play()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Clear()
    {

    }

    public delegate void VoidEvent();
	public VoidEvent OnSegmentCompleted;
}