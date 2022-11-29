---
title: "Rename User-defined Functions"
description: "Rename User-defined Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename user-defined functions

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

You can only rename user-defined functions in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

## <a id="Restrictions"></a> Limitations and restrictions

- Function names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

- Renaming a user-defined function won't change the name of the corresponding object name in the definition column of the **sys.sql_modules** catalog view. Therefore, we recommend that you don't rename this object type. Instead, drop and re-create the stored procedure with its new name.

- Changing the name or definition of a user-defined function can cause dependent objects to fail when the objects aren't updated to reflect the changes that have been made to the function.

## Permissions

Dropping the function requires either ALTER permission on the schema to which the function belongs, or CONTROL permission on the function. To re-create the function, requires CREATE FUNCTION permission in the database and ALTER permission on the schema in which the function is being created.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, select the plus sign next to the database that contains the function you wish to rename and then

1. Select the plus sign next to the **Programmability** folder.

1. Select the plus sign next to the folder that contains the function you wish to rename:

   - Table-valued Function
   - Scalar-valued Function
   - Aggregate Function

1. Right-click the function you wish to rename and select **Rename**.

1. Enter the function's new name.

## <a id="TsqlProcedure"></a> Use Transact-SQL

This task can't be performed using Transact-SQL statements. To rename a user-defined function using Transact-SQL, you must first delete the existing function, and then re-create it with the new name. Ensure that all code and applications that used the function's old name now use the new name.

For more information, see [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md) and [DROP FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-function-transact-sql.md).

## See also

- [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [View user-defined functions](../../relational-databases/user-defined-functions/view-user-defined-functions.md)
