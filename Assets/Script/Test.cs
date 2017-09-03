using UnityEngine;
public class Test : MonoBehaviour {
    public GameObject a, b;
	void Start () {
        a = b;
        print(a.GetComponent<SphereCollider>().radius);
        print(a.GetComponent<BoxCollider>().size.z);
        //print(a.name);
        //b.name = "new name";
        //b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + 1, b.transform.position.z);
        //print(a.name);

	}
	void Update () {
		
	}
}
