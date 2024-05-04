using UnityEngine.Audio;
using System;
using UnityEngine;

public class AdaptiveAudioManager : MonoBehaviour
{
	[Tooltip("A container for a Curves script. This script stores AnimationCurves in an array. (Optional)")]public Curves optionalCurveSource;

	[Tooltip("The default mixer group that all sounds will inherit. This can be changed per-sound.")] public AudioMixerGroup mixerGroup;
	[Tooltip("The default fade curve that all sounds will inherit. This can be changed using the setCurve(AnimationCurve) method.")] public AnimationCurve defaultCurve;
	[Tooltip("The default fade duration that all sounds will inherit. This can be changed using the setFadeDuration(float) method.")]public float fadeDuration = 3f;
	[Tooltip("When true, all sounds will restart after they are finished.")]public bool loop;
	[Tooltip("When true, the stopStart() method will be called before the first Update() call.")]public bool playOnStart;
	[Tooltip("Add your sounds here.")]public Sound[] layers;
	
	public bool isPaused = false;
	public bool isStopped = true;
	private bool exclusiveIsActive;

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
			s.source.loop = loop;
			s.source.outputAudioMixerGroup = mixerGroup;
			s.source.volume = s.volume;

			if(s.isExclusive && s.isActive)
				exclusiveIsActive = true;
		}
	}

	void Start()
	{
		if(playOnStart)
			stopStart();
	}

	void FixedUpdate()
	{
		foreach(Sound s in layers)
			s.Update();
	}

	public void setFadeDuration(float d)
	{
		fadeDuration = d;
	}

	public void setCurve(AnimationCurve curve)
	{
		defaultCurve = curve;
	}

	public void Play(int index)
	{
		if(layers[index].isExclusive)
		{
			if(layers[index].isActive)
			{
				fadeOut(layers[index]);
				exclusiveIsActive = false;
			}
			else
			{
				fadeOut();
				fadeIn(layers[index]);
				exclusiveIsActive = true;
			}
		}
		else if(exclusiveIsActive)
		{
			fadeOut();
			fadeIn(layers[index]);
			exclusiveIsActive = false;
		}
		else
		{
			if(layers[index].isActive)
				fadeOut(layers[index]);
			else
				fadeIn(layers[index]);
		}
	}

	public void stopStart()
	{
		if(isStopped)
		{
			foreach(Sound s in layers)
			{	
				s.source.Play();
			}
			isStopped = false;
		}
		else
		{
			foreach(Sound s in layers)
			{
				s.source.Stop();
				
			}
			isStopped = true;
		}
	}

	public void playPause()
	{
		if(isPaused)
		{
			foreach(Sound s in layers)
			{	
				s.source.pitch = 1;
			}
			isPaused = false;
		}
		else
		{
			foreach(Sound s in layers)
			{
				s.source.pitch = 0;
				
			}
			isPaused = true;
		}
	}



	private void fadeOut(Sound sound)
	{
		sound.fadeOut(defaultCurve, fadeDuration);
	}

	private void fadeOut()
	{
		foreach(Sound s in layers)
		{
			s.fadeOut(defaultCurve, fadeDuration);
		}
	}

	private void fadeIn(Sound sound)
	{
		sound.fadeIn(defaultCurve, fadeDuration);
	}

	private void fadeIn()
	{
		foreach(Sound s in layers)
		{
			s.fadeIn(defaultCurve, fadeDuration);
		}
	}

}
	