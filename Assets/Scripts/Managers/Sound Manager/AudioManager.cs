using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixer audioMixer;
	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}

	public void Play(string sound, int index=-1)
	{
		if (index > 0) sound += " " + UnityEngine.Random.Range(1, index+1);

		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public bool IsPlaying(string sound, int index=-1) {
		Sound s = null;

		if (index > 0) {
			for (int i = 0; i < index; i++) {
				string currSound = sound + " " + (i+1);
				s = Array.Find(sounds, item => item.name == currSound);
				if (s.source.isPlaying) return true;
			}

			return false;
		}
		
		s = Array.Find(sounds, item => item.name == sound);
		return s.source.isPlaying;
	}

	public void StopAllSound() {
		foreach (Sound s in sounds) {
			s.source.Stop();
		}
	}

	public void StopAllInMixer(string mixerName) {
		Sound[] ms = Array.FindAll(sounds, item => item.mixerGroup.name == mixerName);

		foreach (Sound s in ms) {
			s.source.Stop();
		}
	}
}
