using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
 {

	[Tooltip("The name of the track.")] public string name;
	[Tooltip("Place your audio file here.")] public AudioClip clip;
	[Tooltip("The starting volume of your track. If this is the track that the music starts with, set this to 1, otherwise set to 0.")][Range(0f, 1f)] [Min(0f)] public float volume = 1f;
	[Tooltip("If enabling this track will disable all other tracks, and vice versa.")]public bool isExclusive;
	[Tooltip("The mixer group of this track.")]public AudioMixerGroup mixerGroup;
	[Space(5)]
	[Header("Script Controlled Variables - Should not edit.")]
	[Tooltip("When true, this track is faded in.")] public bool isActive;
	private bool isFadingIn; 
	private bool isFadingOut;
	private float elapsedTimeIn;
	private float elapsedTimeOut;
	private float percentageCompleteIn;
	private float percentageCompleteOut;
	private float fadeDuration;
	private AnimationCurve curve;
	
	[HideInInspector] public AudioSource source;

	public void Update()
	{
		if(isFadingIn == true)
		{
			fadeIn(curve, fadeDuration);
		}

		if(isFadingOut == true)
		{
			fadeOut(curve, fadeDuration);
		}
	}

	
	public void fadeIn(AnimationCurve curve, float fadeDuration)
	{
		this.fadeDuration = fadeDuration;
		this.curve = curve;
		if(isFadingOut)
		{
			fadeOutEnd();
			isFadingIn = true;
		}
		else
		{
				isActive = true;
				isFadingIn = true;
				float startingVol = source.volume;
				elapsedTimeIn += Time.deltaTime; 
				percentageCompleteIn = elapsedTimeIn / fadeDuration;

				source.volume = Mathf.Lerp(startingVol, 1, curve.Evaluate(percentageCompleteIn));

				if(percentageCompleteIn >= 1)
					fadeInEnd();
		}
	}

	public void fadeInEnd()
	{
		isFadingIn = false;
		percentageCompleteIn = 0;
		elapsedTimeIn = 0;
	}

	public void fadeOut(AnimationCurve curve, float fadeDuration)
	{
		this.fadeDuration = fadeDuration;
		this.curve = curve;
		if(isFadingIn)
		{
			fadeInEnd();
			isFadingOut = true;
		}
		else
		{
			isActive = false;
			isFadingOut = true;
			float startingVol = source.volume;
			elapsedTimeOut += Time.deltaTime;
			percentageCompleteOut = elapsedTimeOut / fadeDuration;

			source.volume = Mathf.Lerp(startingVol, 0, curve.Evaluate(percentageCompleteOut));
			
			if(percentageCompleteOut >= 1)
				fadeOutEnd();
		}
	}

	public void fadeOutEnd()
	{
		isFadingOut = false;
		percentageCompleteOut = 0;
		elapsedTimeOut = 0;
	}

	

}
