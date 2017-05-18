using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{ 

    public AnimationCurve anim;
    float range = 0;

    public Image img;

    Coroutine routine;

    static LoadingScreen instance;
    public static LoadingScreen get()
    {
        if (instance == null ||instance.transform == null)
        {
            instance = FindObjectOfType<LoadingScreen>();
        }
        return instance;
    }

    public void LaunchAnim(float speed, float timeBlack)
    {

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(animateAlpha(speed, timeBlack));

    }

    IEnumerator animateAlpha(float speed, float timeBlack)
    {
        range = 0;
        while (range < 1)
        {
            range += Time.deltaTime * speed;

            img.color = new Color(img.color.r, img.color.g, img.color.b, anim.Evaluate(range));

            yield return null;
        }

        yield return new WaitForSeconds(timeBlack);

        while (range > 0)
        {
            range -= Time.deltaTime * speed;

            img.color = new Color(img.color.r, img.color.g, img.color.b, anim.Evaluate(range));

            yield return null;
        }

    }

}
