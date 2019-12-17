using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlock : MonoBehaviour
{
    GridManager gm = null;

    public enum ObjectType
    {
        Static,     // 이동하지 않는 물체
        Moveable,   // 이동하는 물체
        Swamp
    }

    public ObjectType objType = ObjectType.Static;

    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = Camera.main.GetComponent<GridManager>() as GridManager;

        orderObj(gameObject);
        currentPos = transform.position;
        if (objType == ObjectType.Static || objType == ObjectType.Moveable)
            objToWall(gameObject);
        else if (objType == ObjectType.Swamp)
            objToType(gameObject, GridManager.TileType.Swamp);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (objType == ObjectType.Static || objType == ObjectType.Swamp)
            return;

        // x축 또는 z축으로 1이상 움직였으면
        if (currentPos.x + 1 < transform.position.x ||
            currentPos.x - 1 > transform.position.x ||
            currentPos.z + 1 < transform.position.z ||
            currentPos.z - 1 < transform.position.z)
        {
            // 기존위치의 wall을 지우는 코드
            objToType(gameObject, GridManager.TileType.Plain);

            objToWall(gameObject);
            currentPos = transform.position;
        }
    }

    // 게임오브젝트의 위치를 정리하는 함수
    void orderObj(GameObject gobj)
    {
        Vector3 size = gobj.GetComponent<Renderer>().bounds.size;
        Vector3 temp = transform.position;// - new Vector3(size.x / 2, 0, size.z / 2);
        if (temp.x % 1.0f > 0)
            temp.x += (1.0f - temp.x % 1.0f);
        if (temp.z % 1.0f > 0)
            temp.z += (1.0f - temp.z % 1.0f);
        transform.position = temp;
    }

    // 받은 게임오브젝트를 GridManager내에서 인식하게 하는 함수
    void objToWall(GameObject gobj)
    {
        Vector3 size = gobj.GetComponent<Renderer>().bounds.size;
        Vector3 leftBottomPos = transform.position + new Vector3(-size.x / 2, 0, -size.z / 2);
        Vector3 rightTopPos = transform.position + new Vector3(size.x / 2 - 1.0f, 0, size.z / 2 - 1.0f);

        // 첫 열 벽으로 만듬
        Vector3 baseCenter = gm.pos2center(leftBottomPos);
        rightTopPos = gm.pos2center(rightTopPos);
        gm.SetAsWall(baseCenter);
        Vector3 tempPos = baseCenter;
        tempPos += Vector3.forward;
        gm.SetAsWall(tempPos);
        tempPos += Vector3.forward;
        gm.SetAsWall(tempPos);
        // 이후 열들을 벽으로 만듬
        for (int i = 0; i < size.x / 1.0f; i++)
        {
            baseCenter += Vector3.right;
            gm.SetAsWall(baseCenter);

            tempPos = baseCenter;
            tempPos += Vector3.forward;
            gm.SetAsWall(tempPos);
            tempPos += Vector3.forward;
            gm.SetAsWall(tempPos);
        }
    }

    void objToType(GameObject gobj, GridManager.TileType tileType)
    {
        Vector3 size = gobj.GetComponent<Renderer>().bounds.size;
        Vector3 leftBottomPos = currentPos + new Vector3(-size.x / 2, 0, -size.z / 2);
        Vector3 rightTopPos = currentPos + new Vector3(size.x / 2 - 1.0f, 0, size.z / 2 - 1.0f);

        // 첫 열 벽으로 만듬
        Vector3 baseCenter = gm.pos2center(leftBottomPos);
        rightTopPos = gm.pos2center(rightTopPos);
        gm.SetAsType(baseCenter, tileType);
        Vector3 tempPos = baseCenter;
        tempPos += Vector3.forward;
        gm.SetAsType(tempPos, tileType);
        tempPos += Vector3.forward;
        gm.SetAsType(tempPos, tileType);
        // 이후 열들을 벽으로 만듬
        for (int i = 0; i < size.x / 1.0f; i++)
        {
            baseCenter += Vector3.right;
            gm.SetAsType(baseCenter, tileType);

            tempPos = baseCenter;
            tempPos += Vector3.forward;
            gm.SetAsType(tempPos, tileType);
            tempPos += Vector3.forward;
            gm.SetAsType(tempPos, tileType);
        }
    }
}
