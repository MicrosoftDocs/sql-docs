---
title: "DECLARE @local_variable (Transact-SQL)"
description: "Transact-SQL reference for using DECLARE to define local variables for use in a batch or procedure."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECLARE"
  - "DECLARE_TSQL"
helpviewer_keywords:
  - "table-valued parameters"
  - "variables [SQL Server], declaring"
  - "DECLARE statement"
  - "declaring variables"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DECLARE @local_variable (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Variables are declared in the body of a batch or procedure with the DECLARE statement and are assigned values by using either a SET or SELECT statement. Cursor variables can be declared with this statement and used with other cursor-related statements. After declaration, all variables are initialized as NULL, unless a value is provided as part of the declaration.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

The following syntax is for SQL Server and Azure SQL Database:

```syntaxsql
DECLARE
{
  { @local_variable [AS] data_type [ = value ] }
  | { @cursor_variable_name CURSOR }
} [ ,...n ]
| { @table_variable_name [AS] <table_type_definition> }

<table_type_definition> ::=
    TABLE ( { <column_definition> | <table_constraint> | <table_index> } } [ ,...n ] )

<column_definition> ::=
    column_name { scalar_data_type | AS computed_column_expression }
    [ COLLATE collation_name ]
    [ [ DEFAULT constant_expression ] | IDENTITY [ (seed, increment ) ] ]
    [ ROWGUIDCOL ]
    [ <column_constraint> ]
    [ <column_index> ]

<column_constraint> ::=
{
    [ NULL | NOT NULL ]
    { PRIMARY KEY | UNIQUE }
      [ CLUSTERED | NONCLUSTERED ]
      [ WITH FILLFACTOR = fillfactor
        | WITH ( < index_option > [ ,...n ] )
      [ ON { filegroup | "default" } ]
  | [ CHECK ( logical_expression ) ] [ ,...n ]
}

<column_index> ::=
    INDEX index_name [ CLUSTERED | NONCLUSTERED ]
    [ WITH ( <index_option> [ ,... n ] ) ]
    [ ON { partition_scheme_name (column_name )
         | filegroup_name
         | default
         }
    ]
    [ FILESTREAM_ON { filestream_filegroup_name | partition_scheme_name | "NULL" } ]

<table_constraint> ::=
{
    { PRIMARY KEY | UNIQUE }
      [ CLUSTERED | NONCLUSTERED ]
      ( column_name [ ASC | DESC ] [ ,...n ]
        [ WITH FILLFACTOR = fillfactor
        | WITH ( <index_option> [ ,...n ] )
  | [ CHECK ( logical_expression ) ] [ ,...n ]
}

<table_index> ::=
{
    {
      INDEX index_name  [ UNIQUE ] [ CLUSTERED | NONCLUSTERED ]
         (column_name [ ASC | DESC ] [ ,... n ] )
    | INDEX index_name CLUSTERED COLUMNSTORE
    | INDEX index_name [ NONCLUSTERED ] COLUMNSTORE ( column_name [ ,... n ] )
    }
    [ WITH ( <index_option> [ ,... n ] ) ]
    [ ON { partition_scheme_name ( column_name )
         | filegroup_name
         | default
         }
    ]
    [ FILESTREAM_ON { filestream_filegroup_name | partition_scheme_name | "NULL" } ]
}

<index_option> ::=
{
  PAD_INDEX = { ON | OFF }
  | FILLFACTOR = fillfactor
  | IGNORE_DUP_KEY = { ON | OFF }
  | STATISTICS_NORECOMPUTE = { ON | OFF }
  | STATISTICS_INCREMENTAL = { ON | OFF }
  | ALLOW_ROW_LOCKS = { ON | OFF }
  | ALLOW_PAGE_LOCKS = { ON | OFF }
  | OPTIMIZE_FOR_SEQUENTIAL_KEY = { ON | OFF }
  | COMPRESSION_DELAY = { 0 | delay [ Minutes ] }
  | DATA_COMPRESSION = { NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE }
       [ ON PARTITIONS ( { partition_number_expression | <range> }
       [ , ...n ] ) ]
  | XML_COMPRESSION = { ON | OFF }
      [ ON PARTITIONS ( { <partition_number_expression> | <range> }
      [ , ...n ] ) ] ]
}
```

The following syntax is for Azure Synapse Analytics and Parallel Data Warehouse:

```syntaxsql
DECLARE
{ { @local_variable [AS] data_type } [ = value [ COLLATE <collation_name> ] ] } [ ,...n ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### @*local_variable*

The name of a variable. Variable names must begin with an at (@) sign. Local variable names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

*data_type*  
Any system-supplied, common language runtime (CLR) user-defined table type, or alias data type. A variable can't be of **text**, **ntext**, or **image** data type.

For more information about system data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md). For more information about CLR user-defined types or alias data types, see [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md).

= *value*  
Assigns a value to the variable in-line. The value can be a constant or an expression, but it must either match the variable declaration type or be implicitly convertible to that type. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).

#### @*cursor_variable_name*

The name of a cursor variable. Cursor variable names must begin with an at (@) sign and conform to the rules for identifiers.

CURSOR  
Specifies that the variable is a local cursor variable.

@*table_variable_name*  
The name of a variable of type **table**. Variable names must begin with an at  (@) sign and conform to the rules for identifiers.

\<table_type_definition>  
Defines the **table** data type. The table declaration includes column definitions, names, data types, and constraints. The only constraint types allowed are PRIMARY KEY, UNIQUE, NULL, and CHECK. An alias data type can't be used as a column scalar data type if a rule or default definition is bound to the type.

#### \<table_type_definition>

A subset of information used to define a table in CREATE TABLE. Elements and essential definitions are included here. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).

*n*  
A placeholder indicating that multiple variables can be specified and assigned values. When declaring **table** variables, the **table** variable must be the only variable being declared in the DECLARE statement.

#### *column_name*

The name of the column in the table.

*scalar_data_type*  
Specifies that the column is a scalar data type.

*computed_column_expression*  
An expression defining the value of a computed column. It is computed from an expression using other columns in the same table. For example, a computed column can have the definition **cost** AS **price \* qty**. The expression can be a noncomputed column name, constant, built-in function, variable, or any combination of these connected by one or more operators. The expression can't be a subquery or a user-defined function. The expression can't reference a CLR user-defined type.

#### [ COLLATE *collation_name* ]

Specifies the collation for the column. *collation_name* can be either a Windows collation name or an SQL collation name, and is applicable only for columns of the **char**, **varchar**, **text**, **nchar**, **nvarchar**, and **ntext** data types. If not specified, the column is assigned either the collation of the user-defined data type (if the column is of a user-defined data type) or the collation of the current database.

For more information about the Windows and SQL collation names, see [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md).

#### DEFAULT

Specifies the value provided for the column when a value isn't explicitly supplied during an insert. DEFAULT definitions can be applied to any columns except those defined as **timestamp** or those with the IDENTITY property. DEFAULT definitions are removed when the table is dropped. Only a constant value, such as a character string; a system function, such as a SYSTEM_USER(); or NULL can be used as a default. To maintain compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a constraint name can be assigned to a DEFAULT.

*constant_expression*  
A constant, NULL, or a system function used as the default value for the column.

#### IDENTITY

Indicates that the new column is an identity column. When a new row is added to the table, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a unique incremental value for the column. Identity columns are commonly used with PRIMARY KEY constraints to serve as the unique row identifier for the table. The IDENTITY property can be assigned to **tinyint**, **smallint**, **int**, **decimal(p,0)**, or **numeric(p,0)** columns. Only one identity column can be created per table. Bound defaults and DEFAULT constraints can't be used with an identity column. You must specify both the seed and increment, or neither. If neither is specified, the default is (1,1).

*seed*  
The value used for the first row loaded into the table.

*increment*  
The incremental value added to the identity value of the previous row that was loaded.

#### ROWGUIDCOL

Indicates that the new column is a row global unique identifier column. Only one **uniqueidentifier** column per table can be designated as the ROWGUIDCOL column. The ROWGUIDCOL property can be assigned only to a **uniqueidentifier** column.

#### NULL | NOT NULL

Indicates if null is allowed in the variable. The default is **NULL**.

#### PRIMARY KEY

A constraint that enforces entity integrity for a given column or columns through a unique index. Only one PRIMARY KEY constraint can be created per table.

#### UNIQUE

A constraint that provides entity integrity for a given column or columns through a unique index. A table can have multiple UNIQUE constraints.

#### CLUSTERED | NONCLUSTERED

Indicate that a clustered or a nonclustered index is created for the PRIMARY KEY or UNIQUE constraint. PRIMARY KEY constraints use CLUSTERED, and UNIQUE constraints use NONCLUSTERED.

CLUSTERED can be specified for only one constraint. If CLUSTERED is specified for a UNIQUE constraint and a PRIMARY KEY constraint is also specified, the PRIMARY KEY uses NONCLUSTERED.

#### CHECK

A constraint that enforces domain integrity by limiting the possible values that can be entered into a column or columns.

*logical_expression*  
A logical expression that returns TRUE or FALSE.

#### \<index_option>

Specifies one or more index options. Indexes can't be created explicitly on table variables, and no statistics are kept on table variables. Starting with SQL [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)], new syntax was introduced which allows you to create certain index types inline with the table definition. Using this new syntax, you can create indexes on table variables as part of the table definition. In some cases, performance may improve by using temporary tables instead, which provide full index support and statistics. 

For a complete description of these options, see [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md).

## Table variables and row estimates

Table variables don't have distribution statistics. In many cases, the optimizer will build a query plan on the assumption that the table variable has zero rows or one row. For more information, review [table data type - Limitations and restrictions](../data-types/table-transact-sql.md#limitations-and-restrictions).

For this reason, you should be cautious about using a table variable if you expect a larger number of rows (greater than 100). Consider the following alternatives:

 - Temp tables may be a better solution than table variables when it is possible for the rowcount to be larger (greater than 100). 
 - For queries that join the table variable with other tables, use the RECOMPILE hint, which will cause the optimizer to use the correct cardinality for the table variable. 
- In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], the table variable deferred compilation feature will propagate cardinality estimates that are based on actual table variable row counts, providing a more accurate row count for optimizing the execution plan. For more information, see [Intelligent query processing in SQL databases](../../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation).

## Remarks

Variables are often used in a batch or procedure as counters for WHILE, LOOP, or for an IF...ELSE block.

Variables can be used only in expressions, not in place of object names or keywords. To construct dynamic SQL statements, use EXECUTE.

The scope of a local variable is the batch in which it's declared.

A table variable isn't necessarily memory resident. Under memory pressure, the pages belonging to a table variable can be pushed out to `tempdb`.

You can define an inline index in a table variable.

A cursor variable that currently has a cursor assigned to it can be referenced as a source in a:

- CLOSE statement
- DEALLOCATE statement
- FETCH statement
- OPEN statement
- Positioned DELETE or UPDATE statement
- SET CURSOR variable statement (on the right side)

In all of these statements, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] raises an error if a referenced cursor variable exists but doesn't have a cursor currently allocated to it. If a referenced cursor variable doesn't exist, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] raises the same error raised for an undeclared variable of another type.

A cursor variable:

- Can be the target of either a cursor type or another cursor variable. For more information, see [SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md).

- Can be referenced as the target of an output cursor parameter in an EXECUTE statement if the cursor variable doesn't have a cursor currently assigned to it.

- Should be regarded as a pointer to the cursor.

## Examples

### A. Using DECLARE

The following example uses a local variable named `@find` to retrieve contact information for all last names beginning with `Man`.

```sql
USE AdventureWorks2012;
GO
DECLARE @find VARCHAR(30);
/* Also allowed:
DECLARE @find VARCHAR(30) = 'Man%';
*/
SET @find = 'Man%';
SELECT p.LastName, p.FirstName, ph.PhoneNumber
FROM Person.Person AS p
JOIN Person.PersonPhone AS ph ON p.BusinessEntityID = ph.BusinessEntityID
WHERE LastName LIKE @find;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
LastName            FirstName               Phone
------------------- ----------------------- -------------------------
Manchepalli         Ajay                    1 (11) 500 555-0174
Manek               Parul                   1 (11) 500 555-0146
Manzanares          Tomas                   1 (11) 500 555-0178

(3 row(s) affected)
```

### B. Using DECLARE with two variables

The following example retrieves the names of [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] sales representatives who are located in the North American sales territory and have at least $2,000,000 in sales for the year.

```sql
USE AdventureWorks2012;
GO
SET NOCOUNT ON;
GO
DECLARE @Group nvarchar(50), @Sales MONEY;
SET @Group = N'North America';
SET @Sales = 2000000;
SET NOCOUNT OFF;
SELECT FirstName, LastName, SalesYTD
FROM Sales.vSalesPerson
WHERE TerritoryGroup = @Group and SalesYTD >= @Sales;
```

### C. Declaring a variable of type table

The following example creates a `table` variable that stores the values specified in the OUTPUT clause of the UPDATE statement. Two `SELECT` statements follow that return the values in `@MyTableVar` and the results of the update operation in the `Employee` table. The results in the `INSERTED.ModifiedDate` column differ from the values in the `ModifiedDate` column in the `Employee` table. This is because the `AFTER UPDATE` trigger, which updates the value of `ModifiedDate` to the current date, is defined on the `Employee` table. However, the columns returned from `OUTPUT` reflect the data before triggers are fired. For more information, see [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md).

```sql
USE AdventureWorks2012;
GO
DECLARE @MyTableVar TABLE (
    EmpID INT NOT NULL,
    OldVacationHours INT,
    NewVacationHours INT,
    ModifiedDate DATETIME);
UPDATE TOP (10) HumanResources.Employee
SET VacationHours = VacationHours * 1.25
OUTPUT INSERTED.BusinessEntityID,
       DELETED.VacationHours,
       INSERTED.VacationHours,
       INSERTED.ModifiedDate
INTO @MyTableVar;
--Display the result set of the table variable.
SELECT EmpID, OldVacationHours, NewVacationHours, ModifiedDate
FROM @MyTableVar;
GO
--Display the result set of the table.
--Note that ModifiedDate reflects the value generated by an
--AFTER UPDATE trigger.
SELECT TOP (10) BusinessEntityID, VacationHours, ModifiedDate
FROM HumanResources.Employee;
GO
```

### D. Declaring a variable of type table, with inline indexes

The following example creates a `table` variable with a clustered inline index and two nonclustered inline indexes.

```sql
DECLARE @MyTableVar TABLE (
    EmpID INT NOT NULL,
    PRIMARY KEY CLUSTERED (EmpID),
    UNIQUE NONCLUSTERED (EmpID),
    INDEX CustomNonClusteredIndex NONCLUSTERED (EmpID)
);
GO
```

The following query returns information about the indexes created in the previous query.

```sql
SELECT *
FROM tempdb.sys.indexes
WHERE object_id < 0;
GO
```

### E. Declaring a variable of user-defined table type

The following example creates a table-valued parameter or table variable called `@LocationTVP`. This requires a corresponding user-defined table type called `LocationTableType`. For more information about how to create a user-defined table type, see [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md). For more information about table-valued parameters, see [Use Table-Valued Parameters &#40;Database Engine&#41;](../../relational-databases/tables/use-table-valued-parameters-database-engine.md).

```sql
DECLARE @LocationTVP
AS LocationTableType;
```

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### F. Using DECLARE

The following example uses a local variable named `@find` to retrieve contact information for all last names beginning with `Walt`.

```sql
-- Uses AdventureWorks

DECLARE @find VARCHAR(30);
/* Also allowed:
DECLARE @find VARCHAR(30) = 'Man%';
*/
SET @find = 'Walt%';

SELECT LastName, FirstName, Phone
FROM DimEmployee
WHERE LastName LIKE @find;
```

### G. Using DECLARE with two variables

The following example retrieves uses variables to specify the first and last names of employees in the `DimEmployee` table.

```sql
-- Uses AdventureWorks

DECLARE @lastName VARCHAR(30), @firstName VARCHAR(30);

SET @lastName = 'Walt%';
SET @firstName = 'Bryan';

SELECT LastName, FirstName, Phone
FROM DimEmployee
WHERE LastName LIKE @lastName AND FirstName LIKE @firstName;
```

## See also

- [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)
- [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [table &#40;Transact-SQL&#41;](../../t-sql/data-types/table-transact-sql.md)
- [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)
