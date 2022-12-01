---
title: "Create, alter, and drop secondary selective XML index"
description: Learn how to create a new secondary selective XML index, or alter or drop an existing secondary selective XML index.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Create, alter, and drop secondary selective XML indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Describes how to create a new secondary selective XML index, or alter or drop an existing secondary selective XML index.

## <a id="create"></a> Create a secondary selective XML index

You can create a secondary selective XML index using Transact-SQL by calling the CREATE XML INDEX statement. For more information, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/create-xml-index-selective-xml-indexes.md).

The following example creates a secondary selective XML index on the path `'pathabc'`. The path to index is identified by the name that was given to it when it was created with the CREATE SELECTIVE XML INDEX statement. For more information, see [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md).

```sql
CREATE XML INDEX filt_sxi_index_c
ON Tbl(xmlcol)
USING XML INDEX sxi_index
FOR
(
    pathabc
);
```

## <a id="alter"></a> Alter a secondary selective XML index

The ALTER statement isn't supported for secondary selective XML indexes. To change a secondary selective XML index, drop the existing index and recreate it.

1. Drop the existing secondary selective XML index by calling the DROP INDEX statement. For more information, see [DROP INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/drop-index-selective-xml-indexes.md).

2. Recreate the index with the desired options by calling the CREATE XML INDEX statement. For more information, see [CREATE XML INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/create-xml-index-selective-xml-indexes.md).

The following example changes a secondary selective XML index by dropping it and recreating it.

```sql
DROP INDEX Tbl.filt_sxi_index_c
GO
CREATE XML INDEX filt_sxi_index_c
ON Tbl(xmlcol)
USING XML INDEX sxi_index
FOR
(
    pathabc
);
```

## <a id="drop"></a> Drop a secondary selective XML index

Drop a secondary selective XML index using Transact-SQL by calling the DROP INDEX statement. For more information, see [DROP INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/drop-index-selective-xml-indexes.md).

The following example shows a DROP INDEX statement.

```sql
DROP INDEX ssxi_index
ON tbl;
```

## See also

- [Selective XML indexes &#40;SXI&#41;](selective-xml-indexes-sxi.md)
