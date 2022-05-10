using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MapEditManager mapEditManager;
    
    //마우스 커서를 갖다 댄 블럭이 바뀔 마테리얼
    public Material cursoredMat;

    //카메라 이동속도
    public float moveSpeed = 5.0f;

    //생성되는 맵의 X, Z 포지션의 최대 값
    float mapXMaxValue;
    float mapZMaxValue;
    float mapHeightMax;
    float mapHeightMin;

    // Start is called before the first frame update
    void Start()
    {
        //카메라가 움직일 영역 범위 숫자설정
        mapXMaxValue = mapEditManager.blockSize * mapEditManager.mapSize.x;
        mapZMaxValue = mapEditManager.blockSize * mapEditManager.mapSize.y;

        mapHeightMax = mapEditManager.blockSize * mapEditManager.mapSize.y;
        mapHeightMin = 3.0f + mapEditManager.blockSize;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        MouseInput();
        
    }

    //맵 에디터에서 플레이어 이동
    //앞뒤옆 이랑 상하 조절
    void CameraMove()
    {
        //카메라 상하좌우 이동
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z >= mapZMaxValue)
            {
                transform.position.Set(transform.position.x, transform.position.y, mapZMaxValue);
            }
            else transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z <= 0)
            {
                transform.position.Set(transform.position.x, transform.position.y, 0);
            }
            else transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x >= mapXMaxValue)
            {
                transform.position.Set(mapXMaxValue, transform.position.y, transform.position.z);
            }
            else transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x <= 0)
            {
                transform.position.Set(0, transform.position.y, transform.position.z);
            }
            else transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //카메라 높낮이 이동
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (transform.position.y >= mapHeightMax)
            {
                transform.position.Set(transform.position.x, mapHeightMax, transform.position.z);
            }
            else { transform.Translate(Vector3.up); }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (transform.position.y <= mapHeightMin)
            {
                transform.position.Set(transform.position.x, mapHeightMin, transform.position.z);
            }
            else { transform.Translate(Vector3.down); }
        }
    }

    //마우스 클릭 부분
    //커서를 댄 블럭 확인
    //클릭을 통해 블럭 설치 및 해제
    void MouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.gameObject);

            //마우스 커서 갖다 댄 큐브 마테리얼 변경
            Material[] mat = hit.transform.gameObject.GetComponent<MeshRenderer>().materials;
            if (mat[0].name != "CursoredBlock")
            {
                mat[0] = cursoredMat;
                hit.transform.gameObject.GetComponent<MeshRenderer>().materials = mat;
            }

            //마우스 좌클릭(블럭 설정)
            if(Input.GetMouseButtonDown(0))
            {

            }

            //마우스 우클릭(블럭 해제: Noneblock으로 변경)
            else if(Input.GetMouseButtonDown(1))
            {

            }
        }
    }
}
