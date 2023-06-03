using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] private float amplitude = 3f;
    [SerializeField] private float speed = 1.5f;

    public GameObject platformPrefab;
    const int PLATFORMS_NUM = 5;
    GameObject[] platforms;
    Vector3[] positions;

    

  //  private void Start()
   // {  
    //}

    private void Awake()
    {
        platforms = new GameObject[PLATFORMS_NUM];
        positions = new Vector3[PLATFORMS_NUM];

        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            float x = transform.position.x + i * 2.5f; 
            float y = transform.position.y + Mathf.Sin(i * 0.5f) * amplitude; 
            Vector3 platformPosition = new Vector3(x, y, 0f);

            positions[i] = platformPosition;

            platforms[i] = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        }

    }

    private void Update()
    {
        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            float y = transform.position.y + Mathf.Sin(Time.time * speed + i * 0.5f) * amplitude; 
            Vector3 platformPosition = new Vector3(positions[i].x, y, positions[i].z);

            platforms[i].transform.position = platformPosition; 
        }
    }
}
