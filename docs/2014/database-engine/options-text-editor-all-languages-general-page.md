---
title: "Options (Text Editor - All Languages - General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: bf18907c-94e2-4c09-9b2b-0925ac04c627
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - All Languages - General Page)
  Use this dialog box to set general editing options in all five of the editors in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. To display these options, click **Options** on the **Tools** menu. Select the **Text Editor** folder, expand the **All Languages** folder and then click **General**.  
  
## Option Settings by Editor  
 You must use the **All Languages** dialogs to set options for the DMX, MDX, and SQL Server Compact editors. Options set here are also applied to the Plain Text, Transact-SQL, and XML editors, but you can set options separately for those editors by expanding the subfolders for those languages and selecting their option pages.  
  
> [!CAUTION]  
>  If you set an option using this dialog, but want a different setting for the Plain Text, Transact-SQL, or XML editors, you must set the options for those editors after applying your selections in the **All Languages** dialog.  
  
 Some editors may not support all of the options listed on this page. A shaded check mark is displayed when an option has been selected on the **General** page of the **Options** dialog box for some programming languages, but not for others.  
  
## Statement Completion  
 **Auto list members**  
 Display pop-up lists of available members, properties, or values as you type in the editor. Choose any item from the pop-up list to insert the item into your code. Selecting this check box enables the **Hide advanced members** option.  
  
 **Hide advanced members**  
 Shorten pop-up statement completion lists by displaying only those items most commonly used. Other items are filtered from the list. This option will not be available if no members are marked as advanced members.  
  
 **Parameter information**  
 Display the complete syntax for the current declaration or procedure to the left of the insertion point in the editor with all of its available parameters. The next parameter you can assign is displayed in bold.  
  
## Settings  
 **Enable virtual space**  
 Position comments at a consistent point next to your code. When this check box is selected, you can position the cursor beyond the last character in the row. When you type, tabs or spaces are automatically added to complete the row to the insertion point.  
  
 **Word wrap**  
 Display on the next line any portion of a line that extends horizontally beyond the viewable editor area. Selecting this check box enables the **Show visual glyphs for word wrap** option.  
  
 **Show visual glyphs for word wrap**  
 Display a return-arrow indicator where a long line wraps onto a second line.  
  
> [!NOTE]  
>  These reminder arrows are not added to your code, and they do not print. They are for reference only. This feature is not available in all types of editors.  
  
 **Apply Cut/Copy commands to blank lines when there is no selection**  
 Set the behavior of the editor when you place the insertion point on a blank line, select nothing, and then click **Copy** or **Cut**.  
  
 When this check box is selected, the blank line is copied or cut. If you then click **Paste**, a new, blank line is inserted.  
  
 When this check box is cleared, nothing is copied or cut. If you then click **Paste**, the content most recently copied is pasted. If nothing has been copied, nothing is pasted.  
  
 This setting has no effect on the **Copy** or **Cut** commands when a line is not blank. If nothing is selected, the entire line is copied or cut. If you then click **Paste**, the text of the entire line and its end line character are pasted.  
  
## Display  
 **Line numbers**  
 Display a line number next to each line of code.  
  
> [!NOTE]  
>  These line numbers are not added to your code, and they do not print. They are for reference only.  
  
 **Enable single-click URL navigation**  
 Change the cursor to a symbol of a pointing hand when it passes over a URL in the editor. You can click the URL to display the indicated page in your Web browser.  
  
 **Navigation bar**  
 Display a navigation bar at the top of the Code Editor. Use its **Objects**and **Procedures** drop-down lists to choose a particular object in your code, select from its procedures, and insert an instance of the selected procedure. The navigation baris not available for all code types.  
  
  
