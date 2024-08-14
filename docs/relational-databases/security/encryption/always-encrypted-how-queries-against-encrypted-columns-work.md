---
title: "Always Encrypted - How queries against encrypted columns work"
description: Learn about how queries against encrypted columns work in the Always Encrypted feature in SQL Server and Azure SQL Database.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: 8/14/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Always Encrypted, cryptography system"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Always Encrypted - How queries against encrypted columns work
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]


To execute a query on encrypted database columns, insert data into these columns, retrieve plaintext values, or perform supported operations (such as point lookup searches) using deterministic encryption, the user or application issuing the query must fulfil the following prerequisites:

- Have access to the column master key protecting the data. This key access is required in addition to database-level permissions, like SELECT on the relevant table.
- Connect to the database with Always Encrypted enabled in the database connection. Most SQL tools and SQL client drivers support enabling Always Encrypted for database connections.

> [!NOTE]  
> If the user has required database permissions to read the data, but no access to the keys protecting it, the user can still retrieve cyphertext (encrypted) data by connecting to the database without enabling Always Encrypted in the database connection.

:::image type="content" source="media/always-encrypted-database-engine/always-encrypted-how-queries-against-encrypted-columns-work.png" alt-text="Schema how queries against encrypted columns work.":::

Here's how queries on encrypted columns work:

1. When an application issues a parameterized query, the SQL client driver within the application transparently contacts the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] (by calling [sp_describe_parameter_encryption (Transact-SQL)](../../system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) to determine which parameters target encrypted columns and should be encrypted. For each parameter that needs to be encrypted, the driver receives the encryption algorithm, encryption type, and key metadata, including the encrypted column encryption key and the location of its corresponding column master key.
1. The driver calls the key store, containing column master keys in order to decrypt the encrypted column encryption key values. The resultant plaintext column encryption keys are cached to reduce the number of round trips to the key store on subsequent uses of the same column encryption keys.
1. The driver uses the obtained plaintext column encryption keys to encrypt the query parameters corresponding to encrypted columns.
1. The driver substitutes the plaintext values of the parameters targeting encrypted columns with their encrypted values, and it sends the query to the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] for processing.
1. The [!INCLUDE [ssDE](../../../includes/ssde-md.md)] executes the query, which might involve equality comparisons on columns using deterministic encryption.
1. If query results include data from encrypted columns, the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] attaches encryption metadata for each column, including the information about the encryption algorithm, the encryption type, and key metadata to the result set.
1. The [!INCLUDE [ssDE](../../../includes/ssde-md.md)] sends the result set to the client application.
1. For each encrypted column in the received result set, the driver first tries to find the plaintext column encryption key in the local cache, and only makes a round trip to a key store holding the column master key if it can't find the key in the cache.
1. The driver decrypts the results and returns plaintext values to the application.

A client driver interacts with a key store, containing a column master key, using a column master key store provider, which is a client-side software component that encapsulates a key store containing the column master key. Providers for common types of key stores are available in client-side driver libraries from Microsoft, or as standalone downloads. You can also implement your own provider. Always Encrypted capabilities, including built-in column master key store providers vary by a driver library and its version.

See [Develop applications using Always Encrypted](always-encrypted-client-development.md) for the list of client drivers supporting Always Encrypted and for information on how to develop applications that query encrypted columns.

You can also query encrypted columns using SQL tools, for example [Azure Data Studio](always-encrypted-query-columns-ads.md) or [SSMS](always-encrypted-query-columns-ssms.md).
  
## See also  
 - [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 - [Develop applications using Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-client-development.md)  
  
