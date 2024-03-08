using UnityEngine;
using UnityEngine.UI;

namespace Doublsb.Dialog
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
        public string Name;
        public Emotion Emotion;
        public AudioClip[] ChatSE;
        public AudioClip[] CallSE;
    }
}