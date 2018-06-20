using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTestController : MonoBehaviour {

    [SerializeField]
    private Text debugText;

    // Use this for initialization
    void Start() {

        if (debugText == null)
            Debug.Log("Need to assign Debug Text on" + gameObject.name);
    }

    public void FadeIn()
    {
        debugText.text = "Fading In";
        VRFade.Instance.FadeIn();
    }

    public void FadeOut()
    {
        debugText.text = "Fading Out";
        VRFade.Instance.FadeOut();
    }

    public void FadeOutAndIn()
    {
        debugText.text = "Fading Out Then In";
        VRFade.Instance.FadeOutAndIn();
    }
    public void FadeInCallback()
    {
        debugText.text = "Fading In";
        VRFade.Instance.FadeIn(FadeInFinished);
;    }

    public void FadeOutCallback()
    {
        debugText.text = "Fading Out";
        VRFade.Instance.FadeOut(FadeOutFinished);
    }

    public void FadeOutAndInCallback()
    {
        debugText.text = "Fading In Then Out";
        VRFade.Instance.FadeOutAndIn(null, FadeOutAndInFinished);
    }

    private void FadeOutFinished()
    {
        debugText.text = "Fade out finished";
    }

    private void FadeInFinished()
    {
        debugText.text = "Fade in finished";
    }

    private void FadeOutAndInFinished()
    {
        debugText.text = "Fade and out in finished";
    }


}
