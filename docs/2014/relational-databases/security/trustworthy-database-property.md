---
title: "TRUSTWORTHY Database Property | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "TRUSTWORTHY database property"
ms.assetid: 64b2a53d-4416-4a19-acc0-664a61b45348
author: VanMSFT
ms.author: vanto
manager: craigg
---
# TRUSTWORTHY Database Property
  The TRUSTWORTHY database property is used to indicate whether the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trusts the database and the contents within it. By default, this setting is OFF, but can be set to ON by using the ALTER DATABASE statement. For example, `ALTER DATABASE AdventureWorks2012 SET TRUSTWORTHY ON;`.  
  
> [!NOTE]  
>  To set this option, you must be a member of the **sysadmin** fixed server role.  
  
 This property can be used to reduce certain threats that can exist as a result of attaching a database that contains one of the following objects:  
  
-   Malicious assemblies with an EXTERNAL_ACCESS or UNSAFE permission setting. For more information, see [CLR Integration Security](../clr-integration/security/clr-integration-security.md).  
  
-   Malicious modules that are defined to execute as high privileged users. For more information, see [EXECUTE AS Clause &#40;Transact-SQL&#41;](/sql/t-sql/statements/execute-as-clause-transact-sql).  
  
 Both of these situations require a specific degree of privileges and are protected against by appropriate mechanisms when they are used in the context of a database that is already attached to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, if the database is taken offline, a user that has access to the database file can potentially attach it to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] of his or her choice and add malicious content to the database. When databases are detached and attached in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], certain permissions are set on the data and log files that restrict access to the database files.  
  
 Because a database that is attached to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be immediately trusted, the database is not allowed to access resources beyond the scope of the database until the database is explicitly marked trustworthy. Also, modules that are designed to access resources outside the database, and assemblies with either the EXTERNAL_ACCESS and UNSAFE permission setting, have additional requirements in order to run successfully.  
  
## Related Content  
 [Security Center for SQL Server Database Engine and Azure SQL Database](security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)  
  
  
