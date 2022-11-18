using UnityEngine;

[CreateAssetMenu(menuName = "IzumiTools/TranslatableSentence")]
public class TranslatableSentenceSO : ScriptableObject
{
    public TranslatableSentence sentence = new TranslatableSentence();
    public override string ToString()
    {
        return sentence;
    }
    public static implicit operator string(TranslatableSentenceSO sentenceSO)
    {
        return sentenceSO.sentence.ToString();
    }
    public static implicit operator TranslatableSentence(TranslatableSentenceSO sentenceSO)
    {
        return sentenceSO.sentence;
    }
}
