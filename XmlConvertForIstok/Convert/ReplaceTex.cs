/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 24.12.2014
 * Время: 13:45
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace XmlConvertForIstok.Convert
{
	/// <summary>
	/// Description of ReplaceTex.
	/// </summary>
	public static class ReplaceTex
	{
		public static string CleanedTex(string enterstr)
		{
			return  Regex.Replace(CleanTeX(enterstr),@"([\u005F][^\\\^\$]+)([\\\^][^\$]+)", "$2$1");
		}

		
		public static string EnterSimbol(string str) 
		{			
			return Regex.Replace(
				ReplaceTeX(str),
				@"(([^if\+\-\/\u005F\^\;\n\*0-9\,\(\)\=]+)([\u005F][\w\(\)\ \,]*)?([\\\^][\\\w\(\)\ \^\,]*)?)",
				@"$$$1$");
		}
		static private void AddStringBefore(StringBuilder sb, string text)
        {
            int s = 0;
            int i;

            if (sb == null) throw new ArgumentNullException("sb");

            for (i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] == ')') s--;
                else
                    if (sb[i] == '(')
                    {
                        s++;
                        if (s == 0) break;
                    }
                    else
//                        if (sb[i] == ' ' && i < sb.Length - 1 && s == 0)
                    	if (i < sb.Length - 1 && s == 0)	
                            break;
            }
            if (i < 0) i = 0;
            sb.Insert(i, text);
        }

        private static string ReplaceTeX(string sTeX)
        {			 
			StringBuilder sb = new StringBuilder();            
            const char c773 = (char)773; //" ̅"            
            
            foreach (var ch in sTeX)
            {
                switch (ch)
                {
                    case '〖': break;
                    case '〗': break;
                    case c773: AddStringBefore(sb, "\\bar "); /*sb.Append("\\bar");*/ break;
                    case '^': sb.Append(@"\^"); break;
                    			
                    //case 'A':
                    case 'Α': sb.Append("\\Alpha "); break;
                    case 'Β': sb.Append("\\Beta "); break;
                    case 'Γ': sb.Append("\\Gamma "); break;
                    case 'Δ':
                    case '∆': sb.Append("\\Delta "); break;
                    case 'Ε': sb.Append("\\Epsilon "); break;
                    case 'Ζ': sb.Append("\\Zeta "); break;
                    case 'Η': sb.Append("\\Eta "); break;
                    case 'Θ': sb.Append("\\Theta "); break;
                    case 'Ι': sb.Append("\\Iota "); break;
                    case 'Κ': sb.Append("\\Kappa "); break;
                    case 'Λ': sb.Append("\\Lambda "); break;
                    case 'Μ': sb.Append("\\Mu "); break;
                    case 'Ν': sb.Append("\\Nu "); break;
                    case 'Ξ': sb.Append("\\Xi "); break;
                    case 'Π': sb.Append("\\Pi "); break;
                    case 'Ρ': sb.Append("\\Pho "); break;
                    case 'Σ': sb.Append("\\Sigma "); break;
                    case 'Τ': sb.Append("\\Tau "); break;
                    case 'Υ': sb.Append("\\Upsilon "); break;
                    case 'Φ': sb.Append("\\Phi "); break;
                    case 'Χ': sb.Append("\\Chi "); break;
                    case 'Ψ': sb.Append("\\Psi "); break;
                    case 'Ω': sb.Append("\\Omega "); break;

                    case 'α': sb.Append("\\alpha "); break;
                    case 'β': sb.Append("\\beta "); break;
                    case 'γ': sb.Append("\\gamma "); break;
                    case 'δ': sb.Append("\\delta "); break;
                    case 'ε': sb.Append("\\epsilon "); break;
                    case 'ζ': sb.Append("\\zeta "); break;
                    case 'η': sb.Append("\\eta "); break;
                    case 'θ': sb.Append("\\theta "); break;
                    case 'ι': sb.Append("\\iota "); break;
                    case 'κ': sb.Append("\\kappa "); break;
                    case 'λ': sb.Append("\\lambda "); break;
                    case 'μ': sb.Append("\\mu "); break;
                    case 'ν': sb.Append("\\nu "); break;
                    case 'ξ': sb.Append("\\xi "); break;
                    case 'π': sb.Append("\\pi "); break;
                    case 'ρ': sb.Append("\\pho "); break;
                    case 'σ': sb.Append("\\sigma "); break;
                    case 'τ': sb.Append("\\tau "); break;
                    case 'υ': sb.Append("\\upsilon "); break;
                    case 'φ': sb.Append("\\phi "); break;
                    case 'χ': sb.Append("\\chi "); break;
                    case 'ψ': sb.Append("\\psi "); break;
                    case 'ω': sb.Append("\\omega "); break;
                    case '∙': sb.Append("*"); break;
                    

                    //case '1': sb.Append(""); break;

                    default: sb.Append(ch); break;
                }
            }            
            return sb.ToString();
        }

        static private bool IsSpecChar(char c)
        {
            switch (c)
            {
                case '{':
                case '}':
                case '_':
                case '^':
                    return true;
                default:
                    return false;
            }
        }
        
         static private string CleanTeX(string txt)
        {
            StringBuilder sb = new StringBuilder();
            bool skip;
            char prev = '0';
            int s = 0;
            int i, j;

            for (i = 0; i < txt.Length; i++)
            {
                skip = false;
                switch (txt[i])
                {
                    case ' ':
                        if ((i > 0 && IsSpecChar(txt[i - 1]))
                            || (i + 1 < txt.Length && IsSpecChar(txt[i + 1])))
                            skip = true;
                        break;
                    //case '^':
                    //case '_':

                    //    break;
                    case '(':
                        if (prev == '^' || prev == '_')
                        {
                            s = 1;
                            for (j = i + 1; j < txt.Length; j++)
                            {
                                if (txt[j] == '(')
                                    s++;
                                else
                                    if (txt[j] == ')') s--;
                                if (s == 0) break;
                            }
                            if (s == 0)
                            {
                                string ss = txt.Substring(i + 1, j - i - 1);
                                ss = CleanTeX(ss);
                                if (ss.Length > 1)
                                {
                                    sb.Append("{");
                                    sb.Append(ss);
                                    sb.Append("}");
                                }
                                else
                                    sb.Append(ss);
                                i = j;
                                skip = true;
                            }
                        }
                        break;
                    case ')':
                        break;
                    default:
                        if (prev == '^' || prev == '_')
                        {
                            if (i > 1 && txt[i - 2] == '\\') break;
                            string ss;

                            for (j = i; j < txt.Length; j++)
                            {
                                if (IsSpecChar(txt[j])
                                    || char.IsSeparator(txt[j])
                                    || char.IsPunctuation(txt[i])) break;
                            }

                            ss = txt.Substring(i, j - i);
                            if (ss.Length > 0)
                            {
                                ss = CleanTeX(ss);
                                if (ss.Length > 1)
                                {                                	
                                    sb.Append("{");                                    
									string nss = ss.Trim('\\');                                    
                                    sb.Append(nss);
                                    sb.Append("}");
                                }
                                else
                                    sb.Append(ss);
                                if (j < txt.Length)
                                    i = j - 1;
                                else
                                    i = j;
                                skip = true;
                            }
                        }
                        break;
                }

                if (!skip)
                {
                    prev = txt[i];
                    sb.Append(prev);
                }
            }

            return sb.ToString();
        }
	}
}
