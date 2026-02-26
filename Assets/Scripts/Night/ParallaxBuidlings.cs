using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ParallaxBuidlings : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] Transform blockParent;
    [SerializeField] float spawnPoint, scrollSpeed;
    public List<GameObject> blocks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject b in  blocks) 
        {
            b.transform.position -= new Vector3 (0, 0, scrollSpeed * Time.deltaTime);
        }
        if (blocks[blocks.Count - 1].transform.position.z < spawnPoint)
        {
            SpawnNewBlock();
        }
    }
    void SpawnNewBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, blocks[blocks.Count - 1].transform.position + new Vector3(0, 0, 167), Quaternion.identity, blockParent);
        blocks.Add(newBlock);
    }
}
