---
title: "sp_help (Transact-SQL)"
description: Reports information about a database object (any object listed in the sys.sysobjects compatibility view), a user-defined data type, or a data type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help"
  - "sp_help_TSQL"
helpviewer_keywords:
  - "sp_help"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Reports information about a database object (any object listed in the `sys.sysobjects` compatibility view), a user-defined data type, or a data type.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help [ [ @objname = ] N'objname' ]
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

The name of any object, in `sys.sysobjects` or any user-defined data type in the `sys.systypes` table. *@objname* is **nvarchar(776)**, with a default of `NULL`. Database names aren't acceptable. Two or three part names might be delimited, such as `Person.AddressType` or `[Person].[AddressType]`.

## Return code values

`0` (success) or `1` (failure).

## Result set

The result sets that are returned depend on whether *@name* is specified, when it's specified, and which database object it is.

1. If `sp_help` is executed with no arguments, summary information of objects of all types that exist in the current database is returned.

   | Column name | Data type | Description |
   | --- | --- | --- |
   | `Name` | **nvarchar(128)** | Object name |
   | `Owner` | **nvarchar(128)** | Object owner (The database principal that owns object. Defaults to the owner of the schema that contains the object.) |
   | `Object_type` | **nvarchar(31)** | Object type |

1. If *@name* is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type or user-defined data type, `sp_help` returns this result set.

   | Column name | Data type | Description |
   | --- | --- | --- |
   | `Type_name` | **nvarchar(128)** | Data type name. |
   | `Storage_type` | **nvarchar(128)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] type name. |
   | `Length` | **smallint** | Physical length of the data type (in bytes). |
   | `Prec` | **int** | Precision (total number of digits). |
   | `Scale` | **int** | Number of digits to the right of the decimal. |
   | `Nullable` | **varchar(35)** | Indicates whether `NULL` values are allowed: `Yes` or `No`. |
   | `Default_name` | **nvarchar(128)** | Name of a default bound to this type.<br /><br />`NULL` = No default is bound. |
   | `Rule_name` | **nvarchar(128)** | Name of a rule bound to this type.<br /><br />`NULL` = No default is bound. |
   | `Collation` | **sysname** | Collation of the data type. `NULL` for non-character data types. |

1. If *@name* is any database object other than a data type, `sp_help` returns this result set and also additional result sets, based on the type of object specified.

   | Column name | Data type | Description |
   | --- | --- | --- |
   | `Name` | **nvarchar(128)** | Table name |
   | `Owner` | **nvarchar(128)** | Table owner |
   | `Type` | **nvarchar(31)** | Table type |
   | `Created_datetime` | **datetime** | Date table created |

   Depending on the database object specified, `sp_help` returns additional result sets.

   If *@name* is a system table, user table, or view, `sp_help` returns the following result sets. However, the result set that describes where the data file is located on a file group isn't returned for a view.

   - The following result set is also returned on column objects:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `Column_name` | **nvarchar(128)** | Column name. |
     | `Type` | **nvarchar(128)** | Column data type. |
     | `Computed` | **varchar(35)** | Indicates whether the values in the column are computed: `Yes` or `No`. |
     | `Length` | **int** | Column length in bytes.<br /><br />**Note:** If the column data type is a large value type (**varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**), the value displays as `-1`. |
     | `Prec` | **char(5)** | Column precision. |
     | `Scale` | **char(5)** | Column scale. |
     | `Nullable` | **varchar(35)** | Indicates whether `NULL` values are allowed in the column: `Yes` or `No`. |
     | `TrimTrailingBlanks` | **varchar(35)** | Trim the trailing blanks. Returns `Yes` or `No`. |
     | `FixedLenNullInSource` | **varchar(35)** | For backward compatibility only. |
     | `Collation` | **sysname** | Collation of the column. `NULL` for noncharacter data types. |

   - The following result set is also returned on identity columns:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `Identity` | **nvarchar(128)** | Column name whose data type is declared as identity. |
     | `Seed` | **numeric** | Starting value for the identity column. |
     | `Increment` | **numeric** | Increment to use for values in this column. |
     | `Not For Replication` | **int** | `IDENTITY` property isn't enforced when a replication login, such as **sqlrepl**, inserts data into the table:<br /><br />`1` = True<br />`0` = False |

   - The following result set is also returned on columns:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `RowGuidCol` | **sysname** | Name of the global unique identifier column. |

   - The following result set is also returned on filegroups:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `Data_located_on_filegroup` | **nvarchar(128)** | Filegroup in which the data is located: `Primary`, `Secondary`, or `Transaction Log`. |

   - The following result set is also returned on indexes:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `index_name` | **sysname** | Index name. |
     | `Index_description` | **varchar(210)** | Description of the index. |
     | `index_keys` | **nvarchar(2078)** | Column names on which the index is built. Returns `NULL` for memory optimized columnstore indexes. |

   - The following result set is also returned on constraints:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `constraint_type` | **nvarchar(146)** | Type of constraint. |
     | `constraint_name` | **nvarchar(128)** | Name of the constraint. |
     | `delete_action` | **nvarchar(9)** | Indicates whether the `DELETE` action is one of `NO_ACTION`, `CASCADE`, `SET_NULL`, `SET_DEFAULT`, or `N/A`.<br /><br />Only applicable to FOREIGN KEY constraints. |
     | `update_action` | **nvarchar(9)** | Indicates whether the `UPDATE` action is one of `NO_ACTION`, `CASCADE`, `SET_NULL`, `SET_DEFAULT`, or `N/A`.<br /><br />Only applicable to `FOREIGN KEY` constraints. |
     | `status_enabled` | **varchar(8)** | Indicates whether the constraint is enabled: `Enabled`, `Disabled`, or `N/A`.<br /><br />Only applicable to `CHECK` and `FOREIGN KEY` constraints. |
     | `status_for_replication` | **varchar(19)** | Indicates whether the constraint is for replication.<br /><br />Only applicable to `CHECK` and `FOREIGN KEY` constraints. |
     | `constraint_keys` | **nvarchar(2078)** | Names of the columns that make up the constraint or, in the case for defaults and rules, the text that defines the default or rule. |

   - The following result set is also returned on referencing objects:

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `Table is referenced by` | **nvarchar(516)** | Identifies other database objects that reference the table. |

   - The following result set is also returned on stored procedures, functions, or extended stored procedures.

     | Column name | Data type | Description |
     | --- | --- | --- |
     | `Parameter_name` | **nvarchar(128)** | Stored procedure parameter name. |
     | `Type` | **nvarchar(128)** | Data type of the stored procedure parameter. |
     | `Length` | **smallint** | Maximum physical storage length, in bytes. |
     | `Prec` | **int** | Precision or total number of digits. |
     | `Scale` | **int** | Number of digits to the right of the decimal point. |
     | `Param_order` | **smallint** | Order of the parameter. |

## Remarks

The `sp_help` procedure looks for an object in the current database only.

When *@name* isn't specified, `sp_help` lists object names, owners, and object types for all objects in the current database. `sp_helptrigger` provides information about triggers.

`sp_help` exposes only orderable index columns; therefore, it doesn't expose information about XML indexes or spatial indexes.

## Permissions

Requires membership in the **public** role. The user must have at least one permission on *@objname*. To view column constraint keys, defaults, or rules, you must have `VIEW DEFINITION` permission on the table.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return information about all objects

The following example lists information about each object in the `master` database.

```sql
USE master;
GO
EXEC sp_help;
GO
```

### B. Return information about a single object

The following example displays information about the `Person.Person` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_help 'Person.Person';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_helpindex (Transact-SQL)](sp-helpindex-transact-sql.md)
- [sp_helprotect (Transact-SQL)](sp-helprotect-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [sp_helptrigger (Transact-SQL)](sp-helptrigger-transact-sql.md)
- [sp_helpuser (Transact-SQL)](sp-helpuser-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sysobjects (Transact-SQL)](../system-compatibility-views/sys-sysobjects-transact-sql.md)
