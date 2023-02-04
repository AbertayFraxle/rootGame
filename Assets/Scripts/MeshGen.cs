using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MeshGen : MonoBehaviour
{
    //init vars
    // the length of segment (world space)
    public float SegmentLength = 5;

    // the segment resolution (number of horizontal points)
    public int SegmentResolution = 32;

    // the size of meshes in the pool
    public int MeshCount = 4;

    // the maximum number of visible meshes. Should be lower or equal than MeshCount
    public int VisibleMeshes = 4;

    // the prefab including MeshFilter and MeshRenderer
    public MeshFilter SegmentPrefab;

    // helper array to generate new segment without further allocations
    private Vector3[] _vertexArray;

    // the pool of free mesh filters
    private List<MeshFilter> _freeMeshFilters = new List<MeshFilter>();
    private List<Segment> _usedSegments = new List<Segment>();

    //bigger teh steepness the less steep it is it makes sense
    public float steepness = 7.0f;

    private float goUp = 0;

    void Awake()
    {
        _vertexArray = new Vector3[SegmentResolution*2];

        int iterations = _vertexArray.Length/2-1;
        var triangles = new int[(_vertexArray.Length-2)*3];

        for(int i = 0; i<iterations; ++i)
        {
            int i2 = i * 6;
            int i3 = i * 2;

            //the order of vertices to get for the triangles, 210 & 231
            triangles[i2] = i3+2;   
            triangles[i2+1] = i3 + 1;
            triangles[i2+2] = i3 + 0;

            triangles[i2+3] = i3 + 2;
            triangles[i2+4] = i3 + 3;
            triangles[i2+5] = i3 + 1;
        }

        var colours = new Color32[_vertexArray.Length];
        for(int i = 0;i<colours.Length;++i)
        {
            colours[i] = new Color32(255, 255, 255, 255);
        }

        for(int i = 0; i<MeshCount;i++)
        {
            MeshFilter filter = Instantiate(SegmentPrefab);

            Mesh mesh = filter.mesh;
            mesh.Clear();

            mesh.vertices = _vertexArray;
            mesh.triangles = triangles;

            filter.gameObject.SetActive(false);
            _freeMeshFilters.Add(filter);
        }   
    }
    
    private float getHeight(float position)
    {
        return ((Mathf.Cos(position+100.0f) + 1.5f + Mathf.Sin(position * 1.75f) + 150f) / steepness);//the important bit callum look here callum this is the bit callum look callum
    }

    public void GenerateSegment(int index, ref Mesh mesh)
    {
        float startPosition = index * SegmentLength;
        float step = SegmentLength / (SegmentResolution-1);

        for(int i = 0; i < SegmentResolution;i++)
        {
            float xPos = step * i;
            float yPosTop = getHeight(startPosition+xPos); //MUST be ABSOLUTE number
            _vertexArray[i * 2] = new Vector3(xPos, yPosTop,0);//z is 0 cause its not heremight make it vector2 later

            //always y=0
            _vertexArray[i*2+1] = new Vector3(xPos,0,0);
        }  

        mesh.vertices = _vertexArray;

        mesh.RecalculateBounds();

        goUp += 1;
    }
    
    private bool IsSegmentInSight(int index)
    {
        Vector3 worldLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 worldRight= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float x1 = index * SegmentLength;
        float x2 = x1 + SegmentLength;

        return x1 <= worldRight.x && x2 >= worldLeft.x;
    }

    private bool IsSegmentVisible(int index)
    {
        return SegmentCurrentlyVisibleListIndex(index) != -1;
    }
    private struct Segment
    {
        public int Index { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }

    private int SegmentCurrentlyVisibleListIndex(int index)
    {
        for (int i = 0; i < _usedSegments.Count; ++i)
        {
            if (_usedSegments[i].Index == index)
            {
                return i;
            }
        }

        return -1;
    }

    private void EnsureSegmentVisible(int index)
    {
        if (!IsSegmentVisible(index))
        {
            // get from the pool
            int meshIndex = _freeMeshFilters.Count - 1;
            MeshFilter filter = _freeMeshFilters[meshIndex];
            _freeMeshFilters.RemoveAt(meshIndex);
            
            // generate
            Mesh mesh = filter.mesh;
            GenerateSegment(index, ref mesh);
            
            // position
            filter.transform.position = new Vector3(index * SegmentLength, 0, 0);
            
            // make visible
            filter.gameObject.SetActive(true);
            
            // register as visible segment
            var segment = new Segment();
            segment.Index = index;
            segment.MeshFilter = filter;
            
            _usedSegments.Add(segment);
        }
    }

    private void EnsureSegmentNotVisible(int index)
    {
        if (IsSegmentVisible(index))
        {
            int listIndex = SegmentCurrentlyVisibleListIndex(index);
            Segment segment = _usedSegments[listIndex];
            _usedSegments.RemoveAt(listIndex);

            MeshFilter filter = segment.MeshFilter;
            filter.gameObject.SetActive(false);

            _freeMeshFilters.Add(filter);
        }
    }

    void Update()
    {
        // get the index of visible segment by finding the center point world position
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        int currentSegment = (int)(worldCenter.x / SegmentLength);

        // Test visible segments for visibility and hide those if not visible.
        for (int i = 0; i < _usedSegments.Count;)
        {
            int segmentIndex = _usedSegments[i].Index;
            if (!IsSegmentInSight(segmentIndex))
            {
                EnsureSegmentNotVisible(segmentIndex);
            }
            else
            {
                // EnsureSegmentNotVisible will remove the segment from the list
                // that's why I increase the counter based on that condition
                ++i;
            }
        }

        // Test neighbor segment indexes for visibility and display those if should be visible.
        for (int i = currentSegment - VisibleMeshes / 2; i < currentSegment + VisibleMeshes / 2; ++i)
        {
            if (IsSegmentInSight(i))
            {
                EnsureSegmentVisible(i);
            }
        }
    }
} 
