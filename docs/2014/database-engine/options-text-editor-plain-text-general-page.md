---
title: "Options (Text Editor - Plain Text - General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 53bfa594-ba36-4c9c-8dd5-4c2dcce7d2dc
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor - Plain Text - General Page)
  Use this dialog box to change the general editing behavior of the Text Editor, which is used to edit a document not associated with a particular development language. To display these settings, click **Options** on the **Tools** menu, expand **Text Editor**, expand **Plain Text,** and then click **General**.  
  
## Setting Options in Multiple Locations  
 Options for the Plain Text Editor can also be set in the **All Languages General** dialog. If you use the **All Languages** dialogs to set different options for the other [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, such as the DMX or MDX editors, you must reset the Plain Text Editor options using this dialog.  
  
## Statement Completion  
 **Auto list members**  
 The plain text editor does not support this feature.  
  
 **Hide advanced members**  
 The plain text editor does not support this feature.  
  
 **Parameter information**  
 The plain text editor does not support this feature.  
  
## Settings  
 **Enable virtual space**  
 Insert spaces at the end of each line of text. Select this check box to position comments at a consistent point next to your text.  
  
 **Word wrap**  
 Display any portion of a line that extends horizontally beyond the viewable editor area on the next line. Selecting this check box enables the **Show visual glyphs for word wrap** option.  
  
 **Show visual glyphs for word wrap**  
 Display a return-arrow indicator where a long line wraps onto a second line. Clear this check box if you prefer not to display these indicators.  
  
> [!NOTE]  
>  These reminder arrows are not added to your code, and they do not print. They are for reference only.  
  
 **Apply Cut/Copy commands to blank lines when there is no selection**  
 Set the behavior of the editor when you place the insertion point on a blank line, select nothing, and then click **Copy** or **Cut**.  
  
 When this check box is selected, the blank line is copied or cut. If you then click **Paste**, a new, blank line is inserted.  
  
 When this check box is cleared, nothing is copied or cut. If you then click **Paste**, the content most recently copied is pasted. If nothing has been copied, nothing is pasted.  
  
 This setting has no effect on **Copy** or **Cut** when a line is not blank. If nothing is selected, the entire line is copied or cut. If you then click **Paste**, the text of the entire line and its end line character are pasted.  
  
## Display  
 **Line numbers**  
 Display a line number next to each line of text.  
  
> [!NOTE]  
>  These line numbers are not added to your text, and they do not print. They are for reference only.  
  
 **Enable single-click URL navigation**  
 Change the cursor to a pointing hand as it passes over a URL in the editor. You can click the URL to display the indicated page in your Web browser.  
  
 **Navigation bar**  
 This check box is unavailable.  
  
  
