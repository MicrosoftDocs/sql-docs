---
title: "T-SQL Tutorial: Delete database objects"
description: "This short lesson removes the objects that you created in Lesson 1 and Lesson 2, and then drops the database."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 12/02/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "deleting database objects"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Lesson 3: Delete database objects

[!INCLUDE[sql-asdb-asdbmi-pdw-md](../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

This short lesson removes the objects that you created in Lesson 1 and Lesson 2, and then drops the database.

Before you delete objects, make sure you are in the correct database:

```sql
USE TestData;
GO
```

## Revoke stored procedure permissions

Use the `REVOKE` statement to remove execute permission for `Mary` on the stored procedure:

```sql
REVOKE EXECUTE ON pr_Names FROM Mary;
GO
```

## Drop permissions

1. Use the `DROP` statement to remove permission for `Mary` to access the `TestData` database:

   ```sql
   DROP USER Mary;
   GO
   ```

1. Use the `DROP` statement to remove permission for `Mary` to access this instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]:

   ```sql
   DROP LOGIN [<computer_name>\Mary];
   GO
   ```

1. Use the `DROP` statement to remove the store procedure `pr_Names`:

   ```sql
   DROP PROC pr_Names;
   GO
   ```

1. Use the `DROP` statement to remove the view `vw_Names`:

   ```sql
   DROP VIEW vw_Names;
   GO
   ```

## Delete table

1. Use the `DELETE` statement to remove all rows from the `Products` table:

    ```sql
    DELETE FROM Products;
    GO
    ```

1. Use the `DROP` statement to remove the `Products` table:

    ```sql
    DROP TABLE Products;
    GO
    ```

## Remove database

You can't remove the `TestData` database while you are in the database; therefore, first switch context to another database, and then use the `DROP` statement to remove the `TestData` database:

  ```sql
  USE MASTER;
  GO
  DROP DATABASE TestData;
  GO
  ```

This concludes the Writing [!INCLUDE[tsql](../includes/tsql-md.md)] Statements tutorial. Remember, this tutorial is a brief overview and it doesn't describe all the options to the statements that are used. Designing and creating an efficient database structure and configuring secure access to the data requires a more complex database than that shown in this tutorial.

## Next steps

- [Back Up and Restore of SQL Server Databases](../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)
- [Maintenance Plans](../relational-databases/maintenance-plans/maintenance-plans.md)
- [sp_configure (Transact-SQL)](../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
