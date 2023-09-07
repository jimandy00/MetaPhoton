using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPun
{

    // inputFiled ��������
    public InputField chatInput;

    // chat item prefab
    public GameObject chatItemFactory;

    // scrollview�� content�� rectTransform
    public RectTransform rtContent;

    public RectTransform rtScrollView;

    // ä���� �߰��Ǳ� ���� Content H ���� ������ �ִ� ����
    float prevContentH;

    public Color nickNameColor;

    // Start is called before the first frame update
    void Start()
    {
        // �г��� ���� �����ϰ� ����
        nickNameColor = new Color32(
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255));

        // enter key�� ������ input filed�� �ִ� �ؽ�Ʈ ���� �˷��ִ� �Լ� ���
        // onsubmit : �Լ��� ���� �� �ִ� ���� ��������Ʈ? Unity�� Unity Action ���
        chatInput.onSubmit.AddListener(OnSubmit);

        // input filed�� ������ ����� ������ ȣ�����ִ� �Լ� ���
        chatInput.onValueChange.AddListener(OnValueChanged);

        // input feild�� focusing�� ������� �� ȣ�����ִ� �Լ�
        chatInput.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSubmit(string s)
    {

        // s�� ���� ������ ���� �� || s�� ���̰� 0�̶��
        if(s.Length == 0)
        {
            return; // method ������
        }

        // ���ο� ä���� �߰��Ǳ� ���� content�� H���� ����
        prevContentH = rtContent.sizeDelta.y;

        print("OnSubmit : " + s);

        // chat item �����
        GameObject ci = Instantiate(chatItemFactory);

        // ������� item�� �θ� content�� �Ѵ�.
        ci.transform.parent.SetParent(rtContent);

        // ������� item���� text ������Ʈ�� �����´�.
        ChatItem item = ci.GetComponent<ChatItem>();

        // ������ ������Ʈ�� text ���� s�� �����Ѵ�.
        item.SetText(s);

        // �г����� �ٿ��� ä�� ������ ������
        // "<color=#ffff00> ���ϴ� ���� </color>"
        string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" +
            PhotonNetwork.NickName + "</color>" + " : " + s;

        StartCoroutine(AutoScrollBottom());

        // RPC �Լ��� ��� ������� ä�� ����
        photonView.RPC(nameof(AddChatRpc), RpcTarget.All, chat);


    }

    [PunRPC]
    void AddChatRpc(string chat)
    {

    }


    // ä�� ��ũ���� �ڵ����� �����ִ� �Լ�
    IEnumerator AutoScrollBottom()
    {
        yield return 0;
        // scrollView�� H < content�� H (ä���� �׿��� scrolView���� Ŀ����)
        if(rtContent.sizeDelta.y > rtScrollView.sizeDelta.y) 
        {
            // ������ �ٴڿ� ��� �־��ٸ�?
            if(prevContentH - rtScrollView.sizeDelta.y <= rtContent.anchoredPosition.y)
            {
                // content�� y�� �缳��(��ũ���� �� �ٴ����� ��ġ�ϰԲ�)
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
