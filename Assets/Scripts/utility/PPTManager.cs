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

    ControllerV3 controller;

    float timer = 0.25f;
    float timing = 0;

	// Use this for initialization
	void Awake ()
    {
        canvas = GetComponent<Canvas>();
        image = GetComponentInChildren<RawImage>();
        canvas.enabled = true;
        image.color = new Color(1, 1, 1, 0);
        controller = GetComponent<ControllerV3>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timing -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Keypad4) || controller.Get_LatRightBoostInput()!= 0) changeSlide(1);

        if (Input.GetKeyDown(KeyCode.Keypad6) || controller.Get_LatLeftBoostInput() != 0) changeSlide(-1);

        if (Input.GetMouseButtonUp(2) || controller.Get_SelectInput() != 0) TogglePPT(false);

    }

    void changeSlide(int value)
    {
        if (timing > 0) return;

        timing = timer;

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
        if (GameManager.get() == null) yield break; 
        GameManager.get().currentGameState = (direction == 1)? GameManager.GameState.Paused: GameManager.GameState.Playing;
    }

}
