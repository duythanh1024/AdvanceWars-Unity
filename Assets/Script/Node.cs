using UnityEngine;
public class Node : MonoBehaviour{
    Manager manager;
   
    Node parent;
	int x,y, move;
    public Node(int _x, int _y, int _mana, Node _parent)
    {
        x = _x;
        y = _y;
        move = _mana;
        parent = _parent;
    }
    public void Up()
    {

    }
}
