using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CreatZipFile
{
    class Program
    {
        static void EditFile(string Path, string proxy)
        {
            string line = null;
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader(Path);
            // read all the lines in the file and store them in the List
            //int index = 0;
            //int count = 0;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
                //if (line.Contains("host:"))
                //    index = count;
                //count++;
            }
            reader.Close();

            lines[5] = "            host: \"" + proxy + "\",";
            //lines[index] = "            host: \"" + proxy + "\",";

            using (StreamWriter sw = new StreamWriter(Path, false))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    sw.WriteLine(lines[i]);
                }
            }
        }

        static void CreateZipFileAndConvertToCrxFile(string startPath, string zipPath)
        {
            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, false);
            string newNameZip = zipPath.Replace(".zip", ".crx");
            //Console.WriteLine("Path: " + zipPath);
            //Convert .zip to File .crx
            System.IO.File.Move(zipPath, newNameZip);
        }

        static void Main(string[] args)
        {
            string _ProxyPath = @"D:\TOOL_SUPPORT_DATATOOL\Temp\2020-05-11 23 19 01.txt";
            string _Path = @"D:\TOOL_SUPPORT_DATATOOL\ProxyZip\background.js";
            string startPath = @"D:\TOOL_SUPPORT_DATATOOL\ProxyFile";
            string zipPath = @"D:\TOOL_SUPPORT_DATATOOL\ProxyFile\";


            //List<string> newProxy = new List<string>(){
            //    "23.81.55.109",
            //    "23.81.55.137",
            //    "23.81.55.144",
            //    "23.81.55.145",
            //    "23.81.55.162",
            //    "23.81.55.191",
            //    "23.81.55.216",
            //    "23.81.55.231",
            //    "23.81.55.246",
            //    "23.81.55.253",
            //    "23.81.55.48",
            //    "23.81.55.56",
            //    "23.81.55.6",
            //    "23.81.55.64",
            //    "23.81.55.66",
            //    "23.81.55.8",
            //    "23.81.55.80"
            //};

            //string proy = "172.241.153.124";
            //23.80.154.60
            List<string> ProxyList = GetProxyList(_ProxyPath);
            foreach (var proxy in ProxyList)
            {
                EditFile(_Path, proxy);
                CreateZipFileAndConvertToCrxFile(startPath, zipPath + proxy.Replace(".", "_") + ".zip");
            }
            //ZipFile.CreateFromDirectory(@"D:\TOOL_SUPPORT_DATATOOL\ProxyZip", @"D:\TOOL_SUPPORT_DATATOOL\test.zip", CompressionLevel.Fastest, false);
            //System.IO.File.Move(@"D:\TOOL_SUPPORT_DATATOOL\test.zip", @"D:\TOOL_SUPPORT_DATATOOL\ProxyZip\test.crx");
            Console.WriteLine("Xong");
            Console.Read();
        }

        static List<string> GetProxyList(string path)
        {
            string line = null;
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader(path);
            
            while ((line = reader.ReadLine()) != null)
            {
                string[] proxy = line.Split(':');
                Console.WriteLine(proxy[0]);
                lines.Add(proxy[0]);
            }
            reader.Close();
            return lines;
        }
    }
}
