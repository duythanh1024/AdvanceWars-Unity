using UnityEngine;
public class Finder : MonoBehaviour {
    
    Finder parent;
    int x, y;
    int mana;
	void OnEnable () {
		
	}
    void Clone(int _mana, int _x, int _y)
    {
        GameObject t = new GameObject();
        mana = _mana;
        x = _x;
        y = _y;
    }
    void MoveUp()
    {
        Clone(mana - 1, x, y + 1);
    }
	void Update () {
		
	}
}
