using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatedTextMeshPro : MonoBehaviour
{
    [SerializeField]
    TranslatableSentence sentence;
    private void UpdateText()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
            text.text = sentence.ToString();
    }
    private void Start()
    {
        UpdateText();
    }
    void OnValidate()
    {
        if (sentence == null)
            return;
        UpdateText();
    }
}
