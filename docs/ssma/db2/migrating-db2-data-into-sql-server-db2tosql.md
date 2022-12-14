---
title: "Migrating DB2 Data into SQL Server (DB2ToSQL) | Microsoft Docs"
description: Learn how to migrate data from a DB2 database to SQL Server or Azure SQL Database, after you synchronize the converted objects.
ms.service: sql
ms.custom:
  - intro-migration
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 86cbd39f-6dac-409a-9ce1-7dd54403f84b
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.db2.general.f1"
---
# Migrating DB2 Data into SQL Server (DB2ToSQL)
After you have successfully synchronized the converted objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can migrate data from DB2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
> If the engine being used is Server Side Data Migration Engine, then, before you can migrate data, you must install the SSMA for DB2 Extension Pack and the DB2 providers on the computer that is running SSMA. The SQL Server Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA Components on SQL Server](./installing-ssma-components-on-sql-server-db2tosql.md)  
  
## Setting Migration Options  
Before migrating data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], review the project migration options in the **Project Settings** dialog box.  
  
-   By using this dialog box you can set options such as migration batch size, table locking, constraint checking, null value handling, and identity value handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](./project-settings-migration-db2tosql.md).  
  
-   The **Migration Engine** in the **Project Settings** dialog box, allows the user to perform the migration process using two types of data migration engines:  
  
    1.  Client Side Data Migration Engine  
  
    2.  Server Side Data Migration Engine  
  
**Client Side Data Migration:**  
  
-   To initiate data-migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
-   In **Project Settings**, the **Client Side Data Migration Engine** option is set.  
  
    > [!NOTE]  
    > The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.  
  
**Server Side Data Migration:**  
  
-   During the Server side data migration, the engine resides on the target database. It is installed through the extension pack. For more information on how to install the extension pack, see [Installing SSMA Components on SQL Server](./installing-ssma-components-on-sql-server-db2tosql.md)  
  
-   To initiate migration on the server side, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
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
  
3.  To migrate data, two cases arise:  
  
    **Client Side Data Migration:**  
  
    -   For performing **Client Side Data Migration**, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
    **Server Side Data Migration:**  
  
    -   Before performing data migration on the server side, ensure:  
  
        1.  The SSMA for DB2 Extension Pack is installed on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
        2.  The SQL Server Agent service is running on the instance of SQL Server.  
  
    -   For performing **Server Side Data Migration**, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
4.  Right-click **Schemas** in DB2 Metadata Explorer, and then click **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder; select the **Migrate Data** option.  
  
    > [!NOTE]  
    > If the SSMA for DB2 Extension Pack is not installed on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and if **Server Side Data Migration Engine** is selected, then while migrating the data to the target database, the following error is encountered: 'SSMA Data Migration components were not found on SQL Server, server-side data migration will not be possible. Please check if Extension Pack is installed correctly'. Click **Cancel** to terminate the data migration.  
  
5.  In the **Connect to DB2** dialog box, enter the connection credentials, and then click **Connect**. For more information on connecting to DB2, see [Connecting to DB2 Database &#40;DB2ToSQL&#41;](../../ssma/db2/connecting-to-db2-database-db2tosql.md)  
  
    For connecting to the target database [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], enter the connection credentials in the **Connect to SQL Server** dialog box, and click **Connect**. For more information on connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Connecting to SQL Server](./connecting-to-sql-server-db2tosql.md)  
  
    Messages will appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data did not migrate, click the row that contains the errors, and then click **Details**. When you are finished with the report, click **Close**. For more information on Data Migration Report, see [Data Migration Report (SSMA Common)](../sybase/data-migration-report-sybasetosql.md)  
  
> [!NOTE]  
> When SQL Express edition is used as the target database, only client side data migration is allowed and server side data migration is not supported.  
  
## See Also  
[Migrating DB2 Data into SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-data-into-sql-server-db2tosql.md)  
