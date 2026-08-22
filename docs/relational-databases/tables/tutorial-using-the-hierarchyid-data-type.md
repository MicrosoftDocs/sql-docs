---
title: "Tutorial: Using the hierarchyid Data Type"
description: "Tutorial: Using the hierarchyid Data Type"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: quickstart
ms.custom:
  - intro-quickstart
  - ignite-2025
helpviewer_keywords:
  - "tutorials [hierarchyid]"
  - "hierarchyid [Database Engine], tutorial"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Tutorial: Using the hierarchyid Data Type
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This tutorial is intended for users who are experienced with [!INCLUDE[tsql](../../includes/tsql-md.md)], but are new to the **hierarchyid** data type.  
  
## What You Will Learn  
This tutorial is divided into two lessons:  
  
[Lesson 1: Converting a Table to a Hierarchical Structure](../../relational-databases/tables/lesson-1-converting-a-table-to-a-hierarchical-structure.md)  
In this lesson, you take an existing employee table that is structured as a parent/child hierarchy and move the data into a new table that represents the hierarchy by using the **hierarchyid** data type. This lesson requires the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
[Lesson 2: Creating and Managing Data in a Hierarchical Table](../../relational-databases/tables/lesson-2-creating-and-managing-data-in-a-hierarchical-table.md)  
In this lesson, you create a table by using the **hierarchyid** data type to represent the hierarchy structure. Then, you manipulate the data in the table by using the hierarchical methods.  
  
## Requirements  
Your system must have the following installed:  
  
-   Any edition of [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] or later.  
  
-   Either [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Express.  
  
-   Internet Explorer 6 or a later version.  
  
## Related content

- [Tutorial: Get started with the Database Engine](../tutorial-getting-started-with-the-database-engine.md)
- [Tutorial: Write Transact-SQL statements](../../t-sql/tutorial-writing-transact-sql-statements.md)
- [hierarchyid data type method reference](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
- [Hierarchical data (SQL Server)](../hierarchical-data-sql-server.md)
