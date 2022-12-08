---
title: CREATE XML INDEX (Transact-SQL)
description: CREATE XML INDEX (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/09/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "CREATE_XML_INDEX_TSQL"
  - "XML INDEX"
  - "CREATE_XML_TSQL"
  - "XML"
  - "CREATE XML"
  - "CREATE XML INDEX"
  - "XML_INDEX_TSQL"
helpviewer_keywords:
  - "CREATE XML INDEX statement"
  - "CREATE INDEX statement"
  - "index creation [SQL Server], XML indexes"
  - "XML indexes [SQL Server], creating"
dev_langs:
  - "TSQL"
---

# CREATE XML INDEX (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Creates an XML index on a specified table. An index can be created before there is data in the table. XML indexes can be created on tables in another database by specifying a qualified database name.

> [!NOTE]
> To create a relational index, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md). For information about how to create a spatial index, see [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-spatial-index-transact-sql.md).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
--Create XML Index   
CREATE [ PRIMARY ] XML INDEX index_name
    ON <object> ( xml_column_name )
    [ USING XML INDEX xml_index_name
        [ FOR { VALUE | PATH | PROPERTY } ] ]
    [ WITH ( <xml_index_option> [ ,...n ] ) ]
[ ; ]

<object> ::=
{ database_name.schema_name.table_name | schema_name.table_name | table_name }

<xml_index_option> ::=
{
    PAD_INDEX  = { ON | OFF }
  | FILLFACTOR = fillfactor
  | SORT_IN_TEMPDB = { ON | OFF }
  | IGNORE_DUP_KEY = OFF
  | DROP_EXISTING = { ON | OFF }
  | ONLINE = OFF
  | ALLOW_ROW_LOCKS = { ON | OFF }
  | ALLOW_PAGE_LOCKS = { ON | OFF }
  | MAXDOP = max_degree_of_parallelism
  | XML_COMPRESSION = { ON | OFF }
}
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### [PRIMARY] XML

Creates an XML index on the specified **xml** column. When PRIMARY is specified, a clustered index is created with the clustered key formed from the clustering key of the user table and an XML node identifier. Each table can have up to 249 XML indexes. Note the following when you create an XML index:

- A clustered index must exist on the primary key of the user table.

- The clustering key of the user table is limited to 15 columns.

- Each **xml** column in a table can have one primary XML index and multiple secondary XML indexes.

- A primary XML index on an **xml** column must exist before a secondary XML index can be created on the column.

- An XML index can only be created on a single **xml** column. You can't create an XML index on a non-**xml** column, nor can you create a relational index on an **xml** column.

- You can't create an XML index, either primary or secondary, on an **xml** column in a view, on a table-valued variable with **xml** columns, or **xml** type variables.

- You can't create a primary XML index on a computed **xml** column.

- The SET option settings must be the same as those required for indexed views and computed column indexes. Specifically, the option ARITHABORT must be set to ON when an XML index is created and when inserting, deleting, or updating values in the **xml** column.
  
For more information, see [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md).

#### *index_name*

The name of the index. Index names must be unique within a table but don't have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).

Primary XML index names can't start with the following characters: `#`, `##`, `@`, or `@@`.

#### *xml_column_name*

The **xml** column on which the index is based. Only one **xml** column can be specified in a single XML index definition; however, multiple secondary XML indexes can be created on an **xml** column.
  
#### USING XML INDEX *xml_index_name*

Specifies the primary XML index to use in creating a secondary XML index.

#### FOR { VALUE | PATH | PROPERTY }

Specifies the type of secondary XML index.

VALUE  
Creates a secondary XML index on columns where key columns are (node value and path) of the primary XML index.

PATH  
Creates a secondary XML index on columns built on path values and node values in the primary XML index. In the PATH secondary index, the path and node values are key columns that allow efficient seeks when searching for paths.

PROPERTY  
Creates a secondary XML index on columns (PK, path and node value) of the primary XML index where PK is the primary key of the base table.

#### \<object>::=

The fully qualified or nonfully qualified object to be indexed.

*database_name*  
The name of the database.

*schema_name*  
The name of the schema to which the table belongs.

*table_name*  
The name of the table to be indexed.

#### \<xml_index_option> ::=

Specifies the options to use when you create the index.

#### PAD_INDEX = { ON | OFF }

Specifies index padding. The default is **OFF**.

ON  
The percentage of free space that is specified by *fillfactor* is applied to the intermediate-level pages of the index.

OFF or *fillfactor* isn't specified  
The intermediate-level pages are filled to near capacity, leaving sufficient space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages.

The PAD_INDEX option is useful only when FILLFACTOR is specified, because PAD_INDEX uses the percentage specified by FILLFACTOR. If the percentage specified for FILLFACTOR isn't large enough to allow for one row, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] internally overrides the percentage to allow for the minimum. The number of rows on an intermediate index page is never less than two, regardless of how low the value of *fillfactor*.

#### FILLFACTOR = *fillfactor*

Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or rebuild. *fillfactor* must be an integer value from 1 to 100. The default is 0. If *fillfactor* is 100 or 0, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates indexes with leaf pages filled to capacity.

> [!NOTE]  
> Fill factor values 0 and 100 are the same in all respects.

The FILLFACTOR setting applies only when the index is created or rebuilt. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] doesn't dynamically keep the specified percentage of empty space in the pages. To view the fill factor setting, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.

> [!IMPORTANT]  
> Creating a clustered index with a FILLFACTOR less than 100 affects the amount of storage space the data occupies because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] redistributes the data when it creates the clustered index.

For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).

#### SORT_IN_TEMPDB = { ON | OFF }

Specifies whether to store temporary sort results in **tempdb**. The default is **OFF**.

ON  
The intermediate sort results that are used to build the index are stored in **tempdb**. This may reduce the time required to create an index if **tempdb** is on a different set of disks than the user database. However, this increases the amount of disk space that is used during the index build.

OFF  
The intermediate sort results are stored in the same database as the index.

In addition to the space required in the user database to create the index, **tempdb** must have about the same amount of additional space to hold the intermediate sort results. For more information, see [SORT_IN_TEMPDB Option For Indexes](../../relational-databases/indexes/sort-in-tempdb-option-for-indexes.md).

#### IGNORE_DUP_KEY = OFF

Has no effect for XML indexes because the index type is never unique. Don't set this option to ON, or else an error is raised.

#### DROP_EXISTING = { ON | OFF }

Specifies that the named, preexisting XML index is dropped and rebuilt. The default is **OFF**.

ON  
The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, sort order, partition scheme, or index options.

OFF  
An error is displayed if the specified index name already exists.

The index type can't be changed by using DROP_EXISTING. Also, a primary XML index can't be redefined as a secondary XML index, or vice versa.

#### ONLINE = OFF

Specifies that underlying tables and associated indexes aren't available for queries and data modification during the index operation. In this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], online index builds aren't supported for XML indexes. If this option is set to ON for an XML index, an error is raised. Either omit the ONLINE option or set ONLINE to OFF.

An offline index operation that creates, rebuilds, or drops an XML index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table during the operation.

> [!NOTE]  
> Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).

#### ALLOW_ROW_LOCKS = { ON | OFF }

Specifies whether row locks are allowed. The default is **ON**.

ON  
Row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.

OFF  
Row locks aren't used.

#### ALLOW_PAGE_LOCKS = { ON | OFF }

Specifies whether page locks are allowed. The default is **ON**.

ON  
Page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.

OFF  
Page locks aren't used.

#### MAXDOP = *max_degree_of_parallelism*

Overrides the [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) configuration option during the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

> [!IMPORTANT]  
> Although the MAXDOP option is syntactically supported for all XML indexes, for a primary XML index, CREATE XML INDEX uses only a single processor.

*max_degree_of_parallelism* can be:

1  
Suppresses parallel plan generation.

\>1  
Restricts the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload.

0 (default)  
Uses the actual number of processors or fewer based on the current system workload.

For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).

> [!NOTE]  
> Parallel index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).

## Remarks

Computed columns derived from **xml** data types can be indexed either as a key or included nonkey column as long as the computed column data type is allowable as an index key column or nonkey column. You can't create a primary XML index on a computed **xml** column.

To view information about XML indexes, use the [sys.xml_indexes](../../relational-databases/system-catalog-views/sys-xml-indexes-transact-sql.md) catalog view.

For more information about XML indexes, see [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md).

### XML compression

**Applies to**: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

- XML indexes don't inherit the compression property of the table. To compress indexes, you must explicitly enable XML compression on XML indexes.
- Secondary XML indexes don't inherit the compression property of the Primary XML index.
- By default, the XML compression setting for XML indexes is set to OFF when the index is created.

## Additional remarks on index creation

For more information about index creation, see the "Remarks" section in [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).

## Examples

The following examples use the `AdventureWorks2012` sample database.

### A. Creating a primary XML index

The following example creates a primary XML index on the `CatalogDescription` column in the `Production.ProductModel` table.

```sql
IF EXISTS (SELECT * FROM sys.indexes
            WHERE name = N'PXML_ProductModel_CatalogDescription')
    DROP INDEX PXML_ProductModel_CatalogDescription
        ON Production.ProductModel;  
GO  
CREATE PRIMARY XML INDEX PXML_ProductModel_CatalogDescription
    ON Production.ProductModel (CatalogDescription);  
GO
```

### B. Creating a primary XML index with XML compression

The following example creates a primary XML index on the `CatalogDescription` column in the `Production.ProductModel` table.

```sql
IF EXISTS (SELECT * FROM sys.indexes
            WHERE name = N'PXML_ProductModel_CatalogDescription')
    DROP INDEX PXML_ProductModel_CatalogDescription
        ON Production.ProductModel;  
GO  
CREATE PRIMARY XML INDEX PXML_ProductModel_CatalogDescription
    ON Production.ProductModel (CatalogDescription)
    WITH (XML_COMPRESSION = ON);  
GO
```

### C. Creating a secondary XML index

The following example creates a secondary XML index on the `CatalogDescription` column in the `Production.ProductModel` table.

```sql
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IXML_ProductModel_CatalogDescription_Path')
    DROP INDEX IXML_ProductModel_CatalogDescription_Path
        ON Production.ProductModel;  
GO  
CREATE XML INDEX IXML_ProductModel_CatalogDescription_Path
    ON Production.ProductModel (CatalogDescription)
    USING XML INDEX PXML_ProductModel_CatalogDescription FOR PATH ;
GO
```

### D. Creating a secondary XML index with XML compression

The following example creates a secondary XML index on the `CatalogDescription` column in the `Production.ProductModel` table.

```sql
IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IXML_ProductModel_CatalogDescription_Path')
    DROP INDEX IXML_ProductModel_CatalogDescription_Path
        ON Production.ProductModel;  
GO  
CREATE XML INDEX IXML_ProductModel_CatalogDescription_Path
    ON Production.ProductModel (CatalogDescription)
    USING XML INDEX PXML_ProductModel_CatalogDescription FOR PATH
    WITH (XML_COMPRESSION = ON);
GO
```

## See also

- [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)
- [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)
- [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)
- [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)
- [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-spatial-index-transact-sql.md)
- [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)
- [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
- [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)
- [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md)
- [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)
- [sys.xml_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-xml-indexes-transact-sql.md)
- [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)
- [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md)
