using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[DisallowMultipleComponent]
public class SBA_NumberApproachingTextMeshPro : MonoBehaviour
{
    public int targetValue;
    public float step = 0.25F;
    public float updateSpan = 0.05F;
    public Color incresingColor = Color.green, decresingColor = Color.red, normalColor = Color.white;
    [Header("prefix/suffix")]
    public string prefix, suffix;

    //data
    TextMeshProUGUI text;
    public TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());
    float waitTime;
    private void Update()
    {
        if ((waitTime += Time.deltaTime) < updateSpan)
            return;
        waitTime = 0;
        UpdateText();
    }
    public void UpdateText()
    {
        int displayValue = Convert.ToInt32(Text.text);
        if (displayValue == targetValue)
            Text.color = normalColor;
        else
        {
            Text.color = displayValue < targetValue ? incresingColor : decresingColor;
            Text.text = prefix + SBA_NumberApproachingText.Approach(displayValue, targetValue, step).ToString() + suffix;
        }
    }
    public void SetValueImmediate(int value)
    {
        targetValue = value;
        Text.text = value.ToString();
    }
    public void SetValueImmediate()
    {
        Text.text = targetValue.ToString();
    }
}
