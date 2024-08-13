---
title: "Create views"
description: "Create views in the Database Engine with SQL Server Management Studio or Transact-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/19/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "views [SQL Server], creating"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create views

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can create views in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. A view can be used for the following purposes:

- To focus, simplify, and customize the perception each user has of the database.

- As a security mechanism by allowing users to access data through the view, without granting the users permissions to directly access the underlying base tables.

- To provide a backward compatible interface to emulate a table whose schema has changed.

## Limitations

A view can be created only in the current database.

A view can have a maximum of 1,024 columns.

## Permissions

Requires CREATE VIEW permission in the database and ALTER permission on the schema in which the view is being created.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, expand the database where you want to create your new view.

1. Right-click the **Views** folder, then select **New View...**.

1. In the **Add Table** dialog box, select the element or elements that you want to include in your new view from one of the following tabs: Tables, Views, Functions, and Synonyms.

1. Select **Add**, then select **Close**.

1. In the **Diagram Pane**, select the columns or other elements to include in the new view.

1. In the **Criteria Pane**, select additional sort or filter criteria for the columns.

1. On the **File** menu, select **Save _view name_**.

1. In the **Choose Name** dialog box, enter a name for the new view and select **OK**.

     For more information about the query and view designer, see [Query and View Designer Tools (Visual Database Tools)](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md).

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2022;
   GO
   
   CREATE VIEW HumanResources.EmployeeHireDate
   AS
   SELECT p.FirstName,
       p.LastName,
       e.HireDate
   FROM HumanResources.Employee AS e
   INNER JOIN Person.Person AS p
       ON e.BusinessEntityID = p.BusinessEntityID;
   GO
   
   -- Query the view
   SELECT FirstName,
       LastName,
       HireDate
   FROM HumanResources.EmployeeHireDate
   ORDER BY LastName;
   GO
   ```

## Next step

> [!div class="nextstepaction"]
> [CREATE VIEW (Transact-SQL)](../../t-sql/statements/create-view-transact-sql.md)
