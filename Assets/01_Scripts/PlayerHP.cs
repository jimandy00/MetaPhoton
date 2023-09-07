using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    // �ִ� HP
    float maxHP = 100f;

    // ���� HP
    float currentHP = 0f;

    // HP bar
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {

        // ���� HP�� �ִ� HP�� ����
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �������� �¾��� �� HP�� �ٿ��ִ� �Լ�
    public void UpdateHP(float damage)
    {
        // ���� HP�� ����Ű��ŭ ����
        currentHP += damage;

        // hpBar bar ����
        hpBar.fillAmount = currentHP / maxHP;
    }
}
