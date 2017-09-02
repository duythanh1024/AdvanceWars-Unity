using System;
using UnityEngine;
public class Paint : MonoBehaviour {
    [SerializeField]
    CreateMap.Typelayer layer = CreateMap.Typelayer.army;
    [SerializeField]
    int x, y;
    CreateMap createMap;
    SpriteRenderer render;
    void Awake()
    {
        createMap = GameObject.Find("Manager").GetComponent<CreateMap>();
        createMap.currentType = CreateMap.Type.blueInfantry;
        render = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            createMap.notifi.text = "";
            render.sprite = null;
            if (layer == CreateMap.Typelayer.army)
                createMap.dataSaveMap_LayerArmy[15 * y + x] = 0;
            else
                createMap.dataSaveMap_LayerTerrain[15 * y + x] = 0;
        }
        if (Input.GetMouseButton(0))
        {
            createMap.notifi.text = "";
            render.transform.localPosition = new Vector3();
            render.transform.localScale = new Vector3(1f, 1f, 1f);
            render.sortingLayerID = SortingLayer.NameToID("1");
            switch (createMap.currentType)
            {
                case CreateMap.Type.redTank:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.redTank;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.redTank;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.redTank;                        
                    break;
                case CreateMap.Type.redInfantry:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.redInfantry;
                    if(layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.redInfantry;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.redInfantry;                        
                    break;   
                case CreateMap.Type.redMech:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.redMech;
                    if(layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.redMech;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.redMech;                        
                    break;
                case CreateMap.Type.redHeadquater:
                    render.sortingLayerID = SortingLayer.NameToID("2");
                    render.transform.localPosition = new Vector3(0f, .8f,0f);
                    render.transform.localScale = new Vector3(1.012569f, 1.043834f, 1f);
                    render.sprite = createMap.redHeadquater;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.redHeadquater;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.redHeadquater;
                    break;
                case CreateMap.Type.blueTank:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.blueTank;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.blueTank;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.blueTank;
                    break;
                case CreateMap.Type.blueInfantry:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.blueInfantry;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.blueInfantry;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.blueInfantry;
                    break;
                case CreateMap.Type.blueMech:
                    render.sortingLayerID = SortingLayer.NameToID("3");
                    render.sprite = createMap.blueMech;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.blueMech;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.blueMech;
                    break;
                case CreateMap.Type.blueHeadquater:
                    render.sortingLayerID = SortingLayer.NameToID("2");
                    render.transform.localPosition = new Vector3(-0.026f, .774f,0);
                    render.transform.localScale = new Vector3(0.5383165f, 0.505105f, 1f);
                    render.sprite = createMap.blueHeadquater;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.blueHeadquater;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.blueHeadquater;
                    break;
                case CreateMap.Type.river:
                    render.sprite = createMap.river;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.river;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.river;
                    break;
                case CreateMap.Type.streetHor:
                    render.sprite = createMap.streetHor;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetHor;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetHor;
                    break;
                case CreateMap.Type.streetVer:
                    render.sprite = createMap.streetVer;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetVer;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetVer;
                    break;
                case CreateMap.Type.streetTopRight:
                    render.sprite = createMap.streetTopRight;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetTopRight;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetTopRight;
                    break;
                case CreateMap.Type.streetTopLeft:
                    render.sprite = createMap.streetTopLeft;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetTopLeft;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetTopLeft;
                    break;
                case CreateMap.Type.streetBottomRight:
                    render.sprite = createMap.streetBottomRight;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetBottomRight;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetBottomRight;
                    break;
                case CreateMap.Type.streetBottomLeft:
                    render.sprite = createMap.streetBottomLeft;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.streetBottomLeft;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.streetBottomLeft;
                    break;
                case CreateMap.Type.moutain:
                    render.sprite = createMap.moutain;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.moutain;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.moutain;
                    break;
                case CreateMap.Type.city:
                    render.sortingLayerID = SortingLayer.NameToID("2");
                    render.transform.localPosition = new Vector3(0f, .75f,0f);
                    render.transform.localScale = new Vector3(0.5485638f, 0.6786448f, 1f);
                    render.sprite = createMap.city;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.city;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.city;                    
                    break;
                case CreateMap.Type.wood:
                    render.sprite = createMap.wood;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.wood;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.wood;
                    break;
                case CreateMap.Type.grass:
                    render.sprite = createMap.grass;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.grass;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.grass;
                    break;
                case CreateMap.Type.bridge:
                    render.sprite = createMap.bridge;
                    if (layer == CreateMap.Typelayer.army)
                        createMap.dataSaveMap_LayerArmy[15 * y + x] = (int)CreateMap.Type.bridge;
                    else
                        createMap.dataSaveMap_LayerTerrain[15 * y + x] = (int)CreateMap.Type.bridge;
                    break;
            }
        }//end if
        #region MyRegion
        //if (Input.GetMouseButton(0))
        //{
        //    switch (createMap.currentType)
        //    {
        //        case ChoiceType.Type.redTank:
        //            Instantiate(createMap.redTank).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.redInfantry:
        //            Instantiate(createMap.redInfantry).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.redMech:
        //            Instantiate(createMap.redMech).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.redHeadquater:
        //            Instantiate(createMap.redHeadquater).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.blueTank:
        //            Instantiate(createMap.blueTank).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.blueInfantry:
        //            Instantiate(createMap.blueInfantry).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.blueMech:
        //            Instantiate(createMap.blueMech).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.blueHeadquater:
        //            Instantiate(createMap.blueHeadquater).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.river:
        //            Instantiate(createMap.river).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetHor:
        //            Instantiate(createMap.streetHor).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetVer:
        //            Instantiate(createMap.streetVer).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetTopRight:
        //            Instantiate(createMap.streetTopRight).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetTopLeft:
        //            Instantiate(createMap.streetTopLeft).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetBottomRight:
        //            Instantiate(createMap.streetBottomRight).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.streetBottomLeft:
        //            Instantiate(createMap.streetBottomLeft).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.moutain:
        //            Instantiate(createMap.moutain).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.city:
        //            Instantiate(createMap.city).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.wood:
        //            Instantiate(createMap.wood).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.grass:
        //            Instantiate(createMap.grass).transform.localPosition = transform.localPosition;
        //            break;
        //        case ChoiceType.Type.bridge:
        //            Instantiate(createMap.bridge).transform.localPosition = transform.localPosition;
        //            break;
        //    }
        //}//end if
        #endregion
    }
}
