/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 13.01.2015
 * Время: 13:58
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Collections.Generic;

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Какой тип расчета ручной или расчет ТЭП
	/// </summary>
	public enum KindOfTable
	{
		TEP,
		manual
	}
	/// <summary>
	/// Description of TableToModel.
	/// </summary>
	public class TableToModel : ITableToModel
	{
		private INodeBilder nodeBld;
		private INodeBilder  mainBilder;
			
		public TableToModel(INodeBilder _nodeBld)
		{
			mainBilder = _nodeBld;
		}		
		#region ITableToModel implementation
		public ITableToModel CreateStation(string stationName)
		{
			nodeBld = mainBilder.AddNode(stationName,"Station")
				.AddProperty("sortindex","string");			
			return this;			
		}	
		
		public void SerializeModel(string fileName)
		{
			mainBilder.Serialyze(fileName);
		}

		public bool TableToModelConv(string tblName,
		                             DataTable table, 
		                             List<int> listColumnsNumber,
		                             List<int> listtmplNumber,
		                             string interval,
		                             KindOfTable kot)
		{
			if (nodeBld == null) nodeBld = mainBilder;
			var tableNode = nodeBld.AddNode(tblName,"Folder").AddProperty("sortindex",nodeBld.GetNodesNumber().ToString());
			
			switch (kot) {
				case KindOfTable.TEP: TEPMethod(table,listColumnsNumber, listtmplNumber, tableNode, "TEPTemplate","TEP",interval);
				break;	
				case KindOfTable.manual: TEPMethod(table,listColumnsNumber, listtmplNumber, tableNode, "ManualGate","ManualParameter",interval);
				break;					
			}
								
			return true;
		}

		#endregion

		void TEPMethod(DataTable table, 
                      List<int> listColumnsNumber,
                      List<int> listtmplNumber, 
                      INodeBilder tableNode, 
                      string typetable,
                     string typeChannel,
                    	string interval)
		{
			for (int j = 0, tableRowsCount = table.Rows.Count; j < tableRowsCount; j++) {
				var elem = table.Rows[j];
				mainBilder.AddParamValues(elem.Field<string>(listColumnsNumber[3]));
			}
			foreach (int i in listtmplNumber) {
				var colName = table.Columns[i].ColumnName;				
				var tmplNode = tableNode.AddNode(colName, typetable)
					.AddProperty("interval","[UTC+03]=1d")
					.AddProperty("sortindex",tableNode.GetNodesNumber().ToString());
				
				foreach (DataRow row in table.Rows) {
					if (row.Field<string>(i) == "+") {
						var chanNode = tmplNode
							.AddNode(row.Field<string>(listColumnsNumber[1]),typeChannel)
							.AddProperty("formula_cod",row.Field<string>(listColumnsNumber[3]) + "," + colName + "," + interval)
							.AddProperty("formula_text",row.Field<string>(listColumnsNumber[4]))
							.AddProperty("index",row.Field<string>(listColumnsNumber[0]))
							.AddProperty("sortindex",tmplNode.GetNodesNumber().ToString());
					}
				}
			}
		}
	}
}
