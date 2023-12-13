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
    private float valueMin = 0.7f;
    [SerializeField]
    private float valueMax = 1;

    public int randomNum = 1;

    public void OnChange()
    {
        Color color =Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        onChanged?.Invoke(color);
        randomNum = Random.Range(2, 8);
    }
}
