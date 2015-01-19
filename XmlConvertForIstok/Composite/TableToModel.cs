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
	
	public interface ITableToModel
	{
		/// <summary>
		/// Создание нодды станции, позже к ней добавляем ноды таблиц
		/// </summary>
		/// <param name="stationName">Название Станции, например "НТЭЦ4"</param>
		/// <returns>this для fluen добавления метода</returns>
		ITableToModel CreateStation(string stationName);
		/// <summary>
		/// метод добавления данных из таблицы в уже созданную ноду станции
		/// </summary>
		/// <param name="tblName">Имя таблицы</param>
		/// <param name="table">Таблица данных для конвертации(основной массив данных)</param>
		/// <param name="listColumnsNumber">Столбцы основных параметров: 0-п\п, 1-Наименование,2-Размерность,3-Обозначение,4-Формула</param>
		/// <param name="listtmplNumber">Столбцы участия в шаблонах</param>
		/// <returns>Удалась ли конвертация в модель</returns>
		/// <param name = "interval">Временной интервал для расчета</param>
		/// <param name = "kot">Ручные или автоматические параметры</param>
		bool TableToModelConv(string tblName,
		                      DataTable table,
		                      List<int> listColumnsNumber,
		                      List<int> listtmplNumber,
		                      string interval,
		                      KindOfTable kot);
		
		void SerializeModel(string fileName);
	}
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
