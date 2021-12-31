using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// first set object body y for height then set rotation
public class ObjectSpawner : MonoBehaviour
{
    public float SpawnInterval;
    [SerializeField] float minHeight, maxHeight;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int[] weights;
    int randomRange;
    int[] maxNums;
    void Awake()
    {
        maxNums = new int[weights.Length];
        int sum = 0;
        for (int i = 0; i < maxNums.Length; i++){
            sum += weights[i];
            maxNums[i] = sum;
        }
        randomRange = sum;
    }
    void OnEnable() => StartCoroutine(SpawnCoroutine());

    GameObject SelectRandomPrefab(){
        var rand = UnityEngine.Random.Range(1, randomRange+1);
        for (int i = 0; i < randomRange; i++)
            if(rand <= maxNums[i])
                return prefabs[i];
        return null;
    }

    void Spawn(GameObject prefab, float height, Quaternion rotations){
        var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var body = obj.transform.Find("Body");
        body.transform.localPosition = new Vector3(0, height);
        obj.transform.rotation = rotations;
        Debug.Log("Object spawned");
    }

    IEnumerator SpawnCoroutine()
    {
        while (true){
            var prefab = SelectRandomPrefab();
            float height = UnityEngine.Random.Range(minHeight, maxHeight);
            Quaternion rotations = Quaternion.Euler(
                UnityEngine.Random.Range(0, 360),
                UnityEngine.Random.Range(0, 360),
                UnityEngine.Random.Range(0, 360)
            );
            Spawn(prefab, height, rotations);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
