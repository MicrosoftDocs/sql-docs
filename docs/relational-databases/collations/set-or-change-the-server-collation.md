---
title: "Set or Change the Server Collation | Microsoft Docs"
ms.custom: ""
ms.date: "01/22/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "server collations [SQL Server]"
  - "collations [SQL Server], server"
ms.assetid: 3242deef-6f5f-4051-a121-36b3b4da851d
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.reviewer: carlrab
---
# Set or Change the Server Collation

[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]
  The server collation acts as the default collation for all system databases that are installed with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and also any newly created user databases. You should carefully choose server-level collation because it affects:
 - Sorting and comparison rules in `=`, `JOIN`, `ORDER BY` and other operators that compare textual data.
 - Collation of the `CHAR`, `VARCHAR`, `NCHAR`, and `NVARCHAR` columns in system views, system functions, and the objects in TempDB (for example, temporary tables).
 - Names of the variables, cursors, and `GOTO` labels. Variables @pi and @PI are considered as different variables if the server-level collation is case-sensitive, and the same variables if the server-level collation is case-insensitive.
  
## Setting the server collation in SQL Server

  The server collation is specified during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Default server-level collation is **SQL_Latin1_General_CP1_CI_AS**. Unicode-only collations cannot be specified as the server-level collation. For more information, see [Server Configuration - Collation](/sql/sql-server/install/server-configuration-collation.md).
  
## Changing the server collation in SQL Server

 Changing the default collation for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be a complex operation and involves the following steps:  
  
- Make sure you have all the information or scripts needed to re-create your user databases and all the objects in them.  
  
- Export all your data using a tool such as the [bcp Utility](../../tools/bcp-utility.md). For more information, see [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md).  
  
- Drop all the user databases.  
  
- Rebuild the master database specifying the new collation in the SQLCOLLATION property of the **setup** command. For example:  
  
    ```sql  
    Setup /QUIET /ACTION=REBUILDDATABASE /INSTANCENAME=InstanceName
    /SQLSYSADMINACCOUNTS=accounts /[ SAPWD= StrongPassword ]
    /SQLCOLLATION=CollationName  
    ```  
  
     For more information, see [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md).  
  
- Create all the databases and all the objects in them.  
  
- Import all your data.  
  
> [!NOTE]  
> Instead of changing the default collation of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can specify a default collation for each new database you create.  
  
## Setting the server collation in Managed Instance

Server-level collation (Preview) in Azure SQL Managed Instance can be specified when the instance is created and cannot be changed later. You can set server-level collation via [Azure portal](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-get-started#create-a-managed-instance) or [PowerShell and Resource Manager template](https://docs.microsoft.com/azure/sql-database/scripts/sql-managed-instance-create-powershell-azure-resource-manager-template) while you are creating the instance. Default server-level collation is **SQL_Latin1_General_CP1_CI_AS**. Unicode-only and new UTF-8 collations cannot be specified as server-level collation.
If you are migrating databases from SQL Server to Managed Instance, check the server collation in the source SQL Server using `SERVERPROPERTY(N'Collation')` function and create a Managed Instance that matches the collation of your SQL Server. Migrating a database from SQL Server to Managed Instance with the server-level collations that are not matched might cause several unexpected errors in the queries. You cannot change the server-level collation on the existing Managed Instance.

## See Also

 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
 [Set or Change the Database Collation](../../relational-databases/collations/set-or-change-the-database-collation.md)   
 [Set or Change the Column Collation](../../relational-databases/collations/set-or-change-the-column-collation.md)   
 [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)  
 
