---
title: Configure Column Encryption Using Always Encrypted Wizard in SSMS
description: Learn how to configure Always Encrypted for database columns by using the Always Encrypted Wizard in SSMS 21 and later versions.
author: pietervanhove
ms.author: pivanho
ms.reviewer: vanto, maghan, randolphwest, mathoma
ms.date: 11/18/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.alwaysencryptedwizard.f1"
helpviewer_keywords:
  - "Wizard, Always Encrypted"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Configure column encryption using Always Encrypted Wizard in SSMS

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Always Encrypted Wizard is a powerful tool that allows you to set the desired [Always Encrypted](always-encrypted-database-engine.md) configuration for selected database columns. Depending on the current configuration and the desired target configuration, the wizard can encrypt a column, decrypt it (remove encryption), or re-encrypt it (for example, using a new column encryption key or an encryption type that is different from the current type, configured for the column). Multiple columns can be configured in a single run of the wizard.

The wizard allows you to encrypt columns with existing encryption keys, generate a new column encryption key, or both a new column encryption key and a new column master key.

The wizard's assessment allows you to evaluate the tables in a selected database or choose specific tables to analyze. It identifies columns suitable for encryption and highlights those incompatible with Always Encrypted due to their data type, constraints, and other factors. The wizard provides a detailed list of restrictions for each field, explaining why specific columns can't be encrypted.

When your database is configured with a secure enclave, you can run cryptographic operations in place without moving data out of the database. The wizard removes all dependencies blocking the schema change of the column to be encrypted. It issues an in-place encryption for each column using the enclave within the database engine. When the encryption is finished, the wizard recreates the dependencies. For more information about Always Encrypted with secure enclaves, see [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

When your database isn't configured with a secure enclave, the wizard allows you to enable a secure enclave. Suppose you choose not to enable a secure enclave or you aren't using enclave-enabled keys. In that case, the wizard moves data out of the database and performs cryptographic operations within the SSMS process. The wizard creates a new table (or tables) with the desired encryption configuration in the database, loads all data from the original tables, performs the requested cryptographic operations, uploads the data to the new tables, and then swaps one or more original tables with the new tables.

> [!TIP]  
> Using in-place encryption using Always Encrypted with secure enclaves, if available in your environment, might substantially reduce the time and the reliability of cryptographic operations.

Running cryptographic operations can take a long time. During that time, your database isn't available to write transactions. PowerShell is a recommended tool for cryptographic operations on larger tables. See [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md) or [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md).

- For an end-to-end walk-through that shows how to configure Always Encrypted with the wizard and use it in a client application, see the following Azure SQL Database tutorials:

  - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Windows certificate store](/azure/azure-sql/database/always-encrypted-certificate-store-configure)

  - [Protect sensitive data in Azure SQL Database with Always Encrypted and column master keys in Azure Key Vault](/azure/sql-database/sql-database-always-encrypted-azure-key-vault)

- For information about Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).

- For information about encryption types supported in Always Encrypted, see [Selecting Deterministic or Randomized Encryption](always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption).

## Permissions

To perform cryptographic operations using the wizard, you must have the `VIEW ANY COLUMN MASTER KEY DEFINITION` and `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` permissions. You also need key store permissions to create, access, and use your column master key. For detailed information on key store permissions, go to [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md), or find a section relevant to your key store.

## Open the Always Encrypted wizard

You can launch the wizard at three different levels:

- At a database level, if you want to encrypt multiple columns in different tables.
- At a table level, if you want to encrypt multiple columns in the same table.
- At a column level, if you want to encrypt one specific column.

1. Connect to your [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] with the Object Explorer component of [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].

1. To encrypt:

   - Multiple columns located in different tables in a database, right-click your database, point to **Tasks**, and then select **Always Encrypted Wizard**.

   - Multiple columns in the same table, navigate to the table, right-click on it, and then select **Always Encrypted Wizard**.

   - An individual column, navigate to the column, right-click on it, and then select **Always Encrypted Wizard**.

## Column Selection page

On this page, you select columns to encrypt, re-encrypt, or decrypt and define the target encryption configuration for them.

To encrypt a plaintext column (a column that isn't encrypted), select an encryption type (**Deterministic** or **Randomized**) and an encryption key for the column.

Select the desired encryption type and the key to change an encryption type or rotate (change) a column encryption key for an already encrypted column.

If you want the wizard to encrypt or re-encrypt one or more columns using a new column encryption key, pick a key containing **(New)** in its name. The wizard generates the key.

To decrypt a currently encrypted column, select **Plaintext** for the encryption type.

> [!TIP]  
> If you want to use in-place encryption and you're using existing keys, make sure you select enclave-enabled keys, annotated with **(enclave-enabled)**.

The wizard doesn't support cryptographic operations on temporal and in-memory tables. You can create empty temporal or in-memory tables using Transact-SQL and insert data using your application.

## Column Assessment page

The selected tables and columns are assessed for suitability for Always Encrypted, or Always Encrypted with secure enclaves. The assessment starts automatically by showing a status bar and a list of the tables and columns it currently assesses, which are done, and which are to do. The assessment checks if a table column meets the requirements for Always Encrypted or Always Encrypted with secure enclaves based on the [limitations](always-encrypted-database-engine.md#limitations).

If a column doesn't meet the requirements, the assessment displays an "Error" status for that column. Selecting the Messages link lets you get detailed information about why that specific column can't be encrypted.

You can encrypt the passed assessment columns by checking the checkbox. The wizard automatically skips any columns that don't pass the assessment. Additionally, you can export the results to a CSV or text file by selecting the Report button.

## Master Key Configuration page

If you selected an autogenerated column encryption key for any column on the previous page, you need to either select an existing column master key or configure a new column master key that encrypts the column encryption key on this page.

When configuring a new column master key, you can either pick an existing key in Windows Certificate Store or Azure Key Vault and have the wizard create just a metadata object for the key in the database, or you can choose to generate both the key and the metadata object describing the key in the database.

To use in-place encryption, select **Allow enclave computations** for a new column master key. Selecting this checkbox is allowed only if your database is configured with a secure enclave.

For more information about creating and storing column master keys in Windows Certificate Store, Azure Key Vault, or other key stores, see [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) or [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md).

> [!TIP]  
> The wizard allows you to browse and create keys only in the Windows Certificate Store and Azure Key Vault. It also autogenerates the names of the new keys and the database metadata objects describing them. Suppose you need more control over how your keys are provisioned (and more choices for a key store containing your column master key). In that case, you can use the **New Column Master Key** and **New Column Encryption Key** dialogs to create the keys first, and then run the wizard and pick the keys you have created. See [Provision Column Master Keys with the New Column Master Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-master-keys-with-the-new-column-master-key-dialog) or [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md) and [Provision Column Encryption Keys with the New Column Encryption Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-encryption-keys-with-the-new-column-encryption-key-dialog).

## In-Place Encryption Settings page

If you have configured a secure enclave in your database and you're using enclave-enabled keys, this page allows you to specify the enclave attestation parameters required for in-place encryption. If you don't want to use in-place encryption, unselect **Use in-place encryption for eligible columns** to proceed with client-side encryption. We recommend leaving this checkbox enabled so that the wizard can use in-place encryption.

For more information about enclave attestation, see [Configure attestation for Always Encrypted using Azure Attestation](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation)

## Run Settings page

The wizard supports two approaches for setting up the target encryption configuration: online and offline.

With the offline approach, the target tables and any tables related to the target tables (for example, any tables a target table have foreign key relationships with) are unavailable to write transactions throughout the duration of the operation. The semantics of foreign key constraints (CHECK or NOCHECK) are always preserved when using the offline approach.

With the online approach, the operation of copying, encrypting, decrypting, or re-encrypting the data is performed incrementally. Applications can read and write data from and to the target tables throughout the data movement operation, except the last iteration, the duration of which is limited by the *Maximum downtime* parameter. To detect and process the changes applications can make while the data is being copied, the wizard enables Change Tracking in the target database. Because of that, the online approach is likely to consume more resources on the database side than the offline approach. The operation might also take more time with the online approach, especially if a write-heavy workload is running against the database. The online approach can be used to encrypt **one table at a time** and the table must have a primary key. By default, foreign key constraints are recreated with the NOCHECK option to minimize the impact on applications. You can enforce preserving the semantics of foreign key constraints by enabling the `Keep check foreign key constraints` option.

Here are the guidelines for choosing between the offline and online approaches:

Use the offline approach:

- To minimize the duration of the operation.
- To encrypt/decrypt/re-encrypt columns in multiple tables at the same time.
- If the target table doesn't have a primary key.

Use the online approach:

- To minimize the downtime/unavailability of the database to your applications.

## Post encryption

Clear the plan cache for all batches and stored procedures that access the table to refresh parameters encryption information.

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
   ```

   > [!NOTE]  
   > If you don't remove the plan for the affected query from the cache, the first execution of the query after encryption might fail.
   >
   > Use `ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE` or `DBCC FREEPROCCACHE` to clear the plan cache carefully, as it can result in temporary query performance degradation. To minimize the negative impact of clearing the cache, you can selectively remove the plans for only the affected queries.

Call [sp_refresh_parameter_encryption](../../system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) to update the metadata for the parameters of each module (stored procedure, function, view, trigger) that are persisted in [sys.parameters](../../system-catalog-views/sys-parameters-transact-sql.md) and might have been invalidated by encrypting the columns.

## Related content

- [Always Encrypted](always-encrypted-database-engine.md)
- [Always Encrypted with secure enclaves](always-encrypted-enclaves.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md)
