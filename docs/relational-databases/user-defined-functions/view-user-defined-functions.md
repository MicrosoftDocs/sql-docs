---
title: "View User-defined Functions"
description: "View User-defined Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.udfproperties.general.f1"
  - "sql13.swb.functionproperties.general.f1"
helpviewer_keywords:
  - "displaying user-defined functions"
  - "viewing user-defined functions"
  - "user-defined functions [SQL Server], viewing"
  - "status information [SQL Server], user-defined functions"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View user-defined functions

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can gain information about the definition or properties of a user-defined function in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You may need to see the definition of the function to understand how its data is derived from the source tables or to see the data defined by the function.

If you change the name of an object referenced by a function, you must modify that function so that its text reflects the new name. Therefore, before renaming an object, display the dependencies of the object first to determine if any functions are affected by the proposed change.

## Permissions

Using `sys.sql_expression_dependencies` to find all the dependencies on a function requires VIEW DEFINITION permission on the database and SELECT permission on `sys.sql_expression_dependencies` for the database. System object definitions, like the ones returned in OBJECT_DEFINITION, are publicly visible.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Show a user-defined function's properties

1. In **Object Explorer**, select the plus sign next to the database that contains the function to which you want to view the properties, and then select the plus sign to expand the **Programmability** folder.

1. Select the plus sign to expand the **Functions** folder.

1. Select the plus sign to expand the folder that contains the function to which you want to view the properties:

   - Table-valued Function
   - Scalar-valued Function
   - Aggregate Function

1. Right-click the function of which you want to view the properties and select **Properties**.

   The following properties appear in the **Function Properties -** *function_name* dialog box.

   |Function name|Description|
   | --- | --- |
   |Database|The name of the database containing this function.|
   |Server|The name of the current server instance.|
   |User|The name of the user of this connection.|
   |Created date|Displays the date the function was created.|
   |Execute As|Execution context for the function.|
   |Name|The name of the current function.|
   |Schema|Displays the schema that owns the function.|
   |System object|Indicates whether the function is a system object. Values are `True` and `False`.|
   |ANSI NULLs|Indicates if the object was created with the ANSI NULLs option.|
   |Encrypted|Indicates whether the function is encrypted. Values are `True` and `False`.|
   |Function Type|The type of user defined function.|
   |Quoted identifier|Indicates if the object was created with the quoted identifier option.|
   |Schema bound|Indicates whether the function is schema-bound. Values are True and False. For information about schema-bound functions, see the SCHEMABINDING section of [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).|

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Get the definition and properties of a function

1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste one of the following examples into the query window and select **Execute**.

   The following code sample gets the function name, definition, and relevant properties.

   ```sql
   USE AdventureWorks2012;
   GO
   -- Get the function name, definition, and relevant properties
   SELECT sm.object_id,
      OBJECT_NAME(sm.object_id) AS object_name,
      o.type,
      o.type_desc,
      sm.definition,
      sm.uses_ansi_nulls,
      sm.uses_quoted_identifier,
      sm.is_schema_bound,
      sm.execute_as_principal_id
   -- using the two system tables sys.sql_modules and sys.objects
   FROM sys.sql_modules AS sm
   JOIN sys.objects AS o ON sm.object_id = o.object_id
   -- from the function 'dbo.ufnGetProductDealerPrice'
   WHERE sm.object_id = OBJECT_ID('dbo.ufnGetProductDealerPrice')
   ORDER BY o.type;
   GO
   ```

   The following code sample gets the definition of the example function `dbo.ufnGetProductDealerPrice`.

   ```sql
   USE AdventureWorks2012;
   GO
   -- Get the definition of the function dbo.ufnGetProductDealerPrice
   SELECT OBJECT_DEFINITION (OBJECT_ID('dbo.ufnGetProductDealerPrice')) AS ObjectDefinition;
   GO
   ```

For more information, see [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) and [OBJECT_DEFINITION &#40;Transact-SQL&#41;](../../t-sql/functions/object-definition-transact-sql.md).

### Get the dependencies of a function

1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2012;
   GO
   -- Get all of the dependency information
   SELECT OBJECT_NAME(sed.referencing_id) AS referencing_entity_name,
       o.type_desc AS referencing_desciption,
       COALESCE(COL_NAME(sed.referencing_id, sed.referencing_minor_id), '(n/a)') AS referencing_minor_id,
       sed.referencing_class_desc, sed.referenced_class_desc,
       sed.referenced_server_name, sed.referenced_database_name, sed.referenced_schema_name,
       sed.referenced_entity_name,
       COALESCE(COL_NAME(sed.referenced_id, sed.referenced_minor_id), '(n/a)') AS referenced_column_name,
       sed.is_caller_dependent, sed.is_ambiguous
   -- from the two system tables sys.sql_expression_dependencies and sys.object
   FROM sys.sql_expression_dependencies AS sed
   INNER JOIN sys.objects AS o ON sed.referencing_id = o.object_id
   -- on the function dbo.ufnGetProductDealerPrice
   WHERE sed.referencing_id = OBJECT_ID('dbo.ufnGetProductDealerPrice');
   GO
   ```

For more information, see [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) and [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).
