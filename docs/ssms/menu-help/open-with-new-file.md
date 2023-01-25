---
description: "Open With (New File)"
title: "Open With (New File)"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: ui-reference
helpviewer_keywords: 
  - "Open With dialog box"
ms.assetid: 9531588c-e7ec-4049-9f9c-ee000c49c5de
author: "markingmyname"
ms.author: "maghan"
---
# Open With (New File)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
You can open a document in one or more editors by clicking **Open** on the **File** menu and then clicking **File**. In the **Open File** dialog box, select the file, click the **Open** arrow, and then click **Open With**. In the **Open With** dialog box, in the **Select a program to open** list, click the preferred program, and then click **Open**.  
  
## Options  
**Select a program to open**  
Lists the editors available in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio for the selected type of file. Choose the editor with which to open the document from the list displayed, or click **Add** to include a new editor in the list.  
  
**Open**  
Click **Open** to open the document in the selected editor.  
  
**Add**  
Click this button to add a program to the list under **Select a program to open**. You can either type the file path to the program in the **Program Name** field or browse to the program's location by clicking **Browse**. In **Friendly Name**, enter a program name to display in the list under **Select a program to open**.  
  
**Remove**  
To remove a program, select the program and then click **Remove**.  
  
**Set as Default**  
To specify a default editor (and language encoding options, if applicable) for the type of file selected, choose a program from the list under **Select a program to open** and then click **Set as Default**. The next time you open this type of file in SQL Server Management Studio, the document will open in the new default editor.  
  
> [!NOTE]  
> In the list of programs under **Select a program to open**, the name of the default editor for the type of file selected is followed by **(Default)**.  
  
## See Also  
[Associating File Extensions to a Code Editor](../scripting/associate-file-extensions-to-a-code-editor.md)