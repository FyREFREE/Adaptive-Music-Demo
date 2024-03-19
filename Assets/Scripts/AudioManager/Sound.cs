using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
 {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;


	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

	private bool isFadingIn; 
	
	private bool isFadingOut;
	private float elapsedTimeIn;
	private float elapsedTimeOut;
	 [SerializeField]
	private float percentageCompleteIn;
	 [SerializeField]
	private float percentageCompleteOut;
	private float fadeDuration;
	private AnimationCurve curve;

	public bool isActive;



	public void Update()
	{
		//Debug.Log("" + name + " fading in status: " + isFadingIn);
		//Debug.Log("" + name + " fading out status: " + isFadingOut);
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
				//Debug.Log("To fade in = " + toFadeIn.name);
				isFadingIn = true;
				float startingVol = source.volume;
				elapsedTimeIn += Time.deltaTime; 
				percentageCompleteIn = elapsedTimeIn / fadeDuration;

				source.volume = Mathf.Lerp(startingVol, 1, curve.Evaluate(percentageCompleteIn));

				//Debug.Log(percentageComplete);
				if(percentageCompleteIn >= 1)
				{
					fadeInEnd();
				}
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
			//Debug.Log("To fade out = " + toFadeOut.name);
			isFadingOut = true;
			float startingVol = source.volume;
			elapsedTimeOut += Time.deltaTime;
			percentageCompleteOut = elapsedTimeOut / fadeDuration;

			source.volume = Mathf.Lerp(startingVol, 0, curve.Evaluate(percentageCompleteOut));
			
			//Debug.Log(percentageComplete);
			if(percentageCompleteOut >= 1)
			{
				fadeOutEnd();
			}
		}
	}

	public void fadeOutEnd()
	{
		isFadingOut = false;
		percentageCompleteOut = 0;
		elapsedTimeOut = 0;
	}

	

}
