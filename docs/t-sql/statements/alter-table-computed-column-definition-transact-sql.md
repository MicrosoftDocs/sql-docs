---
title: "computed_column_definition (Transact-SQL)"
description: "ALTER TABLE computed_column_definition specifies the properties of a computed column that is added to a table by using ALTER TABLE."
author: markingmyname
ms.author: maghan
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "ALTER TABLE statement"
dev_langs:
  - "TSQL"
---
# ALTER TABLE computed_column_definition (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Specifies the properties of a computed column that is added to a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
column_name AS computed_column_expression  
[ PERSISTED [ NOT NULL ] ]  
[   
    [ CONSTRAINT constraint_name ]  
    { PRIMARY KEY | UNIQUE }  
        [ CLUSTERED | NONCLUSTERED ]  
        [ WITH FILLFACTOR = fillfactor ]  
        [ WITH ( <index_option> [, ...n ] ) ]  
        [ ON { partition_scheme_name ( partition_column_name ) | filegroup   
            | "default" } ]  
    | [ FOREIGN KEY ]   
        REFERENCES ref_table [ ( ref_column ) ]   
        [ ON DELETE { NO ACTION | CASCADE } ]   
        [ ON UPDATE { NO ACTION } ]   
        [ NOT FOR REPLICATION ]   
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )  
]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *column_name*  
 Is the name of the column to be altered, added, or dropped. *column_name* can be 1 through 128 characters. For new columns, *column_name* can be omitted for columns created with a **timestamp** data type. If no *column_name* is specified for a **timestamp** data type column, the name **timestamp** is used.  
  
#### *computed_column_expression*  
 Is an expression that defines the value of a computed column. A computed column is a virtual column that is not physically stored in the table but is computed from an expression that uses other columns in the same table. An expression must yield a value. For example, a computed column could have the definition: cost AS price * qty. Another example with bitwise operators: is_finalised AS is_checked | is_approved. The expression can be a noncomputed column name, constant, function, variable, and any combination of these connected by one or more operators. The expression cannot be a search condition, subquery or include an alias data type.  
  
 Computed columns can be used in select lists, WHERE clauses, ORDER BY clauses, or any other locations where regular expressions can be used, but with the following exceptions:  
  
-   A computed column cannot be used as a DEFAULT or FOREIGN KEY constraint definition or with a NOT NULL constraint definition. However, if the computed column value is defined by a deterministic expression and the data type of the result is allowed in index columns, a computed column can be used as a key column in an index or as part of any PRIMARY KEY or UNIQUE constraint.  
  
     For example, if the table has integer columns a and b, the computed column a + b may be indexed, but computed column a + DATEPART(dd, GETDATE()) cannot be indexed, because the value might change in subsequent invocations.  
  
-   A computed column cannot be the target of an INSERT or UPDATE statement.  
  
    > [!NOTE]  
    >  Because each row in a table can have different values for columns involved in a computed column, the computed column may not have the same result for each row.  
  
#### PERSISTED  
 Specifies that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will physically store the computed values in the table, and update the values when any other columns on which the computed column depends are updated. Marking a computed column as PERSISTED allows an index to be created on a computed column that is deterministic, but not precise. For more information, see [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md). Any computed columns used as partitioning columns of a partitioned table must be explicitly marked PERSISTED. *computed_column_expression* must be deterministic when PERSISTED is specified. 

#### NULL | NOT NULL  
 Specifies whether null values are allowed in the column. NULL is not strictly a constraint but can be specified like NOT NULL. NOT NULL can be specified for computed columns only if PERSISTED is also specified.  
  
#### CONSTRAINT  
 Specifies the start of the definition for a PRIMARY KEY or UNIQUE constraint.  
  
*constraint_name*  
 Is the new constraint. Constraint names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name cannot start with a number sign (#). If *constraint_name* is not supplied, a system-generated name is assigned to the constraint.  
  
#### PRIMARY KEY  
 Is a constraint that enforces entity integrity for a specified column or columns by using a unique index. Only one PRIMARY KEY constraint can be created for each table.  
  
#### UNIQUE  
 Is a constraint that provides entity integrity for a specific column or columns by using a unique index.  
  
#### CLUSTERED | NONCLUSTERED  
 Specifies that a clustered or nonclustered index is created for the PRIMARY KEY or UNIQUE constraint. PRIMARY KEY constraints default to CLUSTERED. UNIQUE constraints default to NONCLUSTERED.  
  
 If a clustered constraint or index already exists on a table, CLUSTERED cannot be specified. If a clustered constraint or index already exists on a table, PRIMARY KEY constraints default to NONCLUSTERED.  
  
#### WITH FILLFACTOR =*fillfactor*  
 Specifies how full the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] should make each index page used to store the index data. User-specified *fillfactor* values can be from 1 through 100. If a value is not specified, the default is 0.  
  
> [!IMPORTANT]  
>  Documenting WITH FILLFACTOR = *fillfactor* as the only index option that applies to PRIMARY KEY or UNIQUE constraints is maintained for backward compatibility, but will not be documented in this manner in future releases. Other index options can be specified in the [index_option &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-index-option-transact-sql.md) clause of ALTER TABLE.  
  
#### FOREIGN KEY REFERENCES  
 Is a constraint that provides referential integrity for the data in the column or columns. FOREIGN KEY constraints require that each value in the column exists in the corresponding referenced column or columns in the referenced table. FOREIGN KEY constraints can reference only columns that are PRIMARY KEY or UNIQUE constraints in the referenced table or columns referenced in a UNIQUE INDEX on the referenced table. Foreign keys on computed columns must also be marked PERSISTED.  
  
*ref_table*  
 Is the name of the table referenced by the FOREIGN KEY constraint.  
  
(*ref_column* )  
 Is a column from the table referenced by the FOREIGN KEY constraint.  
  
#### ON DELETE { **NO ACTION** | CASCADE }  
 Specifies what action happens to rows in the table if those rows have a referential relationship and the referenced row is deleted from the parent table. The default is NO ACTION.  
  
NO ACTION  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and the delete action on the row in the parent table is rolled back.

CASCADE  
 Corresponding rows are deleted from the referencing table if that row is deleted from the parent table.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the ProductVendor table has a referential relationship with the Vendor table. The ProductVendor.BusinessEntityID foreign key references the Vendor.BusinessEntityID primary key.  
  
 If a DELETE statement is executed on a row in the Vendor table, and an ON DELETE CASCADE action is specified for ProductVendor.BusinessEntityID, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the ProductVendor table. If any exist, the dependent rows in the ProductVendor table are deleted, in addition to the row referenced in the Vendor table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the delete action on the Vendor row when there is at least one row in the ProductVendor table that references it.  
  
 Do not specify CASCADE if the table will be included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
#### ON UPDATE { **NO ACTION** }  
 Specifies what action happens to rows in the table created when those rows have a referential relationship and the referenced row is updated in the parent table. When NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the update action on the Vendor row if there is at least one row in the ProductVendor table that references it.  
  
#### NOT FOR REPLICATION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.  
  
 Can be specified for FOREIGN KEY constraints and CHECK constraints. If this clause is specified for a constraint, the constraint is not enforced when replication agents perform insert, update, or delete operations.  
  
#### CHECK  
 Is a constraint that enforces domain integrity by limiting the possible values that can be entered into a column or columns. CHECK constraints on computed columns must also be marked PERSISTED.  
  
*logical_expression*  
 Is a logical expression that returns TRUE or FALSE. The expression cannot contain a reference to an alias data type.  
  
#### ON { *partition_scheme_name*(*partition_column_name*) | *filegroup*| "default"}  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.  
  
 Specifies the storage location of the index created for the constraint. If *partition_scheme_name* is specified, the index is partitioned and the partitions are mapped to the filegroups that are specified by *partition_scheme_name*. If *filegroup* is specified, the index is created in the named filegroup. If "default" is specified or if ON is not specified at all, the index is created in the same filegroup as the table. If ON is specified when a clustered index is added for a PRIMARY KEY or UNIQUE constraint, the whole table is moved to the specified filegroup when the clustered index is created.  
  
> [!NOTE]  
>  In this context, default is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON "default" or ON [default]. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
## Remarks  
 Each PRIMARY KEY and UNIQUE constraint generates an index. The number of UNIQUE and PRIMARY KEY constraints cannot cause the number of indexes on the table to exceed 999 nonclustered indexes and 1 clustered index.  

 `SET QUOTED_IDENTIFIER` must be ON when you are creating or changing indexes on computed columns or indexed views. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md).
  
## Next steps

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
- [Specify Computed Columns in a Table](../../relational-databases/tables/specify-computed-columns-in-a-table.md)
- [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md)
