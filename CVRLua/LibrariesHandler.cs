using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace CVRLua
{
    static class LibrariesHandler
    {
        static readonly string ms_librariesPath = Path.Combine(MelonLoader.Utils.MelonEnvironment.UserLibsDirectory, "native");
        static readonly List<string> ms_libraries = new List<string>()
        {
            "lua54.dll"
        };

        internal static void ExtractDependencies()
        {
            Assembly l_assembly = Assembly.GetExecutingAssembly();
            string l_assemblyName = l_assembly.GetName().Name;

            if(!Directory.Exists(ms_librariesPath))
                Directory.CreateDirectory(ms_librariesPath);

            foreach(string l_library in ms_libraries)
            {
                Stream l_libraryStream = l_assembly.GetManifestResourceStream(l_assemblyName + ".libs." + l_library);
                string l_filePath = Path.Combine(ms_librariesPath, l_library);

                if(!File.Exists(l_filePath))
                {
                    try
                    {
                        Stream l_fileStream = File.Create(l_filePath);
                        l_libraryStream.CopyTo(l_fileStream);
                        l_fileStream.Flush();
                        l_fileStream.Close();
                    }
                    catch(Exception)
                    {
                       Core.Logger?.Error("Unable to extract embedded {0} library", l_library);
                    }
                }
                else
                {
                    try
                    {
                        FileStream l_fileStream = File.Open(l_filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        SHA256 l_hasher = SHA256.Create();
                        string l_libraryHash = BitConverter.ToString(l_hasher.ComputeHash(l_libraryStream));
                        string l_fileHash = BitConverter.ToString(l_hasher.ComputeHash(l_fileStream));

                        if(l_fileHash != l_libraryHash)
                        {
                            l_fileStream.SetLength(l_libraryStream.Length);
                            l_fileStream.Position = 0;
                            l_libraryStream.Position = 0;
                            l_libraryStream.CopyTo(l_fileStream);
                            l_fileStream.Flush();

                            Core.Logger.Msg("Updated {0} library from embedded one", l_library);
                        }

                        l_fileStream.Close();
                    }
                    catch(Exception)
                    {
                        Core.Logger?.Error("Unable to compare/update {0} library, delete it from game folder manually and restart.", l_library);
                    }
                }

                l_libraryStream.Close();
            }
        }
    }
}
