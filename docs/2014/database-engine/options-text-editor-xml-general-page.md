---
title: "Options (Text Editor - XML - General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 46a9f913-d0b9-40ff-b382-9bbdec7461a6
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - XML - General Page)
  Use this dialog to change the general editing behavior of the XML Editor, which is used to edit XML documents. To display these settings, click **Options** on the **Tools** menu, expand the **XML** subfolder, and then click **General**.  
  
## Setting Options in Multiple Locations  
 Options for the XML Editor can also be set in the **All Languages General** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the XML Editor options using this dialog.  
  
## Statement Completion  
 **Auto list members**  
 When this check box is selected, lists of available members, properties, values, or methods are displayed as you type in the editor. Choose any item from the pop-up list to insert it into your code.  
  
 **Hide advanced members**  
 This check box is unavailable.  
  
 **Parameter information**  
 When this check box is selected, the complete syntax for the current declaration or procedure is displayed to the left of the insertion point in the editor, with all of its available parameters. The next parameter you can assign is displayed in bold.  
  
## Settings  
 **Enable virtual space**  
 When this check box is selected, spaces are inserted at the end of each line of code. Select this check box to position comments at a consistent point next to your code.  
  
 **Word wrap**  
 When this check box is selected, any portion of a line that extends horizontally beyond the viewable editor area is automatically displayed on the next line. Selecting this check box enables the **Show visual glyphs for word wrap** check box.  
  
 **Show visual glyphs for word wrap**  
 When this check box is selected, a return-arrow indicator is displayed where a long line wraps onto a second line. Clear this check box if you prefer not to display these indicators.  
  
> [!NOTE]  
>  These reminder arrows are not added to your code, and they do not print. They are for reference only.  
  
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
  
  
