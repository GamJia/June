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
    public class SetQuestStateBehaviour : PlayableBehaviour
    {

        [QuestPopup]
        public string quest;

        [Tooltip("Change the quest's main state.")]
        public bool setQuestState;

        [QuestState]
        public QuestState questState;

        [Tooltip("Change a quest entry's state.")]
        public bool setQuestEntryState;

        public int questEntryNumber;

        [QuestState]
        public QuestState questEntryState;

    }
}
#endif
#endif
