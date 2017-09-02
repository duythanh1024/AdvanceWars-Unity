using UnityEngine;
public class ChoiceType : MonoBehaviour {
    Transform layerTerrain, layerArmy;
    [SerializeField]
    private CreateMap.Type myType;
    private Transform posSelect;
    private Transform select;
    CreateMap createMap;
    void Awake()
    {
        layerTerrain = GameObject.Find("LayerTerrain").transform;
        layerArmy = GameObject.Find("LayerArmy").transform;
        createMap = GameObject.Find("Manager").GetComponent<CreateMap>();
        select = GameObject.Find("Select").transform;
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    public void OnMouseDown()
    {
        createMap.currentType = myType;
        select.transform.position = posSelect.position;
        switch (myType)
        {
            case CreateMap.Type.redTank:
            case CreateMap.Type.redInfantry:
            case CreateMap.Type.redMech:
            case CreateMap.Type.blueTank:
            case CreateMap.Type.blueInfantry:
            case CreateMap.Type.blueMech:
                layerArmy.position = new Vector3(layerArmy.position.x, layerArmy.position.y, -1f);
                layerTerrain.position = new Vector3(layerTerrain.position.x, layerTerrain.position.y, 0f);
                break;
            case CreateMap.Type.blueHeadquater:
            case CreateMap.Type.redHeadquater:
            case CreateMap.Type.river:
            case CreateMap.Type.streetHor:
            case CreateMap.Type.streetVer:
            case CreateMap.Type.streetTopRight:
            case CreateMap.Type.streetTopLeft:
            case CreateMap.Type.streetBottomRight:
            case CreateMap.Type.streetBottomLeft:
            case CreateMap.Type.moutain:
            case CreateMap.Type.city:
            case CreateMap.Type.wood:
            case CreateMap.Type.grass:
            case CreateMap.Type.bridge:
                layerArmy.position = new Vector3(layerArmy.position.x, layerArmy.position.y, 0f);
                layerTerrain.position = new Vector3(layerTerrain.position.x, layerTerrain.position.y, -1f);
                break;
        }
    }
}
