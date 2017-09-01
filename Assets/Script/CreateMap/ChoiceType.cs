using UnityEngine;
public class ChoiceType : MonoBehaviour {
    [SerializeField]
    Manager.Type myType;
    CreateMap createMap;
    void Awake()
    {
        createMap = GameObject.Find("Manager").GetComponent<CreateMap>();
    }
    void OnMouseDonw()
    {
        createMap.currentType = myType;
        
    }
}
