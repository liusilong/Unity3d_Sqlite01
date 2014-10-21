using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class DbAccess  {
	private SqliteConnection dbConnection;
	private SqliteCommand dbCommand;
	private SqliteDataReader reader;

	public DbAccess(string connectionString){
		OpenDB(connectionString);
	}
	public DbAccess(){

	}
	//开启数据库
	public void OpenDB(string connectionString){
		try{
			dbConnection=new SqliteConnection(connectionString);
			dbConnection.Open();
			Debug.Log("Connected to db");
		}catch(SqliteException e){
			string temp1=e.ToString();
			Debug.Log(temp1);
		}
	}
	//Close DB
	public void CloseSqlConnection(){
		if(dbCommand!=null){
			dbCommand.Dispose();
		}
		dbCommand=null;
		if(reader!=null){
			reader.Dispose ();
		}
		reader=null;
		if(dbConnection!=null){
			dbConnection.Close();
		}
		dbConnection=null;
		Debug.Log("Disconnected from db");
	}
	//数据查询方法 : 根据传递的sql语句查询数据
	public SqliteDataReader ExecuteQuery(string sqlQuery){
		dbCommand=dbConnection.CreateCommand();
		dbCommand.CommandText=sqlQuery;
		reader=dbCommand.ExecuteReader();
		return reader;
	}
	//根据表名查询数据
	public SqliteDataReader ReadFullTable(string tableName){
		string query="SELECT * FROM "+tableName;
		return ExecuteQuery(query);
	}
	//插入数据 传入表名和一个字符串数组
	//like:insert into users values("001","jack",21);
	public SqliteDataReader InsertInto(string tableName,string[] values){
		string query ="INSERT INTO "+tableName+" VALUES ("+values[0];
		for(int i=1;i<values.Length;++i){
			query+=", "+values[i];
		}
		query+=")";
		Debug.Log("InsertInto"+query);
		return ExecuteQuery(query);
	}
	//更新数据的方法（参数1：表名，参数2：被修改的字段名，参数3：修改后的字段值，参数4：修改条件的字段名，参数5：修改条件的字段值）
	//like:update users set username=rose where userid=001;
	//UPDATE momo SET 'qq' = '1157867422' , 'email' = '1157867422@qq.com' WHERE 'name' = '刘斯龙' 
	public SqliteDataReader UpdateInto(string tableName,string[] cols,string[] colsvalues,string selectkey,string selectvalue){
		string query="UPDATE "+tableName+" SET "+cols[0]+" = "+colsvalues[0];
		for(int i=1;i<colsvalues.Length;++i){
			query+=" , "+cols[i]+" = "+colsvalues[i];
		}
		query +=" WHERE "+selectkey+" = "+selectvalue+" ";
		Debug.Log("UpdateInto"+query);
		return ExecuteQuery(query);
	}
	//根据条件删除数据 like: delete from users where username=jack
	//可同时删除多条数据比如删除  momo表中name=“jack”和qq=“1157867422”的两条数据
	//DELETE FROM momo WHERE name = 'jack' or qq = '1157867422'
	//db.Delete("momo",new string[]{"name","qq"},new string[]{"'jack'","'1157867422'"});
	public SqliteDataReader Delete(string tableName,string[] cols,string [] colsvalues){
		string query="DELETE FROM "+tableName+" WHERE "+cols[0]+" = "+colsvalues[0];
		for(int i=1;i<colsvalues.Length;++i){
			query +=" or "+cols[i]+" = "+colsvalues[i];
		}
		Debug.Log("Delete"+query);
		return ExecuteQuery(query);
	}
	// 插入数据  like: insert into users(userid,username,userage) values ("001","jack",21);
	public SqliteDataReader InsertIntoSpecific (string tableName, string[] cols, string[] values)
		
	{
		
		if (cols.Length != values.Length) {
			
			throw new SqliteException ("columns.Length != values.Length");
			
		}
		
		string query = "INSERT INTO " + tableName + "(" + cols[0];
		
		for (int i = 1; i < cols.Length; ++i) {
			
			query += ", " + cols[i];
			
		}
		
		query += ") VALUES (" + values[0];
		
		for (int i = 1; i < values.Length; ++i) {
			
			query += ", " + values[i];
			
		}
		
		query += ")";
		Debug.Log("InsertIntoSpecific"+query);
		return ExecuteQuery (query);
		
	}
	//删除表  like: delete from users
	public SqliteDataReader DeleteContents(string tablename){
		string query="DELETE FROM "+ tablename;
		return ExecuteQuery(query);
	}
	//创建表  like: create table users(userid text,username,text,userage int);
	public SqliteDataReader CreateTable (string name, string[] col, string[] colType)
		
	{
		
		if (col.Length != colType.Length) {
			
			throw new SqliteException ("columns.Length != colType.Length");
			
		}
		
		string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
		
		for (int i = 1; i < col.Length; ++i) {
			
			query += ", " + col[i] + " " + colType[i];
			
		}
		
		query += ")";
		
		return ExecuteQuery (query);
		
	}
	//条件查询 
	//SELECT name, email FROM momo WHERE qq='123039403' 
	//SqliteDataReader sdr=db.SelectWhere("momo",new string[]{"name","email"},new string[]{"qq"},new string[]{"="},new string[]{"123039403"});
	public SqliteDataReader SelectWhere (string tableName, string[] items, string[] col, string[] operation, string[] values)
		
	{
		
		if (col.Length != operation.Length || operation.Length != values.Length) {
			
			throw new SqliteException ("col.Length != operation.Length != values.Length");
			
		}
		
		string query = "SELECT " + items[0];
		
		for (int i = 1; i < items.Length; ++i) {
			
			query += ", " + items[i];
			
		}
		
		query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
		
		for (int i = 1; i < col.Length; ++i) {
			
			query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
			
		}
		Debug.Log("SelectWhere"+query);
		return ExecuteQuery (query);
		
	}































}
