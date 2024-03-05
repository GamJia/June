using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Dialogue_0 : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject scrollView;

    private void Start() 
    {
        scrollView.SetActive(false);
    }

    public void Script_0()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/size:init/휴, 드디어 퇴근이다.", "June"));
        dialogTexts.Add(new DialogData("/size:init/하, 오늘도 부장님이 엄청 쪼아대서 진짜 한계였어...", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/응? 이게 뭐야? 지하철역이 왜 이렇게 된 거지?", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/설마 내가 갈 때가 된 건가?", "June"));

        //dialogTexts[dialogTexts.Count - 1].Callback = () => scrollView.SetActive(true);
        //dialogTexts[dialogTexts.Count - 1].Callback = () =>CinemachineManager.Instance.ChangeTarget(false);

        dialogTexts[dialogTexts.Count - 1].Callback = () =>
        {
            scrollView.SetActive(true);
            CinemachineManager.Instance.ChangeTarget(false);
        };
        DialogManager.Show(dialogTexts);

    }
}
