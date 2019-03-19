---
title: "Migrating MySQL Data into SQL Server - Azure SQL DB (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Migration, server side data migration"
  - "Data Migration,client side data migration"
ms.assetid: a6a7f4d6-68aa-4a38-93bf-53eba0d7dc82
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Migrating MySQL Data into SQL Server - Azure SQL DB (MySQLToSQL)
After you have successfully synchronized the converted objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you can migrate data from MySQL to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.  
  
> [!IMPORTANT]  
> If the engine being used is Server Side Data Migration Engine, then, before migrating data, you must install the SSMA for MySQL Extension Pack and the MySQL providers on the computer that is running SSMA. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA Components on SQL Server (MySQL to SQL)](https://msdn.microsoft.com/6772d0c5-258f-4d7b-afb0-b5f810e71af1)  
  
## Setting Migration Options  
Before migrating data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, review the project migration options in the **Project Settings** dialog box.  
  
-   By using this dialog box you can set options such as migration batch size, table locking, constraint checking, null value handling and identity value-handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](https://msdn.microsoft.com/2a3cba9e-cd54-4a8b-b858-8fc4cf2580d9).  
  
    For more information on **Extended Data Migration Settings**, see [Data Migration Settings](data-migration-settings-mysqltosql.md)  
  
-   The **Migration Engine** in the **Project Settings** dialog box, allows the user to perform the migration process using two types of data migration engines:  
  
    1.  Client Side Data Migration Engine  
  
    2.  Server Side Data Migration Engine  
  
**Client Side Data Migration:**  
  
-   To initiate data-migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
-   In **Project Settings**, the **Client Side Data Migration Engine** option is set.  
  
    > [!NOTE]  
    > The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.  
  
**Server Side Data Migration:**  
  
-   During the Server side data migration, the engine resides on the target database. It is installed through the extension pack. For more information on how to install the extension pack, see [Installing SSMA Components on SQL Server (MySQL to SQL)](https://msdn.microsoft.com/6772d0c5-258f-4d7b-afb0-b5f810e71af1)  
  
-   To initiate migration on the server side, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
> [!IMPORTANT]  
> **Client Side Data Migration** option is available for SQL Azure only.  
  
## Migrating Data to SQL Server or SQL Azure  
Migrating data is a bulk-load operation that moves rows of data from MySQL tables into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables in transactions. The number of rows loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in each transaction is configured in the project settings.  
  
To view migration messages, make sure that the Output pane is visible. Otherwise, from the **View** menu, select **Output**.  
  
**To migrate data**  
  
1.  Verify the following:  
  
    -   The MySQL providers are installed on the computer that is running SSMA.  
  
    -   You have synchronized the converted objects with the target database (SQL Server / SQL Azure).  
  
2.  In MySQL Metadata Explorer, select the objects that contain the data that you want to migrate:  
  
    -   To migrate data for all schemas, select the check box next to **Schemas**.  
  
    -   To migrate data or omit individual tables, first expand the schema, expand **Tables**, and then select or clear the check box next to the table.  
  
3.  To migrate data, two cases arise:  
  
    **Client Side Data Migration:**  
  
    -   For performing **Client Side Data Migration**, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
    **Server Side Data Migration:**  
  
    -   Before performing data migration on the server side, ensure:  
  
        1.  The SSMA for MySQL Extension Pack is installed on the instance of SQL Server.  
  
        2.  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is running on the instance of SQL Server  
  
    -   For performing **Server Side Data Migration**, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog box.  
  
4.  Right-click **Schemas** in MySQL Metadata Explorer, and then click **Migrate Data**. You can also migrate data for individual objects or categories of objects: Right-click the object or its parent folder; select the **Migrate Data** option.  
  
    > [!NOTE]  
    > If the SSMA for MySQL Extension Pack is not installed on the instance of SQL Server, and if **Server Side Data Migration Engine** is selected, then while migrating the data to the target database, the following error is encountered: 'SSMA Data Migration components were not found on SQL Server, server-side data migration will not be possible. Please check if Extension Pack is installed correctly'. Click **Cancel** to terminate the data migration.  
  
5.  In the **Connect to MySQL** dialog box, enter the connection credentials, and then click **Connect**. For more information on connecting to MySQL, see [Connect to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connect-to-mysql-mysqltosql.md)  
  
    If the target database is SQL Server, then, enter the connection credentials in the **Connect to SQL Server** dialog box, and click **Connect**. For more information on connecting to SQL Server, see [Connect to SQL Server](https://msdn.microsoft.com/bb8c4bde-cfc2-4636-92ae-5dd24abe9536)  
  
    If the target database is SQL Azure, then enter the connection credentials in the **Connect to SQL Azure** dialog box, and click **Connect**. For more information on connecting to SQL Azure, see [Connect to Azure SQL DB &#40;MySQLToSQL&#41;](../../ssma/mysql/connect-to-azure-sql-db-mysqltosql.md)  
  
    Messages will appear in the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data did not migrate, click the row that contains the errors, and then click **Details**. When you are finished with the report, click **Close**. For more information on Data Migration Report, see [Data Migration Report (SSMA Common)](https://msdn.microsoft.com/bbfb9d88-5a98-4980-8d19-c5d78bd0d241)  
  
> [!NOTE]  
> When SQL Express edition is used as the target database, only client side data migration is allowed and server side data migration is not supported.  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
