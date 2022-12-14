---
title: "Create XML Indexes"
description: Learn how to create primary and secondary XML indexes in SQL Server.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "indexes [XML in SQL Server]"
  - "XML indexes [SQL Server], creating"
author: MikeRayMSFT
ms.author: mikeray
---
# Create XML indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to create primary and secondary XML indexes.

## Create a primary XML index

To create a primary XML index, use the [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] DDL statement. Not all options available for non-XML indexes are supported on XML indexes.

Note the following when you're creating an XML index:

- To create a primary XML index, the table that contains the XML column being indexed, called the base table, must have a clustered index on the primary key. This clustered index makes sure that if the base table is partitioned, the primary XML index can be partitioned by using the same partitioning scheme and partitioning function.

- If an XML index exists, the clustered, primary key of the table can't be modified. You'll have to drop all XML indexes on the table before modifying the primary key.

- A primary XML index can be created on a single **xml** type column. You can't create any other type of index with the **xml** type column as a key column. However, you can include the **xml** type column in a non-XML index. Each **xml** type column in a table can have its own primary XML index. However, only one primary XML index per **xml** type column is permitted.

- XML indexes exist in the same namespace as non-XML indexes. Therefore, you can't have an XML index and a non-XML index on the same table with the same name.

- IGNORE_DUP_KEY and ONLINE options of are always set to OFF for XML indexes. You can specify these options with a value of OFF.

- The filegroup or partitioning information of the user table is applied to the XML index, and can't be specified separately.

- The DROP_EXISTING index option can drop a primary XML index and create a new primary XML index, or drop a secondary XML index and create a new secondary XML index. However, this option can't drop a secondary XML index to create a new primary XML index or vice versa.

- Primary XML index names have the same restrictions as view names.

  You can't create an XML index on an **xml** type column in a view, on a **table** valued variable with **xml** type columns, or **xml** type variables.

- To change an **xml** type column from untyped to typed XML, or vice versa, by using the ALTER TABLE ALTER COLUMN option, no XML index on the column should exist. If one does exist, it must be dropped before the column type change is tried.

- The option ARITHABORT must be set to ON when an XML index is created. To query, insert, delete, or update values in the XML column using **xml** data type methods, the same option must be set on the connection. If it isn't, the **xml** data type methods will fail.

    > [!NOTE]
    > Information about an XML index can be found in catalog views. However, **sp_helpindex** isn't supported. Examples provided later in this topic show how to query the catalog views to find XML index information.

When creating or recreating a primary XML index on an **xml** data type column that contains values of the XML Schema types `xs:date` or `xs:dateTime` (or any subtypes of these types) that have a year of less than 1, the index creation will fail in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] allowed these values, so this problem can occur when creating indexes in a database generated in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).

### Example: Create a primary XML index

Table `T (pk INT PRIMARY KEY, xCol XML)` with an untyped XML column is used in most of the examples. This example can be extended to typed XML in a straightforward way. For simplicity, queries are described for XML data instances as shown in the following sample:

```xml
<book genre="security" publicationdate="2002" ISBN="0-7356-1588-2">
   <title>Writing Secure Code</title>
   <author>
      <first-name>Michael</first-name>
      <last-name>Howard</last-name>
   </author>
   <author>
      <first-name>David</first-name>
      <last-name>LeBlanc</last-name>
   </author>
   <price>39.99</price>
</book>
```

The following statement creates an XML index, called `idx_xCol`, on the XML column `xCol` of table `T`:

```sql
CREATE PRIMARY XML INDEX idx_xCol on T (xCol)
```

## Create a secondary XML index

Use the [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] DDL statement to create secondary XML indexes and specify the type of the secondary XML index that you want.

Note the following when you're creating secondary XML indexes:

- All indexing options that apply to a nonclustered index, except IGNORE_DUP_KEY and ONLINE, are permitted on secondary XML indexes. The two options must always be set to OFF for secondary XML indexes.

- The secondary indexes are partitioned just like the primary XML index.

- DROP_EXISTING can drop a secondary index on the user table and create another secondary index on the user table.

You can query the **sys.xml_indexes** catalog view to retrieve XML index information. The **secondary_type_desc** column in the **sys.xml_indexes** catalog view provides the type of secondary index:

```sql
SELECT  *
FROM    sys.xml_indexes;
```

The values returned in the `secondary_type_desc` column can be NULL, PATH, VALUE, or PROPERTY. For the primary XML index, the value returned is NULL.

### Example: Create secondary XML indexes

The following example illustrates how secondary XML indexes are created. The example also shows information about the XML indexes that you created.

```sql
CREATE TABLE T (Col1 INT PRIMARY KEY, XmlCol XML);
GO
-- Create primary index.
CREATE PRIMARY XML INDEX PIdx_T_XmlCol
ON T(XmlCol);
GO
-- Create secondary indexes (PATH, VALUE, PROPERTY).
CREATE XML INDEX PIdx_T_XmlCol_PATH ON T(XmlCol)
USING XML INDEX PIdx_T_XmlCol
FOR PATH;
GO
CREATE XML INDEX PIdx_T_XmlCol_VALUE ON T(XmlCol)
USING XML INDEX PIdx_T_XmlCol
FOR VALUE;
GO
CREATE XML INDEX PIdx_T_XmlCol_PROPERTY ON T(XmlCol)
USING XML INDEX PIdx_T_XmlCol
FOR PROPERTY;
GO
```

You can query the `sys.xml_indexes` catalog view to retrieve XML indexes information. The `secondary_type_desc` column provides the secondary index type.

```sql
SELECT  *
FROM    sys.xml_indexes;
```

You can also query the catalog view for index information.

```sql
SELECT *
FROM sys.xml_indexes
WHERE object_id = object_id('T');
```

You can add sample data and then review the XML index information.

```sql
INSERT INTO T VALUES (1,
'<doc id="123">
<sections>
<section num="2">
<heading>Background</heading>
</section>
<section num="3">
<heading>Sort</heading>
</section>
<section num="4">
<heading>Search</heading>
</section>
</sections>
</doc>');
GO
-- Check XML index information.
SELECT *
FROM   sys.dm_db_index_physical_stats (db_id(), object_id('T'), NULL, NULL, 'DETAILED');
GO
-- Space usage of primary XML index
DECLARE @index_id int;
SELECT  @index_id = i.index_id
FROM    sys.xml_indexes i
WHERE   i.name = 'PIdx_T_XmlCol' and object_name(i.object_id) = 'T';

SELECT *
FROM sys.dm_db_index_physical_stats (db_id(), object_id('T') , @index_id, DEFAULT, 'DETAILED');
GO
--- Space usage of secondary XML index (for example PATH secondary index)  PIdx_T_XmlCol_PATH
DECLARE @index_id int;
SELECT  @index_id = i.index_id
FROM    sys.xml_indexes i
WHERE  i.name = 'PIdx_T_XmlCol_PATH' and object_name(i.object_id) = 'T';

SELECT *
FROM sys.dm_db_index_physical_stats (db_id(), object_id('T') , @index_id, DEFAULT, 'DETAILED');
GO

-- Space usage of all secondary XML indexes for a particular table
SELECT i.name, object_name(i.object_id), stats.*
FROM   sys.dm_db_index_physical_stats (db_id(), object_id('T'), NULL, DEFAULT, 'DETAILED') stats
JOIN sys.xml_indexes i ON (stats.object_id = i.object_id and stats.index_id = i.index_id)
WHERE secondary_type is not null;
-- Drop secondary indexes.
DROP INDEX PIdx_T_XmlCol_PATH ON T;
GO
DROP INDEX PIdx_T_XmlCol_VALUE ON T;
GO
DROP INDEX PIdx_T_XmlCol_PROPERTY ON T;
GO
-- Drop primary index.
DROP INDEX PIdx_T_XmlCol ON T;
-- Drop table T.
DROP TABLE T;
GO
```

## See also

- [XML indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md)
- [XML data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)
