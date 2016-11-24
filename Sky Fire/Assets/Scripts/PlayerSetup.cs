using UnityEngine;
using System.Collections;

public class PlayerSetup : Photon.MonoBehaviour {

	private Transform spawnPos;
	public GameObject shipModel;

	public Material redStrip;
	public Material blueStrip;
	public Material greenStrip;
	public Material yellowStrip;

	// Use this for initialization
	void Start () {

		if (!photonView.isMine) {
			return;
		}

		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag ("Player");
		int playerCount = players.Length;

		Material[] stripMats = shipModel.GetComponent<MeshRenderer> ().materials;

		if (playerCount == 1) {
			spawnPos = GameObject.FindWithTag("Start Pos One").transform;
			stripMats[1] = redStrip;
			shipModel.GetComponent<MeshRenderer>().materials = stripMats;

		} else if (playerCount == 2){
			spawnPos = GameObject.FindWithTag("Start Pos Two").transform;
			stripMats[1] = blueStrip;
			shipModel.GetComponent<MeshRenderer>().materials = stripMats;

			Material[] stripMatsP1 = players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP1 [1] = redStrip;
			players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP1;

			MakeNewPlayerBlue ();

		} else if (playerCount == 3){
			spawnPos = GameObject.FindWithTag("Start Pos Three").transform;

			stripMats[1] = greenStrip;
			shipModel.GetComponent<MeshRenderer>().materials = stripMats;

			Material[] stripMatsP1 = players [2].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP1 [1] = redStrip;
			players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP1;

			Material[] stripMatsP2 = players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP2 [1] = blueStrip;
			players [2].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP2;

			MakeNewPlayerGreen ();

		} else if (playerCount == 4){
			spawnPos = GameObject.FindWithTag("Start Pos Four").transform;
			stripMats[1] = yellowStrip;
			shipModel.GetComponent<MeshRenderer>().materials = stripMats;

			Material[] stripMatsP1 = players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP1 [1] = redStrip;
			players [1].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP1;

			Material[] stripMatsP2 = players [2].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP2 [1] = blueStrip;
			players [2].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP2;

			Material[] stripMatsP3 = players [3].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
			stripMatsP3 [1] = greenStrip;
			players [3].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP3;

			MakeNewPlayerYellow ();
		}

		gameObject.transform.position = spawnPos.position;
		GetComponent<NetworkPlayerModule>().lastCheckpointPos = spawnPos.position;
	
	}
	
	public void MakeNewPlayerBlue(){
		GetComponent<PhotonView> ().RPC (
			"MakeNewPlayerBlueRPC",
			//PhotonNetwork.playerList[1]);
			PhotonTargets.Others);
	}

	public void MakeNewPlayerGreen(){
		GetComponent<PhotonView> ().RPC (
			"MakeNewPlayerGreenRPC",
			//PhotonNetwork.playerList[1]);
			PhotonTargets.Others);
	}

	public void MakeNewPlayerYellow(){
		GetComponent<PhotonView> ().RPC (
			"MakeNewPlayerYellowRPC",
			//PhotonNetwork.playerList[1]);
			PhotonTargets.Others);
	}
		

	[PunRPC]
	void MakeNewPlayerBlueRPC () {
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag ("Player");
		int lastPlayer = players.Length - 1;

		Material[] stripMatsP2 = players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
		stripMatsP2 [1] = blueStrip;
		players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP2;

	}

	[PunRPC]
	void MakeNewPlayerGreenRPC () {
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag ("Player");
		int lastPlayer = players.Length - 1;

		Material[] stripMatsP3 = players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
		stripMatsP3 [1] = greenStrip;
		players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP3;

	}

	[PunRPC]
	void MakeNewPlayerYellowRPC () {
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag ("Player");
		int lastPlayer = players.Length - 1;

		Material[] stripMatsP4 = players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials;
		stripMatsP4 [1] = yellowStrip;
		players [lastPlayer].transform.GetChild (0).GetComponent<MeshRenderer> ().materials = stripMatsP4;
	}




}
