using ImageFS.Utilities;
using System;
using System.IO;
using ImageFS.Stego;
using ImageFS.FileSystem;
using Newtonsoft.Json;
using ImageFS.FileSystem.Dokan;
using DokanNet;

namespace ImageFS
{
    public class Program
    {
        private static string volumeFile;
        private static string volumePassword;
        private static bool volumeOpen;

        private static ImageFSVolume volumeTable;

        private static MountVolume dokanMount;

        private static Reader imageReader;
        private static Writer imageWriter;

        static void Main(string[] args)
        {
            Logger.Log("ImageFS In Development");
            Logger.Log("By LostSoulfly");

            Logger.Log("Please enter the path to your Volume image.");
            Logger.Log("If the image does not contain a volume, it will be created.");
            Console.Write("ImageFS> ");

            volumeFile = Console.ReadLine();

            Logger.Log("Please enter the volume password:");
            Console.Write("ImageFS> ");
            volumePassword = Console.ReadLine();

            if (!File.Exists(volumeFile))
            {
                Logger.Log("Unable to locate Volume image!", Logger.LOG_LEVEL.ERR);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            if (volumePassword.Length <= 3)
            {
                Logger.Log("Password is too short.", Logger.LOG_LEVEL.ERR);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            Logger.Log("Attempting to extract ImageFS Volume..");

            imageReader = new Stego.Reader(volumeFile, volumePassword);
            imageWriter = new Writer();

            (PNGMask_Core.DataType dataType, object data) = imageReader.ReadData(ImageFSHelpers.StorageMethod.EOF);
            (PNGMask_Core.DataType dataType2, object data2) = imageReader.ReadData(ImageFSHelpers.StorageMethod.IDAT);

            Logger.Log("EOF Data: " + Enum.GetName(typeof(PNGMask_Core.DataType), dataType));
            Logger.Log("IDAT Data: " + Enum.GetName(typeof(PNGMask_Core.DataType), dataType2));

            if (dataType == PNGMask_Core.DataType.FileSystem)
            {
                Console.WriteLine();

                volumeTable = JsonConvert.DeserializeObject<ImageFSVolume>((string)data);

                Logger.Log($"MaxFileSize: {volumeTable.maxFileSize}");
                Logger.Log($"MaxFileCount: {volumeTable.maxFileCount}");
                Logger.Log($"volumeDirectories: {volumeTable.volumeDirectories.Count}");
                Logger.Log($"CurrentFileCount: {volumeTable.currentFileCount}");
                Logger.Log($"ImagesUsed: {volumeTable.volumeData.Count}");
                volumeOpen = true;
            }
            else if (dataType2 == PNGMask_Core.DataType.FileSystem)
            {
                Logger.Log("Falling back to volume backup file..");
                volumeTable = JsonConvert.DeserializeObject<ImageFSVolume>((string)data2);

                Logger.Log($"MaxFileSize: {volumeTable.maxFileSize}");
                Logger.Log($"MaxFileCount: {volumeTable.maxFileCount}");
                Logger.Log($"volumeDirectories: {volumeTable.volumeDirectories.Count}");
                Logger.Log($"CurrentFileCount: {volumeTable.currentFileCount}");
                Logger.Log($"ImagesUsed: {volumeTable.volumeData.Count}");
                volumeOpen = true;
            }
            else if(dataType == PNGMask_Core.DataType.None)
            {
                Logger.Log("File does not contain a ImageFS Volume. Create one?");
                Console.Write("ImageFS> ");

                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    volumeTable = new ImageFSVolume();
                    Logger.Log("Volume created. Writing new volume to image..");
                    imageWriter.HideFileSystem(volumeFile, JsonConvert.SerializeObject(volumeTable), volumePassword, ImageFSHelpers.StorageMethod.IDAT);

                    Logger.Log("Writing volume backup to EOF section..");
                    imageWriter.HideFileSystem(volumeFile, JsonConvert.SerializeObject(volumeTable), volumePassword, ImageFSHelpers.StorageMethod.EOF);
                    volumeOpen = true;
                }
            }

            if (!volumeOpen)
            {
                Console.ReadKey();
                Environment.Exit(0);
            }

            int userInput = 0;
            do
            {
                userInput = MainMenu();

                switch (userInput)
                {
                    case 1: // extract file
                        ExtractFileMenu();
                        break;

                    case 2: // insert file
                        InsertFileMenu();
                        break;

                    case 3: // new volume directory
                        CreateVolumeDirectoryMenu();
                        break;

                    case 4: // Add donor images
                        AddDonorImagesMenu();
                        break;

                    case 5: // volume maintenance
                        VolumeMaintenanceMenu();
                        break;

                    case 6: // Mount volume
                        dokanMount = new MountVolume(volumeTable);
                        dokanMount.Mount("n:\\");
                        break;
                }

            } while (userInput != 0);

            Logger.Log("Exiting ImageFS. Save volume?");

            Console.Write("ImageFS> ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Logger.Log("Writing volume to image IDAT..");
                imageWriter.HideFileSystem(volumeFile, JsonConvert.SerializeObject(volumeTable), volumePassword, ImageFSHelpers.StorageMethod.IDAT);

                Logger.Log("Writing volume backup to EOF section..");
                imageWriter.HideFileSystem(volumeFile, JsonConvert.SerializeObject(volumeTable), volumePassword, ImageFSHelpers.StorageMethod.EOF);
            }

        }

        private static int MainMenu()
        {
            Console.WriteLine();
            Logger.Log("ImageFS Main Menu");
            Console.WriteLine();
            Logger.Log("0. Exit");
            Logger.Log("1. Extract file from volume");
            Logger.Log("2. Insert file into volume");
            Logger.Log("3. Create new volume directory");
            Logger.Log("4. Add donor images to store data in");
            Logger.Log("5. Perform volume maintenance");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        private static void InsertFileMenu()
        {

        }

        private static void ExtractFileMenu()
        {

        }

        private static void CreateVolumeDirectoryMenu()
        {
            volumeTable.volumeDirectories.Add(new ImageFSDirectory("test"));
        }

        private static void AddDonorImagesMenu()
        {

        }

        private static void VolumeMaintenanceMenu()
        {
            foreach (var item in volumeTable.volumeDirectories)
            {
                Logger.Log($"{item.directoryName} - SubDirectories: {item.subDirectories.Count} Files: {item.directoryFiles.Count}");
            }
        }

    }
}
