using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image[] segments;

    private Color[] filledColors;

    private void Awake()
    {
        filledColors = new Color[segments.Length];
        for (int i = 0; i < segments.Length; i++)
        {
            filledColors[i] = segments[i].color; 
        }
        ResetProgressBar();
    }

    public void UpdateProgressBar(float percentage)
    {
      
        int filledSegments = Mathf.FloorToInt(percentage * segments.Length);
      

        for (int i = 0; i < segments.Length; i++)
        {
            if (i < filledSegments)
                segments[i].color = filledColors[i];
           
        }
    }
    public void ResetProgressBar()
    {
        foreach (var segment in segments)
        {
            segment.color = Color.clear;
        }
    }
}