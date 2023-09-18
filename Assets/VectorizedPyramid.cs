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

    void Start()
    {
        SetVectors();
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
        Vector3 origin = new Vector3(0, 0, 0);

        Vector3 originPos = origin;
        Vector3 firstVectorPos = initialVector;
        Vector3 secondVectorPos = secondVector;
        Vector3 crossVectorPos = crossResult;
        Vector3 lastVerticePos = firstVectorPos + secondVectorPos;

        Vector3 displacementX = new Vector3(initialVector.x * segmentSize, initialVector.y * segmentSize, initialVector.z * segmentSize);
        Vector3 displacementY = new Vector3(secondVector.x * segmentSize, secondVector.y * segmentSize, secondVector.z * segmentSize);

        Vector3 upRightDisplacement = displacementX + displacementY;
        Vector3 upLeftDisplacement = -displacementX + displacementY;
        Vector3 downRightDisplacement = displacementX - displacementY;
        Vector3 downLeftDisplacement = -displacementX - displacementY;

        for (int i = 0; i < (1 / segmentSize) / 2; i++)
        {
            Gizmos.DrawLine(originPos + (upRightDisplacement * i), originPos + (upRightDisplacement * i) + crossVectorPos);
            Gizmos.DrawLine(firstVectorPos + (upLeftDisplacement * i), firstVectorPos + (upLeftDisplacement * i) + crossVectorPos);
            Gizmos.DrawLine(secondVectorPos + (downRightDisplacement * i), secondVectorPos + (downRightDisplacement * i) + crossVectorPos);
            Gizmos.DrawLine(lastVerticePos + (downLeftDisplacement * i), lastVerticePos + (downLeftDisplacement * i) + crossVectorPos);

            Gizmos.DrawLine(originPos + (upRightDisplacement * i), firstVectorPos + (upLeftDisplacement * i));
            Gizmos.DrawLine(originPos + (upRightDisplacement * i), secondVectorPos + (downRightDisplacement * i));
            Gizmos.DrawLine(lastVerticePos + (downLeftDisplacement * i), firstVectorPos + (upLeftDisplacement * i));
            Gizmos.DrawLine(lastVerticePos + (downLeftDisplacement * i), secondVectorPos + (downRightDisplacement * i));

            originPos += crossVectorPos;
            firstVectorPos += crossVectorPos;
            secondVectorPos += crossVectorPos;
            lastVerticePos += crossVectorPos;

            Gizmos.DrawLine(originPos + (upRightDisplacement * i), firstVectorPos + (upLeftDisplacement * i));
            Gizmos.DrawLine(originPos + (upRightDisplacement * i), secondVectorPos + (downRightDisplacement * i));
            Gizmos.DrawLine(lastVerticePos + (downLeftDisplacement * i), firstVectorPos + (upLeftDisplacement * i));
            Gizmos.DrawLine(lastVerticePos + (downLeftDisplacement * i), secondVectorPos + (downRightDisplacement * i));
        }
    }

    private void OnDrawGizmos()
    {
        BuildPyramid();
    }
}
