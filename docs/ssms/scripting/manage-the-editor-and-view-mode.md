---
title: "Manage the Editor and View Mode"
description: "Learn how to select either of two view modes of SQL Server Management Studio: Tabbed Documents mode and Multiple Document Interface mode. Learn also about split views, word wrap, Virtual Space mode, displaying line numbers, Full Screen mode, and Auto Hide All." 
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Editor [SQL Server Management Studio], managing window behavior"
  - "workbench view modes [SQL Server Management Studio]"
  - "full screen mode [SQL Server Management Studio]"
  - "fonts [SQL Server Management Studio]"
  - "word wraps [SQL Server Management Studio]"
  - "virtual space mode [SQL Server Management Studio]"
  - "splitting document views"
  - "displaying line numbers"
  - "view modes [SQL Server Management Studio]"
ms.assetid: 25c58a14-9f94-4296-9770-7d84c6bc3969
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Manage the Editor and View Mode

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The Editor gives you a number of ways to control the view of your code.  

## Changing the View Mode  

SQL Server Management Studio features a view mode called **Tabbed Documents**, which allows you to open multiple editors and documents simultaneously and access them through tabs at the top of the Editor. You can alternatively open the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] environment in Multiple Document Interface (MDI) mode, which joins windows without the tabs, and allows each window to be tiled, minimized, and so on.  
  
#### To switch between view modes  
  
1.  Click **Options** on the **Tools** menu.  
  
2.  Click **Environment**. Click **General**.  
  
3.  Click **Tabbed documents** or **MDI environment**.  
  
    > [!NOTE]  
    >  The changes will not take effect until SQL Server Management Studio is restarted.  
  
## Splitting the View  
 An Editor window can be split into two separate parts for easier editing.  
  
#### To split a window  
  
1.  Click the splitter bar (located above the scroll bar).  
  
2.  Drag the splitter bar downward.  
  
3.  To go back to a single pane, double-click the splitter bar dividing the two panes.  
  
 The new pane contains the same document, and any changes made to one pane are reflected in the other pane as long as that pane displays the same place in the document.  
  
## Word Wrap  
 When you activate Word Wrap, the horizontal scrollbar is removed and lines of code that exceed the width of the Editor's window size automatically wraps to the next displayed line rather than scrolling off the side of the window.  
  
#### To activate word wrap  
  
1.  Click **Options** on the **Tools** menu.  
  
2.  Click **Text Editor**.  
  
3.  Open the appropriate language folder (or **All Languages** to affect all languages).  
  
4.  Select **Word wrap**.  
  
## Enabling Virtual Space Mode  
 In **Virtual Space** mode, the Editor acts as if the space past the end of each line is filled with an infinite number of spaces, allowing code lines to continue off the side of the visible screen area.  
  
#### To enable Virtual Space mode  
  
1.  Click **Options** on the **Tools** menu.  
  
2.  Click **Text Editor**.  
  
3.  Open the appropriate language folder (or **All Languages** to affect all languages).  
  
4.  Select **Enable virtual space**.  
  
 When Virtual Space mode is not enabled, the cursor wraps from the end of one line to the first character of the next line and vice-versa.  
  
## Displaying Line Numbers  
 You can turn on line numbering in your code. Line numbers are useful for navigating code. For more information, see [Navigate Code and Text](./navigate-code-and-text.md).  
  
> [!NOTE]  
>  Turning on line numbering does not mean that the document will print with line numbers. For line numbers to print, you must select the **Line numbers** check box in the **Page Setup** command on the **File** menu.  
  
#### To display line numbers in code  
  
1.  Click **Options** on the **Tools** menu.  
  
2.  Click **Text Editor**.  
  
3.  Click **All Languages**.  
  
4.  Click **General**.  
  
5.  Select **Line numbers**.  
  
 To specify line numbering for only some programming languages, select **Line Numbers** in the appropriate folder.  
  
## Enabling Full Screen Mode  
 You can choose to hide all tool windows and view only document windows by enabling Full Screen mode.  
  
#### To enable Full Screen mode  
  
1.  Press ALT+SHIFT+ENTER to toggle Full Screen mode.  
  
## Using Auto Hide All  
  
#### To hide all the tool windows at once  
  
1.  Select **Auto Hide All** on the **Window** menu.  
  
