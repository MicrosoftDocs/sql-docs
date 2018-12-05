---
title: "Set or Change the Server Collation | Microsoft Docs"
ms.custom: ""
ms.date: "12/03/2017"
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
  The server collation acts as the default collation for all system databases that are installed with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and also any newly created user databases. 
  
## Setting the server collation in SQL Server

  The server collation is specified during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. For more information, see [Server Configuration - Collation](/sql/sql-server/install/server-configuration-collation.md).
  
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
  
## Setting the server collation in Azure SQL Database Managed Instance

Server collation in Azure SQL Managed Instance can be specified when the instance is created. For a script template demonstrating how to set the server collation in Azure SQL Database Managed instance, see [Set Managed Instance collation using Resource Manager template](https://docs.microsoft.com/azure/sql-database/scripts/sql-managed-instance-create-powershell-azure-resource-manager-template). If you are migrating databases from SQL Server to Managed Instance, check the server collation in SQL server using `SELECT SERVERPROPERTY(N'Collation')` function and create a Managed Instance that matches the collation of your SQL Server. You cannot change server-level collation on the existing Managed Instance.

## See Also

 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
 [Set or Change the Database Collation](../../relational-databases/collations/set-or-change-the-database-collation.md)   
 [Set or Change the Column Collation](../../relational-databases/collations/set-or-change-the-column-collation.md)   
 [Rebuild System Databases](../../relational-databases/databases/rebuild-system-databases.md)  
 
