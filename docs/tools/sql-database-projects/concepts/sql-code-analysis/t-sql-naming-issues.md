---
title: "T-SQL naming issues"
description: "Naming issue rules included with SQL code analysis."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
---

# T-SQL naming issues

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you analyze the T-SQL code in your database project, one or more warnings might be categorized as naming issues. You should address naming issues to avoid the following situations:

- The name that you specified for an object might conflict with the name of a system object.
- The name that you specified always needs to be enclosed in escape characters (in SQL Server, '[' and ']').
- The name that you specified might confuse others who try to read and understand your code.
- The code might break if you run it with future releases of SQL Server.

In general, you might suppress a naming issue if other applications that you can't change depend on the current name.

The provided rules identify the following naming issues:

- [SR0011: Avoid using special characters in object names](#sr0011-avoid-using-special-characters-in-object-names)
- [SR0012: Avoid using reserved words for type names](#sr0012-avoid-using-reserved-words-for-type-names)
- [SR0016: Avoid using sp_ as a prefix for stored procedures](#sr0016-avoid-using-sp_-as-a-prefix-for-stored-procedures)

## SR0011: Avoid using special characters in object names

If you name a database object by using any character in the following table, you make it more difficult not only to reference that object but also to read code that contains the name of that object:

| Character | Description |
| --- | --- |
| ` ` | Whitespace character |
| `[` | Left square bracket |
| `]` | Right square bracket |
| `'` | Single quotation mark |
| `"` | Double quotation mark |

### How to fix violations

To resolve this issue, you must remove all special characters from the object name. If the object is referenced in other locations in your database project (such as in database unit tests), you should use database refactoring to update the references. For more information, see Rename All References to a Database Object.

### Example

In the first example, a table contains a column that has a special character in its name. In the second example, the name doesn't contain a special character.

```sql
CREATE TABLE [dbo].[TableWithProblemColumn]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[Small'String] VARCHAR(10)
)
ON [PRIMARY]

CREATE TABLE [dbo].[FixedTable]
(
[ID] INT NOT NULL IDENTITY(0, 1),
[SmallString] VARCHAR(10)
)
ON [PRIMARY]
```

## SR0012: Avoid using reserved words for type names

You should avoid using a reserved word as the name of a user-defined type because readers have a harder time understanding your database code. You can use reserved words in SQL Server as identifiers and object names only if you use delimited identifiers. For more information, see the [full list of reserved keywords](../../../../t-sql/language-elements/reserved-keywords-transact-sql.md).

### How to fix violations

You must rename the user-defined type or object name.

### Example

The first example shows the definition for a user-defined type that triggers this warning. The second example shows one way to correct the user-defined type and resolve the issue.

```sql
-- Potential misuse of a keyword as a type name
CREATE TYPE Alter
FROM nvarchar(11) NOT NULL;

-- Corrected type name
CREATE TYPE AlterType
FROM nvarchar(11) NOT NULL;
```

## SR0016: Avoid using sp_ as a prefix for stored procedures

In SQL Server, the `sp_` prefix designates system stored procedures. If you use that prefix for your stored procedures, the name of your procedure might conflict with the name of a system stored procedure that will be created in the future. If such a conflict occurs, your application might break if your application refers to the procedure without qualifying the reference by schema. In this situation, the name binds to the system procedure instead of to your procedure.

### How to fix violations

To resolve this issue, you must replace `sp_` with a different prefix to designate user stored procedures, or you must use no prefix at all.

### Example

In the first example, the procedure name causes this warning to be issued. In the second example, the procedure uses an `usp_` prefix instead of `sp_` and avoids the warning.

```sql
CREATE PROCEDURE [dbo].[sp_procWithWarning]
(
@Value1 INT,
)
AS
BEGIN
-- Additional statements here
RETURN 0;
END

CREATE PROCEDURE [dbo].[usp_procFixed]
(
@Value1 INT,
)
AS
BEGIN
-- Additional statements here
RETURN 0;
END
```

## Related content

- [SQL code analysis to improve code quality](sql-code-analysis.md)
- [T-SQL design issues](t-sql-design-issues.md)
- [T-SQL performance issues](t-sql-performance-issues.md)
- [Analyze T-SQL code to find defects](../../howto/analyze-t-sql-code-to-find-defects.md)
