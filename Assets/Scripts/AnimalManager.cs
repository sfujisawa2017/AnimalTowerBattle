using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour {

    [SerializeField, HeaderAttribute("動物リスト")]
    public List<GameObject> animalPrefabs = null;

    List<GameObject> listAnimals;

    public GameObject CreateAnimal(Vector3 position)
    {
        GameObject prefab = animalPrefabs.GetAtRandom();
        GameObject animal = Instantiate(prefab, position, Quaternion.identity);

        return animal;
    }

    public void AddAnimal(GameObject animal)
    {
        listAnimals.Add(animal);
    }

    public float GetMaxYPos()
    {
        float maxYPos = 0.0f;

        foreach(var animal in listAnimals)
        {
            maxYPos = Mathf.Max(animal.transform.position.y, maxYPos);
        }

        return maxYPos;
    }

    private void Start()
    {
        listAnimals = new List<GameObject>();
    }    
}
