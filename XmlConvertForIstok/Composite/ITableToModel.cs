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
		bool TableToModelConv(string tblName, DataTable table, List<int> listColumnsNumber, List<int> listtmplNumber, string interval, KindOfTable kot);

		void SerializeModel(string fileName);
	}
}


