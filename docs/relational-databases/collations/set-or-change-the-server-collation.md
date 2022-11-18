---
description: "Set or Change the Server Collation"
title: "Set or Change the Server Collation"
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "server collations [SQL Server]"
  - "collations [SQL Server], server"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Set or change the server collation

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  The server collation acts as the default collation for all system databases that are installed with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and also any newly created user databases. 

  You should carefully consider the server-level collation, because it may affect:
 - Sorting and comparison rules in `=`, `JOIN`, `ORDER BY` and other operators that compare textual data.
 - Collation of the `CHAR`, `VARCHAR`, `NCHAR`, and `NVARCHAR` columns in system views, system functions, and the objects in TempDB (for example, temporary tables).
 - Names of the variables, cursors, and `GOTO` labels. Variables @pi and @PI are considered as different variables if the server-level collation is case-sensitive, and the same variables if the server-level collation is case-insensitive.
  
## Server collation in SQL Server

  The server collation is specified during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. The default server-level collation is based upon the locale of the operating system. 
  
  For example, the default collation for systems using US English (en-US) is **SQL_Latin1_General_CP1_CI_AS**. For more information, including the list of OS locale to default collation mappings, see the "Server-level collations" section of [Collation and Unicode Support](collation-and-unicode-support.md#Server-level-collations).

> [!NOTE]  
> The server-level collation for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express LocalDB is **SQL_Latin1_General_CP1_CI_AS** and cannot be changed, either during or after installation.  

## Change the server collation in SQL Server
 
 Changing the default collation for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be a complex operation.

> [!NOTE]  
> Instead of changing the default collation of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can specify a default collation for each new database you create via the `COLLATE` clause of the `CREATE DATABASE` and `ALTER DATABASE` statements. For more information, see [Set or Change the Database Collation](set-or-change-the-database-collation.md). 

 Changing the instance collation involves the following steps:  
  
- Make sure you have all the information or scripts needed to re-create your user databases and all the objects in them.  
  
- Export all your data using a tool such as the [bcp Utility](../../tools/bcp-utility.md). For more information, see [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md).  
  
- Drop all the user databases.  
  
- Rebuild the `master` database specifying the new collation in the SQLCOLLATION property of the **setup** command. For example:  
  
    ```console  
    Setup /QUIET /ACTION=REBUILDDATABASE /INSTANCENAME=InstanceName
    /SQLSYSADMINACCOUNTS=accounts /[ SAPWD= StrongPassword ]
    /SQLCOLLATION=CollationName  
    ```  
  
     For more information, see [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md).  
  
- Create all the databases and all the objects in them.  
  
- Import all your data.  

  
## Setting the server collation in Managed Instance

Server-level collation in Azure SQL Managed Instance can be specified when the instance is created and cannot be changed later. You can set server-level collation via [Azure portal](/azure/sql-database/sql-database-managed-instance-get-started#create-a-managed-instance) or [PowerShell and Resource Manager template](/azure/sql-database/scripts/sql-managed-instance-create-powershell-azure-resource-manager-template) while you are creating the instance. Default server-level collation is **SQL_Latin1_General_CP1_CI_AS**. 

If you are migrating databases from SQL Server to Managed Instance, check the server collation in the source SQL Server using `SERVERPROPERTY(N'Collation')` function and create a Managed Instance that matches the collation of your SQL Server. Migrating a database from SQL Server to Managed Instance with the server-level collations that are not matched might cause several unexpected errors in the queries. You cannot change the server-level collation on the existing Managed Instance.

## Next steps

 - [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
 - [Set or Change the Database Collation](../../relational-databases/collations/set-or-change-the-database-collation.md)   
 - [Set or Change the Column Collation](../../relational-databases/collations/set-or-change-the-column-collation.md)   
 - [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)  

