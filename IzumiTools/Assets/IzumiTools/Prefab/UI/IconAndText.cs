using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class IconAndText : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Image frame;
    [SerializeField]
    Text text;

    public Image Icon => icon;
    public Image Frame => frame;
    public Text Text => text;
}
