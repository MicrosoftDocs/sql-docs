---
title: "XML Indexes (SQL Server)"
description: Learn how creating XML indexes on xml data type columns can benefit your application by improving query performance.
ms.custom:
- event-tier1-build-2022
ms.date: 05/09/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "removing indexes"
  - "deleting indexes"
  - "secondary indexes [XML in SQL Server]"
  - "xml data type [SQL Server], indexes"
  - "dropping indexes"
  - "PATH index"
  - "DROP_EXISTING clause"
  - "XML [SQL Server], indexes"
  - "primary indexes [XML in SQL Server]"
  - "indexes [SQL Server], XML"
  - "XML indexes [SQL Server], secondary"
  - "BLOBs, XML indexes"
  - "disabling indexes"
  - "XML indexes [SQL Server], modifying"
  - "XML indexes [SQL Server]"
  - "XML indexes [SQL Server], primary"
  - "modifying indexes"
  - "XML indexes [SQL Server], dropping"
  - "VALUE index"
  - "XML indexes [SQL Server], xml data type"
  - "PROPERTY index"
  - "XML indexes [SQL Server], creating"
author: MikeRayMSFT
ms.author: mikeray
---
# XML indexes (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

XML indexes can be created on **xml** data type columns. They index all tags, values and paths over the XML instances in the column and benefit query performance. Your application may benefit from an XML index in the following situations:

- Queries on XML columns are common in your workload. XML index maintenance cost during data modification must be considered.

- Your XML values are relatively large and the retrieved parts are relatively small. Building the index avoids parsing the whole data at run time and benefits index lookups for efficient query processing.

Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can use [XML compression](#xml-compression), which provides a method to compress off-row XML data for both XML columns and indexes, improving capacity requirements.

XML indexes fall into the following categories:

- Primary XML index
- Secondary XML index

The first index on the **xml** type column must be the primary XML index. Using the primary XML index, the following types of secondary indexes are supported: PATH, VALUE, and PROPERTY. Depending on the type of queries, these secondary indexes might help improve query performance.

> [!NOTE]
> You cannot create or modify an XML index unless the database options are set correctly for working with the **xml** data type. For more information, see [Use Full-Text Search with XML Columns](../../relational-databases/xml/use-full-text-search-with-xml-columns.md).

XML instances are stored in **xml** type columns as large binary objects (BLOBs). These XML instances can be large, and the stored binary representation of **xml** data type instances can be up to 2 GB. Without an index, these binary large objects are shredded at run time to evaluate a query. This shredding can be time-consuming. For example, consider the following query:

```sql
;WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS "PD")

SELECT CatalogDescription.query('
  /PD:ProductDescription/PD:Summary
') as Result
FROM Production.ProductModel
WHERE CatalogDescription.exist ('/PD:ProductDescription/@ProductModelID[.="19"]') = 1;
```

To select the XML instances that satisfy the condition in the `WHERE` clause, the XML binary large object (BLOB) in each row of table `Production.ProductModel` is shredded at run time. Then, the expression `(/PD:ProductDescription/@ProductModelID[.="19"]`) in the `exist()` method is evaluated. This run-time shredding can be costly, depending on the size and number of instances stored in the column.

If querying XML binary large objects (BLOBs) is common in your application environment, it helps to index the **xml** type columns. However, there's a cost associated with maintaining the index during data modification.

## Primary XML index

The primary XML index indexes all tags, values, and paths within the XML instances in an XML column. To create a primary XML index, the table in which the XML column occurs must have a clustered index on the primary key of the table. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this primary key to correlate rows in the primary XML index with rows in the table that contains the XML column.

The primary XML index is a shredded and persisted representation of the XML BLOBs in the **xml** data type column. For each XML binary large object (BLOB) in the column, the index creates several rows of data. The number of rows in the index is approximately equal to the number of nodes in the XML binary large object. When a query retrieves the full XML instance, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the instance from the XML column. Queries within XML instances use the primary XML index, and can return scalar values or XML subtrees by using the index itself.

Each row stores the following node information:

- Tag name such as an element or attribute name.

- Node value.

- Node type such as an element node, attribute node, or text node.

- Document order information, represented by an internal node identifier.

- Path from each node to the root of the XML tree. This column is searched for path expressions in the query.

- Primary key of the base table. The primary key of the base table is duplicated in the primary XML index for a back join with the base table, and the maximum number of columns in the primary key of the base table is limited to 15.

This node information is used to evaluate and construct XML results for a specified query. For optimization purposes, the tag name and the node type information are encoded as integer values, and the Path column uses the same encoding. Also, paths are stored in reverse order to allow matching paths when only the path suffix is known. For example:

- `//ContactRecord/PhoneNumber` where only the last two steps are known

OR

- `/Book/*/Title` where the wildcard character `*` is specified in the middle of the expression.

The query processor uses the primary XML index for queries that involve [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md) and returns either scalar values or the XML subtrees from the primary index itself. (This index stores all the necessary information to reconstruct the XML instance.)

For example, the following query returns summary information stored in the `CatalogDescription`**xml** type column in the `ProductModel` table. The query returns `<Summary>` information only for product models whose catalog description also stores the `<Features>` description.

```sql
;WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS "PD")

SELECT CatalogDescription.query('  /PD:ProductDescription/PD:Summary') as Result
FROM Production.ProductModel
WHERE CatalogDescription.exist ('/PD:ProductDescription/PD:Features') = 1
```

Regarding the primary XML index, instead of shredding each XML binary large object instance in the base table, the rows in the index that correspond to each XML binary large object are searched sequentially for the expression specified in the `exist()` method. If the path is found in the Path column in the index, the `<Summary>` element together with its subtrees is retrieved from the primary XML index and converted into an XML binary large object as the result of the `query()` method.

The primary XML index isn't used when retrieving a full XML instance. For example, the following query retrieves from the table the whole XML instance that describes the manufacturing instructions for a specific product model.

```sql
USE AdventureWorks2012;

SELECT Instructions
FROM   Production.ProductModel
WHERE  ProductModelID = 7;
```

## Secondary XML indexes

To enhance search performance, you can create secondary XML indexes. A primary XML index must first exist before you can create secondary indexes. These are the types:

- PATH secondary XML index

- VALUE secondary XML index

- PROPERTY secondary XML index

Following are some guidelines for creating one or more secondary indexes:

- If your workload uses path expressions significantly on XML columns, the PATH secondary XML index is likely to speed up your workload. The most common case is the use of the `exist()` method on XML columns in the WHERE clause of Transact-SQL.

- If your workload retrieves multiple values from individual XML instances by using path expressions, clustering paths within each XML instance in the PROPERTY index may be helpful. This scenario typically occurs in a property bag scenario when properties of an object are fetched and its primary key value is known.

- If your workload involves querying for values within XML instances without knowing the element or attribute names that contain those values, you may want to create the VALUE index. This typically occurs with descendant axes lookups, such as `//author[last-name="Howard"]`, where `<author>` elements can occur at any level of the hierarchy. It also occurs in wildcard queries, such as `/book [@* = "novel"]`, where the query looks for `<book>` elements that have some attribute having the value `"novel"`.

### PATH secondary XML index

If your queries generally specify path expressions on **xml** type columns, a PATH secondary index may be able to speed up the search. As described earlier in this article, the primary index is helpful when you have queries that specify `exist()` method in the WHERE clause. If you add a PATH secondary index, you may also improve the search performance in such queries.

Although a primary XML index avoids having to shred the XML binary large objects at run time, it may not provide the best performance for queries based on path expressions. Because all rows in the primary XML index corresponding to an XML binary large object are searched sequentially for large XML instances, the sequential search may be slow. In this case, having a secondary index built on the path values and node values in the primary index can significantly speed up the index search. In the PATH secondary index, the path and node values are key columns that allow for more efficient seeks when searching for paths. The query optimizer may use the PATH index for expressions such as those shown in the following:

- `/root/Location` which specify only a path

OR

- `/root/Location/@LocationID[.="10"]` where both the path and the node value are specified.

The following query shows where the PATH index is helpful:

```sql
;WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS "PD")

SELECT CatalogDescription.query('
  /PD:ProductDescription/PD:Summary
') AS Result
FROM Production.ProductModel
WHERE CatalogDescription.exist ('/PD:ProductDescription/@ProductModelID[.="19"]') = 1;
```

In the query, the path expression `/PD:ProductDescription/@ProductModelID` and value `"19"` in the `exist()` method correspond to the key fields of the PATH index. This allows for direct seek in the PATH index and provides better search performance than the sequential search for path values in the primary index.

### VALUE secondary XML index

If queries are value based, for example, `/Root/ProductDescription/@*[. = "Mountain Bike"]` or `//ProductDescription[@Name = "Mountain Bike"]`, and the path isn't fully specified or it includes a wildcard, you might obtain faster results by building a secondary XML index that is built on node values in the primary XML index.

The key columns of the VALUE index are (node value and path) of the primary XML index. If your workload involves querying for values from XML instances without knowing the element or attribute names that contain the values, a VALUE index may be useful. For example, the following expression will benefit from having a VALUE index:

- `//author[LastName="someName"]` where you know the value of the `<LastName>` element, but the `<author>` parent can occur anywhere.

- `/book[@* = "someValue"]` where the query looks for the `<book>` element that has some attribute having the value `"someValue"`.

The following query returns `ContactID` from the `Contact` table. The `WHERE` clause specifies a filter that looks for values in the `AdditionalContactInfo`**xml** type column. The contact IDs are returned only if the corresponding additional contact information XML binary large object includes a specific telephone number. Because the `telephoneNumber` element may appear anywhere in the XML, the path expression specifies the descendent-or-self axis.

```sql
;WITH XMLNAMESPACES (
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactInfo' AS CI,
  'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ContactTypes' AS ACT
)

SELECT ContactID
FROM Person.Contact
WHERE AdditionalContactInfo.exist('//ACT:telephoneNumber/ACT:number[.="111-111-1111"]') = 1;
```

In this situation, the search value for `<number>` is known, but it can appear anywhere in the XML instance as a child of the `telephoneNumber` element. This kind of query might benefit from an index lookup based on a specific value.

### PROPERTY secondary index

Queries that retrieve one or more values from individual XML instances might benefit from a PROPERTY index. This scenario occurs when you retrieve object properties by using the `value()` method of the **xml** type and when the primary key value of the object is known.

The PROPERTY index is built on columns (PK, path and node value) of the primary XML index where PK is the primary key of the base table.

For example, for product model `19`, the following query retrieves the `ProductModelID` and `ProductModelName` attribute values using the `value()` method. Instead of using the primary XML index or the other secondary XML indexes, the PROPERTY index may provide faster execution.

```sql
;WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS "PD")

SELECT CatalogDescription.value('(/PD:ProductDescription/@ProductModelID)[1]', 'int') AS ModelID,
  CatalogDescription.value('(/PD:ProductDescription/@ProductModelName)[1]', 'varchar(30)') AS ModelName
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

Except for the differences described later in this article, creating an XML index on an**xml** type column is similar to creating an index on a non-**xml** type column. The following [!INCLUDE[tsql](../../includes/tsql-md.md)] DDL statements can be used to create and manage XML indexes:

- [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)
- [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)

## XML compression

**Applies to**: [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later, and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Preview.

Enabling XML compression changes the physical storage format of the data that is associated with the XML data type to a *compressed binary format*, but doesn't change XML data syntax or semantics. Application changes aren't required when one or more tables are enabled for XML compression.

Only the XML data type is affected by XML compression. XML data is compressed with the [Xpress Compression Algorithm](/openspecs/windows_protocols/ms-xca/a8b7cb0a-92a6-4187-a23b-5e14273b96f8). Any existing XML indexes are compressed using [data compression](../data-compression/data-compression.md). Data compression is enabled internally for XML indexes when XML compression is enabled.

XML compression can be enabled side-by-side with data compression on the same tables.

XML indexes don't inherit the compression property of the table. To compress indexes, you must explicitly enable XML compression on XML indexes.

Secondary XML indexes don't inherit the compression property of the Primary XML index.

By default, the XML compression setting for XML indexes is set to OFF when the index is created.

## Get information about XML indexes

XML index entries appear in the catalog view `sys.indexes` with the index `type` of `3`. The name column contains the name of the XML index.

XML indexes are also recorded in the catalog view `sys.xml_indexes`. This contains all the columns of `sys.indexes` and some specific ones that are useful for XML indexes. The value `NULL` in the column `secondary_type` indicates a primary XML index; the values `P`, `R` and `V` stand for PATH, PROPERTY, and VALUE secondary XML indexes, respectively.

The space use of XML indexes can be found in the table-valued function [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md). It provides information, such as the number of data pages occupied, average row size in bytes, and number of records, for all index types. This also includes XML indexes. This information is available for each database partition. XML indexes use the same partitioning scheme and partitioning function of the base table.

## See also

- [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)
- [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)
