using UnityEngine;
using UnityEngine.U2D.IK;

public class IKTargetHandler : MonoBehaviour
{
    [SerializeField] LimbSolver2D[] limbSolvers;
    public Transform target;
    private Transform[] defaultTargets;

    void Awake()
    {
        defaultTargets = new Transform[limbSolvers.Length];
        for(int i = 0; i < limbSolvers.Length; i++)
        {
            defaultTargets[i] = limbSolvers[i].GetChain(0).target;
        }
    }
    void Update()
    {
    }
    public void SetTarget(Transform target, int index)
    {
        limbSolvers[index].GetChain(0).target = target;
    }
    public void SetAllTarget(Transform target)
    {
        foreach(LimbSolver2D limb in limbSolvers)
        {
            limb.GetChain(0).target = target;
        }
    }

    public void ResetTarget(int index)
    {
        limbSolvers[index].GetChain(0).target = defaultTargets[index];
    }
    public void ResetAllTarget()
    {
        for(int i = 0; i < limbSolvers.Length; i++)
        {
            ResetTarget(i);
        }
    }
}
