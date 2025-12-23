# File-Collection-Utility
#### By Anthony W. van Leeuwen
#### December 19, 2025
## What Does the File Collection Utility Do?
The **File Collection Utility** copies picture or photo files from user-specified directories, backup folders, thumb drives, SD cards, CD/DVDs, and/or external hard drives connected to the computer using a USB cable adapter.  The photos are copied and organized in a single collection and located in a user designated folder, which serves as the root folder for the photo collection.  The collection is organized in a two-layer folder structure, first by year, second by creation date.  The utility skips duplicate files and verifies the uniqueness of each file copied.  In the event that two files have the same name and same creation date, but are different, the file is renamed by appending a numeric identifier to the filename.  An entry is made in the XML log file for each file copied, skipped, or renamed.  The log file can be inspected using a normal text editor or by a spreadsheet program such as Microsoft Excel.

## Why Was This Utility Developed?
Over the years, the author, like so many others, have had multiple cameras, photo applications, and numerous backups making it difficult to find old photos.  Therefore, the File Collection Utility was developed as a means of creating a single collection of picture files from multiple folders located on the internal hard drives, or on an external hard drives, SD cards, thumb drives, CD/DVDs, or other media.  Even from hard drives removed from old computers and connected to a USB port via a drive adapter.  Goals for the utility included the following: (1) create a single collection of photo files; (2) organized by the photo’s creation date; and (3) eliminate duplicate files. 

## How is the File Creation Date Determined?
The file system Metadata has three timestamps for each photo or electronic file.  The first is called "Creation Time" which generally identifies when the file was written to the current file system.  The second is called the "Last Access Time" which generally identifies when the file was last accessed.  The third is the "Last Write Time" which is the date and time the file's content was originally written to the file.  The utility determines the actual creation date to be the lesser of the file's "Last Write Time" and the file's "Creation Date".  The actual creation date is then used by the utility to locate the file in the collection.  The year value of the actual creation date “YYYY” is used to name the first level calendar year folder.  The second level folder uses the actual creation date as a folder name in the form of "YYYY-MM-DD". 

## How does the utility work?
The utility opens the source folder and examines the file name and creation date for each file in the source folder.  The file's actual creation date is used first to determine if there is a year folder under the Root Directory and then determine if a folder with the creation date name is located in the year folder.  If they do not exist, they are created and the file is copied.  If the folders already exist, then the target folder is checked to see if a file by the same name already exists.  If so, the new file from the source folder is compared to the file that already exists.  If the file name is different, then the file is copied to the target folder.  If the file name is the same, then we need to determine if the new file and the existing file are the same.  If the files have the same file name but have different Last Write Time's and different lengths, then the file content is different, and the file is renamed and copied to the target directory.  If the file name and Last Write Time and file length are the same, then an MD5 hash is run on both files.  If the hash is different the file is renamed and copied, else the file is skipped.

## Criteria Used to Identify a Duplicate File?
The criteria used to determine that two files with the same name and extension are the same and considered to be duplicate files is as follows:

1. The files have the same file name including extension.
2. The files have the same “Last Write Time”.
3. The files have the same length in bytes.
4. The files have the same MD5 hash.

## Precautions To Observe Before Using the Utility!
The user needs to be aware that the Root Directory size can grow rapidly, because color picture files are relatively large in size and users tend to have very large collections of pictures.  It is recommended that a large external hard drive be used to contain the collection. 
 
## File Collection Utility Design
The File Collection Utility consists of a single Main Form.

![2025-12-21_162129.png](C:/Users/Anthony/source/repos/Project22/FileColllectionUtility/2025-12-21_162129.png "")

### File Collection Utility Main Form
The File Collection Utility Main Form consists of five groups of controls: 
* Root Directory and Log File
* Source Directory and Description
* Statistics
* Process Source Directory
* Status Bar and Close Button

### Root Directory and Log File
The Root Directory and Log File group of controls contains two buttons that allow the user to select the Root Directory and the Log Filename.  In addition, two textboxes are included to display the selected Root Directory and selected Log File Name. 
 
### Root Directory
The “Root Directory” is the top-level directory where the photo or file collection is stored and into which folders are created and files copied.  The Root Directory contains multiple calendar year folders denoting the year the photo or video was created.  The year folder, e.g. “YYYY”, contains additional folders identified by the photo or video creation date, e.g. “YYYY-MM-DD”.  These folders are also identified herein as the target folder for the copy operation.

### Log File Name
The Log Filename is the name of the XML log file which records the following information for each file processed:

* Source Description or by default the Source folder name 
* Source Full File Name and Path
* File size (in bytes)
* File Created Date
* File Status (copied, renamed, or skipped)
* Target Full File Name and Path

### Source Directory and Description
The Source Directory and Description section contains a single button, two textboxes, and two checkboxes.
  
#### Select Source Directory Button
The **Select Source Directory** button that opens the folder dialog to select the source directory where all the photo or video files to be collected are located.  The selected Source Directory is displayed in the Source Directory textbox.  Note that the Source Directory can include subdirectories which are also scanned for photo and video files.

### Source Description Textbox
One text box is labeled **Source Descriptio**n which allows the user to enter a descriptive name for the source folder or source drive.  For example, the user should mark each USB stick, SD Card, CD/DVD, external hard drive, with a unique label and enter that label into the Source Description textbox.  If the user does not enter a description, by default the name of the source folder is used.

#### “Restrict Files To” section
The “Restrict Files To” section contains two checkboxes to select **Photos and Image Files** and/or **Video Files**.  The purpose of the two checkboxes is to restrict the collection of files to either photo or image files and/or video files.  If the two checkboxes are not checked then ALL files encountered by the utility in the source directory and subdirectories will be copied to target folder.  This feature provides the capability to retrieve pictures, photos, and videos mixed in with document files or other files.

## Statistics
The statistics section describes the number of files detected in the source directory and subdirectories, the number of files copied, files renamed, and files skipped.  These statistics are updated while the background worker process is running.  

## Process Source Directory
The Process Source Directory contains two buttons, the** Process Source Files** button and the **Cancel Process** button.  

### Process Source Files Button
The **Process Source Files** button kicks off a background worker process which updates the statistics and updates the progress bar located in the Status strip.  The background worker process continues until all files in the source folder are processed or the user presses the Cancel Process button.

### Cancel Process Button
The **Cancel Process** button sets a flag causing the background worker process to terminate.  This button provides the user with a means to cancel the background worker process. 
 
## Form Status Bar and Close Button
###Form Status Bar
The **Form Status Bar** contains the utility name and version, a progress bar, and a license notice.  The progress bar identifies how far along the process is in copying files located in the source directory. 
 
### Form Close Button
The form **Close** button when pressed closes the form.



