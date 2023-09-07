using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPun
{

    // inputFiled 가져오기
    public InputField chatInput;

    // chat item prefab
    public GameObject chatItemFactory;

    // scrollview의 content의 rectTransform
    public RectTransform rtContent;

    public RectTransform rtScrollView;

    // 채팅이 추가되기 전에 Content H 값을 가지고 있는 변수
    float prevContentH;

    public Color nickNameColor;

    // Start is called before the first frame update
    void Start()
    {
        // 닉네임 색상 랜덤하게 설정
        nickNameColor = new Color32(
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255));

        // enter key를 누르면 input filed에 있는 텍스트 내용 알려주는 함수 등록
        // onsubmit : 함수를 담을 수 있는 변수 딜리게이트? Unity는 Unity Action 사용
        chatInput.onSubmit.AddListener(OnSubmit);

        // input filed의 내용이 변경될 때마다 호출해주는 함수 등록
        chatInput.onValueChange.AddListener(OnValueChanged);

        // input feild의 focusing이 사라졌을 때 호출해주는 함수
        chatInput.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSubmit(string s)
    {

        // s의 값이 들어오지 않을 때 || s의 길이가 0이라면
        if(s.Length == 0)
        {
            return; // method 나가기
        }

        // 새로운 채팅이 추가되기 전에 content의 H값을 저장
        prevContentH = rtContent.sizeDelta.y;

        print("OnSubmit : " + s);

        // chat item 만들기
        GameObject ci = Instantiate(chatItemFactory);

        // 만들어진 item의 부모를 content로 한다.
        ci.transform.parent.SetParent(rtContent);

        // 만들어진 item에서 text 컴포넌트를 가져온다.
        ChatItem item = ci.GetComponent<ChatItem>();

        // 가져온 컴포넌트의 text 값을 s로 셋팅한다.
        item.SetText(s);

        // 닉네임을 붙여서 채팅 내용을 만들자
        // "<color=#ffff00> 원하는 내용 </color>"
        string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" +
            PhotonNetwork.NickName + "</color>" + " : " + s;

        StartCoroutine(AutoScrollBottom());

        // RPC 함수로 모든 사람에게 채팅 전송
        photonView.RPC(nameof(AddChatRpc), RpcTarget.All, chat);


    }

    [PunRPC]
    void AddChatRpc(string chat)
    {

    }


    // 채팅 스크롤을 자동으로 내려주는 함수
    IEnumerator AutoScrollBottom()
    {
        yield return 0;
        // scrollView의 H < content의 H (채팅이 쌓여서 scrolView보다 커지면)
        if(rtContent.sizeDelta.y > rtScrollView.sizeDelta.y) 
        {
            // 이전에 바닥에 닿아 있었다면?
            if(prevContentH - rtScrollView.sizeDelta.y <= rtContent.anchoredPosition.y)
            {
                // content의 y을 재설정(스크롤이 맨 바닥으로 위치하게끔)
                rtContent.anchoredPosition = new Vector2(0, rtContent.sizeDelta.y - rtScrollView.sizeDelta.y);
            }

        }
        
    }

    void OnValueChanged(string s)
    {
        print("OnValueChanged : " + s);
    }

    void OnEndEdit(string s) 
    {
        print("OnEndEdit :" + s);
    }
}
