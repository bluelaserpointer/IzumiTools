using System;
using System.Collections.Generic;
using UnityEngine;

public enum Language { Chinese, English, Japanese }

[Serializable]
public class TranslatableSentence
{
    public TranslatableSentence() { }
    public TranslatableSentence(TranslatableSentence sample)
    {
        defaultString = sample.defaultString;
        foreach(LanguageAndSentence pair in sample.languageAndSentences)
        {
            languageAndSentences.Add(new LanguageAndSentence(pair.language, pair.sentence));
        }
    }
    public static Language currentLanguage = Language.Chinese;
    [Serializable]
    public class LanguageAndSentence
    {
        public Language language;
        [TextArea]
        public string sentence;
        public LanguageAndSentence(Language language, string sentence)
        {
            this.language = language;
            this.sentence = sentence;
        }
    }
    [TextArea]
    public string defaultString = "?missing?";
    public List<LanguageAndSentence> languageAndSentences = new List<LanguageAndSentence>();
    public override string ToString() {
        LanguageAndSentence pair = languageAndSentences.Find(eachPair => eachPair.language.Equals(currentLanguage));
        return pair != null ? pair.sentence : defaultString;
    }
    public static implicit operator string(TranslatableSentence sentence)
    {
        return sentence.ToString();
    }
    public void PutSentence(Language language, string str)
    {
        foreach(LanguageAndSentence pair in languageAndSentences)
        {
            if(pair.language.Equals(language))
            {
                pair.sentence = str;
                return;
            }
        }
        languageAndSentences.Add(new LanguageAndSentence(language, str));
    }
    public void PutSentence_EmptyStrMeansRemove(Language language, string str)
    {
        if (str.Length == 0) //remove
        {
            languageAndSentences.RemoveOne(pair => pair.language.Equals(language));
        }
        else //add
        {
            PutSentence(language, str);
        }
    }
}
