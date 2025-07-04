using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Scene Souns")]
    public AudioSource clickSound;
    public AudioSource MainMenuMusic;

    [Header("Shading")]
    public RawImage fadeImage;
    public float fadeDuration = 1.5f;
    public CanvasGroup uiGroup;

    [Header("scene to load in start")]
    public string sceneToLoad = "GameScene";



    private void Start()
    {
        PlayBackgroundMusic();

        if (fadeImage != null)
        {
            
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
        }
    }
    public void StartGame()
    {
        PlayClickSound();

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(FadeAndLoadScene());
        }
        else
        {
            Debug.LogWarning("Scene name not set in MainMenuManager.");
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        if (fadeImage != null)
        {
            float elapsed = 0f;
            Color color = fadeImage.color;
            
            if (uiGroup != null)
            {
                uiGroup.interactable = false;
                uiGroup.blocksRaycasts = false;
            }

            while (elapsed < fadeDuration)
            {
                float t = elapsed / fadeDuration;
                float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);                
                fadeImage.color = new Color(color.r, color.g, color.b, alpha);
                if (uiGroup != null)
                {
                    uiGroup.alpha = Mathf.Lerp(1f, 0f, t);
                }
                elapsed += Time.deltaTime;
                yield return null;
            }
            if (uiGroup != null) uiGroup.alpha = 0f;            
            fadeImage.color = new Color(color.r, color.g, color.b, 1f);
        }

        yield return new WaitForSeconds(0.2f); // brief pause before scene switch
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        PlayClickSound();
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    private void PlayBackgroundMusic()
    {
        if (MainMenuMusic != null && !MainMenuMusic.isPlaying)
        {           
            MainMenuMusic.loop = true;
            MainMenuMusic.Play();
        }
    }
}
