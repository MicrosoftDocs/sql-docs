---
title: "Options (Text Editor - All Languages -Tabs Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: bd715d6b-f873-41d4-aa10-57b7098b61cc
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - All Languages -Tabs Page)
  Use this dialog box to set the tabbing behavior in all five of the editors in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. To display these options, click **Options** on the **Tools** menu. Select the **Text Editor** folder, expand the **All Languages** folder and then click **Tabs**.  
  
## Tabbing Options by Editor  
 You must use the **All Languages** dialogs to set options for the DMX, MDX, and SQL Server Compact editors. Options set here are also applied to the Plain Text, Transact-SQL, and XML editors, but you can set options separately for those editors by expanding the subfolders for those languages and selecting their option pages. Some languages do not support all of the tabbing options.  
  
> [!CAUTION]  
>  If you set an option using this dialog, but want a different setting for the Plain Text, Transact-SQL, or XML editors, you must set the options for those editors after applying your selections in the **All Languages** dialog.  
  
 The message "The indentation (or tab) settings for individual text formats conflict with each other" is displayed when different settings have been selected for particular editors. For example, this reminder is displayed if the **Block indenting** option is selected for **Plain Text**, but **None** is selected for **XML**.  
  
## Indenting  
 **None**  
 When this option is selected, the new line created when you press ENTER is not indented. The cursor is placed at the first column of the new line.  
  
 **Block**  
 When this option is selected, the new line created when you press ENTER is automatically indented the same distance as the previous line was indented.  
  
 **Smart**  
 When this option is selected, the new line created when you press ENTER is positioned according to the context.  
  
## Tabs  
 **Tab size**  
 Sets the distance in spaces between tab stops. The default is four spaces.  
  
 **Indent size**  
 Sets the size in spaces of an automatic indentation. The default is four spaces. Tab characters, space characters, or both will be inserted to fill the specified size.  
  
 **Insert spaces**  
 When this option is selected, indent operations insert only space characters, not tab characters. If **Indent size** is set to 5, for example, then five space characters are inserted whenever you press the TAB key or click the **Increase Indent** button on the toolbar of the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] main window.  
  
 **Keep tabs**  
 When this option is selected, indent operations insert as many tab characters as possible. Each tab character fills the number of spaces specified in **Tab size**. If **Indent size** is not an even multiple of **Tab size**, space characters are added to fill in the difference.  
  
  
