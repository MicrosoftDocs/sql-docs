---
title: "Find Results Windows | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords:
  - "vs.findresults2"
helpviewer_keywords: 
  - "Find Results Windows dialog box"
ms.assetid: 3b68dbb7-26d6-4bc9-bd2c-c27e5dc385c3
author: MightyPen
ms.author: genemi
manager: craigg
---
# Find Results Windows
  The two Find Results windows display matches found by using the **Find in Files** or **Replace in Files** tabs of the **Find and Replace** dialog. The **Result Options** command for **Find in Files** and **Replace in Files** allows you to choose the Find Results window where any matches found will be listed.  
  
 The selected Find Results window opens automatically whenever matches are found. To display a Find Results window manually, click **Other Windows** on the **View** menu and then click **Find Results 1** or **Find Results 2**.  
  
 To display the code file and jump to the line where a match occurs, double-click any line in the results list. The source file is displayed in the Code Editor with the insertion point placed where the matched text begins. A symbol appears in the indicator margin of the editor to mark the line that includes the match, and the status bar displays its full text.  
  
## Toolbar Buttons  
 The following toolbar buttons are available to help you scan through the results list and jump to lines in your code where matches were found.  
  
 **page flag + up arrow**  
 Go to the line where the selected match was found.  
  
 **page + left arrow**  
 Go to the line of the previous match.  
  
 **page + right arrow**  
 Go to the line of the next match.  
  
 **Clear all**  
 Remove all matches from the **Results** list.  
  
## Shortcut Keys  
 The following shortcut keys are available to help you navigate quickly through the matches found.  
  
 CTRL+HOME  
 Scroll to the top of the Find Results window.  
  
 CTRL+END  
 Scroll to the bottom of the Find Results window.  
  
 PAGE UP  
 Scroll up to the next group of matches.  
  
 PAGE DOWN  
 Scroll down to the next group of matches.  
  
 UP ARROW  
 Select the previous match.  
  
 DOWN ARROW  
 Select the next match.  
  
## Search Result Entries  
 Each entry in the results list provides the following information:  
  
-   Full path  
  
-   File name  
  
-   Line number  
  
-   The full text of the line containing the match  
  
> [!TIP]  
>  You can use **Quick Find** to scan through a lengthy results list. Open and dock the Find Results window, and then click the triangular **View** button on the **Find** tab and switch to **Quick Find**. Set the **Look in** field of the search to **Active Window**, enter a **Find what** string, and then click **Find Next**. This lets you scan the results list for matches found in particular folders or files, or matches on lines of code where some other key term also occurs.  
  
## Summary Lines  
 Each set of search results begins with a line that states the search parameters, and ends with a line of statistics. For example, the results list from a **Find in Files** search in all open documents for any strings matched by the regular expression "`var[1-3]&par`" might begin with this line of search parameters:  
  
 `Find all "var[1-3]&par" Regular Expression, All Open Documents`  
  
 and might conclude with this line of statistics:  
  
 `Total found: 57  Matching files: 23  Total files searched: 59`  
  
 The results list from a **Replace in Files** search in all open documents that replaced any strings matched by the regular expression "`var[1-3]&par`" with the string "`sample`" might begin with this line of search parameters:  
  
 `Replace "var[1-3]&par", "sample", Regular Expression, All Open Documents`  
  
 and might conclude with this line of statistics:  
  
 `Total replaced: 57  Matching files: 23  Total files searched: 59`  
