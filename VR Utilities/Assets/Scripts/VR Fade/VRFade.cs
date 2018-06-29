using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// VRFade class controls the fading in and out 
/// (From clear to black and vice versa).
/// Used for scene and game transitions
/// </summary>
public class VRFade : MonoBehaviour
{
    #region Singleton Instance

    private static VRFade _instance;
    static public VRFade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VRFade>();
            }
            return _instance;
        }
    }
    #endregion

    #region Class Variables
    [SerializeField]
    private bool fadeToClearOnStart = true;
    [SerializeField]
    public CanvasGroup fadeCanvas;
    [SerializeField]
    private float defaultFadeTime = 3f;
    private bool inTransition = false;
    public bool InTransition { get { return inTransition; } }
    private bool fadeToClear = false;
    private bool fadeToBlack = false;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        //if checked fade in on start, useful for starting a level
        if (fadeToClearOnStart)
        {
            FadeToClear(null);
        }
    }


    public bool FadingStatus()
    {
        //return false only if fade
        if (!fadeToClear && !fadeToBlack)
            return false;
        else
            return true;
    }

    /// <summary>
    /// Triggers fade in coroutine and allows for function callback on completion
    /// Fade in is the transition from black to clear
    /// </summary>
    /// <param name="fadeToClearCallback"></param>
    public void FadeToClear(Action fadeToClearCallback = null)
    {
        //if already fading in dont fade again
        if (fadeToClear) return;
        fadeToClear = true;
        StartCoroutine(FadeToClearCoroutine(fadeToClearCallback));
    }

    /// <summary>
    /// Triggers fade out coroutine and allows for function callback on completion
    /// Fade out is the transition from clear to black
    /// </summary>
    /// <param name="fadeToBlackCallback"></param>
    public void FadeToBlack(Action fadeToBlackCallback = null)
    {
        //if already fading out dont fade again
        if (fadeToBlack) return;
        fadeToBlack = true;
        StartCoroutine(FadeToBlackCoroutine(fadeToBlackCallback));
    }

    /// <summary>
    /// Overload function allows for control over fade time with additional parameter
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadeToClearCallback"></param>
    public void FadeToClear(float duration, Action fadeToClearCallback = null)
    {
        //if already fading in dont fade again
        if (fadeToClear) return;

        fadeToClear = true;
        StartCoroutine(FadeToClearCoroutine(duration, fadeToClearCallback));
    }

    /// <summary>
    /// Overload function allows for control over fade time with the additional parameter
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadeToBlackCallback"></param>
    public void FadeToBlack(float duration, Action fadeToBlackCallback = null)
    {
        //if already fading out dont fade again
        if (fadeToBlack) return;

        fadeToBlack = true;
        StartCoroutine(FadeToBlackCoroutine(duration, fadeToBlackCallback));
    }

    /// <summary>
    /// Triggers the fade in and fade out transition with parameters allowing for
    /// functions to be called after each transition
    /// </summary>
    /// <param name="fadeToBlackCallback"></param>
    /// <param name="fadeToClearCallback"></param>
    public void FadeToBlackToClear(Action fadeToBlackCallback = null, Action fadeToClearCallback = null)
    {
        FadeToBlack(() =>
        {
            if (fadeToBlackCallback != null)
            {
                fadeToBlackCallback();
            }
            FadeToClear(fadeToClearCallback);
        });
    }

    /// <summary>
    /// Triggers the fade in and fade out transition with parameters allowing for
    /// functions to be called after each transition
    /// </summary>
    /// <param name="fadeToBlackCallback"></param>
    /// <param name="fadeToClearCallback"></param>
    public void FadeToBlackToClear(float duration, Action fadeToBlackCallback = null, Action fadeToClearCallback = null)
    {
        FadeToBlack(duration / 2, () =>
        {
            if (fadeToBlackCallback != null)
            {
                fadeToBlackCallback();
            }
            FadeToClear(duration / 2, fadeToClearCallback);
        });
    }

    /// <summary>
    /// Coroutine for fade in transition
    /// </summary>
    /// <param name="fadeToClearCallback"></param>
    /// <returns></returns>
    IEnumerator FadeToClearCoroutine(Action fadeToClearCallback = null)
    {
        float currentTime = 0;
        inTransition = true;
        do
        {
            currentTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, currentTime / defaultFadeTime);

            yield return null;
        } while (fadeCanvas.alpha != 0);
        inTransition = false;
    

        //fade to clear finished
        fadeToClear = false;
        if (fadeToClearCallback != null)
        {
            fadeToClearCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade in with extra parameter controlling fade duration
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadeToClearCallback"></param>
    /// <returns></returns>
    IEnumerator FadeToClearCoroutine(float duration, Action fadeToClearCallback = null)
    {
        float currentTime = 0;
        inTransition = true;
        do
        {
            currentTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, currentTime / duration);

            yield return null;
        } while (fadeCanvas.alpha != 0);
        inTransition = false;

        //fade to clear finished
        fadeToClear = false;
        if (fadeToClearCallback != null)
        {
            fadeToClearCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade out transition
    /// </summary>
    /// <param name="fadeToBlackCallback"></param>
    /// <returns></returns>
    IEnumerator FadeToBlackCoroutine(Action fadeToBlackCallback = null)
    {
        float currentTime = 0;
        inTransition = true;
        do
        {
            currentTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, currentTime / defaultFadeTime);

            yield return null;
        } while (fadeCanvas.alpha != 1);
        inTransition = false;

        //fade to black finished
        fadeToBlack = false;
        if (fadeToBlackCallback != null)
        {
            fadeToBlackCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade out with extra parameter controlling fade duration
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadeToBlackCallback"></param>
    /// <returns></returns>
    IEnumerator FadeToBlackCoroutine(float duration, Action fadeToBlackCallback = null)
    {
       
        float currentTime = 0;
        inTransition = true;
        do
        {
            currentTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, currentTime / duration);

            yield return null;
        } while (fadeCanvas.alpha != 1);

        inTransition = false;

        //fade to black finished
        fadeToBlack = false;
        if (fadeToBlackCallback != null)
        {
            fadeToBlackCallback();
        }
    }

}
