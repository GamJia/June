using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Dialogue : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject gaugeBar;
    

    public void Script_0()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/size:init/휴, 드디어 퇴근이다.", "June"));
        dialogTexts.Add(new DialogData("/size:init/하, 오늘도 부장이 엄청 쪼아대서 진짜 한계였어...", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/응? 이게 뭐야? 지하철역이 왜 이렇게 된 거지?", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/이게 그 말로만 듣던 이세계인가?", "June"));
        dialogTexts.Add(new DialogData("/size:init/네 눈을 의심하지 마라, 인간", "God"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/누구세요?", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Molu/내 이름은 퍼즐의 신. 내가 가진 힘으로 이 세계를 조각나지 않게 유지하는 역할을 하지", "God"));
        dialogTexts.Add(new DialogData("/size:init//emote:Molu/그러나 최근 주식 하락으로 내 멘탈이 조각나 버리면서 세계가 조각나버리고 말았네. 그래서 이렇게 조각을 맞추느라 애쓰고 있었지.", "God"));
        dialogTexts.Add(new DialogData("/size:init/대부분의 퍼즐은 내 힘으로 해결했지만, 아직 몇 개의 조각이 남아 있네. 자네가 나서주면 이 세계를 금방 원래대로 돌려놓을 수 있을걸세.", "God"));
        dialogTexts.Add(new DialogData("/size:init/그런데 왜 하필 많고 많은 사람들 중에 저를 선택하신건가요?", "June"));
        dialogTexts.Add(new DialogData("/size:init//emote:Molu/제일 만만해보였기 때문이다.", "God"));
        dialogTexts.Add(new DialogData("/size:init//emote:Embrassed/????????????????", "June"));
        //dialogTexts[dialogTexts.Count - 1].Callback = () => scrollView.SetActive(true);
        //dialogTexts[dialogTexts.Count - 1].Callback = () =>CinemachineManager.Instance.ChangeTarget(false);

        dialogTexts[dialogTexts.Count - 1].Callback = () =>
        {
            scrollView.SetActive(true);
            gaugeBar.SetActive(true);
            CinemachineManager.Instance.ChangeTarget(false);
            PlayerPrefs.SetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "true");
        };
        DialogManager.Show(dialogTexts);

    }
}
