---
title: "Associate File Extensions to a Code Editor"
description: Learn how to associate a file extension to a specific code editor so that when you double-click a file with the extension it is opened by the associated editor.
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "file extensions [SQL Server]"
  - "associating file extensions [SQL Server]"
  - "Query Editor [SQL Server Management Studio], associating file extensions"
ms.assetid: 193630f4-93de-4950-8f36-68702531f925
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Associate File Extensions to a Code Editor

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Associating file extensions to a specific code editor allows you to open a file with the appropriate SQL Server Management Studio code editor when you double-click a file in Windows Explorer. The extensions commonly used by [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], such as .sql and .mdx, are associated during installation. New file extensions must also be associated to [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] in the file system. You can use this feature to open files created with other editors, or to open files you have renamed, such as backups of .sql files that were renamed .bak.  
  
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
  
1.  In SQL Server Management Studio, from the **Tools** menu, click **Options**.  
  
2.  In the **Options** dialog box, click **Text Editor**, and then click **File Extension**.  
  
3.  In the **Extension** box, type your new file extension.  
  
4.  In the **Editor** box, click the code editor that you wish to use to open this file type, click **Add**, and then click **OK**.  
  
## See Also  
 [Ssms Utility](../ssms-utility.md)  
  
