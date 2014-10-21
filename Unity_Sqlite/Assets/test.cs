using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DbAccess db=new DbAccess("data source=liusilong.db");
//		db.CreateTable("momo",new string[]{"name","qq","email","blog"},new string[]{"text","text","text","text"});
//		db.InsertInto("momo",new string[]{" '刘斯龙' ","'123039403'","'123039403@gmail.com'","'www.rose.com'"});
//		db.UpdateInto("momo",new string[]{"qq","email"},new string[]{"'1157867422'","'1157867422@qq.com'"},"name","'刘斯龙'");
		//删掉两条数据
//		db.Delete("momo",new string[]{"name","qq"},new string[]{"'jack'","'1157867422'"});
		//
		SqliteDataReader sdr=db.SelectWhere("momo",new string[]{"name","email"},new string[]{"qq"},new string[]{"="},new string[]{"123039403"});
		while(sdr.Read()){
			Debug.Log(sdr.GetString(sdr.GetOrdinal("name"))+sdr.GetString(sdr.GetOrdinal("email")));
		}
		db.CloseSqlConnection();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Home)){
			Application.Quit();
		}
	}
}
