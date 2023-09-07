using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    // 최대 HP
    float maxHP = 100f;

    // 현재 HP
    float currentHP = 0f;

    // HP bar
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {

        // 현재 HP를 최대 HP로 설정
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 데미지를 맞았을 때 HP를 줄여주는 함수
    public void UpdateHP(float damage)
    {
        // 현재 HP를 데미키만큼 감소
        currentHP += damage;

        // hpBar bar 갱신
        hpBar.fillAmount = currentHP / maxHP;
    }
}
