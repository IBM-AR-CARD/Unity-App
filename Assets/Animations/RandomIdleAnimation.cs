using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : StateMachineBehaviour
{
    float aniTimer = 0;
    string[] aniTriggers = {"bored","happy","look_around","rub_shoulder","streching"};
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(aniTimer<=0){
           randomAnimation(animator);
           aniTimer = Random.Range(20,50); //random between 20 - 50 seconds
       }else{
           aniTimer -= Time.deltaTime;
       }
    }

    void randomAnimation(Animator animator){
        System.Random rnd = new System.Random();
        int rndIndex = rnd.Next(aniTriggers.Length);
        string trigger = aniTriggers[rndIndex];
        animator.SetTrigger(trigger);
        Debug.Log(trigger);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
