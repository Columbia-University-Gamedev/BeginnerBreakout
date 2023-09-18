using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    CanvasGroup[] ToFade;

    [SerializeField]
    AnimationCurve FadeCurve;

    int _fadeIndex = 0;

    static FadeToBlack _instance = null;
    public static FadeToBlack Instance => _instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        if(_instance != this)
        {
            Destroy(this);
        }
        
        StartFade(false, 1.5f);
    }

    public void StartFade(bool toBlack, float duration, float delay = 0.0f)
    {
        StartCoroutine(PerformFade(toBlack, duration, delay));
    }

    IEnumerator PerformFade(bool toBlack, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        int thisIndex = ++_fadeIndex;
        float elapsed = toBlack ? 0 : duration;
        float timeConstant = toBlack ? 1 : -1;
        while(thisIndex == _fadeIndex)
        {
            elapsed += timeConstant * Time.deltaTime;
            float amount = FadeCurve.Evaluate(Mathf.Clamp(elapsed / duration, 0, 1));
            foreach(CanvasGroup cg in ToFade)
            {
                cg.alpha = amount;
            }

            if(elapsed > duration || elapsed < 0)
            {
                break;
            }

            yield return null;
        }
    }
}
