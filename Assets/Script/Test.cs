using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour {
    public GameObject a, b;
    List<Vector2> p,q;
	void Start () {
        //a = b;
        //print(a.GetComponent<SphereCollider>().radius);
        //print(a.GetComponent<BoxCollider>().size.z);
        //print(a.name);
        //b.name = "new name";
        //b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + 1, b.transform.position.z);
        //print(a.name);
        p = new List<Vector2>();
        p.Add(new Vector2(1, 1));
        p.Add(new Vector2(2, 2));
        print(p.Count);
        q = p;
        q.Add(new Vector2(3, 3));
        print(p.Count);
	}
	void Update () {
        
	}
   
}
