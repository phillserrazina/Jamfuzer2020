using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] AudioMixer aMixer = null;
    [SerializeField] TMP_Dropdown resolutionDropdown = null;
    [SerializeField] Animator camAnim = null;

    Resolution[] resolutions;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        var am = FindObjectOfType<AudioManager>();
        am?.StopAllSound();
        am?.Play("Menu Theme");
    }

    public void SetResolution(int resIndex)
    {
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
    }

    public void StartGame()
    {
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        camAnim.SetTrigger("BlinkNoEvent");
        yield return new WaitForSeconds(0.4f);
        FindObjectOfType<AudioManager>()?.Play("Blink");
        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        if (aMixer != null)
            aMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SwitchScreen(GameObject e)
    {
        camAnim.SetTrigger("BlinkNoEvent");
        StartCoroutine(switchMe(e));
    }

    IEnumerator switchMe(GameObject e)
    {
        camAnim.SetTrigger("BlinkNoEvent");
        yield return new WaitForSeconds(0.4f);
        if (e.activeSelf)
        {
            e.SetActive(false);

        }
        else
        {
            e.SetActive(true);
        }
    }
}
