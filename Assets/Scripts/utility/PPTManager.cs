using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPTManager : MonoBehaviour {

    public List<Texture2D> slides = new List<Texture2D>();
    RawImage image;

    public bool pptActivated;

    Canvas canvas;

    public int currentSlide = 0;

    Coroutine fade;

	// Use this for initialization
	void Awake ()
    {
        canvas = GetComponent<Canvas>();
        image = GetComponentInChildren<RawImage>();
        canvas.enabled = true;
        image.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetMouseButtonUp(0)) changeSlide(1);

        if (Input.GetMouseButtonUp(1)) changeSlide(-1);

        if (Input.GetMouseButtonUp(2)) TogglePPT(false);

    }

    void changeSlide(int value)
    {
        if(pptActivated)
            currentSlide = (int)Mathf.Repeat(currentSlide + value, slides.Count);

        image.texture = slides[currentSlide];

        TogglePPT(true);
    }

    void TogglePPT(bool state)
    {
        pptActivated = state;

        if (fade != null) StopCoroutine(fade);

        fade = StartCoroutine(fadeImage((state)?1:0));
    }

    IEnumerator fadeImage(int direction)
    {
        while((direction <= 0 && image.color.a > 0) || (direction >= 1 && image.color.a < 1))
        {
            Color col = image.color;
            col.a += Time.deltaTime * Mathf.Sign(direction - 0.5f) * 5;
            image.color = col;
            yield return true;
        }

        Color coll = image.color;
        coll.a = direction;
        image.color = coll;

        GameManager.get().currentGameState = (direction == 1)? GameManager.GameState.Paused: GameManager.GameState.Playing;
    }

}
