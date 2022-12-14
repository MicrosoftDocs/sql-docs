---
title: "View or change the compatibility level of a database"
description: Learn how to view or change the compatibility level of a database in SQL Server or Azure SQL by using SQL Server Management Studio or Transact-SQL.
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility levels [SQL Server], viewing"
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], changing"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "2/2/2022"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View or change the compatibility level of a database
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This article describes how to view or change the compatibility level of a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure SQL Managed Instance by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. 

> [!IMPORTANT]
> Before you change the compatibility level of a database, you should understand the impact of the change on your applications. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a>Use SQL Server Management Studio  
  
 To view or change the compatibility level of a database using [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md)
  
1. Connect to the appropriate server or instance hosting your database.

2. Select the server name in Object Explorer.

3. Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  

> [!Note]
> You cannot modify the compatibility level of system databases in Azure SQL Database.

4.  Right-click the database, and then select **Properties**.  
 
     The **Database Properties** dialog box opens.  
  
5. In the **Select a page** pane, select **Options**. 

6. The current compatibility level is displayed in the **Compatibility level** list box.  

    To change the compatibility level, select a different option from the list. The available options for different [!INCLUDE[ssde_md](../../includes/ssde_md.md)] versions are listed in the [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#supported-dbcompats) page.  

## <a name="TsqlProcedure"></a>Use Transact-SQL

You can use Transact-SQL to view or change the compatibility level of a database using SSMS or [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).
  
### View the compatibility level of a database
  
1. Connect to the appropriate server or instance hosting your database.
  
2. Open a **New Query**.  
  
3. Copy and paste the following example into the query window and select **Execute**. This example returns the compatibility level of the **AdventureWorks2019** [sample database](../../samples/adventureworks-install-configure.md).  
  
```sql  
USE AdventureWorks2019;  
GO  
SELECT compatibility_level  
FROM sys.databases WHERE name = 'AdventureWorks2019';  
GO  
```  
  
### Change the compatibility level of a database
  
1. Connect to the appropriate server or instance hosting your database.  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example changes the compatibility level of the `AdventureWorks2019` database to `150`, which is the compatibility level for [!INCLUDE[ssSQL19](../../includes/sssql19-md.md)].  

  
```sql  
ALTER DATABASE AdventureWorks2019  
SET COMPATIBILITY_LEVEL = 150;  
GO
```  
  
## Next steps
 - [ALTER DATABASE &#40;Transact-SQL&#41; Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
