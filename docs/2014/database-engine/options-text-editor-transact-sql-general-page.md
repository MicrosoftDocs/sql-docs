---
title: "Options (Text Editor - Transact-SQL- General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Text_Editor.SQL.General"
dev_langs: 
  - "TSQL"
ms.assetid: 7021ecb7-8fb5-4d8c-b984-3d34fcde8be2
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - Transact-SQL- General Page)
  Use the **General** options dialog box to change the general editing behavior of the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor, which is used to edit [!INCLUDE[tsql](../includes/tsql-md.md)] scripts. To display these settings, click **Options** on the **Tools** menu, expand the **Transact-SQL** subfolder, and then click **General**.  
  
## Setting Options in Multiple Locations  
 Options for the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor can also be set in the **All Languages General** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor options using this dialog.  
  
## Statement Completion  
 **Auto list members**  
 When this check box is selected, lists of available database and schema objects, columns, table-valued functions, or functions are displayed as you type in the editor. Choose any item from the pop-up list to insert it into your code.  
  
 **Hide advanced members**  
 This check box is unavailable.  
  
 **Parameter information**  
 When this check box is selected, parameter information is displayed for a stored procedure or function that is immediately to the left of the insertion point (cursor). The information includes a list of the available parameters with their names and data types.  
  
## Settings  
 **Enable virtual space**  
 When this check box is selected, you can click anywhere beyond the end of a line of code and type. Select this check box to position comments at a consistent point next to your code. Selecting this check box disables the **Word wrap** check box.  
  
 **Word wrap**  
 When this check box is selected, any portion of a line that extends horizontally beyond the viewable editor area is automatically displayed on the next line. Selecting this check box enables the **Show visual glyphs for word wrap** check box and disables the **Enable virtual space** check box.  
  
 **Show visual glyphs for word wrap**  
 When this check box is selected, a return arrow indicator is displayed where a long line wraps to the next line.  
  
 **Apply Cut/Copy commands to blank lines when there is no selection**  
 This check box sets the behavior of the editor when you place the insertion point on a blank line, select nothing, and then click **Copy** or **Cut**.  
  
 When this check box is selected, the blank line is copied or cut. If you then click **Paste**, a new, blank line is inserted.  
  
 When this check box is cleared, nothing is copied or cut. If you then click **Paste**, the content most recently copied is pasted. If nothing has been copied previously, nothing is pasted.  
  
 This setting has no effect on **Copy** or **Cut** when a line is not blank. If nothing is selected, the entire line is copied or cut. If you then click **Paste**, the text of the entire line and its end line character are pasted.  
  
## Display  
 **Line numbers**  
 When this check box is selected, a line number appears next to each line of code.  
  
> [!NOTE]  
>  These line numbers are not added to your code, and they do not print. They are for reference only.  
  
 **Enable single-click URL navigation**  
 When this check box is selected, the cursor changes to a pointing hand as it passes over a URL in the editor. You can click the URL to display the indicated page in your Web browser.  
  
 **Navigation bar**  
 This check box is unavailable.  
  
  
