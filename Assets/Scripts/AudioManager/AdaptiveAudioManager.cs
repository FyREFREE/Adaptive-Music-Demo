using UnityEngine.Audio;
using System;
using UnityEngine;

public class AdaptiveAudioManager : MonoBehaviour
{

	//public static AdaptiveAudioManager instance;

	public AudioMixerGroup mixerGroup;

	public AnimationCurve curve;
	public float fadeDuration = 3f;
	public Sound[] layers;

	private bool isPaused = false;


	void Awake()
	{

		if (layers == null)
		{
			Debug.LogWarning("The Adaptive Sound Manager has not been given any audio to play. Add audio tracks to the layers[] array in the editor.");
			return;
		}	
		
		
		foreach (Sound s in layers)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;

			s.source.volume = s.volume;
		}
	}

	void Start()
	{
		foreach(Sound s in layers)
		{
			s.source.Play();
		}
	}

	void FixedUpdate()
	{
		if(layers[0].isActive)
		{
			masterIsActive = true;
		}
		else
		{
			masterIsActive = false;
		}

		foreach(Sound s in layers)
		{
			s.Update();
		}
	}

	public void setFadeDuration(float d)
	{
		fadeDuration = d;
	}

	public bool masterIsActive = true;
	public void Play(int index)
	{
		if(index == 0)
		{
			if(masterIsActive)
			{
				fadeOut(layers[index]);
			}
			else
			{
				fadeOut();
				fadeIn(layers[index]);
			}
		}
		else if(layers[index].isActive)
		{
			fadeOut(layers[index]);
		}
		else
		{
			fadeOut(layers[0]);
			fadeIn(layers[index]);
		}
	}

    public void fadeOut(Sound sound)
	{
		sound.fadeOut(curve, fadeDuration);
	}

	public void fadeOut()
	{
		foreach(Sound s in layers)
		{
			s.fadeOut(curve, fadeDuration);
		}
	}

	public void fadeIn(Sound sound)
	{
		sound.fadeIn(curve, fadeDuration);
	}

	public void fadeIn()
	{
		foreach(Sound s in layers)
		{
			s.fadeIn(curve, fadeDuration);
		}
	}

	public void pause()
	{
		foreach(Sound s in layers)
		{
			if(isPaused)
			{
				s.source.UnPause();
				isPaused = false;
			}
			else
			{
				s.source.Pause();
				isPaused = true;
			}
		}
	}
}
	