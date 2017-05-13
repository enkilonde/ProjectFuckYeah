using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using MIConvexHull;

namespace RockGeneration
{

    [System.Serializable]
    public class RockGenerator : MonoBehaviour
    {       

        public RockSettings settings;
        
        // Folder where mesh will be exported to.        
        public string folderLocation;

        
        // List that will be used to store the Vertices of the rock generated in the editor.        
        public List<Vector3> editorList;

        
        // Sets Random.seed when a rock is generated.        
        public int seed;

                        
        public enum ExportType
        {
            ObjWavefront,
        }

        public ExportType currentExportType;
                

        /// <summary>
        /// Method that handles rock generation. (Editor)
        /// </summary>        
        public void EditorExport(bool random)
        {
            editorList.Clear(); //Clears the list that holds the editor vertices

            if (random)
            {
                RandomSeed();
            }

            if (settings.randomType == RandomType.Vector3)
            {
                editorList = RandomPointsVectorial(settings);

                ExportRock(editorList);
            }
            else
            {
                editorList = RandomPointsUnitCircle(settings);

                ExportRock(editorList);
            }
        }

        /// <summary>
        /// Exports a Rock
        /// </summary>        
        void ExportRock(IEnumerable<Vector3> points)
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            Mesh m = GenerateRunTimeRock(points);

            filter.sharedMesh = m;

            m = NormalInverter.InvertUVs(m); //Invers the normals for exporting

            ObjExporter.trans = transform;
            ObjExporter.filter = filter;

            ObjExporter.ExportObj(m, folderLocation); //Exports to .obj file                         

            filter.sharedMesh = NormalInverter.InvertUVs(filter.sharedMesh); //Inverts the UVs again
        }

        
        /// <summary>
        /// Used to preview a rock. (Editor)
        /// </summary>
        /// <param name="verts">Number of vertices.</param>
        /// <param name="random">Defines if this preview is random.</param>
        public void PreviewRock(bool random)
        {
            editorList = new List<Vector3>(settings.verts);

            if (random)
                RandomSeed();            

            if (settings.randomType == RandomType.Vector3)
            {
                editorList = RandomPointsVectorial(settings);
            }
            else
            {
                editorList = RandomPointsUnitCircle(settings);                
            }

            PreviewRockInternal(editorList);
        }


        /// <summary>
        /// Internal process to preview a rock.
        /// </summary>
        private void PreviewRockInternal(IEnumerable<Vector3> verts)
        {
            Mesh m = GenerateRunTimeRock(verts);

            MeshFilter filter = GetComponent<MeshFilter>();
            filter.sharedMesh = m;
        }

        

        /// <summary>
        /// Generates a rock from a given set of points. 
        /// </summary>       
        public Mesh GenerateRunTimeRock(IEnumerable<Vector3> points) 
        {
            Mesh m = new Mesh();            

            List<int> triangles = new List<int>();

            var vertices = points.Select(x => new Vertex(x)).ToList();

            var result = ConvexHull.Create(vertices);

            m.vertices = result.Points.Select(x => x.ToVec()).ToArray();

            var resultPoints = result.Points.ToList();

            foreach (var face in result.Faces)
            {
                triangles.Add(resultPoints.IndexOf(face.Vertices[0]));
                triangles.Add(resultPoints.IndexOf(face.Vertices[1]));
                triangles.Add(resultPoints.IndexOf(face.Vertices[2]));
            }

            m.triangles = triangles.ToArray();
            m.RecalculateNormals();

            m = LowPolyConverter.Convert(m); //Converts the generated mesh to low poly

            return m;
        }
                
                
        

        /// <summary>
        /// Returns a List of points to be used in runtime generation. (Uses vectors instead of unit sphere).
        /// </summary>
        public List<Vector3> RandomPointsVectorial(RockSettings settings)
            {
            List<Vector3> list = new List<Vector3>();

            Random.seed = seed;

            for (int i = 0; i < settings.verts; i++)
            {
                list.Add(new Vector3(
                                    Random.Range(settings.min.x, settings.max.x), 
                                    Random.Range(settings.min.y, settings.max.y), 
                                    Random.Range(settings.min.z, settings.max.z))); //Adds a point between min and max to the list.
            }

            return list; 
        }


        /// <summary>
        /// Returns a List of points to be used in runtime generation. (Uses unity sphere instead of vectors).
        /// </summary>        
        public List<Vector3> RandomPointsUnitCircle(RockSettings settings)
        {
            List<Vector3> list = new List<Vector3>();

            Random.seed = seed;

            for (int i = 0; i < settings.verts; i++)
            {
                list.Add(new Vector3(
                                    Random.insideUnitSphere.x * settings.unitCircleProduct.x, 
                                    Random.insideUnitSphere.y * settings.unitCircleProduct.y, 
                                    Random.insideUnitSphere.z * settings.unitCircleProduct.z)); //Generates a point inside the unit sphere and multiplies it by the given multiplier
            }

            return list; //Returns the generated vertices.
        }

        public void RandomSeed()
        {
            seed = Random.Range(0, 10000);
        }

    }
}