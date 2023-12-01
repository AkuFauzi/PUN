using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
     public   float speed;
    bool belok = false;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //    Applyrules();
        //    transform.Translate(0, 0, Time.deltaTime * speed);
        Bounds b = new Bounds(myManager.transform.position, myManager.swimlimits * 2);
        RaycastHit hit = new RaycastHit();

        Vector3 arah = Vector3.zero;

        if (!b.Contains(transform.position))
        {
            belok = true;
            arah = myManager.transform.position - transform.position;

        }
                
           else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))     
        {
            belok = true;
           Debug.DrawRay(this.transform.position, this.transform.forward * 50, Color.red);
         
            arah = Vector3.Reflect(this.transform.forward, hit.normal);

            Debug.DrawRay(hit.point, arah * 50, Color.blue);

        }
                
                
           
        else belok = false;

        if (belok)
        {
            // belok ke arah titik pusat baru (arah)
         
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(arah),
                myManager.rotationSpeed * Time.deltaTime);
        
        
        }
        else
        {

            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                Applyrules();
            
        }
transform.Translate(0, 0, Time.deltaTime * speed);
    }
    void Applyrules()
    {
        GameObject[] gerombol;
        gerombol = myManager.allfish;

        Vector3 ratacentre = Vector3.zero;
        Vector3 ratavoidance = Vector3.zero;
        float Grupspeed = 0.01f;
        float neighbourdistance;
        int groupsize = 0;

        foreach (GameObject go in gerombol)
        {
            if (go != this.gameObject)
            {
                neighbourdistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighbourdistance <= myManager.neighbourDistance)
                {
                    ratacentre += go.transform.position;
                    groupsize++;
                    if (neighbourdistance < 1.0f)
                    {

                        ratavoidance = ratavoidance + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    Grupspeed = Grupspeed + anotherFlock.speed;

                }

            }
        }
        if (groupsize > 0)
        {
            ratacentre = ratacentre / groupsize+ (myManager.goalpos - this.transform.position);
            Grupspeed = Grupspeed / groupsize;

            Vector3 direction = (ratacentre + ratavoidance) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);

        }
    }
}
