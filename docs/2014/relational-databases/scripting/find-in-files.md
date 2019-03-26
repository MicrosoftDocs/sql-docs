---
title: "Find in Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Find in Files dialog box"
ms.assetid: bf92770a-33df-43ef-85ad-5a9223649b98
author: MightyPen
ms.author: genemi
manager: craigg
---
# Find in Files
  The **Find in Files** tab of the Find and Replace window enables you to search the code of a specified set of files for a string or expression. The matches found and actions taken are listed in the Find Results window selected in **Result Options**.  
  
 Toolbar buttons and shortcut keys are also available to open the **Find and Replace** dialog box.  
  
 The sections that follow list controls available on the **Find in Files** tab.  
  
## Find What  
 These controls on the **Find in Files** tab enable you to specify the string or expression that will be matched.  
  
 **Find what**  
 Type the text to search for. The dialog box attempts to fill in a probable search text, using text selected with the cursor before opening the dialog box, or nearby text, or previously searched-for text. You can reuse one of the last 20 search strings by selecting it from this drop-down list.  
  
 **[string with wildcards]**  
 If you want to use wildcards such as asterisks (`*`) and question marks (`?`) in your search string, select the **Use** check box under **Find Options** and then click **Wildcards**.  
  
 **[regular expression]**  
 To cause the search engine to interpret your search string as a regular expression, select the **Use** check box under **Find Options** and then click **Regular expressions**.  
  
 **Expression Builder**  
 This triangular button next to the **Find what** box becomes available when the **Use** check box is selected in **Find Options**. Click this button to display a list of wildcards or regular expressions, depending upon the **Use** option selected. Choosing any item from this list adds it to the **Find what** string.  
  
## Look In  
 The option chosen from the **Look in** drop-down list determines whether **Find in Files** searches only in currently active files or in all files stored within certain folders. Select a search scope from the list, type a folder path, or click the **Browse** button to display the **Custom Directory Set Dialog Box** and choose a set of folders to search.  
  
> [!NOTE]  
>  If the **Look in** option selected causes you to search a file that you have checked out from source code control, only the version of that file which has been downloaded to your local computer is searched.  
  
 **Look in**  
 Select a predefined search scope from this list, or use the **Custom Directory Set** dialog box to enter your own set of directories.  
  
 **Current Document**  
 This option is available when a document is open in an editor. It searches only the active document for the string in **Find what**.  
  
 **All Open Documents**  
 Searches all files currently opened for editing.  
  
 **Current Project**  
 Searches all files in the active project.  
  
 **Entire Solution**  
 Searches all files in the active solution.  
  
 **Include subfolders**  
 Specifies that subfolders of the folder specified in **Look in** will be searched. It requires a custom directory set.  
  
 **Browse**  
 Click this button to display the **Custom Directory Set** dialog box, where you can assemble, edit, save, and select named sets of directories to enter in the **Look in** box.  
  
## Find Options  
 You can expand or collapse the **Find Options** section. The following options can be selected or cleared.  
  
 **Match case**  
 When this check box is selected, the Find Results windows will only display instances of the string specified in **Find what** that are matched both by content and by case. For example, a search for **MyObject** with the **Match case** check box selected will return "MyObject" but not "myobject" or "MYOBJECT."  
  
 **Match whole word**  
 When this check box is selected, the Find Results windows will only display instances of the string specified in **Find what** that are matched in complete words. For example, a search for **MyObject** will return "MyObject" but not "CMyObject" or "MyObjectC."  
  
 **Use**  
 Indicates how to interpret special characters entered in the **Find what** or **Replace with** text boxes. The options include **Wildcards** and **Regular Expressions**.  
  
 **Regular Expressions**  
 Special notations define patterns of text to match. For a list, see [Search Text with Regular Expressions](search-text-with-regular-expressions.md).  
  
 **Wildcards**  
 Special characters such as asterisks (`*`) and question marks (`?`) represent one or more characters. For a list, see [Search Text with Wildcards](search-text-with-wildcards.md).  
  
 **Look at these file types**  
 This list indicates the types of files to search through in the directories specified in **Look in**. If this field is left blank, all of the files in the directories specified in **Look in** will be searched.  
  
```  
*.[ext]; *.[ext] (manual)  
```  
  
 To find files of a particular type, enter an asterisk wildcard (`*`) for the file name, followed by a period (`.`) and the file extension. To find more than one file type, enter multiple search strings separated by a semicolon (`;`).  
  
```  
*.[ext]; *.[ext] (from list)  
```  
  
 Select any item in the list to enter a preconfigured search string that will find files of particular types.  
  
## Result Options  
 Determines the location of the results when you click **Find All**. You can expand or collapse the **Result Options** section. The following options can be selected or cleared.  
  
 **Find Results 1 window**  
 When this check box is selected, the results of the current search will be appended to the content of the Find Results 1 window. This window opens automatically to display your search results. To open this window manually, click **Other Windows** on the **View** menu and then click **Find Results 1**.  
  
 **Find Results 2 window**  
 When this check box is selected, the results of the current search will be appended to the content of the Find Results 2 window. This window opens automatically to display your search results. To open this window manually, click **Other Windows** on the **View** menu and then click **Find Results 2**.  
  
 **Display file names only**  
 Displays one entry per file containing a search match rather than one entry per search match in either the Find Results 1 or Find Results 2 window. This option is not available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **Keep modified files open after Replace All**  
 When selected, leaves open all files in which replacements have been made, so you can undo or save the changes. Memory constraints might limit the number of files that can remain open after a replace operation.  
  
> [!CAUTION]  
>  You can use **Undo** only on files that remain open for editing. If this option is not selected, files that were not already open for editing will remain closed, and no **Undo** option will be available in those files.  
  
## Find and Replace Views  
 The tabs at the top of the Find and Replace window include **View** menus. These menus enable you to choose a set of fields to display in the active pane. You can leave the Find and Replace window docked in a convenient location and then change from tab to tab and view to view to perform any type of find or replace operation.  
  
 **Switch to Quick Find**  
 This toolbar tab changes the dialog box to a **Quick Find** dialog box.  
  
 **Switch to Find in Files**  
 This toolbar tab changes the dialog box to a **Find in Files** dialog box.  
  
 **Switch to Find Symbols**  
 This toolbar tab changes the dialog box to a **Find in Symbols** dialog box.  
  
## See Also  
 [SQL Server Management Studio Keyboard Shortcuts](../../ssms/sql-server-management-studio-keyboard-shortcuts.md)  
