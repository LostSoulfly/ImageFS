# ImageFS
Still planning how to implement the filesystem.

The goal is to have a GUI which accepts a folder (or folders) of PNG images which will be used to store data.
After it is functional, I'd like to implement Dokan for mounting the filesystem as a drive.

```
Utilizing PNG's IDAT chunks and EoF, we can store two files per PNG reliably.

One file stores the ImageFS volume structure and file information. This file must be supplied to open the filesystem.

Volume information stored is protected by a password which is required before the volume information can be accessed.

Flow:
Open Volume ImageFS file
Enter decryption password
Decrypt ImageFSVolume
Load Directories and their files
Determine MaxFileSize
Determine MaxFileCount
Determine CurrentFileCount
Display Root ImageFSDirectory


ImageFSVolume
	VolumeData [List all images as ImageFSFile]
	Directories [List of ImageFSDirectory]
	MaxFileSize
	MaxFileCount
	CurrentFileCount

ImageFSDirectory
	Directories [List of subdirectories as ImageFSDirectory for that folder]
	Files [List of ImageFSFile for that folder]

ImageFSFile
	string ImagePath		// Path to image file
	bool DoesFileExist		// if the original image location exists
	FilePassword
	FilePasswordIv
	FileSlots [List of ImageFSFileSlot]		// currently, EoF and IDAT, so two files can be stored in one PNG
	
// It may be possible to store multiple tEXt/IDAT chunks for unlimited files
// but for now, we'll assume we can only have one of each.
struct StorageMethod
	EoF
	IDAT
	
ImageFSFileSlot
	StorageMethod		 	//storage method
	StoredFileIdentifier		// Each file should have a unique identifier
	StoredFileDirectory		// Each directory should have a unique identifier
	//StoredFiles				// in the future, it could be possible to write large files to multiple images
	StoredFileSize
	bool FileSlotInUse			// Is this StorageType currently in use
	
  ```
