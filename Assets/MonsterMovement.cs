using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNextPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(Wait());
        }
        
    }

    Vector3 randomPosition()
    {
        float pX = Random.Range(-20f, 20f);
        float pZ = Random.Range(-20f, 20f);
        Vector3 p = new Vector3(pX, 0f, pZ);
        return p;
    }

    IEnumerator Wait()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNextPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        if (!validPath) Debug.Log("Found an invalid Path");
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    void GetNewPath()
    {
        target = randomPosition();
        navMeshAgent.SetDestination(target);
    }
}
