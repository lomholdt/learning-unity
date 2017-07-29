using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[SerializeField]
	private AudioSource musicSource;

	[SerializeField]
	private AudioSource sfxSource;


	[SerializeField]
	private AudioSource musicSlider;

	[SerializeField]
	private AudioSource sfxSlider;

	Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	private static SoundManager _instance;

	public static SoundManager Instance { get { return _instance; } }

	// Use this for initialization
	void Start () {
		AudioClip[] clips = Resources.LoadAll<AudioClip> ("Audio") as AudioClip[];

		foreach (AudioClip clip in clips) {
			audioClips.Add (clip.name, clip);
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	public void PlaySfx(string name)
	{
		sfxSource.PlayOneShot (audioClips [name]);
	}
}
