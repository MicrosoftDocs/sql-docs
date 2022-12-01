---
title: "Create, alter, and drop selective XML indexes"
description: Learn how to create a new selective XML index, or alter or drop an existing selective XML index.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
---
# Create, alter, and drop selective XML indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to create a new selective XML index, or alter or drop an existing selective XML index.

For more information about selective XML indexes, see [Selective XML indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md).

## <a id="create"></a> Create a selective XML index

Create a selective XML index using Transact-SQL, by calling the CREATE SELECTIVE XML INDEX statement. For more information, see [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md).

The following example shows the syntax for creating a selective XML index. It also shows several variations of the syntax for describing the paths to be indexed, with optional optimization hints.

```sql
CREATE SELECTIVE XML INDEX sxi_index
ON Tbl(xmlcol)

FOR(
    pathab   = '/a/b' as XQUERY 'node()'
    pathabc  = '/a/b/c' as XQUERY 'xs:double',
    pathdtext = '/a/b/d/text()' as XQUERY 'xs:string' MAXLENGTH(200) SINGLETON
    pathabe = '/a/b/e' as SQL NVARCHAR(100)
)
```

## <a id="alter"></a> Alter a selective XML index

Alter an existing selective XML index using Transact-SQL, by calling the ALTER INDEX statement. For more information, see [ALTER INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/alter-index-selective-xml-indexes.md).

The following example shows an ALTER INDEX statement. This statement adds the path `'/a/b/m'` to the XQuery part of the index and deletes the path `'/a/b/e'` from the SQL part of the index created in the example in the article [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md). The path to delete is identified by the name that was given to it when it was created.

```sql
ALTER INDEX sxi_index
ON Tbl
FOR
(
    ADD pathm = '/a/b/m' as XQUERY 'node()' ,
    REMOVE pathabe
)
```

## <a id="drop"></a> Drop a selective XML index

Drop a selective XML index using Transact-SQL, by calling the DROP INDEX statement. For more information, see [DROP INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/drop-index-selective-xml-indexes.md).

The following example shows a DROP INDEX statement.

```sql
DROP INDEX sxi_index ON tbl
```

## See also

- [Create XML indexes](create-xml-indexes.md)
- [Create, alter, and drop secondary selective XML indexes](create-alter-and-drop-secondary-selective-xml-indexes.md)
