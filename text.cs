using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace text
{
    public static class RTF
    {
        private static string get_content(string _rtfString)
        {
	   /*According to RTF specification,_rtfString[54] should be the beginning of the pure English
	    *text while _rtfString[61] be the start of Chinese-written text.
	    */
            if (_rtfString[54] == 'g')
                return _rtfString.Substring(61, _rtfString.Length - 64);//Chinese
            else
                return _rtfString.Substring(54, _rtfString.Length - 57);//English
        }
	/*RTF text ends with "}"*/
        private static string insert_to_end(string _rtfContent, string _originalRtf)
        {
            return _originalRtf.Insert(_originalRtf.Length - 3, _rtfContent);
        }
        //Combine two or more rtf texts
        public static string rtf_transform(string[] rtf_set)
        {
            string rtf_output = rtf_set[0];//first RTF text
            string pattern = @"\\viewkind4\\uc1.{4,}?\\fs\d{2}";
            string subsitute = @"$& 1.";
            rtf_output = Regex.Replace(rtf_output, pattern, subsitute, RegexOptions.IgnoreCase);
            string rtf_temp = null;
            int i = 1;//length
            for (i = 1; i < rtf_set.Length; i++)
            {
                subsitute = @"$& "+(i+1).ToString()+".";
                rtf_temp = get_content(rtf_set[i]);
                rtf_temp = Regex.Replace(rtf_temp, pattern, subsitute, RegexOptions.IgnoreCase);
                rtf_output = insert_to_end(rtf_temp, rtf_output);
            }
            return rtf_output+"}";
        }  
        //combine two or more rtf texts without numbering
	public static string rtf_combine(string[] rtf_set)
        {
            string rtf_output = rtf_set[0];//first RTF text
            string rtf_temp = null;
            int i = 1;//length
            for (i = 1; i < rtf_set.Length; i++)
            {
                rtf_temp = get_content(rtf_set[i]);
                rtf_output = insert_to_end(rtf_temp, rtf_output);
            }
            return rtf_output+"}";
        } 

    }

}
