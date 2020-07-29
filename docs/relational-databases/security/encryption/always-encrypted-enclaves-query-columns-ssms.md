---
title: "Query columns using Always Encrypted with secure enclaves with SSMS | Microsoft Docs"
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
# Query columns using Always Encrypted with secure enclaves with SSMS
[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

This article describes how to use SQL Server Management Studio to issue queries that use a server-side secure enclave for [Always Encrypted with secure enclaves](always-encrypted-enclaves.md), including:
- Queries that trigger in-place cryptographic operations.
- Queries triggering rich computations, including range comparisons or pattern matching operations on columns encrypted with enclave-enabled keys.
- Queries that create or update indexes on encrypted columns using randomized encryption and enclave-enabled keys.  

To use SSMS to run queries on encrypted columns using a secure enclave, follow the instructions in [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md). Here are a few things that are specific to enclaves you should be aware of:

- You need SSMS version 18.3 or higher.
- Make sure you are running queries in a query window utilizing a secure enclave from a connection that has Always Encrypted and enclave computations enabled. For detailed instructions, see [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md) and [Enabling and disabling Always Encrypted for a database connection ](always-encrypted-query-columns-ssms.md#en-dis).

## Next Steps
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See Also  
- [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)

