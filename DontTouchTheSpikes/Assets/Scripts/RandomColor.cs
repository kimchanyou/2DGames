using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Color> onChanged;
    [SerializeField]
    private float hueMin = 0;   // 색상
    [SerializeField]
    private float hueMax = 1;
    [SerializeField]
    private float saturationMin = 0.7f; // 포화도
    [SerializeField]
    private float saturationMax = 1;
    [SerializeField]
    private float valueMin = 0.05f;
    [SerializeField]
    private float valueMax = 0.3f;

    public int randomNum = 1;

    public float saturation = 0.5f;
    public float value = 0.8f;
    //public void OnChange()
    //{
    //    Color color =Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    //    onChanged?.Invoke(color);
    //    //randomNum = Random.Range(2, 8);
    //}

    public void OnChangeColor()
    {
        Color color = Random.ColorHSV(hueMin, hueMax, saturation, saturation, value, value);
        onChanged?.Invoke(color);
        if (value <= 0.2f)
            return;
        if (saturation <= 0.8f)
            saturation += 0.02f;
        else
            value -= 0.02f;
    }
}
