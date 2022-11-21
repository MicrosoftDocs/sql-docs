---
description: "Enable Always Encrypted with secure enclaves for existing encrypted columns"
title: Enable Always Encrypted with secure enclaves for existing encrypted columns | Microsoft Docs"
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
# Enable Always Encrypted with secure enclaves for existing encrypted columns 
[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This article describes how to unlock the functionality of Always Encrypted with secure enclaves, and enable enclave computations for existing encrypted columns.  

If you have existing columns encrypted with keys that aren't enclave-enabled, you can make the columns encrypted with enclave-enabled keys. Doing so will enable you to use secure enclave in queries on your columns.

You can enable enclave computations for existing encrypted columns in a few different ways, depending on:

- **Scope/granularity:** Do you want to enable the enclave functionality for a subset of columns, or for all columns protected with a given column master key?
- **Data size:** What is the size of the tables containing the column(s) you want to make enclave-enabled?
- Do you also want to change the encryption type for your column(s)? Remember that only randomized encryption supports rich computations (pattern matching, comparison operators). If your column is encrypted using deterministic encryption, you'll also need to re-encrypt it with randomized encryption to unlock rich computations.

The following sections describe the three approaches for enabling enclaves for existing columns.

## Method 1: Rotate the column master key to replace it with an enclave-enabled column master key
Replacing an existing column master key (that isn't enclave-enabled) with a new column master key that is enclave-enabled effectively makes all column encryption keys (associated with the column master key) also enclave-enabled.

- Pros:
  - Doesn't involve re-encrypting data, so it's typically the fastest approach. It's a recommended approach for columns containing large amounts of data, that are already enabled for rich computations, and use deterministic encryption.
  - Can enable the enclave functionality for multiple columns at scale. Replacing the column master key with the enclave-enabled column master key makes all column encryption keys and all encrypted columns associated with the original column master key, enclave-enabled.
  
- Cons:
  - Doesn't support changing the encryption type from deterministic to randomized. While it unlocks in-place encryption for columns encrypted using deterministic encryption, it doesn't enable rich computations. You'll need to re-encrypt the columns using randomized encryption.
  - Doesn't allow you to selectively convert some of the columns associated with a given column master key.
  - Introduces key management overhead. You'll need to create a new column master key and make it available to applications that query the impacted columns.

For information on how to rotate a column master key, see [Rotate enclave-enabled keys](always-encrypted-enclaves-rotate-keys.md).

## Method 2: Rotate the column master key and re-encrypt columns using randomized encryption in-place
This method involves executing Method 1 as the first step, and then re-encrypt the columns. The columns start off using deterministic encryption, and then re-encrypted with randomized encryption to unlock rich queries.

- Pros:
  - Re-encrypts data in-place. It's a recommended method if you need to enable rich queries for encrypted columns that currently use deterministic encryption and contain large amounts of data. Step 1 (the column master key rotation) unlocks in-place encryption for columns using deterministic encryption, and step 2 (re-encrypting columns) can be performed in-place.
  - Can enable the enclave functionality for multiple columns at scale.
  
- Cons:
  - Doesn't allow you to selectively convert some of the columns associated with a given column master key.
  - It introduces key management overhead. You'll need to create a new column master key and make it available to applications that query the impacted columns.

For information on how to rotate a column master key and re-encrypt a column in-place to rotate a column encryption key, see [Rotate enclave-enabled keys](always-encrypted-enclaves-rotate-keys.md).

## Method 3: Re-encrypt a selected column with an enclave-enabled column encryption key on the client side
This method involves re-encrypting a column with an enclave-enabled column encryption key, enable rich queries with randomized encryption. Since the current column encryption key isn't enclave-enabled, you can't re-encrypt the column in-place. Use the Always Encrypted wizard or the Set-SqlColumnEncryption cmdlet to re-encrypt the column outside of the database.

- Pros:
  - Allows you selectively enable the enclave functionality (in-place encryption and rich queries, if you're re-encrypting the column with randomized encryption) for one column or a small subset of columns.
  - It can enable rich computations for columns in one step.
  - It doesn't require creating a new column master key, so it has a smaller impact on applications.
  
- Cons:
  - To re-encrypt the data, the tool will move it out of the database, which can take a long time and is prone to network errors.

For more information on how to rotate a column encryption via a client-side tool, see [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md) and [Rotate Always Encrypted keys using PowerShell](rotate-always-encrypted-keys-using-powershell.md).

## Next Steps
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)
