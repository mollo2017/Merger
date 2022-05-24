using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Merger
{
    public class FilesUtils
    {
        public static string GetFileContent(string pat)
        {
            string content = string.Empty;
            using (StreamReader osr = File.OpenText(pat))
            {
                content = osr.ReadToEnd();
            }
            return content;
        }

        public static List<string> GetFileContentByLine(string pat)
        {
            List<string> content = new List<string>();
            using (StreamReader osr = File.OpenText(pat))
            {
                string line;
                while((line = osr.ReadLine()) != null)
                {
                    content.Add(line);
                }
            }
            return content;
        }

        public static void WriteFile(string pat, string content)
        {
            using (FileStream ofs = File.Create(pat))
            {
                Byte[] dts = new UTF8Encoding(true).GetBytes(content);
                ofs.Write(dts, 0, dts.Length);
            }
        }

        public static string GetFileName(string pat, string FileName, string Extension)
        {
            string FullPath = string.Format(@"{0}\{1}_{2}.{3}", pat, FileName, 0, Extension);
            for (int cnt = 1; File.Exists(FullPath); cnt++)
                FullPath = string.Format(@"{0}\{1}_{2}.{3}", pat, FileName, cnt, Extension);
            return FullPath;
        }

        public static string GetFileName(string FullPath)
        {
            string[] seq = FullPath.Split(@"\");
            return seq[seq.Length - 1];
        }
    }
}
