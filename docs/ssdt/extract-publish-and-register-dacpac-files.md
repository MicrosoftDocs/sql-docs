---
title: "Extract, Publish, and Register .dacpac Files | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.DacTableChooser"
  - "sql.data.tools.DacPublishDialog"
  - "sql.data.tools.DacPropertiesDialog"
  - "sql.data.tools.publishdacproject"
  - "sql.data.tools.DacExtractDialog"
ms.assetid: ed900f93-d3df-40f5-8e62-4d722595e041
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Extract, Publish, and Register .dacpac Files
This topic describes four procedures that you can perform by right clicking a connected database in SQL Server Object Explorer:  
  
-   Publish Data-tier Application  
  
-   Extract Data-tier Application  
  
-   Register Data-tier Application  
  
-   Unregister Data-tier Application  
  
## Publish Data-tier Application  
You can publish a .dacpac to a database. The publish action incrementally updates a database schema to match the schema of a source .dacpac file. If the database does not exist on the server, the publish operation creates it.  
  
This action is also available by selecting the Databases node.  
  
Select the .dacpac file. After you specify a .dacpac file, the **DAC Properties** button is enabled. The **DAC Properties** dialog box displays information about the .dacpac file.  
  
Specify the connection string to the database server, and then specify the database name, if the database name is not in the connection string.  
  
The **Publish** and **Generate Script** buttons are now enabled. If you generate a script, it opens in a document window and can be saved as required. If you choose to publish directly to the database, a summary of the update and the actual script used can be viewed from the **Data Tools Operations** window.  
  
When checked, the **Register as a Data-tier Application** check box causes the resulting database to be registered as a data-tier application in the server metadata. If the database to which you are publishing is registered, you can cause publishing to fail if the schema of the database is different from its current registered dacpac.  
  
Additional publishing configuration is available in the **Advanced Publish Settings** dialog box, which you can access by clicking the **Advanced** button.  
  
## Extract Data-tier Application  
You can extract a .dacpac from a database. Extract creates a database snapshot file (.dacpac) from a live SQL Server or Windows Azure SQL Database that might contain data from user tables, in addition to the database schema.  
  
Specify the .dacpac file to create. The **DAC Properties** button displays the **DAC Properties** dialog box, which lets you specify properties of the .dacpac file.  
  
Modify, as needed, the configuration of the extract process. In the **Extract Settings** section, you can choose to extract schema only or to extract schema and include table data. If you choose to extract schema and data, you can select the tables for which you would like to extract data.  
  
## Register Data-tier Application  
You can register a database as a data-tier application (DAC) within the instance. This stores a representation of the current state of the database schema in system metadata.  
  
In the **Register Data-tier Application** dialog box, specify the properties of the registered DAC.  
  
## Unregister Data-tier Application  
Unregistering allows you to remove the metadata for a registered data-tier application from the instance. Unregistering does not delete the registered database.  
  
## See Also  
[Connected Database Development](../ssdt/connected-database-development.md)  
  
