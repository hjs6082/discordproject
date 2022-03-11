using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;

    private readonly int hashAttack_1_Tr = Animator.StringToHash("ATTACK_1");
    private readonly int hashAttack_2_Tr = Animator.StringToHash("ATTACK_2");
    private readonly int hashAttack_3_Tr = Animator.StringToHash("ATTACK_3");
    private readonly int hashAttack_4_Tr = Animator.StringToHash("ATTACK_4");
    private readonly int hashJump = Animator.StringToHash("JUMP");

    private List<int> hashAttackTrList = new List<int>();

    private void Awake()
    {
        animator = GetComponent<Animator>();

        hashAttackTrList.Add(hashAttack_1_Tr);
        hashAttackTrList.Add(hashAttack_2_Tr);
        hashAttackTrList.Add(hashAttack_3_Tr);
        hashAttackTrList.Add(hashAttack_4_Tr);
    }

    public void Attack(int index)
    {
        animator.SetTrigger(hashAttackTrList[index]);
    }

    public void Jump()
    {
        animator.SetTrigger(hashJump);
    }
}
