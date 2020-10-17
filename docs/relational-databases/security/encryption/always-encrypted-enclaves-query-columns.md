---
description: "Run Transact-SQL statements using enclaves"
description: "Run DDL statements to configure encryption for your column or manage indexes on encrypted columns, and query encrypted columns"
title: "Query columns using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: 12/01/2020
ms.reviewer: vanto
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Run Transact-SQL statements using enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) allows the following Transact-SQL statements to perform confidential computations on encrypted database columns in a server-side secure enclave.

- [Data Definition Language (DDL)](../../../t-sql/statements/statements.md#data-definition-language) statements:
  - [ALTER TABLE column_definition (Transact-SQL)](../../../t-sql/statements/alter-table-column-definition-transact-sql.md) statements that trigger in-place cryptographic operations using enclave-enabled keys - see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
  - [CREATE INDEX (Transact-SQL)](../../../t-sql/statements/create-index-transact-sql.md), [ALTER INDEX (Transact-SQL)](../../../t-sql/statements/alter-index-transact-sql.md) statements that create or alter indexes on enclave-enabled columns using randomized encryption. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).
  
- [Data Manipulation Language (DML)](../../../t-sql/statements/statements.md#data-manipulation-language) statements/queries against enclave-enabled columns using randomized encryption that:
  - Use one or more of the following Transact-SQL operators or clauses supporting enclaves:
    - [Comparison Operators](../../../mdx/comparison-operators.md).
    - [BETWEEN (Transact-SQL)](../../../t-sql/language-elements/bet.ween-transact-sql.md).
    - [IN (Transact-SQL)](../../../t-sql/language-elements/in-transact-sql.md).
    - [LIKE (Transact-SQL)](../../../t-sql/language-elements/like-transact-sql.md).
    - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select).
    - [Joins](../../performance/joins.md). [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)] supports only nested loop joins. [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] supports nested loop, hash and merge joins.
    - [SELECT - ORDER BY Clause (Transact-SQL)](../../../t-sql/queries/select-order-by-clause-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)].
    - [SELECT - GROUP BY Clause (Transact-SQL)](../../../t-sql/queries/select-group-by-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)].
  - Insert, update or delete rows, and trigger inserting and/or removing an index key to/from an index on an enclave-enabled column. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).
- [DBCC (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-transact-sql.md) administrative commands that involve checking the integrity of indexes on enclave-enabled columns using randomized encryption, for example [DBCC CHECKDB (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) or [DBCC CHECKTABLE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).

> [!NOTE]
> Operations of indexes and DML queries using enclaves are only supported on enclave-enabled columns that use randomized encryption. Deterministic encryption is not supported.

> [!NOTE]
> In [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], confidential queries using enclaves on character string columns (`char`, `nchar`) require a binary2 sort order (BIN2) collation is configured for the column. In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], both BIN2 and UTF-8 collations are supported. 

## Next Steps
- [Query columns using Always Encrypted with secure enclaves with SSMS](always-encrypted-enclaves-query-columns-ssms.md)

## See Also
- [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md)

