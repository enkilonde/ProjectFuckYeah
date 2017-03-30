using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiledProcedural : MonoBehaviour {

	[Header("Taille des container à objets")]
	[Range(10f, 1000f)]
	public float blockSize = 25f;
	[Header("Distance du bord pour declencher le spawn")]
	[Range(10f, 400f)]
	public float spawnDistTrigger = 25f;

	GameObject emptyContainer;

	private Transform player;
	private Transform currentBlock;


	void OnEnable () {
		emptyContainer = new GameObject("Block", typeof(FillProceduralBlock));
		player = GameObject.Find("Character").transform;
		currentBlock = Instantiate(emptyContainer, transform).transform;
	}

	void OnValidate()
	{
		//TODO Securiser ces parametres
//		if(blockSize < spawnDistTrigger)
//		{
//			blockSize++;
//			spawnDistTrigger--;
//			Debug.LogWarning("Ne pas rendre la taille du block plus petite que la taille du trigger. (ça sert à rien :/)");
//		}
	}

	void Update () {

		Vector3 playerPos = player.position;

		float distX = currentBlock.position.x - playerPos.x;
		float distY = currentBlock.position.y - playerPos.y;
		float distZ = currentBlock.position.z - playerPos.z;

		//TODO TODO create
	}

	void CreateContainer()
	{

		//If Exists
		//Get it in the pool TODO plus tard
		//If not
		//Instantiate it

	}

	void OnDrawGizmosSelected()
	{
		Vector3 _toDraw = Vector3.zero;
		if(currentBlock != null)
			_toDraw = currentBlock.position;
		Debug.DrawRay(_toDraw - Vector3.up * 10f, Vector3.up * 20f, Color.green);
		Debug.DrawRay(_toDraw - Vector3.right * 10f, Vector3.right * 20f, Color.red);
		Debug.DrawRay(_toDraw - Vector3.forward * 10f, Vector3.forward * 20f, Color.blue);

		//Random area
		float _middleSize = blockSize/2;
		Vector3 LUF = _toDraw + new Vector3(-_middleSize, _middleSize, _middleSize);
		Vector3 RUF = _toDraw + new Vector3(_middleSize, _middleSize, _middleSize);
		Vector3 LDF = _toDraw + new Vector3(-_middleSize, -_middleSize, _middleSize);
		Vector3 RDF = _toDraw + new Vector3(_middleSize, -_middleSize, _middleSize);
		Vector3 LUB = _toDraw + new Vector3(-_middleSize, _middleSize, -_middleSize);
		Vector3 RUB = _toDraw + new Vector3(_middleSize, _middleSize, -_middleSize);
		Vector3 LDB = _toDraw + new Vector3(-_middleSize, -_middleSize, -_middleSize);
		Vector3 RDB = _toDraw + new Vector3(_middleSize, -_middleSize, -_middleSize);

		Color col = Color.white;
		Debug.DrawLine(LUF, RUF, col);
		Debug.DrawLine(LUF, LDF, col);
		Debug.DrawLine(RUF, RDF, col);
		Debug.DrawLine(LDF, RDF, col);
		Debug.DrawLine(LUF, LUB, col);
		Debug.DrawLine(LDF, LDB, col);
		Debug.DrawLine(RUF, RUB, col);
		Debug.DrawLine(RDF, RDB, col);
		Debug.DrawLine(LUB, RUB, col);
		Debug.DrawLine(LUB, LDB, col);
		Debug.DrawLine(RUB, RDB, col);
		Debug.DrawLine(LDB, RDB, col);

		//AreaTriggerer
		_middleSize = (blockSize - spawnDistTrigger)/2;
		LUF = _toDraw + new Vector3(-_middleSize, _middleSize, _middleSize);
		RUF = _toDraw + new Vector3(_middleSize, _middleSize, _middleSize);
		LDF = _toDraw + new Vector3(-_middleSize, -_middleSize, _middleSize);
		RDF = _toDraw + new Vector3(_middleSize, -_middleSize, _middleSize);
		LUB = _toDraw + new Vector3(-_middleSize, _middleSize, -_middleSize);
		RUB = _toDraw + new Vector3(_middleSize, _middleSize, -_middleSize);
		LDB = _toDraw + new Vector3(-_middleSize, -_middleSize, -_middleSize);
		RDB = _toDraw + new Vector3(_middleSize, -_middleSize, -_middleSize);

		col = Color.magenta;
		Debug.DrawLine(LUF, RUF, col);
		Debug.DrawLine(LUF, LDF, col);
		Debug.DrawLine(RUF, RDF, col);
		Debug.DrawLine(LDF, RDF, col);
		Debug.DrawLine(LUF, LUB, col);
		Debug.DrawLine(LDF, LDB, col);
		Debug.DrawLine(RUF, RUB, col);
		Debug.DrawLine(RDF, RDB, col);
		Debug.DrawLine(LUB, RUB, col);
		Debug.DrawLine(LUB, LDB, col);
		Debug.DrawLine(RUB, RDB, col);
		Debug.DrawLine(LDB, RDB, col);
	}

}
