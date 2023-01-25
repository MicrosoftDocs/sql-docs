---
title: "Delete a Stored Procedure"
description: Learn how to delete a stored procedure in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/28/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords:
  - "removing stored procedures"
  - "stored procedures [SQL Server], deleting"
  - "deleting stored procedures"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete a stored procedure

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to delete a stored procedure in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a id="Restrictions"></a> Limitations and restrictions

 Deleting a procedure can cause dependent objects and scripts to fail when the objects and scripts are not updated to reflect the removal of the procedure. However, if a new procedure of the same name and the same parameters is created to replace the one that was deleted, other objects that reference it will still process successfully. For more information, see [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md).

## <a id="Permissions"></a> Permissions

 Requires ALTER permission on the schema to which the procedure belongs, or CONTROL permission on the procedure.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.

1. Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.

1. Expand **Stored Procedures**, right-click the procedure to remove, and then select **Delete**.

1. To view objects that depend on the procedure, select **Show Dependencies**.

1. Confirm the correct procedure is selected, and then select **OK**.

1. Remove references to the procedure from any dependent objects and scripts.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.

1. Expand **Databases**, expand the database in which the procedure belongs, or, from the tool bar, select the database from the list of available databases.

1. On the File menu, select **New Query**.

1. Obtain the name of stored procedure to remove in the current database. From Object Explorer, expand **Programmability** and then expand **Stored Procedures**. Alternatively, in the query editor, run the following statement.

    ```sql
    SELECT name AS procedure_name
        , SCHEMA_NAME(schema_id) AS schema_name
        , type_desc
        , create_date
        , modify_date
    FROM sys.procedures;
    ```

1. Copy and paste the following example into the query editor and insert a stored procedure name to delete from the current database.

    ```sql
    DROP PROCEDURE [<stored procedure name>];
    GO
    ```

1. Remove references to the procedure from any dependent objects and scripts.

## See also

- [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)
- [Modify a Stored Procedure](../../relational-databases/stored-procedures/modify-a-stored-procedure.md)
- [Rename a Stored Procedure](../../relational-databases/stored-procedures/rename-a-stored-procedure.md)
- [View the Definition of a Stored Procedure](../../relational-databases/stored-procedures/view-the-definition-of-a-stored-procedure.md)
- [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)
- [DROP PROCEDURE (Transact-SQL)](../../t-sql/statements/drop-procedure-transact-sql.md)
