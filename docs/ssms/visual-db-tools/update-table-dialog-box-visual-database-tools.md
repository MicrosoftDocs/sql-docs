---
title: Update Table Dialog Box
description: "Update Table Dialog Box (Visual Database Tools)"
author: markingmyname
ms.author: maghan
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords:
  - "vdt.dlgbox.updatetable"
  - "vdtsql.chm:69643"
---

# Update Table Dialog Box (Visual Database Tools)

[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]

This dialog box allows you to specify the table to be updated.

This dialog box appears if more than one table is displayed in the Diagram pane when you change a query's type to be an Update query.  

Select the table to update, and then choose **OK**.

> [!NOTE]
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.

## See Also

[Create Update Queries](../../ssms/visual-db-tools/create-update-queries-visual-database-tools.md)
