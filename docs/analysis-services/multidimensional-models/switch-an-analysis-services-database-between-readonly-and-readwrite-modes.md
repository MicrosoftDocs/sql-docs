---
title: "Switch an Analysis Services database between ReadOnly and ReadWrite modes | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Switch an Analysis Services database between ReadOnly and ReadWrite modes
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database administrators can change the read/write mode of a Tabular or Multidimensional database as part of larger effort that distributes a query workload among multiple query-only servers.  
  
 A database mode can be switched in several ways. This document explains the following common scenarios:  
  
-   Interactively using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
-   Programmatically using AMO  
  
-   Script using XMLA or TMSL  
  
## Switch the read/write mode of a database interactively using Management Studio  
  
1.  In Object Explorer, right-click  the database and select **Properties**.  
  
     Note the location. An empty database storage location indicates that the database folder is located in the server data folder.  
  
2.  Right-click the database and select **Detach...**  
  
3.  Assign a password to the database to be detached, and then click **OK** to execute the detach command.  
  
4.  In Object Explorer, right-click the **Databases** folder and select **Attach...**  
  
5.  In the **folder** text box, type the original location of the database folder. Alternatively, you can use the browse button (**...**) to locate the database folder.  
  
6.  Select the read/write mode for the database.  
  
7.  Type the password and click **OK** to execute the attach command.  
  
## Switch the read/write mode to a database programmatically using AMO  
 In your C# application, invoke `SwitchReadWrite()` with the necessary parameters. Compile and execute your code to move the database.  
  
```  
private void SwitchReadWrite(Server server, string dbName, ReadWriteMode dbReadWriteMode)  
{  
    if (server.Databases.ContainsName(dbName))  
    {  
        Database db;  
        string databaseLocation;  
        db = server.Databases[dbName];  
        databaseLocation = db.DbStorageLocation;  
  
              if (databaseLocation == null)  
            {  
                 string dataDir = server.ServerProperties["DataDir"].Value;  
                 string dataDir = server.ServerProperties["DataDir"].Value;  
                 string dataDir = server.ServerProperties["DataDir"].Value;  
  
    String[] possibleFolders = Directory.GetDirectories(dataDir, string.Concat(dbName,"*"), SearchOption.TopDirectoryOnly);  
  
   if (possibleFolders.Length > 1)  
         {  
         List<String> sortedFolders = new List<string>(possibleFolders.Length);  
         sortedFolders.AddRange(possibleFolders);  
         sortedFolders.Sort();  
         databaseLocation = sortedFolders[sortedFolders.Count - 1];  
         }  
         else  
         {  
         databaseLocation = possibleFolders[0];  
          }  
        }  
    db.Detach();  
    server.Attach(databaseLocation, dbReadWriteMode);  
    }  
}  
  
```  
  
## Switch the read/write mode to a database by script using XMLA  
 The following instructions apply to Multidimensional databases and Tabular databases at compatibility mode 1050, 1100, or 1103.  
  
1.  In Object Explorer, right-click  the database and select **Properties**.  
  
     Note the location. An empty database storage location indicates that the database folder is located in the server data folder.  
  
2.  Right-click the database and select **Detach...**  
  
3.  Open a new XMLA tab in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
4.  Copy the following script template for XMLA:  
  
    ```  
    <Detach xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
       <Object>  
          <DatabaseID>%dbName%</DatabaseID>  
          <Password>%password%</Password>  
       </Object>  
    </Detach>  
    ```  
  
5.  Replace `%dbName%` with the name of the database and `%password%` with the password. The % characters are part of the template and must be removed.  
  
6.  Execute the XMLA command.  
  
7.  Copy the following script template for XMLA in a new XMLA tab  
  
    ```  
    <Attach xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
       <Folder>%dbFolder%</Folder>  
       <ReadWriteMode xmlns="http://schemas.microsoft.com/analysisservices/2008/engine/100">%ReadOnlyMode%</ReadWriteMode>  
    </Attach>  
    ```  
  
8.  Replace `%dbFolder%` with the complete UNC path of the database folder, `%ReadOnlyMode%` with the corresponding value **ReadOnly** or **ReadWrite**, and `%password%` with the password. The % characters are part of the template and must be removed.  
  
9. Execute the XMLA command.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [High availability and Scalability in Analysis Services](../../analysis-services/instances/high-availability-and-scalability-in-analysis-services.md)   
 [Attach and Detach Analysis Services Databases](../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)   
 [Database Storage Location](../../analysis-services/multidimensional-models/database-storage-location.md)   
 [Database ReadWriteModes](../../analysis-services/multidimensional-models/database-readwritemodes.md)   
 [Attach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/attach-element)   
 [Detach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/detach-element)   
 [ReadWriteMode Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/readwritemode-element)   
 [DbStorageLocation Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/dbstoragelocation-element)  
  
  
