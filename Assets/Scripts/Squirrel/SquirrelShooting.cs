using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelShooting : MonoBehaviour {

    private List<HazelNut> listParabolicNut = new List<HazelNut>();
    private List<Nut> listLinearNut = new List<Nut>();
    public HazelNut parabolicNut;
    public Nut linearNut;
    List<Vector3> trayectoryPoints = new List<Vector3>();
    public int quantityOfPoints = 20;
    
    public float velocityParabolicNut;
    public float velocityLinearNut;
    //public Vector3 offsetShoot;
    public delegate void ShootFun();
    private int mode=0;

    public List<ShootFun> shootsModes = new List<ShootFun>();
    public Transform shootZone;

    private void Update()
    {
        int currentMode = MyInputManager.instance.GetFireMode();
        if (currentMode >= 0) {
            mode = currentMode;
        }
        if (currentMode == 2)
        {
            mode = currentMode;
        }
    }
    private void Start()
    {

        for (int i = 0; i < quantityOfPoints; i++)
        {
            var v3 = new Vector3();
            trayectoryPoints.Add(v3);
        }
        shootsModes.Add(ShouldShootNut);
        shootsModes.Add(ShouldShootHazelnut);
    }

    internal void Shoot()
    {
        if (shootsModes.Count <= mode) {
            throw new Exception("Modo de disparo invalido!");
        }
        shootsModes[mode]();
    }

    private void ShouldShootNut()
    {
        //    if (listNut.Count > 0 && Input.GetMouseButtonDown(0))
        if (listLinearNut.Count > 0 &&  MyInputManager.instance.GetButtonDown("Shoot"))
        {
         //   Vector3 sp = Camera.main.WorldToScreenPoint(transform.position + offsetShoot);
           // Vector3 dir = (Input.mousePosition - sp).normalized;
          //  dir.z = transform.position.z;
            Nut h = listLinearNut[0];
           // h.addImpulse(shootZone.position, this.transform.forward, velocityLinearNut);
            listLinearNut.RemoveAt(0);
        }
    }

    private void OnDrawGizmos()
    {

        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(transform.position + offsetShoot, 0.2f);

        if (trayectoryPoints.Count > 0)
        {
            foreach (var point in trayectoryPoints)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
    private void ShouldShootHazelnut()
    {
        if (listParabolicNut.Count > 0)
        {
            HazelNut p = listParabolicNut[0];
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 vel = (Input.mousePosition - sp).normalized;
            predictHazelNut(p.rb, vel * velocityParabolicNut);
            //setTrajectoryPoints(shootZone.position, vel * velocityPineNut);
            //    if (Input.GetMouseButtonDown(0))
            if (MyInputManager.instance.GetButtonDown("Shoot"))
            {
                Debug.Log(vel);
                Debug.Log(vel+ transform.forward);
                p.addImpulse(shootZone.position, vel + transform.forward, velocityParabolicNut);
                listParabolicNut.RemoveAt(0);
            }
        }
    }

    void predictHazelNut(Rigidbody rb, Vector3 vel)
    {
        setTrajectoryPoints(transform.position, vel / rb.mass); 
    }

    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        //fTime += 0.1f;
        for (int i = 0; i < quantityOfPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            float dz = velocity * fTime;
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, pStartPosition.z);
            trayectoryPoints[i] = pos;
            fTime += 0.1f;
        }
    }
    //void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    //{
    //    float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y) );
    //    float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
    //    float fTime = 0;

    //    //fTime += 0.1f;
    //    for (int i = 0; i < quantityOfPoints; i++)
    //    {
    //        float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
    //        float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
    //        Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy);
    //        trayectoryPoints[i] = pos;
    //        fTime += 0.1f;
    //    }
    //}

    internal void TakeHazelNut(HazelNut hz)
    {
        listParabolicNut.Add(hz);
    }

    internal void TakeNut(Nut pn)
    {
        listLinearNut.Add(pn);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 13 ) //NUt
        {
            Nut nut = collision.gameObject.GetComponent<Nut>();

            TakeNut(nut); // TODO: event manager y pool object
            nut.gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == 14) //HazleNUt
        {
            HazelNut Hazelnut = collision.gameObject.GetComponent<HazelNut>();

            TakeHazelNut(Hazelnut); // TODO: event manager y pool object
            Hazelnut.gameObject.SetActive(false);
        }
    }


}
