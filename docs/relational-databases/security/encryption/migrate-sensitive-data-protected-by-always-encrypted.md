---
title: Bulk Load Encrypted Data to Columns Using Always Encrypted
description: Learn how to bulk load data to columns using Always Encrypted with SQL Server.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "Always Encrypted, bulk import"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Bulk load encrypted data to columns using Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

To load encrypted data without performing metadata checks on the server during bulk copy operations, create the user with the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option. This option is intended to be used by legacy tools or third-party Extract-Transform-Load (ETL) workflows that can't use Always Encrypted. This allows a user to securely move encrypted data from one set of tables, containing encrypted columns, to another set of tables with encrypted columns (in the same or a different database).

## The ALLOW_ENCRYPTED_VALUE_MODIFICATIONS option

Both [CREATE USER](../../../t-sql/statements/create-user-transact-sql.md) and [ALTER USER](../../../t-sql/statements/alter-user-transact-sql.md) have an `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option. When set to `ON` (the default is `OFF`), this option suppresses cryptographic metadata checks on the server in bulk copy operations, which enables the user to bulk copy encrypted data between tables or databases, without decrypting the data.

## Data migration scenarios

The following table shows the recommended settings appropriate for several migration scenarios.

:::image type="content" source="media/always-encrypted-migration.png" alt-text="Screenshot of a table showing the recommended settings appropriate for several migration scenarios.":::

## Bulk load encrypted data

Use the following process to load encrypted data.

1. Set the option to `ON` for the user in the database that is the target for the bulk copy operation. For example:

   ```sql
    ALTER USER Bob WITH ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = ON;
   ```

1. Run your bulk copy application or tool connecting as that user. (If your application uses an Always Encrypted enabled client driver, make sure the connection string for the data source doesn't contain `column encryption setting=enabled` to ensure the data retrieved from encrypted columns remains encrypted. For more information, see [Develop applications using Always Encrypted](always-encrypted-client-development.md).)

1. Set the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option back to `OFF`. For example:

   ```sql
   ALTER USER Bob WITH ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = OFF;
   ```

## Potential for data corruption

Improper use of this option can lead to data corruption. The `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option allows the user to insert any data into encrypted columns in the database, including data encrypted with different keys, incorrectly encrypted, or not encrypted at all. If the user accidentally copies the data that isn't correctly encrypted using the encryption scheme (column encryption key, algorithm, encryption type) set up for the target column, you can't decrypt the data (the data is corrupted). This option must be used carefully, as it can lead to corrupting data in the database.

The following scenario demonstrates how improperly importing data could lead to data corruption:

1. The option is set to `ON` for a user.

1. The user runs the application that connects to the database. The application uses bulk APIs to insert plain text values to encrypted columns. The application expects an Always Encrypted-enabled client driver to encrypt the data on insert. However, the application is misconfigured, so that either it ends up using a driver that doesn't support Always Encrypted or the connection string doesn't contain `column encryption setting=enabled`.

1. The application sends plaintext values to the server. As cryptographic metadata checks are disabled in the server for the user, the server lets the incorrect data (plaintext instead of correctly encrypted ciphertext) to be inserted into an encrypted column.

1. The same or another application connects to the database using an Always Encrypted-enabled driver and with `column encryption setting=enabled` in the connection string, and retrieves the data. The application expects the data to be transparently decrypted. However, the driver fails to decrypt the data because the data is incorrect ciphertext.

## Best practice

Use designated user accounts for long running workloads using this option.

For short running bulk copy applications or tools that need to move encrypted data without decrypting it, set the option to `ON` immediately before running the application and set it back to OFF immediately after running the operation.

Don't use this option for developing new applications. Instead, use a client driver that offers an API for suppressing cryptographic metadata checks for a single session, such as the `AllowEncryptedValueModifications` option in .NET Framework Data Provider for SQL Server - see [Copying Encrypted Data using SqlBulkCopy](develop-using-always-encrypted-with-net-framework-data-provider.md#copying-encrypted-data-using-sqlbulkcopy).

## Related content

- [Always Encrypted](always-encrypted-database-engine.md)
- [Migrate data to or from columns using Always Encrypted with SQL Server Import and Export Wizard](always-encrypted-migrate-using-import-export-wizard.md)
- [CREATE USER (Transact-SQL)](../../../t-sql/statements/create-user-transact-sql.md)
- [ALTER USER (Transact-SQL)](../../../t-sql/statements/alter-user-transact-sql.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
