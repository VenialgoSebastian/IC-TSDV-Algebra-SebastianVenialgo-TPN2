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

    [SerializeField] float segmentSize = 0.2f;
    Vector3 initialVector;
    Vector3 secondVector;

    Vector3 crossResult;

    // Start is called before the first frame update
    void Start()
    {
        SetVectors();

        BuildPyramid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetVectors()
    {
        initialVector = new Vector3(Random.Range(0, 11), Random.Range(0, 11), Random.Range(0, 11));
        secondVector = new Vector3(initialVector.y, initialVector.x * -1, initialVector.z);

        crossResult = new Vector3((initialVector.y * segmentSize * secondVector.z * segmentSize) - (initialVector.z * segmentSize * secondVector.y * segmentSize),
                                           ((initialVector.x * segmentSize * secondVector.z * segmentSize) - (initialVector.z * segmentSize * secondVector.x * segmentSize)) * -1,
                                           (initialVector.x * segmentSize * secondVector.y * segmentSize) - (initialVector.y * segmentSize * secondVector.x * segmentSize));


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

    void BuildPyramid()
    {
        float scaleMultiplier;
        int displacement = (int)(1 / segmentSize + 1);
        for (int i = 1; i < 1 / segmentSize + 1; i++)
        {

            scaleMultiplier = (segmentSize * i);

            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.localScale = new Vector3(Mathf.Sqrt(Mathf.Pow(initialVector.x, 2) + Mathf.Pow(initialVector.y, 2) + Mathf.Pow(initialVector.z, 2)) * scaleMultiplier,
                                                     Mathf.Sqrt(Mathf.Pow(secondVector.x, 2) + Mathf.Pow(secondVector.y, 2) + Mathf.Pow(secondVector.z, 2)) * scaleMultiplier,
                                                     Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2)));


            block.transform.Rotate(new Vector3(Vector3.Angle(initialVector, Vector3.up), Vector3.Angle(crossResult, Vector3.forward), Vector3.Angle(secondVector, Vector3.right)));
            block.transform.Translate(Vector3.forward * block.transform.localScale.z * displacement);
            displacement--;
        }
    }
}
