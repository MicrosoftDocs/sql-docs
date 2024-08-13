---
title: sp_describe_undeclared_parameters (Transact-SQL)
description: Returns a result set that contains metadata about undeclared parameters in a Transact-SQL batch.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_describe_undeclared_parameters"
  - "sp_describe_undeclared_parameters_TSQL"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =fabric"
---

# sp_describe_undeclared_parameters (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Returns a result set that contains metadata about undeclared parameters in a [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Considers each parameter that is used in the *@tsql* batch, but not declared in *@params*. A result set is returned that contains one row for each such parameter, with the deduced type information for that parameter. The procedure returns an empty result set if the *@tsql* input batch has no parameters except those declared in *@params*.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_describe_undeclared_parameters
    [ @tsql = ] 'Transact-SQL_batch'
    [ , [ @params = ] N'params [ , ...n ]' ]
```

> [!NOTE]  
> To use this stored procedure in Azure Synapse Analytics in a dedicated SQL pool, set the database compatibility level to `20` or higher. To opt out, change the database compatibility level to `10`.

## Arguments

#### [ @tsql = ] '*tsql*'

One or more [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. *@tsql* might be **nvarchar(*n*)** or **nvarchar(max)**.

#### [ @params = ] N'*params*'

*@params* provides a declaration string for parameters for the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch, similarly to the way `sp_executesql` works. *@params* might be **nvarchar(*n*)** or **nvarchar(max)**.

A string that contains the definitions of all parameters that are embedded in *@tsql*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. If the Transact-SQL statement or batch in the statement doesn't contain parameters, *@params* isn't required. The default value for this parameter is `NULL`.

## Return code values

`sp_describe_undeclared_parameters` always returns status of zero on success. If the procedure throws an error and the procedure is called as an RPC, the return status is populated by the type of error as described in the `error_type` column of `sys.dm_exec_describe_first_result_set`. If the procedure is called from [!INCLUDE [tsql](../../includes/tsql-md.md)], the return value is always zero, even in error cases.

## Result set

`sp_describe_undeclared_parameters` returns the following result set.

| Column name | Data type | Description |
| --- | --- | --- |
| `parameter_ordinal` | **int** | Contains the ordinal position of the parameter in the result set. Position of the first parameter is specified as `1`. Not nullable. |
| `name` | **sysname** | Contains the name of the parameter. Not nullable. |
| `suggested_system_type_id` | **int** | Contains the `system_type_id` of the data type of the parameter as specified in `sys.types`.<br /><br />For CLR types, even though the `system_type_name` column returns `NULL`, this column returns the value `240`. Not nullable. |
| `suggested_system_type_name` | **nvarchar(256)** | Contains the data type name. Includes arguments (such as length, precision, scale) specified for the data type of the parameter. If the data type is a user-defined alias type, the underlying system type is specified here. If it's a CLR user-defined data type, `NULL` is returned in this column. If the type of the parameter can't be deduced, `NULL` is returned. Nullable. |
| `suggested_max_length` | **smallint** | See `sys.columns`. for `max_length` column description. Not nullable. |
| `suggested_precision` | **tinyint** | See `sys.columns`. for precision column description. Not nullable. |
| `suggested_scale` | **tinyint** | See `sys.columns`. for scale column description. Not nullable. |
| `suggested_user_type_id` | **int** | For CLR and alias types, contains the `user_type_id` of the data type of the column as specified in `sys.types`. Otherwise is `NULL`. Nullable. |
| `suggested_user_type_database` | **sysname** | For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is `NULL`. Nullable. |
| `suggested_user_type_schema` | **sysname** | For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is `NULL`. Nullable. |
| `suggested_user_type_name` | **sysname** | For CLR and alias types, contains the name of the type. Otherwise is `NULL`. |
| `suggested_assembly_qualified_type_name` | **nvarchar(4000)** | For CLR types, returns the name of the assembly and class that defines the type. Otherwise is `NULL`. Nullable. |
| `suggested_xml_collection_id` | **int** | Contains the `xml_collection_id` of the data type of the parameter as specified in `sys.columns`. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `suggested_xml_collection_database` | **sysname** | Contains the database in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `suggested_xml_collection_schema` | **sysname** | Contains the schema in which the XML schema collection associated with this type is defined. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `suggested_xml_collection_name` | **sysname** | Contains the name of the XML schema collection associated with this type. This column returns `NULL` if the type returned isn't associated with an XML schema collection. Nullable. |
| `suggested_is_xml_document` | **bit** | Returns `1` if the type being returned is XML and that type is guaranteed to be an XML document. Otherwise returns `0`. Not nullable. |
| `suggested_is_case_sensitive` | **bit** | Returns `1` if the column is of a case-sensitive string type and `0` if it's not. Not nullable. |
| `suggested_is_fixed_length_clr_type` | **bit** | Returns `1` if the column is of a fixed-length CLR type and `0` if it's not. Not nullable. |
| `suggested_is_input` | **bit** | Returns `1` if the parameter is used anywhere other than left side of an assignment. Otherwise returns `0`. Not nullable. |
| `suggested_is_output` | **bit** | Returns `1` if the parameter is used on the left side of an assignment or is passed to an output parameter of a stored procedure. Otherwise returns `0`. Not nullable. |
| `formal_parameter_name` | **sysname** | If the parameter is an argument to a stored procedure or a user-defined function, returns the name of the corresponding formal parameter. Otherwise returns `NULL`. Nullable. |
| `suggested_tds_type_id` | **int** | For internal use. Not nullable. |
| `suggested_tds_length` | **int** | For internal use. Not nullable. |

## Remarks

`sp_describe_undeclared_parameters` always returns status of zero.

The most common use is when an application is given a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that might contain parameters and must process them in some way. An example is a user interface (such as `ODBCTest` or `RowsetViewer`) where the user provides a query with ODBC parameter syntax. The application must dynamically discover the number of parameters and prompt the user for each one.

Another example is when without user input, an application must loop over the parameters and obtain the data for them from some other location (such as a table). In this case, the application doesn't have to pass all the parameter information at once. Instead, the application can get all the parameters information from the provider and obtain the data itself from the table. Code using `sp_describe_undeclared_parameters` is more generic and is less likely to require modification if the data structure changes later.

`sp_describe_undeclared_parameters` returns an error in any of the following cases.

- The input *@tsql* isn't a valid [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Validity is determined by parsing and analyzing the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. Any errors caused by the batch during query optimization or during execution aren't considered when determining whether the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch is valid.

- *@params* isn't `NULL` and contains a string that isn't a syntactically valid declaration string for parameters, or if it contains a string that declares any parameter more than one time.

- The input [!INCLUDE [tsql](../../includes/tsql-md.md)] batch declares a local variable of the same name as a parameter declared in *@params*.

- The statement references temporary tables.

- The query includes the creation of a permanent table that is then queried.

If *@tsql* has no parameters, other than parameters declared in *@params*, the procedure returns an empty result set.

> [!NOTE]  
> You must declare the variable as a scalar Transact-SQL variable, or an error appears.

## Parameter selection algorithm

For a query with undeclared parameters, data type deduction for undeclared parameters proceeds in three steps.

### Step 1: Find the data types of the sub-expressions

The first step in data type deduction for a query with undeclared parameters is to find the data types of all the sub-expressions whose data types don't depend on the undeclared parameters. The type can be determined for the following expressions:

- Columns, constants, variables, and declared parameters.
- Results of a call to a user-defined function (UDF).
- An expression with data types that don't depend on the undeclared parameters for all inputs.

For example, consider the query `SELECT dbo.tbl(@p1) + c1 FROM t1 WHERE c2 = @p2 + 2`. The expressions `dbo.tbl(@p1) + c1` and `c2` have data types, and expression `@p1` and `@p2 + 2` don't.

After this step, if any expression (other than a call to a UDF) has two arguments without data types, type deduction fails with an error. For example, the following all produce errors:

```sql
SELECT * FROM t1 WHERE @p1 = @p2;
SELECT * FROM t1 WHERE c1 = @p1 + @p2;
SELECT * FROM t1 WHERE @p1 = SUBSTRING(@p2, 2, 3);
```

The following example doesn't produce an error:

```sql
SELECT * FROM t1 WHERE @p1 = dbo.tbl(c1, @p2, @p3);
```

### Step 2: Find innermost expressions

For a given undeclared parameter `@p`, the type deduction algorithm finds the innermost expression `E(@p)` that contains `@p` and is one of the following arguments:

- An argument to a comparison or assignment operator.
- An argument to a user-defined function (including table-valued UDF), procedure, or method.
- An argument to a `VALUES` clause of an `INSERT` statement.
- An argument to a `CAST` or `CONVERT`.

The type deduction algorithm finds a target data type `TT(@p)` for `E(@p)`. Target data types for the previous examples are as follows:

- The data type of the other side of the comparison or assignment.
- The declared data type of the parameter to which this argument is passed.
- The data type of the column into which this value is inserted.
- The data type to which the statement is casting or converting.

For example, consider the query `SELECT * FROM t WHERE @p1 = dbo.tbl(@p2 + c1)`. Then `E(@p1) = @p1`, `E(@p2) = @p2 + c1`, `TT(@p1)` is the declared return data type of `dbo.tbl`, and `TT(@p2)` is the declared parameter data type for `dbo.tbl`.

If `@p` isn't contained in any expression listed at the beginning of step 2, the type deduction algorithm determines that `E(@p)` is the largest scalar expression that contains `@p`, and the type deduction algorithm doesn't compute a target data type `TT(@p)` for `E(@p)`. For example, if the query is `SELECT @p + 2` then `E(@p) = @p + 2`, and there's no `TT(@p)`.

### Step 3: Deduce data types

Now that `E(@p)` and `TT(@p)` are identified, the type deduction algorithm deduces a data type for `@p` in one of the following two ways:

- Simple deduction

  If `E(@p) = @p` and `TT(@p)` exists, that is, if `@p` is directly an argument to one of the expressions listed at the beginning of step 2, the type deduction algorithm deduces the data type of `@p` to be `TT(@p)`. For example:

  ```sql
  SELECT * FROM t WHERE c1 = @p1 AND @p2 = dbo.tbl(@p3);
  ```

  The data type for `@p1`, `@p2`, and `@p3` will be the data type of `c1`, the return data type of `dbo.tbl`, and the parameter data type for `dbo.tbl` respectively.

  As a special case, if `@p` is an argument to a `<`, `>`, `<=`, or `>=` operator, simple deduction rules don't apply. The type deduction algorithm will use the general deduction rules explained in the next section. For example, if `c1` is a column of data type **char(30)**, consider the following two queries:

  ```sql
  SELECT * FROM t WHERE c1 = @p;
  SELECT * FROM t WHERE c1 > @p;
  ```

  In the first case, the type deduction algorithm deduces **char(30)** as the data type for `@p` as per rules earlier in this article. In the second case, the type deduction algorithm deduces **varchar(8000)** according to the general deduction rules in the next section.

- General deduction

  If simple deduction doesn't apply, the following data types are considered for undeclared parameters:

  - Integer data types (**bit**, **tinyint**, **smallint**, **int**, **bigint**)

  - Money data types (**smallmoney**, **money**)

  - Floating-point data types (**float**, **real**)

  - **numeric(38, 19)** - Other numeric or decimal data types aren't considered.

  - **varchar(8000)**, **varchar(max)**, **nvarchar(4000)**, and **nvarchar(max)** - Other string data types (such as **text**, **char(8000)**, **nvarchar(30)**, etc.) aren't considered.

  - **varbinary(8000)** and **varbinary(max)** - Other binary data types aren't considered (such as **image**, **binary(8000)**, **varbinary(30)**, etc.).

  - **date**, **time(7)**, **smalldatetime**, **datetime**, **datetime2(7)**, **datetimeoffset(7)** - Other date and time types, such as **time(4)**, aren't considered.

  - **sql_variant**

  - **xml**

  - CLR system-defined types (**hierarchyid**, **geometry**, **geography**)

  - CLR user-defined types

### Selection criteria

Of the candidate data types, any data type that would invalidate the query is rejected. Of the remaining candidate data types, the type deduction algorithm selects one according to the following rules.

1. The data type that produces the smallest number of implicit conversions in `E(@p)` is selected. If a particular data type produces a data type for `E(@p)` that is different from `TT(@p)`, the type deduction algorithm considers this to be an extra implicit conversion from the data type of `E(@p)` to `TT(@p)`.

   For example:

   ```sql
   SELECT * FROM t WHERE Col_Int = Col_Int + @p;
   ```

   In this case, `E(@p)` is `Col_Int + @p` and `TT(@p)` is **int**. **int** is chosen for `@p` because it produces no implicit conversions. Any other choice of data type produces at least one implicit conversion.

1. If multiple data types tie for the smallest number of conversions, the data type with greater precedence is used. For example:

   ```sql
   SELECT * FROM t WHERE Col_Int = Col_smallint + @p;
   ```

   In this case, **int** and **smallint** produce one conversion. Every other data type produces more than one conversion. Because **int** takes precedence over **smallint**, **int** is used for `@p`. For more information about data type precedence, see [Data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md).

   This rule only applies if there's an implicit conversion between every data type that ties according to rule 1 and the data type with the greatest precedence. If there's no implicit conversion, then data type deduction fails with an error. For example in the query `SELECT @p FROM t`, data type deduction fails because any data type for `@p` would be equally good. For example, there's no implicit conversion from **int** to **xml**.

1. If two similar data types tie under rule 1, for example **varchar(8000)** and **varchar(max)**, the smaller data type (**varchar(8000)**) is chosen. The same principle applies to **nvarchar** and **varbinary** data types.

1. For purposes of rule 1, the type deduction algorithm prefers certain conversions as better than others. Conversions in order from best to worst are:

   1. Conversion between same basic data type of different length.
   1. Conversion between fixed-length and variable-length version of same data types (for example, **char** to **varchar**).
   1. Conversion between `NULL` and **int**.
   1. Any other conversion.

For example, for the query `SELECT * FROM t WHERE [Col_varchar(30)] > @p`, **varchar(8000)** is chosen because conversion (a) is best. For the query `SELECT * FROM t WHERE [Col_char(30)] > @p`, **varchar(8000)** is still chosen because it causes a type (b) conversion, and because another choice (such as **varchar(4000)**) would cause a type (d) conversion.

As a final example, given a query `SELECT NULL + @p`, **int** is chosen for `@p` because it results in a type (c) conversion.

## Permissions

Requires permission to execute the *@tsql* argument.

## Examples

The following example returns information such as the expected data type for the undeclared `@id` and `@name` parameters.

```sql
EXEC sp_describe_undeclared_parameters @tsql =
N'SELECT object_id, name, type_desc
FROM sys.indexes
WHERE object_id = @id OR name = @name';
```

When the `@id` parameter is provided as a `@params` reference, the `@id` parameter is omitted from the result set and only the `@name` parameter is described.

```sql
EXEC sp_describe_undeclared_parameters @tsql =
N'SELECT object_id, name, type_desc
FROM sys.indexes
WHERE object_id = @id OR NAME = @name',
@params = N'@id int';
```

## Related content

- [sp_describe_first_result_set (Transact-SQL)](sp-describe-first-result-set-transact-sql.md)
- [sys.dm_exec_describe_first_result_set (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)
- [sys.dm_exec_describe_first_result_set_for_object (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)
