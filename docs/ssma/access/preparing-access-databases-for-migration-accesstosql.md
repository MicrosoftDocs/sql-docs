---
title: "Preparing Access Databases for Migration (AccessToSQL) | Microsoft Docs"
description: Learn how to determine which Access databases to migrate to SQL Server or Azure SQL Database and ensure that those databases are ready for migration.
ms.service: sql
ms.custom: ""
ms.date: "08/15/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Access databases, versions"
  - "Access databases, when to migrate"
  - "Access databases, workgroup security"
  - "backing up databases"
  - "documenting databases"
  - "files, preparing"
  - "migrating databases, versions"
  - "migrating databases, when to migrate"
  - "versions of Access"
  - "workgroup security"
ms.assetid: 9b80a9e0-08e7-4b4d-b5ec-cc998d3f5114
author: cpichuka 
ms.author: cpichuka 
---
# Preparing Access databases for migration (AccessToSQL)
Before you migrate Access databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must determine which databases to migrate and ensure that those databases are ready for migration.  
  
## Determining when to migrate to SQL Server  
The Jet database engine, which is used as the database engine for Access, is a flexible, easy-to-use solution for data management. However, as databases become larger and more mission critical, many users find that they require greater performance, security, or availability. For applications that require a more robust data platform, consider moving the underlying databases for those applications to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about deciding when to migrate, see the [migration information page](https://go.microsoft.com/fwlink/?LinkId=68571) on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Web site.  
  
After you migrate databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can continue to use Access by using linked tables, or you can manually migrate your applications to [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework-based code that interacts directly with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Determining which databases to migrate  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Access can locate Access databases for you. You can then export metadata about those databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about how to export and query metadata, see [Exporting an Access Inventory](exporting-an-access-inventory-accesstosql.md).  

   > [!NOTE]
   > Not all Access features and settings are supported by, or can be easily converted to, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Before you start migrating databases, see [Incompatible Access Features](incompatible-access-features-accesstosql.md).
  
## Preparing for migration  
Use the following guidelines to help prepare your Access databases for migration to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Upgrading older Access databases  
SSMA for Access supports Access 97 and later versions. If you have databases from earlier versions of Access, open and save the databases in Access 97 or a later version.  
  
### Removing workgroup protection  
SSMA cannot migrate databases that use workgroup protection. To remove workgroup protection from an Access database, perform the following steps:  
  
1.  Copy the Access database file to another location.  
  
2.  Open the copied database.  
  
3.  On the **Tools** menu, point to **Security**, and then select **User and Group Permissions**.  
  
4.  Select the **Users** option, select the **Admin** user, and then ensure that the **Administer** permission is selected.  
  
5.  Select the **Groups** option, select the **Users** group, and then ensure that the **Administer** permission is selected.  
  
6.  Click **OK**, and then on the **File** menu, click **Exit**.  
  
You can now use SSMA to migrate the copied database. After you load the schema into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can manually secure the database on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Backing up databases  
Before you migrate your Access databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should back up both the Access databases that you will be migrating as well as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases into which you will migrate Access objects and data.  
  
To back up an Access database, on the **Tools** menu, point to **Database Utilities**, and then select **Back Up Database**.  
  
For information about how to  back up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, see "Backing Up and Restoring Databases in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
### Documenting databases  
You might also want to document the properties, such as lists of database objects, file sizes, and permissions, of your Access databases. To generate this documentation in Access, on the **Tools** menu, point to **Analyze**, and then click **Documented**.  
  
## See also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
[Linking Access Applications to SQL Server](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md)
