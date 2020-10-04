using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFactory : MonoBehaviour
{
    public GameObject robot;
    public GameObject target;
    private float toPlayerDistance;

    // Update is called once per frame
    void Update()
    {
        toPlayerDistance = Vector3.Distance(transform.position, target.transform.position);
        if (toPlayerDistance <= 10.0f)
            InstanceRobot();

        return;
    }

    void InstanceRobot()
    {
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity); 
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Instantiate(robot, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
