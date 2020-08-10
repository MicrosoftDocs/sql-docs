---
title: "Query columns using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: 10/31/2019
ms.reviewer: vanto
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Query columns using Always Encrypted with secure enclaves
[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

This article captures general considerations for running queries on encrypted columns using a server-side secure enclave for [Always Encrypted with secure enclaves](always-encrypted-enclaves.md). 

The following types of queries involve the use of a secure enclave:
- Queries that trigger in-place cryptographic operations using enclave-enabled keys - see [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md).
- *Rich queries* - range comparisons or pattern matching operations on columns encrypted using randomized encryption and enclave-enabled keys.
- Queries that create or update indexes on encrypted columns using randomized encryption and enclave-enabled keys. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).

> [!NOTE]
> Rich queries and indexes are only supported on enclave-enabled columns (columns encrypted with enclave-enabled column encryption keys) that use randomized encryption. Deterministic encryption does not support rich queries or indexes.

> [!NOTE]
> Rich queries on encrypted character string columns require the columns use collations with a binary2 sort order (BIN2 collations). 


## Next Steps
- [Query columns using Always Encrypted with secure enclaves with SSMS](always-encrypted-enclaves-query-columns-ssms.md)

## See Also
- [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md)

