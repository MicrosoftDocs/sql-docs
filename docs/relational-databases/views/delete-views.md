---
title: "Delete views"
description: "Delete (drop) views in the Database Engine using SQL Server Management Studio or Transact-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 05/10/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "dropping views"
  - "deleting views"
  - "views [SQL Server], deleting"
  - "removing views"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete views

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can delete (drop) views in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]

## Limitations and restrictions

- When you drop a view, the definition of the view and other information about the view is deleted from the system catalog. All permissions for the view are also deleted.

- Any view on a table that is dropped by using `DROP TABLE` must be dropped explicitly by using `DROP VIEW`.

## Permissions

Requires ALTER permission on SCHEMA or CONTROL permission on OBJECT.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, expand the database that contains the view you want to delete, and then expand the **Views** folder.

1. Right-click the view you want to delete and select **Delete**.

1. In the **Delete Object** dialog box, select **OK**.

    > [!IMPORTANT]  
    > Select **Show Dependencies** in the **Delete Object** dialog box to open the **_view_name_ Dependencies** dialog box. This will show all of the objects that depend on the view and all of the objects on which the view depends.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. The example deletes the specified view only if the view already exists.

   ```sql
   USE AdventureWorks2022;
   GO
   
   IF OBJECT_ID('HumanResources.EmployeeHireDate', 'V') IS NOT NULL
       DROP VIEW HumanResources.EmployeeHireDate;
   GO
   ```

   You can also use the `IF EXISTS` syntax, introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]:

   ```sql
   USE AdventureWorks2022;
   GO
   
   DROP VIEW IF EXISTS HumanResources.EmployeeHireDate;
   GO
   ```

## Next steps

- [DROP VIEW (Transact-SQL)](../../t-sql/statements/drop-view-transact-sql.md)
