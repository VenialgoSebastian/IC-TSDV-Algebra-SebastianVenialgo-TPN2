using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorizedPyramid : MonoBehaviour
{
    [SerializeField] LineRenderer linePrefab;
    LineRenderer firstVectorLine;
    LineRenderer secondVectorLine;
    LineRenderer crossLine;

    [SerializeField] Material xMat;
    [SerializeField] Material yMat;
    [SerializeField] Material zMat;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initialVector = new Vector3(Random.Range(0, 11), Random.Range(0, 11), Random.Range(0, 11));
        Vector3 secondVector = new Vector3(initialVector.y, initialVector.x * -1, initialVector.z);

        Vector3 crossResult = new Vector3((initialVector.y * secondVector.z) - (initialVector.z * secondVector.y),
                                          ((initialVector.x * secondVector.z) - (initialVector.z * secondVector.x)) * -1,
                                          (initialVector.x * secondVector.y) - (initialVector.y * secondVector.x));
        

        firstVectorLine = Instantiate(linePrefab);
        secondVectorLine = Instantiate(linePrefab);
        crossLine = Instantiate(linePrefab);
        firstVectorLine.material = xMat;
        secondVectorLine.material = yMat;
        crossLine.material = zMat;
        firstVectorLine.positionCount = 2;
        firstVectorLine.SetPosition(1, initialVector);
        secondVectorLine.positionCount = 2;
        secondVectorLine.SetPosition(1, secondVector);
        crossLine.positionCount = 2;
        crossLine.SetPosition(1, crossResult);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
