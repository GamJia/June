// Recompile at 2024-03-22 오전 4:07:41
#if USE_TIMELINE
#if UNITY_2017_1_OR_NEWER
// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.Playables;
using System;

namespace PixelCrushers.DialogueSystem
{

    [Serializable]
    public class PlaySequenceBehaviour : PlayableBehaviour
    {

        [Tooltip("Play this sequence.")]
        [TextArea(5, 5)]
        public string sequence;

        [Tooltip("(Optional) The other subject in the sequence.")]
        public Transform listener;

    }
}
#endif
#endif
