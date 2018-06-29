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

    public void FadeToClear()
    {
        debugText.text = "Fading from black to clear";
        VRFade.Instance.FadeToClear();
    }

    public void FadeToBlack()
    {
        debugText.text = "Fading clear to black";
        VRFade.Instance.FadeToBlack();
    }

    public void FadeToBlackToClear()
    {
        debugText.text = "Fading clear to black to clear";
        VRFade.Instance.FadeToBlackToClear();
    }
    public void FadeToClearCallback()
    {
        debugText.text = "Fading from black to clear";
        VRFade.Instance.FadeToClear(FadeToClearFinishedCallback);
;    }

    public void FadeToBlackCallback()
    {
        debugText.text = "Fading from clear to black";
        VRFade.Instance.FadeToBlack(FadeToBlackFinishedCallback);
    }

    public void FadeToBlackToClearCallback()
    {
        debugText.text = "Fading clear to black to clear then callback";
        VRFade.Instance.FadeToBlackToClear(null, FadeToBlackToClearFinishedCallback);
    }

    private void FadeToBlackFinished()
    {
        debugText.text = "Fade clear to black finished";
    }

    private void FadeToBlackFinishedCallback()
    {
        debugText.text = "CALLBACK - Fade clear to black finished";
    }

    private void FadeToClearFinished()
    {
        debugText.text = "Fade black to clear finished";
    }

    private void FadeToClearFinishedCallback()
    {
        debugText.text = "CALLBACK - Fade black to clear finished";
    }

    private void FadeToBlackToClearFinished()
    {
        debugText.text = "Fade clear to black to clear finished";
    }

    private void FadeToBlackToClearFinishedCallback()
    {
        debugText.text = "CALLBACK - Fade clear to black to clear finished";
    }


}
