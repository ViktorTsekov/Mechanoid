using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundtrack;

    public AudioSource minigunSfx;
    public AudioSource robotMovementSfx;
    public AudioSource rocketBoostSfx;
    public AudioSource rocketLauncherSfx;

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, float> maxVolumes = new Dictionary<string, float>();

    void Awake()
    {
        audioSources.Add("soundtrack", soundtrack);

        audioSources.Add("minigunSfx", minigunSfx);
        audioSources.Add("robotMovementSfx", robotMovementSfx);
        audioSources.Add("rocketBoostSfx", rocketBoostSfx);
        audioSources.Add("rocketLauncherSfx", rocketLauncherSfx);

        maxVolumes.Add("soundtrack", soundtrack.volume);

        maxVolumes.Add("minigunSfx", minigunSfx.volume);
        maxVolumes.Add("robotMovementSfx", robotMovementSfx.volume);
        maxVolumes.Add("rocketBoostSfx", rocketBoostSfx.volume);
        maxVolumes.Add("rocketLauncherSfx", rocketLauncherSfx.volume);

        updateTracks();
    }

    public void updateTracks()
    {
        audioSources["soundtrack"].volume = maxVolumes["soundtrack"] * PlayerPrefs.GetFloat("soundtrackVolume", 1f);

        audioSources["minigunSfx"].volume = maxVolumes["minigunSfx"] * PlayerPrefs.GetFloat("sfxVolume", 1f);
        audioSources["robotMovementSfx"].volume = maxVolumes["robotMovementSfx"] * PlayerPrefs.GetFloat("sfxVolume", 1f);
        audioSources["rocketBoostSfx"].volume = maxVolumes["rocketBoostSfx"] * PlayerPrefs.GetFloat("sfxVolume", 1f);
        audioSources["rocketLauncherSfx"].volume = maxVolumes["rocketLauncherSfx"] * PlayerPrefs.GetFloat("sfxVolume", 1f);
    }

    public AudioSource getTrack(string key)
    {
        return audioSources[key];
    }
}
