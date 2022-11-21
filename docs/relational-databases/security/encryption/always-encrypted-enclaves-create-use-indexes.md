---
description: "Create and use indexes on columns using Always Encrypted with secure enclaves"
title: "Create and use indexes on columns using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: "vanto"
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Create and use indexes on columns using Always Encrypted with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This article describes how to create and use indexes on columns encrypted using enclave-enabled column encryption keys with [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

Always Encrypted with secure enclaves supports:
- Clustered and non-clustered indexes on columns encrypted using deterministic encryption and enclave-enabled keys.
  - Such indexes are sorted based on ciphertext. No special considerations apply to such indexes. You can manage and use them the same way as indexes on columns encrypted using deterministic encryption and keys that aren't enclave-enabled (as with Always Encrypted). 
- Nonclustered indexes on columns encrypted using randomized encryption and enclave-enabled keys.
  - The key values in the index data structure (B-tree) are encrypted and sorted based on their plaintext values. For more information, see [Indexes on enclave-enabled columns](always-encrypted-enclaves.md#indexes-on-enclave-enabled-columns).

> [!NOTE]
> The remainder of this article discusses nonclustered indexes on columns encrypted using randomized encryption and enclave-enabled keys.

Since an index on a column using randomized encryption and an enclave-enabled column encryption key contains encrypted (ciphertext) data sorted based on plaintext, SQL Server Engine must use the enclave for any operation that involves creating, updating, or searching an index, including:

- Creating or rebuilding an index.
- Inserting, updating, or deleting a row in a table (containing an indexed/encrypted column), which triggers inserting and/or removing an index key to/from the index.
- Running `DBCC` commands that involve checking the integrity of indexes, for example [DBCC CHECKDB (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) or [DBCC CHECKTABLE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).
- Database recovery (for example, after SQL Server fails and restarts), if SQL Server needs to undo any changes to the index (more details below).

All of the above operations require the enclave to have the column encryption key for the indexed column. The key is needed to decrypt the index keys. In general, the enclave can obtain a column encryption key in one of two ways:
- Directly from the client application.
- From the cache of column encryption keys.

## Invoke indexing operations with column encryption keys provided directly by the client
For this method for invoking indexing operations to work, the application (including a tool, such as SQL Server Management Studio (SSMS)) issuing a query that triggers an operation on an index must:

- Connect to the database with both Always Encrypted and enclave computations enabled for the database connection.
- The application must have access to the column master key protecting the column encryption key for the indexed column.

Once SQL Server Engine parses the application query and determines it will need to update an index on an encrypted column to execute the query, it instructs the client driver to release the required column encryption key to the enclave over a secure channel. This is exactly the same mechanism that is used to provide the enclave with column encryption keys for processing any other queries that don't use indexes. For example, in-place encryption or queries using pattern matching and range comparisons.

This method is useful to ensure the presence of indexes on encrypted columns is transparent to applications that are already connected to the database with Always Encrypted and enclave computations enabled. The application connection can use the enclave for query processing. After you create an index on a column, the driver inside your app will transparently provide column encryption keys to the enclave for indexing operations. Creating indexes may increase the number of queries that require the application to send the column encryption keys to the enclave.

To use this method, follow the general guidance for running statements using a secure enclave in - [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md).

For step-by-step instructions on how to use this method, see [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](../tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption.md).

## Invoke indexing operations using cached column encryption keys

Once a client application sends a column encryption key to the enclave for processing any query that requires enclave computations, the enclave caches the column encryption key in an internal cache. This cache is located inside the enclave and inaccessible from the outside.

If the same or another client application used by the same or a different user triggers an operation on an index without providing the required column encryption directly, the enclave will look up the column encryption key in the cache. As a result, the operation on the index succeeds, although the client application hasn't provided the key.

For this method of invoking indexing operations to work, the application must connect to the database without Always Encrypted enabled for the connection and the required column encryption key must be available in the cache inside the enclave.

This method of invoking operations is supported only for queries that don't require column encryption keys for other operations, not related to indexes. For example, an application inserting a row using an `INSERT` statement to a table that contains an encrypted column, is required to connect to the database with Always Encrypted enabled in the connection string and it must have access to the keys, regardless if the encrypted column has an index or not.

This method is useful to:
 - Ensure the presence of indexes on enclave-enabled columns using randomized encryption is transparent to applications and users that don't have access to the keys and the data in plaintext. 
 - It ensures creating an index on an encrypted column doesn't brake existing queries. If an application issues a query on a table containing encrypted columns without having to have access to the keys, the application can continue to run without having access to the keys after a DBA creates an index. For example, consider an application that runs the below query on the **Employees** table that contains encrypted columns. The DBA hasn't created an index on any encrypted column.

   ```sql
   DELETE FROM [dbo].[Employees] WHERE [EmployeeID] = 1;
   GO
   ```

   If the application submits the query over a connection without Always Encrypted and enclave computations enabled, the query will succeed. The query does not trigger any computations on encrypted columns. After a DBA creates an index on any encrypted columns, the query triggers the removal of index keys from indexes. The enclave needs the column encryption keys in this situation. However, the application will be able to continue to run this query over the same connection, as long as a data owner has supplied the column encryption keys to the enclave.

 - To achieve role separation when managing indexes, as it enables DBAs to create and alter indexes on encrypted columns, without having access to sensitive data. 

> [!TIP] 
> [sp_enclave_send_keys (Transact-SQL)](../../system-stored-procedures/sp-enclave-send-keys-sql.md) allows you to easily send all enclave-enabled column encryption keys used for indexes to the enclave, and populate the key cache.

For step-by-step instructions on how to use this method, see [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](../tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption.md). 

## Next Steps
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)

## See Also  
- [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](../tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption.md).
- [sp_enclave_send_keys (Transact-SQL)](../../system-stored-procedures/sp-enclave-send-keys-sql.md)
