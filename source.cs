using System;
using System.IO;

class Demo
{
    static String sourceRootPath = @"C:\Users\Administrator\AppData\Local\Packages\Microsoft.624F8B84B80_8wekyb3d8bbwe\SystemAppData\wgs";

   static String distRootPath = @"D:\备份\地平线5存档备份";
    static int count=0;

    public static void CopyFiles(FileSystemInfo info)
    {
           if (!info.Exists) return;
        DirectoryInfo dir = info as DirectoryInfo;
        if (dir == null) return;
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        foreach(FileSystemInfo file in files)
        {
            FileInfo fi = file as FileInfo;
            if (fi == null)
            {
                CopyFiles(file);
            }
            else
            {
                String oldPath = fi.FullName;
                Console.WriteLine("原始位置"+oldPath);
                String newPath = oldPath.Replace(sourceRootPath, distRootPath);
                Console.WriteLine("目标位置" +newPath);
                //String newDir = newPath.Substring(0, newPath.LastIndexOf('\\'));
                Directory.CreateDirectory(Directory.GetParent(newPath).ToString());
                File.Copy(oldPath, newPath);
                count++;

            }
        }        
    }

    static void Main(string[] args)
    {

        String[] dirs = Directory.GetDirectories(distRootPath);
        foreach(String dir in dirs)
        {
            Directory.Delete(dir,true);
        }
        CopyFiles(new DirectoryInfo(sourceRootPath));
        Console.WriteLine($"存档备份成功,共备份{count}个文件");
    }
}


