---
title: "Options (Text Editor - Plain Text - Tabs Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Text_Editor.Plain_Text.Tabs"
ms.assetid: 07d82d10-bca9-4b37-abbb-58ef9bfb264b
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - Plain Text - Tabs Page)
  Use this dialog to change the tabbing behavior of the Text Editor, which is used to edit a document not associated with a particular development language. To display these settings, click **Options** on the **Tools** menu, expand **Text Editor**, expand **Plain Text**, and then click **Tabs**.  
  
## Setting Options in Multiple Locations  
 Options for the Plain Text Editor can also be set in the **All Languages General** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the Plain Text Editor options using this dialog.  
  
## Indenting  
 **None**  
 Do not indent the new line created when you press ENTER. The cursor is placed at the first column of the new line.  
  
 **Block**  
 Indent the new line created when you press ENTER the same distance as the previous line was indented.  
  
 **Smart**  
 The plain text editor does not support this type of formatting.  
  
## Tabs  
 **Tab size**  
 Set the distance in spaces between tab stops. The default is four spaces.  
  
 **Indent size**  
 Set the size in spaces of an automatic indentation. The default is four spaces. Tab characters, space characters, or both will be inserted to fill the specified size.  
  
 **Insert spaces**  
 Insert only space characters, not tab characters, when indenting. If **Indent size** is set to 5, for example, five space characters are inserted whenever you press the TAB key or the **Increase Indent** button on the **Formatting** toolbar.  
  
 **Keep tabs**  
 Insert as many tab characters as possible when indenting. Each tab character fills the number of spaces specified in **Tab size**. If **Indent size** is not an even multiple of the **Tab size**, space characters are added to fill in the difference.  
  
  
