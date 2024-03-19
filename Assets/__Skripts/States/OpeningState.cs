using UnityEngine;
public class OpeningState : StateMachineBehaviour 
{
    private float immersionTime = 0.5f;
    private float startTime;
    private float depthImmersion = 1f;
    private float smoothing = 2f;
    private Vector3 target;
    private Cell cell;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = animator.transform.localPosition + Vector3.up * depthImmersion;
        startTime = Time.time;
        cell = animator.GetComponent<Cell>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 pos = animator.transform.localPosition;
        float u = (Time.time - startTime) / immersionTime;

        if (u >= 1)
        {
            if (cell.state == Cell.eStates.close)
            {
                animator.transform.localPosition = target;
                target += Vector3.down * depthImmersion;
                startTime = Time.time;
                cell.Open();
            } else 
            {
                animator.SetBool("IsOpening", false);
            }
        }

        animator.transform.localPosition = Vector3.Lerp(pos, target, smoothing*Time.fixedDeltaTime);
    }
}
