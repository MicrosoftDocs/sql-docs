---
title: "Options (Text Editor - Transact-SQL - Tabs Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Text_Editor.SQL.Tabs"
dev_langs: 
  - "TSQL"
ms.assetid: a4499784-67f7-46ef-9f7c-2d0fdd117a52
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - Transact-SQL - Tabs Page)
  Use this dialog to change the tabbing behavior of the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor, which is used to program [!INCLUDE[tsql](../includes/tsql-md.md)] scripts. To display these settings, click **Options** on the **Tools** menu, expand the **Text Editor** folder, expand the **Transact-SQL** subfolder and then click **Tabs**.  
  
## Setting Options in Multiple Locations  
 Options for the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor can also be set in the **All Languages Tabs** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor options using this dialog.  
  
## Indenting  
 **None**  
 When this option is selected, the new line created when you press ENTER is not indented. The cursor is placed at the first column of the new line.  
  
 **Block**  
 When this option is selected, the new line created when you press ENTER is automatically indented the same distance as the previous line.  
  
 **Smart**  
 This option is unavailable.  
  
## Tabs  
 **Tab size**  
 Sets the distance in spaces between tab stops. The default is four spaces.  
  
 **Indent size**  
 Sets the size in spaces of an automatic indentation. The default is four spaces. Tab characters, space characters, or both are inserted to fill the specified size.  
  
 **Insert spaces**  
 When this option is selected, indent operations insert only space characters, not tab characters. If **Indent size** is set to 5, for example, then five space characters are inserted whenever you press the TAB key or click the **Increase Indent** button on the toolbar in the main [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] window.  
  
 **Keep tabs**  
 When this option is selected, indent operations insert as many tab characters as possible. Each tab character fills the number of spaces specified in **Tab size**. If **Indent size** is not an even multiple of **Tab size**, space characters are added to fill in the difference.  
  
  
