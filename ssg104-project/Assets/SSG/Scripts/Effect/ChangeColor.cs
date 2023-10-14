using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] Gradient gradient;
    [SerializeField] private float timeSet;
    [SerializeField] private bool activate = true;
    [SerializeField] private bool setAlpha = true;
    [SerializeField] private bool setRGB = false;
    [SerializeField] private bool autoChange = false;
    private Color[] colors;
    internal float ratio = 0f;
    private void Awake()
    {
        List<Color> colorList = new List<Color>();
        foreach (var image in images) colorList.Add(image.color);
        foreach (var text in texts) colorList.Add(text.color);
        colors = colorList.ToArray();
    }
    public void Active()
    {
        activate = true;
        ratio = 0f;
    }
    private void OnEnable()
    {
        if (!autoChange) return;
        Active();
    }
    //Work per frame
    void OnAppearing()
    {
        if (!activate) return;
        // ratio = total(1f) / numOfPartOfTimes
        ratio += 100 * Time.deltaTime / timeSet;
        for (int i = 0; i < colors.Length; i++)
        {
            Color newColor = colors[i];
            if(setAlpha) newColor.a = gradient.Evaluate(ratio / 100f).a;
            if(setRGB)
            {
                newColor.r = gradient.Evaluate(ratio / 100f).r;
                newColor.g = gradient.Evaluate(ratio / 100f).g;
                newColor.b = gradient.Evaluate(ratio / 100f).b;
            }
            if(i < images.Length) images[i].color = newColor;
            else texts[i - images.Length].color = newColor;
        }
        if(ratio >= 100f) activate = false;

    }

    // Update is called once per frame
    void Update()
    {
        OnAppearing();
    }
}
