---
title: "Migrating DB2 Data into SQL Server (DB2ToSQL)"
description: Learn how to migrate data from a DB2 database to SQL Server or Azure SQL Database, after you synchronize the converted objects.
author: cpichuka
ms.author: cpichuka
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.db2.general.f1"
---
# Migrating DB2 Data into SQL Server (DB2ToSQL)
After you have successfully synchronized the converted objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can migrate data from DB2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
  
## Setting Migration Options  
Before migrating data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], review the project migration options in the **Project Settings** dialog box.  
  
-   By using this dialog box you can set options such as migration batch size, table locking, constraint checking, null value handling, and identity value handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](./project-settings-migration-db2tosql.md).  
  
  
**Client Side Data Migration:**  
  
-   To initiate data-migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
-   In **Project Settings**, the **Client Side Data Migration Engine** option is set.  
  
    > [!NOTE]  
    > The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.  
  
  
## Migrating Data to SQL Server  
Migrating data is a bulk-load operation that moves rows of data from DB2 tables into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables in transactions. The number of rows loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in each transaction is configured in the project settings.  
  
To view migration messages, make sure that the Output pane is visible. Otherwise, from the **View** menu, select **Output**.  
  
**To migrate data**  
  
1.  Verify the following:  
  
    -   The DB2 providers are installed on the computer that is running SSMA.  
  
    -   You have synchronized the converted objects with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
2.  In DB2 Metadata Explorer, select the objects that contain the data that you want to migrate:  
  
    -   To migrate data for all schemas, select the check box next to **Schemas**.  
  
    -   To migrate data or omit individual tables, first expand the schema, expand **Tables**, and then select or clear the check box next to the table.  
  
  
3.  Right-click **Schemas** in DB2 Metadata Explorer, and then click **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder; select the **Migrate Data** option.  
  
  
5.  In the **Connect to DB2** dialog box, enter the connection credentials, and then click **Connect**. For more information on connecting to DB2, see [Connecting to DB2 Database &#40;DB2ToSQL&#41;](../../ssma/db2/connecting-to-db2-database-db2tosql.md)  
  
    For connecting to the target database [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enter the connection credentials in the **Connect to SQL Server** dialog box, and click **Connect**. For more information on connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Connecting to SQL Server](./connecting-to-sql-server-db2tosql.md)  
  
    Messages will appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data did not migrate, click the row that contains the errors, and then click **Details**. When you are finished with the report, click **Close**. For more information on Data Migration Report, see [Data Migration Report (SSMA Common)](../sybase/data-migration-report-sybasetosql.md)  
  

  
## See Also  
[Migrating DB2 Data into SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-data-into-sql-server-db2tosql.md)  
