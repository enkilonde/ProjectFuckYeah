using UnityEngine;
using System.Collections.Generic;

namespace RockGeneration
{
    public class RockBuilder : MonoBehaviour
    {

        public AreaOfInterest[] areas;
                
        private List<Vector3> list;

        public int vertices;

        [Range(0, 10000)]
        public int seed;

        public void PreviewRock(bool generate)
        {
            list = new List<Vector3>();

            for (int i = 0; i < areas.Length; i++)
            {
                for (int y = 0; y < vertices; y++)
                {
                    float scale = areas[i].scale;

                    float x = Random.Range(-scale * .5f, scale * .5f) * scale;
                    float y_ = Random.Range(-scale * .5f, scale * .5f) * scale;
                    float z = Random.Range(-scale * .5f, scale * .5f) * scale;

                    Vector3 pos = new Vector3(x, y_, z);

                    pos += areas[i].gameObject.transform.position;

                    list.Add(pos);
                }
            }

            Mesh m = FindObjectOfType<RockGenerator>().GenerateRunTimeRock(list);

            MeshFilter filter = GetComponent<MeshFilter>();

            filter.sharedMesh = m;

            transform.position = Vector3.zero;

            if (generate)
            {                
                ObjExporter.trans = transform;
                ObjExporter.filter = filter;
                ObjExporter.ExportObj(NormalInverter.InvertUVs(m), OpenFilePanel.Open());

                NormalInverter.InvertUVs(m);
            }


        }

        public void RandomSeed ()
        {
         //   seed = Random.Range(0, 10000);
        }
    }
}