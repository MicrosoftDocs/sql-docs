---
title: "sys.index_columns (Transact-SQL)"
description: "sys.index_columns contains one row per column that is part of an index or unordered table (heap)."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.index_columns"
  - "sys.index_columns_TSQL"
  - "index_columns"
  - "index_columns_TSQL"
helpviewer_keywords:
  - "sys.index_columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.index_columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains one row per column that is part of an index or unordered table (heap).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**object_id**|**int**|ID of the object the index is defined on.|
|**index_id**|**int**|ID of the index in which the column is defined.|
|**index_column_id**|**int**|ID of the index column. `index_column_id` is unique only within `index_id`.|
|**column_id**|**int**|ID of the column in `object_id`.<br /><br />`0` = Row Identifier (RID) in a nonclustered index.<br /><br />`column_id` is unique only within `object_id`.|
|**key_ordinal**|**tinyint**|Ordinal (1-based) within set of key-columns.<br /><br />0 = Not a key column, or is an XML index, a columnstore index, or a spatial index.<br /><br />Note: An XML or spatial index cannot be a key because the underlying columns are not comparable, meaning that their values cannot be ordered.|
|**partition_ordinal**|**tinyint**|Ordinal (1-based) within set of partitioning columns. A clustered columnstore index can have at most one partitioning column.<br /><br />0 = Not a partitioning column.|
|**is_descending_key**|**bit**|`1` = Index key column has a descending sort direction.<br /><br />`0` = Index key column has an ascending sort direction, or the column is part of a columnstore or hash index.|
|**is_included_column**|**bit**|`1` = Column is a non-key column added to the index by using the CREATE INDEX INCLUDE clause, or the column is part of a columnstore index.<br /><br />`0` = Column is not an included column.<br /><br />Columns implicitly added because they're part of the clustering key are not listed in `sys.index_columns`.<br /><br />Columns implicitly added because they're a partitioning column are returned as `0`.|
|**column_store_order_ordinal**|**tinyint**|Applies to: [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)] and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]<br />Ordinal (1-based) within set of order columns in an ordered clustered columnstore index. For more on ordered clustered columnstore indexes, see [Columnstore index design guidance](../indexes/columnstore-indexes-design-guidance.md).|

## Permissions

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Examples

 The following example returns all indexes and index columns for the table `Production.BillOfMaterials`.

```sql
USE AdventureWorks2012;
GO
SELECT i.name AS index_name
    ,COL_NAME(ic.object_id,ic.column_id) AS column_name
    ,ic.index_column_id
    ,ic.key_ordinal
,ic.is_included_column
FROM sys.indexes AS i
INNER JOIN sys.index_columns AS ic
    ON i.object_id = ic.object_id AND i.index_id = ic.index_id
WHERE i.object_id = OBJECT_ID('Production.BillOfMaterials');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
index_name                                                 column_name        index_column_id key_ordinal is_included_column
---------------------------------------------------------- -----------------  --------------- ----------- -------------
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate ProductAssemblyID  1               1           0
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate ComponentID        2               2           0
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate StartDate          3               3           0
PK_BillOfMaterials_BillOfMaterialsID                       BillOfMaterialsID  1               1           0
IX_BillOfMaterials_UnitMeasureCode                         UnitMeasureCode    1               1           0
  
(5 row(s) affected)
```

## Next steps

- [Object Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.objects (Transact-SQL)](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [sys.columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)
