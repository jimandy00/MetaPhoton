using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
    Text chatText;
    RectTransform rt;

    private void Awake()
    {
        chatText.GetComponent<Text>();
        rt.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string s)
    {
        // �ؽ�Ʈ ����
        chatText.text = s;

        // �ؽ�Ʈ�� ���缭 ũ�⸦ �����Ѵ�.
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
    }
}
