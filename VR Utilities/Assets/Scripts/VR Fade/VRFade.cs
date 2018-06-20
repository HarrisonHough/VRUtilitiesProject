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
    private bool fadeInOnStart = true;
    [SerializeField]
    public CanvasGroup fadeCanvas;
    [SerializeField]
    private float defaultFadeTime = 3f;
    private bool inTransition = false;
    public bool InTransition { get { return inTransition; } }
    private bool fadeIn = false;
    private bool fadeOut = false;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        //if checked fade in on start, useful for starting a level
        if (fadeInOnStart)
        {
            FadeIn(null);
        }
    }


    public bool FadingStatus()
    {
        //return false only if fade
        if (!fadeIn && !fadeOut)
            return false;
        else
            return true;
    }

    /// <summary>
    /// Triggers fade in coroutine and allows for function callback on completion
    /// Fade in is the transition from black to clear
    /// </summary>
    /// <param name="fadedInCallback"></param>
    public void FadeIn(Action fadedInCallback = null)
    {
        //if already fading in dont fade again
        if (fadeIn) return;
        fadeIn = true;
        StartCoroutine(FadeInCoroutine(fadedInCallback));
    }

    /// <summary>
    /// Triggers fade out coroutine and allows for function callback on completion
    /// Fade out is the transition from clear to black
    /// </summary>
    /// <param name="fadedOutCallback"></param>
    public void FadeOut(Action fadedOutCallback = null)
    {
        //if already fading out dont fade again
        if (fadeOut) return;
        fadeOut = true;
        StartCoroutine(FadeOutCoroutine(fadedOutCallback));
    }

    /// <summary>
    /// Overload function allows for control over fade time with additional parameter
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadedInCallback"></param>
    public void FadeIn(float duration, Action fadedInCallback = null)
    {
        //if already fading in dont fade again
        if (fadeIn) return;

        fadeIn = true;
        StartCoroutine(FadeInCoroutine(duration, fadedInCallback));
    }

    /// <summary>
    /// Overload function allows for control over fade time with the additional parameter
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadedOutCallback"></param>
    public void FadeOut(float duration, Action fadedOutCallback = null)
    {
        //if already fading out dont fade again
        if (fadeOut) return;

        fadeOut = true;
        StartCoroutine(FadeOutCoroutine(duration, fadedOutCallback));
    }

    /// <summary>
    /// Triggers the fade in and fade out transition with parameters allowing for
    /// functions to be called after each transition
    /// </summary>
    /// <param name="fadedOutCallback"></param>
    /// <param name="fadedInCallback"></param>
    public void FadeOutAndIn(Action fadedOutCallback = null, Action fadedInCallback = null)
    {
        FadeOut(() =>
        {
            if (fadedOutCallback != null)
            {
                fadedOutCallback();
            }
            FadeIn(fadedInCallback);
        });
    }

    /// <summary>
    /// Triggers the fade in and fade out transition with parameters allowing for
    /// functions to be called after each transition
    /// </summary>
    /// <param name="fadedOutCallback"></param>
    /// <param name="fadedInCallback"></param>
    public void FadeOutAndIn(float duration, Action fadedOutCallback = null, Action fadedInCallback = null)
    {
        FadeOut(duration / 2, () =>
        {
            if (fadedOutCallback != null)
            {
                fadedOutCallback();
            }
            FadeIn(duration / 2, fadedInCallback);
        });
    }

    /// <summary>
    /// Coroutine for fade in transition
    /// </summary>
    /// <param name="fadedInCallback"></param>
    /// <returns></returns>
    IEnumerator FadeInCoroutine(Action fadedInCallback = null)
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
    

        //fade in finished
        fadeIn = false;
        if (fadedInCallback != null)
        {
            fadedInCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade in with extra parameter controlling fade duration
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadedInCallback"></param>
    /// <returns></returns>
    IEnumerator FadeInCoroutine(float duration, Action fadedInCallback = null)
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

        //fade in finished
        fadeIn = false;
        if (fadedInCallback != null)
        {
            fadedInCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade out transition
    /// </summary>
    /// <param name="fadedOutCallback"></param>
    /// <returns></returns>
    IEnumerator FadeOutCoroutine(Action fadedOutCallback = null)
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

        //fadeOut finished
        fadeOut = false;
        if (fadedOutCallback != null)
        {
            fadedOutCallback();
        }
    }

    /// <summary>
    /// Coroutine for fade out with extra parameter controlling fade duration
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="fadedOutCallback"></param>
    /// <returns></returns>
    IEnumerator FadeOutCoroutine(float duration, Action fadedOutCallback = null)
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

        //fadeOut finished
        fadeOut = false;
        if (fadedOutCallback != null)
        {
            fadedOutCallback();
        }
    }

}
