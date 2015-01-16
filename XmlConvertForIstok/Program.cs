﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConvertForIstok.Presenter;
using XmlConvertForIstok.Readers;
using XmlConvertForIstok.Composite;

namespace XmlConvertForIstok
{
	public delegate int ForProgress(int now,int all);
	
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IConvertForm form = new XMLConvertForm();
            new MyPresenter(form,new WrdReader(),new TableToModel(new NodeBilder()));
            Application.Run((Form)form);
        }
    }
}
