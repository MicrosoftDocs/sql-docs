---
title: "Manage Code Formatting | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "indenting code [SQL Server]"
  - "displaying URLs"
  - "code formatting [SQL Server Management Studio]"
  - "collapsing text"
  - "formats [SQL Server], code formatting in SQL Server Management Studio"
  - "hiding text"
  - "formats [SQL Server]"
  - "text [SQL Server], code formats"
  - "automatic indentation"
  - "converting text to lower case"
  - "Query Editor [SQL Server Management Studio], managing code formats"
  - "URL displayed in code [SQL Server Management Studio]"
  - "converting text to upper case"
  - "text [SQL Server]"
  - "unindenting code"
ms.assetid: ddbac4d2-6bdc-4467-a352-e869ec880eed
author: MightyPen
ms.author: genemi
manager: craigg
---
# Manage Code Formatting
  With the Editor you can format your code with indenting, hidden text, URLs, and so forth. You can also auto-format your code as you type by using Smart Indenting.  
  
## Indenting  
 You can choose three different styles of text indenting. You can also specify how many spaces compose a single indentation or tab, and whether the Editor uses tabs or space characters when indenting.  
  
#### To choose an indenting style  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Click **Text Editor**.  
  
3.  Click the folder, and select **All Languages** to set indenting for all languages.  
  
4.  Click **Tabs**.  
  
5.  Click one of the following options:  
  
    -   **None**. The cursor goes to the beginning of the next line.  
  
    -   **Block**. The cursor aligns the next line with the previous line.  
  
    -   **Smart** (Default). The language service determines the appropriate indenting style to use.  
  
    > [!NOTE]  
    >  Some languages do not offer all three indenting options.  
  
#### To change indent tab settings  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Click **Text Editor**.  
  
3.  Select the folder for **All Languages** to set indenting for all languages.  
  
4.  Click **Tabs**.  
  
5.  To specify tab characters for tab and indent operations, click **Keep tabs**. To specify space characters, select **Insert spaces**.  
  
     If you select **Insert Spaces**, enter the number of space characters each tab or indent represents under **Tab Size** or **Indent Size**, respectively.  
  
#### To indent code  
  
1.  Select the text you want to indent.  
  
2.  Press TAB, or click the **Indent** button on the Standard toolbar.  
  
#### To unindent code  
  
1.  Select the text you want to unindent.  
  
2.  Press SHIFT+TAB, or click the **Unindent** button on the Standard toolbar.  
  
#### To automatically indent all of your code  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Click **Text Editor**.  
  
3.  Click **All Languages**.  
  
4.  Click **Tabs**.  
  
5.  Click **Smart**.  
  
> [!NOTE]  
>  The **Smart** option is not available for some languages.  
  
#### To convert white space to tabs  
  
1.  Select the text whose white space you want to convert to tabs.  
  
2.  On the **Edit** menu, point to **Advanced**, and click **Tabify Selection**.  
  
#### To convert tabs to spaces  
  
1.  Select the text whose tabs you want to convert to spaces.  
  
2.  On the **Edit** menu, point to **Advanced**, and click **Untabify Selection**.  
  
 The behavior of these commands depends on the tab settings in the **Options** dialog box. For example, if the tab setting is 4, **Tabify Selection** creates a tab for every 4 contiguous spaces, and **Untabify Selection** creates 4 spaces for every tab.  
  
## Converting Text to Upper and Lower Case  
 You can use commands to convert text to all uppercase or lower case.  
  
#### To switch text to upper or lower case  
  
1.  Select the text you want to convert.  
  
2.  To convert text to uppercase, press CTRL+SHIFT+U, or click **Make Uppercase** on the **Advanced** submenu of the **Edit** menu.  
  
3.  To convert text to lowercase, press CTRL+SHIFT+L, or click **Make Lowercase** on the **Advanced** submenu of the **Edit** menu.  
  
> [!NOTE]  
>  For a complete list of keyboard shortcut keys, see [SQL Server Management Studio Keyboard Shortcuts](../../ssms/sql-server-management-studio-keyboard-shortcuts.md).  
  
## Displaying and Linking to URLs  
 You can create and display clickable URLs in your code. By default, the URLs:  
  
-   Are underlined.  
  
-   Change the mouse pointer to a hand when you move over them.  
  
-   Open the URL when clicked, if the URL is valid.  
  
#### To display a clickable URL  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Click **Text Editor**.  
  
3.  To change the option for only one language, click that language folder and then click **General**. To change the option for all languages, click **All Languages** and then click **General**.  
  
4.  Select **Enable single-click URL navigation**.  
  
  
