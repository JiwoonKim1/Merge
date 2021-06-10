using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBackFarStars : MonoBehaviour
{
    private ParticleSystem.Particle[] points;

    public int StarMax = 100;

    public float StarSize1 = 0.007f;
    public float StarSize2 = 0.009f;

    public float Speed1 = 0.4f;
    public float Speed2 = 0.7f;

    //public float respawnPoint1 = -1f;
    //public float respawnPoint2 = -3f;

    public float Radius1 = 0.3f;
    public float Radius2 = 0.7f;

    public float starClipDistance = 0.3f;
 
    public GameObject Respawn1;
    public GameObject Respawn2;
    public GameObject Target; //카메라에 보이는 행성
    public Camera Camera;

    private float starClipDistanceSqr;
    private float posZ;
    private float posZZ;
    private Vector3 target;
    private Vector3 camera;

    // Start is called before the first frame update
    void Start()
    {
        starClipDistanceSqr = starClipDistance * starClipDistance;
        target = Target.transform.position;
        camera = Camera.transform.position;
        posZ = Respawn1.transform.position.z;
        posZZ = Respawn2.transform.position.z;

        CreateStars();
    }

    private void CreateStars()
    {
        points = new ParticleSystem.Particle[StarMax];

        //반지름이 (starSize1~starSize2)인 원 둘레에 별이 생성됨
        //원(혹은 구)안에 별이 생성되면 카메라 시야를 방해할 수 있으므로
        for (int i = 0; i < StarMax; i++)
        {
            points[i].color = new Color(1, 1, 1, 1);
            points[i].size = Random.Range(StarSize1, StarSize2);
            points[i].position = setPos();
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < StarMax; i++)
        {
            //랜덤 스피드로 target을 향해 이동 (직진하는 것이 아니라 점점 target쪽으로 모임)
            points[i].position = Vector3.MoveTowards(points[i].position, target, Random.Range(Speed1, Speed2) * Time.deltaTime);

            //별이 target에 가까이 가서 카메라에 안보일때쯤 별을 리스폰
            if (Vector3.Distance(points[i].position, target) < starClipDistance)
            {
                points[i].position = setPos();
                points[i].size = Random.Range(StarSize1, StarSize2);
                points[i].color = new Color(1, 1, 1, 1);
            }

            /*
            //별이 카메라에 가까이 왔을때 알파값을 줄임, 너무 커 보이는 것을 방지하기 위해
            if (Vector3.Distance(points[i].position, camera) <= starClipDistance)
            {
                float percent = (points[i].position - camera).sqrMagnitude / starClipDistanceSqr;

                points[i].color = new Color(1, 1, 1, percent);
                points[i].size *= percent;
            }
            */
            /*
            //별이 카메라에서 적절하게 멀어졌을때 알파값을 높임, 아예 안 보이는 것을 방지하기 위해(***)
            if (Vector3.Distance(points[i].position, camera) >= starClipDistance)
            {
                float percent = (points[i].position - camera).sqrMagnitude / starClipDistanceSqr;

                //points[i].color = new Color(1, 1, 1, percent);
                //points[i].size *= percent;

                points[i].size = Random.Range(StarSize1, StarSize2);
                points[i].color = new Color(1, 1, 1, 1);
            }
            */

        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private Vector3 setPos()
    {
        var a = Random.value * (2 * Mathf.PI) - Mathf.PI;
        var x = Mathf.Cos(a);
        var y = Mathf.Sin(a);
        float radius = Random.Range(Radius1, Radius2);
        float z = Random.Range(posZ, posZZ);
        Vector3 pos = new Vector3(x * radius, y * radius, z);

        return pos;
    }
}
