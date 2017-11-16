using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using App15.Helpers;
using System.Runtime.CompilerServices;
using App15.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace App15.iOS
{
  public class FileHelper : IFileHelper 
	{ 
 		public string GetLocalFilePath(string filename)
		{
      string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      return Path.Combine(path, filename);

    //  string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
 			//string libFolder = Path.Combine(docFolder, "..", "Library", "Databases"); 
 
 
 			//if (!Directory.Exists(libFolder)) 
 			//{ 
 			//	Directory.CreateDirectory(libFolder); 
 			//} 
 
 
 			//return Path.Combine(libFolder, filename); 
 		} 
 	} 

}