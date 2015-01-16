/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 13.01.2015
 * Время: 13:32
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 * В DataTableToModel передаем таблицу отредактированную пользователем, с указанием какие из столбцов за что отвечают
 * а он строит xml с помощью билдера.
 * bool CreateStation(string stationName);
 * bool TableToModel(DataTable table,столбик с порядковым номером, с наименованием,с размерностью,Обозначение,Формула,[]шаблоны)
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NUnit.Framework;
using XmlConvertForIstok.Composite;
using XmlConvertForIstok.Readers;

namespace SecondTest
{
	[TestFixture]
	public class DataTableToModelConverter
	{
		[Test]
		public void GreateDTTMTest()
		{
			INodeBilder nodeBld = new NodeBilder();			
			ITableToModel rep = new TableToModel(nodeBld);
			const string StationName = "StationName";
			
			rep.CreateStation(StationName);
			Node node = (NodeBilder)nodeBld;
			string asssert = node.Nodes[0].Name;
			
			StringAssert.Contains(StationName, asssert);
		}
		
		[Test]
		public void DTTMTestTamplates()
		{
			Node node = NodeFromTable("TableOne", "TestWord.docx");			
			
			int tmplNodes = ((Node)node.Nodes[0]).Nodes.FindAll(nd => nd as Node != null).Count;
			
			Assert.IsTrue(tmplNodes == 2);			
		}
		
		[Test]
		public void DTTMTestTmplTypesNames()
		{
			Node node = NodeFromTable("TableOne", "TestWord.docx");			
			Node tblNode = (Node)node.Nodes[0];
			string assertTableName = tblNode.Name;
			string assertTableType = tblNode.Type;
			
			StringAssert.Contains("TableOne", assertTableName);
			StringAssert.Contains("Folder", assertTableType);
		}
		
		[Test]
		public void DTTMTestChannel()
		{
			Node node = NodeFromTable("TableOne", "TestWord.docx");			
			Node tblNode = (Node)node.Nodes[0];
			Node tmplNode = (Node)tblNode.Nodes[1];
			Node chnlNode = (Node)tmplNode.Nodes[2];
			
			string formulacod = (chnlNode.Nodes[0] as PropertyNode).Text;
			string formulatext = (chnlNode.Nodes[1] as PropertyNode).Text;			
			string index = (chnlNode.Nodes[2] as PropertyNode).Text;
			
			StringAssert.Contains("22", chnlNode.Name);
			StringAssert.Contains("TEP", chnlNode.Type);
			StringAssert.Contains("24", formulacod);
			StringAssert.Contains("25", formulatext);
			StringAssert.Contains("21", index);
		}
		
		[Test]
		public void DTTMSerializeModel()
		{
			ITableToModel repos;
			if (File.Exists("aaaTest.xml")) File.Delete("aaaTest.xml");			
			Node node = NodeFromTable("TableOne", "TestWord.docx", KindOfTable.TEP, out repos);	
			repos.SerializeModel("aaaTest");		
		}
		
		[Test]
		public void DTTMSerializeModelManual()
		{
			ITableToModel repos;
			if (File.Exists("aaaTest.xml")) File.Delete("aaaTest.xml");			
			Node node = NodeFromTable("TableOne", "TestWord.docx", KindOfTable.manual, out repos);	
			repos.SerializeModel("aaaTest");			
		}
		
		private static Node NodeFromTable(string tableName, string filename)
		{
			ITableToModel repos;
			return NodeFromTable("TableOne", "TestWord.docx", KindOfTable.TEP, out repos);			
		}
		
		private static Node NodeFromTable(string tableName, string filename, KindOfTable knd, out ITableToModel rep)
		{
			INodeBilder nodeBld = new NodeBilder();
			rep = new TableToModel(nodeBld);			
			IReader rd = new WrdReader();
			DataTable tbl = rd.GetTable(2, filename).Result;
			var colNumber = new List<int> {
				0,
				1,
				2,
				3,
				4
			};
			var tmpNumber = new List<int> {
				5,
				6
			};
			rep.TableToModelConv(tableName, tbl, colNumber, tmpNumber, knd);
			Node node = (NodeBilder)nodeBld;
			return node;
		}
	}
}
