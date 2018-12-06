---
title: "Associate File Extensions to a Code Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "file extensions [SQL Server]"
  - "associating file extensions [SQL Server]"
  - "Query Editor [SQL Server Management Studio], associating file extensions"
ms.assetid: 193630f4-93de-4950-8f36-68702531f925
author: MightyPen
ms.author: genemi
manager: craigg
---
# Associate File Extensions to a Code Editor
  Associating file extensions to a specific code editor allows you to open a file with the appropriate [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] code editor when you double-click a file in Windows Explorer. The extensions commonly used by [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], such as .sql and .mdx, are associated during installation. New file extensions must also be associated to [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] in the file system. You can use this feature to open files created with other editors, or to open files you have renamed, such as backups of .sql files that were renamed .bak.  
  
 There are two steps in the process. First associate the extension with [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then associate the extension with a specific code editor.  
  
### To associate a new file extension with SQL Server Management Studio  
  
1.  On the **Start** menu, point to **All Programs**, point to **Accessories**, and then click **Windows Explorer**.  
  
2.  In Windows Explorer, on the **Tools** menu, click **Folder Options**.  
  
3.  In the **Folder Options** dialog box, on the **File Types** tab, click **New**.  
  
4.  In the **Create New Extension** dialog box, in the **File Extension** box, type the new file extension that you want to associate, and then click **OK**. Do not start the extension with a period.  
  
5.  In the **Registered file** types box, click on your new extension, and then click **Change**.  
  
6.  In the **Open With** dialog box, click **SSMS - SQL Server Management Studio**, and then click **OK**.  
  
7.  Click **Close** to close the **Folder Options** dialog box, and then close Windows Explorer.  
  
### To associate a new file extension with a code editor in SQL Server Management Studio  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], from the **Tools** menu, click **Options**.  
  
2.  In the **Options** dialog box, click **Text Editor**, and then click **File Extension**.  
  
3.  In the **Extension** box, type your new file extension.  
  
4.  In the **Editor** box, click the code editor that you wish to use to open this file type, click **Add**, and then click **OK**.  
  
## See Also  
 [Ssms Utility](../../ssms/ssms-utility.md)  
  
  
