using System.Collections;
using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib;

namespace terasurware
{
	public class Ziper
	{
		public static byte[] Zip (byte[] source)
		{
			Int32 size = source.Length;
			byte[] result = null;
			MemoryStream memStream = new MemoryStream ();
			
			using (BinaryWriter writer = new BinaryWriter(memStream)) {
				writer.Write (size);
			
				ZipOutputStream bzStream = new ZipOutputStream (memStream);
				
				bzStream.Write (source, 0, source.Length);
				bzStream.Close ();

				result = memStream.ToArray ();

				memStream.Close ();
				writer.Close ();
			}
		
			return result;
		}
	
		public static byte[] Unzip (byte[] source)
		{
			MemoryStream memStream = new MemoryStream (source);
			byte[] result = null;
		
			using (BinaryReader reader = new BinaryReader(memStream)) {

				Int32 size = reader.ReadInt32 ();
				byte[] uncompressed = new byte[size];
			
				ZipInputStream bzStream = new ZipInputStream (memStream);
				bzStream.Read (uncompressed, 0, uncompressed.Length);
				
				bzStream.Close ();
				memStream.Close ();
			
				result = uncompressed;
			
				reader.Close ();
			}
		
			return result;
		}
		
		public static void UnZipFile (string inputFile, string exportDir)
		{
			using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(inputFile) )) {
				ZipEntry entry = null;
				while ((entry= zipStream.GetNextEntry()) != null) {
					
					string output = Path.Combine (exportDir, entry.Name);
					File.Delete (output);
					
					using (FileStream writer = File.Create(output)) {
						byte[] data = new byte[512];
						int length = 0;
						while ((length = zipStream.Read (data, 0, data.Length)) > 0) {
							writer.Write (data, 0, length);
						}
					}
				}
				
				zipStream.Close ();
			}
		}
		
		public static void ZipFile(string[] files, string exportPath)
		{
		}
		
	}
}