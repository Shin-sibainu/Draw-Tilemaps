using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//白いUIを感知するクラス
public class OnGridPointer : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject createBlock;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Tilemap tilemap;

    private void OnMouseEnter()
    {
        selectPanel.SetActive(true);
    }

    private void OnMouseOver()
    {
        //ポインタの上にselect画像を表示させる。
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float maxDistance = 10;
        //光線にヒットした物の情報をhitに格納する。
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, layerMask);
        if(hit.collider)
        {
            Vector3Int gridPos = tilemap.WorldToCell(hit.point);
            Vector3 complementPos = new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0);
            Vector3 worldPos = tilemap.CellToWorld(gridPos) + complementPos;
            selectPanel.transform.position = worldPos;
            //クリックしたら物体を配置する関数を呼ぶ
            if (Input.GetMouseButtonDown(0))
            {
                CreateBlocks(worldPos);
            }
        }
    }

    private void OnMouseExit()
    {
        selectPanel.SetActive(false);
    }

    private void CreateBlocks(Vector3 createPos)
    {         
        Instantiate(createBlock, createPos, Quaternion.identity);
    }
}
