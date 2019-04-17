---
title: "Switch an Analysis Services database between ReadOnly and ReadWrite modes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "ReadOnly property"
  - "ReadWriteMode command"
  - "operations [Analysis Services - multidimensional data]"
ms.assetid: 4eff8181-08dd-4fad-b091-d400fc21a020
author: minewiskan
ms.author: owend
manager: craigg
---
# Switch an Analysis Services database between ReadOnly and ReadWrite modes
  There are often situations when a [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database administrator (dba) wants to change the read/write mode of a tabular or multidimensional database. These situations are often driven by business needs, such as sharing the database among a pool of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] servers for a better user experience.  
  
 A database mode can be switched in many ways. This document explains the following common scenarios:  
  
-   Interactively using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
-   Programmatically using AMO  
  
-   By script using XMLA  
  
## Procedures  
  
#### To switch the read/write mode of a database interactively using Management Studio  
  
1.  Locate the database to be switched in the left or right pane of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  Right-click the database and select **Properties**. Find the database folder and note the location. An empty database storage location indicates that the database folder is located in the server data folder.  
  
    > [!IMPORTANT]  
    >  As soon as the database is detached, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] can no longer help you obtain the database location.  
  
3.  Right-click the database and select **Detach...**  
  
4.  Assign a password to the database to be detached, and then click **OK** to execute the detach command.  
  
5.  Locate the **Databases** folder in the left or right pane of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
6.  Right-click the **Databases** folder and select **Attach...**  
  
7.  In the **folder** text box, type the original location of the database folder. Alternatively, you can use the browse button (**...**) to locate the database folder.  
  
8.  Select the read/write mode for the database.  
  
9. Type the password that was used in step 3 and click **OK** to execute the attach command.  
  
#### To switch the read/write mode to a database programmatically using AMO  
  
1.  In your C# application, adapt the following sample code and complete the indicated tasks.  
  
 `private void SwitchReadWrite(Server server, string dbName,`  
  
 `ReadWriteMode dbReadWriteMode)`  
  
 `{`  
  
 `if (server.Databases.ContainsName(dbName))`  
  
 `{`  
  
 `Database db;`  
  
 `string databaseLocation;`  
  
 `db = server.Databases[dbName];`  
  
 `databaseLocation = db.DbStorageLocation;`  
  
 `if (databaseLocation == null)`  
  
 `{`  
  
 `string dataDir = server.ServerProperties["DataDir"].Value;`  
  
 `String[] possibleFolders = Directory.GetDirectories(dataDir, string.Concat(dbName,"*"), SearchOption.TopDirectoryOnly);`  
  
 `if (possibleFolders.Length > 1)`  
  
 `{`  
  
 `List<String> sortedFolders = new List<string>(possibleFolders.Length);`  
  
 `sortedFolders.AddRange(possibleFolders);`  
  
 `sortedFolders.Sort();`  
  
 `databaseLocation = sortedFolders[sortedFolders.Count - 1];`  
  
 `}`  
  
 `else`  
  
 `{`  
  
 `databaseLocation = possibleFolders[0];`  
  
 `}`  
  
 `}`  
  
 `db.Detach();`  
  
 `server.Attach(databaseLocation, dbReadWriteMode);`  
  
 `}`  
  
 `}`  
  
1.  In your C# application, invoke `SwitchReadWrite()` with the necessary parameters.  
  
2.  Compile and execute your code to move the database.  
  
#### To switch the read/write mode to a database by script using XMLA  
  
1.  Locate the database to be switched in the left or right pane of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  Right-click the database and select **Properties**. Find the database folder and note the location. An empty database storage location indicates that the database folder is located in the server data folder.  
  
    > [!IMPORTANT]  
    >  As soon as the database is detached, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] can no longer help you obtain the database location.  
  
3.  Open a new XMLA tab in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
4.  Copy the following script template for XMLA:  
  
 `<Detach xmlns="https://schemas.microsoft.com/analysisservices/2003/engine">`  
  
 `<Object>`  
  
 `<DatabaseID>%dbName%</DatabaseID>`  
  
 `<Password>%password%</Password>`  
  
 `</Object>`  
  
 `</Detach>`  
  
1.  Replace `%dbName%` with the name of the database and `%password%` with the password. The % characters are part of the template and must be removed.  
  
2.  Execute the XMLA command.  
  
3.  Copy the following script template for XMLA in a new XMLA tab  
  
 `<Attach xmlns="https://schemas.microsoft.com/analysisservices/2003` `/engine` `">`  
  
 `<Folder>%dbFolder%</Folder>`  
  
 `<ReadWriteMode xmlns="https://schemas.microsoft.com/analysisservices/2008/engine/100">%ReadOnlyMode%</ReadWriteMode>`  
  
 `</Attach>`  
  
1.  Replace `%dbFolder%` with the complete UNC path of the database folder, `%ReadOnlyMode%` with the corresponding value `ReadOnly` or `ReadWrite`, and `%password%` with the password. The % characters are part of the template and must be removed.  
  
2.  Execute the XMLA command.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Server.Attach%2A>   
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Attach and Detach Analysis Services Databases](attach-and-detach-analysis-services-databases.md)   
 [Database Storage Location](database-storage-location.md)   
 [Database ReadWriteModes](database-readwritemodes.md)   
 [Attach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/attach-element)   
 [Detach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/detach-element)   
 [ReadWriteMode Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/readwritemode-element)   
 [DbStorageLocation Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/dbstoragelocation-element)  
  
  
