using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class TranslatedText : MonoBehaviour
{
    [SerializeField]
    bool alwaysUpdate;
    [SerializeField]
    TranslatableSentenceSO so_sentence;
    [SerializeField]
    TranslatableSentence sentence;

    // Update is called once per frame
    private void Start()
    {
        UpdateText();
    }
    public void SetText(TranslatableSentence sentence)
    {
        so_sentence = null;
        this.sentence = sentence;
        UpdateText();
    }
    void OnValidate()
    {
        if (so_sentence != null)
        {
            sentence = new TranslatableSentence(so_sentence.sentence);
        }
        else if (sentence == null)
            return;
        UpdateText();
    }
    private void Update()
    {
        if (alwaysUpdate)
            UpdateText();
    }
    private void UpdateText()
    {
        Text text = GetComponent<Text>();
        if (text != null)
            text.text = sentence;
    }
}
