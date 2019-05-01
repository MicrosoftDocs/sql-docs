---
title: "Open File Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vs.openfile"
ms.assetid: 3e01b9f5-2b0a-4fb3-9da8-984d27d17b8a
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Open File Dialog Box
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use the **Open File** dialog box to open an existing file from disk. You can also use this dialog box to open an already opened file using different language encoding options.  
  
To access this dialog box, select **Open** from the **File** menu and then choose **File**. This dialog box also appears when you are opening files from other elements, such as the **External Tools** dialog box. From the **File** menu, select **Open**, and then choose **Project/Solution** to open the similar **Open Project** dialog box.  
  
> [!NOTE]  
> Before opening a project or component in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], determine the trustworthiness of its code. The act of opening the project or component in a [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] may execute its code in a trusted process on your local machine.  
  
## Option  
**Look in**  
Locate the existing project folder from this drop-down menu. Selecting a folder from this list displays the contents of the folder in the primary pane.  
  
## My Places Bar  
**Desktop**  
Displays the files and folders located on the desktop.  
  
**My Projects**  
Displays the files and folders in the users **Projects** folder.  
  
**My Computer**  
Displays the contents of your floppy disk, hard disk, and CD-ROM drive.  
  
## Folder List  
**File name**  
Use this option to filter the files and folders that are displayed. Enter a full or partial file name on which to filter. You can use the asterisk (*) as a wildcard.  
  
**Files of type**  
Use this option to filter the contents of the folder or directory selected in Look in for a particular file type.  
  
**Open With and Encoding Options**  
To use the **Open With Dialog Box** to specify an editor for the target file, select the small rectangle at the right of the **Open** button and choose **Open With**. If necessary, you can also specify a language-encoding scheme to apply when opening the selected file. To do so, select a program in the list that contains "**with Encoding**" and choose **Open** to display the **Encoding Dialog Box**. This button is not always available.  
  
## Toolbar  
**Navigate Back**  
Returns the most recently viewed folder, drive, or internet location.  
  
**Up One Level**  
Navigates the tree to the next highest folder in the tree view.  
  
**Search the Web**  
This button is not available.  
  
**Delete**  
Deletes the selected files or folders from storage.  
  
**New Folder**  
Displays the **New Folder** dialog box. Use this option to create a new child folder under the folder selected in the **Look in** drop-down list box.  
  
## Views  
Provides options for arranging and viewing the contents of the item selected in the **Views** drop-down list box.  
  
**Thumbnails**  
Displays thumbnails for items in the display pane.  
  
**Tiles**  
Displays files and folders as large icons.  
  
**Icons**  
Displays files and folders as small icons.  
  
**List**  
Displays files and folders in a list format.  
  
**Details**  
Displays the name, size, type, and last-modified date of files and folders in a list format. To sort by a particular detail, click its column header.  
  
**WebView**  
This command is not available.  
  
## Tools  
Select a tool to apply to the item selected in the contents pane.  
  
**Delete**  
Deletes the selected file or folder from storage.  
  
**Map Network Drive**  
Opens the **Map Network Drive** dialog box.  
