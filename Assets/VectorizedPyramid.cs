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
    [SerializeField] Material pyramidMat;

    [SerializeField] float segmentSize = 0.2f;
    Vector3 initialVector;
    Vector3 secondVector;

    Vector3 crossResult;

    // Start is called before the first frame update
    void Start()
    {
        SetVectors();

        //BuildPyramid();
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
        //float scaleMultiplier;
        //int displacement = (int)(1 / segmentSize + 1);

        Vector3 origin = new Vector3(0, 0, 0);
        //DrawHeights(origin, initialVector, secondVector, crossResult);

        //Vector3 displacementX = (initialVector * Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2)));
        //Vector3 displacementY = (secondVector * Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2)));

        Vector3 originPos = origin;
        Vector3 firstVectorPos = initialVector;
        Vector3 secondVectorPos = secondVector;
        Vector3 crossVectorPos = crossResult;
        Vector3 lastVerticePos = firstVectorPos + secondVectorPos;
        float crossMagnitude = Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2));

        displacementX = new Vector3(initialVector.x / crossMagnitude, initialVector.y / crossMagnitude, initialVector.z / crossMagnitude);
        displacementY = new Vector3(secondVector.x / crossMagnitude, secondVector.y / crossMagnitude, secondVector.z / crossMagnitude);

        for (int i = 0; i < 1 / segmentSize; i++)
        {
            Vector3 upRightDisplacement = displacementX + displacementY;
            upRightDisplacement.x *= i;
            upRightDisplacement.y *= i;
            upRightDisplacement.z *= i;
            Vector3 upLeftDisplacement = -displacementX + displacementY;
            upLeftDisplacement.x *= i;
            upLeftDisplacement.y *= i;
            upLeftDisplacement.z *= i;
            Vector3 downRightDisplacement = displacementX - displacementY;
            downRightDisplacement.x *= i;
            downRightDisplacement.y *= i;
            downRightDisplacement.z *= i;
            Vector3 downLeftDisplacement = -displacementX - displacementY;
            downLeftDisplacement.x *= i;
            downLeftDisplacement.y *= i;
            downLeftDisplacement.z *= i;

            Gizmos.DrawLine(originPos + upRightDisplacement, crossVectorPos + upRightDisplacement);
            Gizmos.DrawLine(firstVectorPos + upLeftDisplacement, firstVectorPos + upLeftDisplacement + crossVectorPos);
            Gizmos.DrawLine(secondVectorPos + downRightDisplacement, secondVectorPos + downRightDisplacement + crossVectorPos);
            Gizmos.DrawLine(lastVerticePos + downLeftDisplacement, lastVerticePos + downLeftDisplacement + crossVectorPos);

            originPos += crossVectorPos;
            firstVectorPos += crossVectorPos;
            secondVectorPos += crossVectorPos;
            lastVerticePos += crossVectorPos;
        }
        //DrawHeights(displacementX + displacementY, initialVector + crossResult - displacementX + displacementY, secondVector + crossResult + displacementX - displacementY, crossResult);
        //for (int i = 1; i < 1 / segmentSize + 1; i++)
        //{

        //    scaleMultiplier = (segmentSize * i);

        //    //GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    //block.transform.localScale = new Vector3(Mathf.Sqrt(Mathf.Pow(initialVector.x, 2) + Mathf.Pow(initialVector.y, 2) + Mathf.Pow(initialVector.z, 2)) * scaleMultiplier,
        //    //                                         Mathf.Sqrt(Mathf.Pow(secondVector.x, 2) + Mathf.Pow(secondVector.y, 2) + Mathf.Pow(secondVector.z, 2)) * scaleMultiplier,
        //    //                                         Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2)));


        //    //block.transform.Rotate(new Vector3(Vector3.Angle(initialVector, Vector3.up), Vector3.Angle(crossResult, Vector3.forward), Vector3.Angle(secondVector, Vector3.right)));
        //    //block.transform.Translate(Vector3.forward * block.transform.localScale.z * displacement);
        //    //displacement--;



        //}
    }

    private void OnDrawGizmos()
    {
        BuildPyramid();
    }

    public Vector3 displacementY;
    public Vector3 displacementX;
    void DrawHeights(Vector3 initialPos, Vector3 firstVectorPos, Vector3 secondVectorPos, Vector3 crossVectorPos)
    {
        //LineRenderer heightLine1 = Instantiate(linePrefab, this.transform);
        Gizmos.DrawLine(initialPos, crossVectorPos);
        Gizmos.DrawLine(firstVectorPos, firstVectorPos + crossVectorPos);
        Gizmos.DrawLine(secondVectorPos, secondVectorPos + crossVectorPos);
        Gizmos.DrawLine(firstVectorPos + secondVectorPos, firstVectorPos + secondVectorPos + crossVectorPos);

        initialPos += crossVectorPos;
        firstVectorPos += crossVectorPos;
        secondVectorPos += crossVectorPos;

        float crossMagnitude = Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2));

        displacementX = new Vector3(firstVectorPos.x / crossMagnitude, firstVectorPos.y / crossMagnitude, firstVectorPos.z / crossMagnitude);
        displacementY = new Vector3(secondVector.x / crossMagnitude, secondVector.y / crossMagnitude, secondVector.z / crossMagnitude);
        //displacementY = (secondVector * Mathf.Sqrt(Mathf.Pow(crossResult.x, 2) + Mathf.Pow(crossResult.y, 2) + Mathf.Pow(crossResult.z, 2)));

        Gizmos.DrawLine(initialPos + crossVectorPos + displacementX + displacementY, crossVectorPos + crossVectorPos + displacementX + displacementY);
        Gizmos.DrawLine(firstVectorPos + crossVectorPos - displacementX + displacementY, firstVectorPos + crossVectorPos + crossVectorPos - displacementX + displacementY);
        Gizmos.DrawLine(secondVectorPos + crossVectorPos + displacementX - displacementY, secondVectorPos + crossVectorPos + crossVectorPos + displacementX - displacementY);
        Gizmos.DrawLine(firstVectorPos + secondVectorPos + crossVectorPos - displacementX - displacementY, firstVectorPos + secondVectorPos + crossVectorPos + crossVectorPos - displacementX - displacementY);

        //Gizmos.DrawLine(secondVectorPos, secondVectorPos + crossVectorPos);
        //Gizmos.DrawLine(firstVectorPos + secondVectorPos, firstVectorPos + secondVectorPos + crossVectorPos);
        //heightLine1.material = pyramidMat;
        //heightLine1.positionCount = 2;
        //heightLine1.SetPosition(0, initialPos);
        //heightLine1.SetPosition(1, crossVectorPos);

        //LineRenderer heightLine2 = Instantiate(linePrefab, this.transform);

        //heightLine2.material = pyramidMat;
        //heightLine2.positionCount = 2;
        //heightLine2.SetPosition(0, firstVectorPos);
        //heightLine2.SetPosition(1, firstVectorPos + crossVectorPos);

        //LineRenderer heightLine3 = Instantiate(linePrefab, this.transform);

        //heightLine3.material = pyramidMat;
        //heightLine3.positionCount = 2;
        //heightLine3.SetPosition(0, secondVectorPos);
        //heightLine3.SetPosition(1, secondVectorPos + crossVectorPos);

        //LineRenderer heightLine4 = Instantiate(linePrefab, this.transform);

        //heightLine4.material = pyramidMat;
        //heightLine4.positionCount = 2;
        //heightLine4.SetPosition(0, firstVectorPos + secondVectorPos);
        //heightLine4.SetPosition(1, firstVectorPos + secondVectorPos + crossVectorPos);
    }
}
