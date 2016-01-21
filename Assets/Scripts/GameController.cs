 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using System.IO;

public class GameController : MonoBehaviour {

	public GameObject player;

	public GameObject spawner;

	public GameObject staticBlock;
	public GameObject deathBlock;
	public GameObject startBlock;
	public GameObject endBlock;

	public GUIText levelNumberText;

	public GUIText chronoText;
	private int chronoTime;

	public GUIText filePathText;

	public GUIText gameoverText;

	private bool gameState;

	public Level level;
	private int levelType;
	private int levelNumber;

	// Use this for initialization
	void Start () {
		gameState = true;

		levelNumber = 1;
		chronoTime = -1;

		loadLevel (levelNumber);

		filePathText.text = UnityEngine.Application.persistentDataPath;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (gameState) {
			updateTime ();
		}
	}
	
	void updateLevelNumber(){
		levelNumberText.text = "Niveau: " + levelNumber;
	}

	void updateTime(){
		if (chronoTime < (int)Time.time) {
			chronoTime = (int)Time.time;
			chronoText.text = "Temps: " + chronoTime + " secs";
		}
	}

	public void nextLevel(){
		wipeLevel ();
		try{
			levelNumber ++;
			loadLevel (levelNumber);
		}catch(FileNotFoundException e){
			//Fin de la partie
			Debug.Log (e + "\nAucun autre niveau trouvé\nFin de la partie");

			endGame();

			levelNumber = -1;
			updateLevelNumber();
		}
	}

	void loadLevel(int number){
		readXml (number);

		Vector3 position = new Vector3 ();
		position.z = 0;

		foreach(Block block in level.blocks){
			position.x = block.column * 5;
			position.y = block.row * 5;

			switch(block.type){
			case "static":
				Instantiate(staticBlock,position,Quaternion.identity);
				break;
			case "void":
				//Espace vide
				break;
			case "start":
				Instantiate(startBlock,position,Quaternion.identity);
				player.transform.position = position;
				break;
			case "end":
				Instantiate(endBlock,position,Quaternion.identity);
				break;
			case "death":
				Instantiate(deathBlock,position,Quaternion.identity);
				break;
			}
		}

		updateLevelNumber ();
	}

	void wipeLevel(){
		GameObject[] gameObjectsToBeDestroyed;
		
		gameObjectsToBeDestroyed = GameObject.FindGameObjectsWithTag ("StaticBlock");
		foreach(Object block in gameObjectsToBeDestroyed){
			Destroy(block);
		}
		
		gameObjectsToBeDestroyed = GameObject.FindGameObjectsWithTag ("StartBlock");
		foreach(Object block in gameObjectsToBeDestroyed)
		{
			Destroy (block);
		}
		
		gameObjectsToBeDestroyed = GameObject.FindGameObjectsWithTag ("EndBlock");
		foreach(Object block in gameObjectsToBeDestroyed)
		{
			Destroy (block);
		}
		
		gameObjectsToBeDestroyed = GameObject.FindGameObjectsWithTag ("DeathBlock");
		foreach (Object block in gameObjectsToBeDestroyed) 
		{
			Destroy (block);
		}
		
		player.transform.position = new Vector3(0,0);
		player.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
	}

	void endGame(){
		gameState = false;

		gameoverText.text = "Fin de la partie\n" +
			"Votre temps: " + chronoTime + " secondes\n\n" +
			"Appuyez sur " + KeyCode.Space + " pour continuer";
	}

	void readXml(int levelNumber){

		//MÉTHODE WINDOWS

		var serializer = new System.Xml.Serialization.XmlSerializer (typeof(Level));

		serializer.UnknownNode += new System.Xml.Serialization.XmlNodeEventHandler(serializer_UnknownNode);
		serializer.UnknownAttribute += new System.Xml.Serialization.XmlAttributeEventHandler (serializer_UnknownAttribute);

		var fs = new FileStream(UnityEngine.Application.streamingAssetsPath + "/XML/StaticLevel" + levelNumber + ".xml",FileMode.Open);

		level = serializer.Deserialize (fs) as Level;

	}

	void serializer_UnknownNode(object sender, System.Xml.Serialization.XmlNodeEventArgs e){
		Debug.Log ("Unknown Node:" + e.Name + "\t" + e.Text);
	}

	void serializer_UnknownAttribute(object sender, System.Xml.Serialization.XmlAttributeEventArgs e){
		Debug.Log ("Unknown Attribute: " + e.Attr.Name + "='" + e.Attr.Value + "'");
	}


}

[XmlRoot(ElementName = "level")]
[XmlType("level")]
public class Level{
	[XmlAttribute("name")]
	public string name;
	[XmlAttribute("number")]
	public int number;
	[XmlAttribute("width")]
	public int width;
	[XmlAttribute("height")]
	public int height;

	[XmlArray("blocks")]
	[XmlArrayItem("block")]
	public List<Block> blocks = new List<Block>();
}

public class Block{
	[XmlAttribute("type")]
	public string type;
	[XmlAttribute("row")]
	public int row;
	[XmlAttribute("column")]
	public int column;
}