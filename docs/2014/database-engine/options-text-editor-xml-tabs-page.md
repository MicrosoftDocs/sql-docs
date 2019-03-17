---
title: "Options (Text Editor:XML:Tabs Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Text_Editor.XML.Tabs"
ms.assetid: 13bf5f8c-aba3-4c05-b8bb-eb475797c9bd
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor:XML:Tabs Page)
  This dialog box lets you change the tabbing behavior of the XML Editor, which is used to edit XML documents. To display these settings, click **Options** on the **Tools** menu, expand the **Text Editor** folder, expand the **XML** subfolder and then click **Tabs**.  
  
## Setting Options in Multiple Locations  
 Options for the XML Editor can also be set in the **All Languages General** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the XML Editor options using this dialog.  
  
## Indenting  
 **None**  
 When this option is selected, the new line created when you press ENTER is not indented. The cursor is placed at the first column of the new line.  
  
 **Block**  
 When this option is selected, the new line created when you press ENTER is automatically indented the same distance as the previous line.  
  
 **Smart**  
 When this option is selected, the new line created when you press ENTER is positioned according to the context. For example, after an opening brace ({), the included lines are automatically indented an extra tab stop. The matching closing brace (}) is realigned with its opening brace.  
  
## Tabs  
 **Tab size**  
 Sets the distance in spaces between tab stops. The default is four spaces.  
  
 **Indent size**  
 Sets the size in spaces of an automatic indentation. The default is four spaces. Tab characters, space characters, or both are inserted to fill the specified size.  
  
 **Insert spaces**  
 When this option is selected, indent operations insert only space characters, not tab characters. If **Indent size** is set to 5, for example, then five space characters are inserted whenever you press the TAB key or click the **Increase Indent** button on the toolbar in the main [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] window.  
  
 **Keep tabs**  
 When this option is selected, indent operations insert as many tab characters as possible. Each tab character fills the number of spaces specified in **Tab size**. If **Indent size** is not an even multiple of **Tab size**, space characters are added to fill in the difference.  
  
  
