---
title: "View the definition of a stored procedure"
description: Learn how to view the definition of procedure in Object Explorer and by using a system stored procedure, system function, and object catalog view in the Query Editor.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/19/2024
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords:
  - "stored procedures [SQL Server], viewing"
  - "definition of stored procedure"
  - "viewing stored procedures"
  - "displaying stored procedures"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View the definition of a stored procedure

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

## View the definition of a stored procedure

This article describes how to view the definition of procedure in Object Explorer or T-SQL.  

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

**To view the definition a procedure in Object Explorer**:
  
1. In Object Explorer, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
1. Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
1. Expand **Stored Procedures**, right-click the procedure and then select **Script Stored Procedure as**, and then select one of the following: **Create To**, **Alter To**, or **Drop and Create To**.  
  
1. Select **New Query Editor Window**. This will display the procedure definition.  

<a id="to-view-the-definition-of-a-procedure-in-query-editor"></a>
<a id="TsqlProcedure"></a>

## Use Transact-SQL
  
In T-SQL, you can use one of the following three commands:

- [sp_helptext](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)
- [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md)
- [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)

> [!NOTE]
> The system stored procedure `sp_helptext` is not supported in Azure Synapse Analytics. Instead, use `sys.sql_modules` object catalog view.

### Use sp_helptext

1. In Object Explorer, connect to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the toolbar, select **New Query**.  
  
1. In the query window, enter the following statement that uses the `sp_helptext` system stored procedure. Change the database name and stored procedure name to reference the database and stored procedure that you want.  
  
    ```sql  
    USE AdventureWorks2022;  
    GO  
    EXEC sp_helptext N'AdventureWorks2022.dbo.uspLogError';  
    ```  
  
### Use OBJECT_DEFINITION

1. In Object Explorer, connect to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the toolbar, select **New Query**.  
  
1. In the query window, enter the following statements that use the `OBJECT_DEFINITION` system function. Change the database name and stored procedure name to reference the database and stored procedure that you want. This query leverages [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) and [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) to identify the object by its three-part name.
  
    ```sql  
    USE AdventureWorks2022;  
    GO  
    SELECT OBJECT_DEFINITION (OBJECT_ID(N'AdventureWorks2022.dbo.uspLogError'));  
    ```  
  
<a id="sql_modules"></a>

### Use sys.sql_modules

1. In Object Explorer, connect to an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the toolbar, select **New Query**.  
  
1. In the query window, enter the following statements that use the `sys.sql_modules` catalog view. Change the database name and stored procedure name to reference the database and stored procedure that you want.  
  
    ```sql  
    USE AdventureWorks2022;  
    GO  
    SELECT [definition]
    FROM sys.sql_modules  
    WHERE object_id = (OBJECT_ID(N'dbo.uspLogError'));  
    ```  
  
## Related content

- [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)
- [Modify a Stored Procedure](../../relational-databases/stored-procedures/modify-a-stored-procedure.md)
- [Delete a Stored Procedure](../../relational-databases/stored-procedures/delete-a-stored-procedure.md)
- [Rename a Stored Procedure](../../relational-databases/stored-procedures/rename-a-stored-procedure.md)
