---
title: "Migrate data to or from columns by using Always Encrypted with SQL Server Import and Export Wizard"
description: "Migrate data to or from columns by using Always Encrypted with SQL Server Import and Export Wizard"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 09/21/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: security
ms.topic: upgrade-and-migration-article
ms.custom: intro-migration
f1_keywords:
  - "SQL13.SWB.COLUMNMASTERKEY.ROTATION.F1"
helpviewer_keywords:
  - "Always Encrypted, configure with SSMS"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Migrate data to or from columns by using Always Encrypted with SQL Server Import and Export Wizard

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The [SQL Server Import and Export Wizard](../../../integration-services/import-export-data/import-and-export-data-with-the-sql-server-import-and-export-wizard.md) is a tool that lets you copy data from a source to a destination. This article describes how to use the SQL Server Import and Export Wizard when the source, the destination, or both is a SQL Server database that contains columns protected with [Always Encrypted](always-encrypted-database-engine.md).

## Migration scenarios

With the SQL Server Import and Export Wizard, you can implement the following scenarios for migrating data to or from encrypted columns.

### Encrypt plaintext data on migration

If your data source contains plaintext data and your destination is a SQL Server database that contains encrypted columns, you can use the SQL Server Import and Export Wizard to retrieve the plaintext data from the source, encrypt it, and copy the encrypted data (ciphertext) to the encrypted columns in the destination database. For this migration scenario, the data source can be any data store that the SQL Server Import and Export Wizard supports, such as a file, a SQL Server database, or a database in another database system.

To ensure the SQL Server Import and Export Wizard can encrypt data, enable Always Encrypted for the destination database connection, and ensure you have access to the keys that protect the data in the destination database columns. For more information, see [Enable and disable Always Encrypted for a database connection](#enable-and-disable-always-encrypted-for-a-database-connection) and [Permissions for encrypting or decrypting data during migration](#permissions-for-encrypting-or-decrypting-data-during-migration).

### Decrypt encrypted data on migration

If you're migrating data stored in encrypted database columns in a SQL Server database, you can configure the SQL Server Import and Export Wizard to decrypt the data and copy the decrypted (plaintext) data to a destination. The destination can be any data store that the SQL Server Import and Export Wizard supports, such as a file, a SQL Server database, or a database in another database system.

To ensure the SQL Server Import and Export Wizard can decrypt data, enable Always Encrypted for the source database connection, and ensure you have access to the keys that protect the data in the source database columns. For more information, see [Enable and disable Always Encrypted for a database connection](#enable-and-disable-always-encrypted-for-a-database-connection) and [Permissions for encrypting or decrypting data during migration](#permissions-for-encrypting-or-decrypting-data-during-migration).

### Re-encrypt data on migration

If you're copying data from encrypted columns in a source SQL Server database to encrypted columns in the same or another SQL Server database, you can configure the SQL Server Import and Export Wizard to decrypt the data after retrieving it from the source, and re-encrypt it before inserting it into the encrypted columns in the destination database. Use this method if the schema of the target columns, such as column data types, encryption types, and column encryption keys, differs from the schema of the source columns.

To ensure the SQL Server Import and Export Wizard can encrypt and decrypt data, enable Always Encrypted for both the source database connection and the destination database connection, and ensure you have access to the keys that protect the data in both the source and the target database columns. For more information, see [Enable and disable Always Encrypted for a database connection](#enable-and-disable-always-encrypted-for-a-database-connection) and [Permissions for encrypting or decrypting data during migration](#permissions-for-encrypting-or-decrypting-data-during-migration).

### Keep data encrypted during migration

If you're copying data from encrypted columns in a source SQL Server database to encrypted columns in the same or another SQL Server database, and the target columns use **exactly** the same schema as the source columns, including the same data types, encryption types, and column encryption keys, you can configure the SQL Server Import and Export Wizard to retrieve ciphertext from the source columns and insert the encrypted data (ciphertext) into the encrypted column in the target SQL Server database.

For this scenario, use any data provider that supports SQL Server to connect to the source or the destination SQL Server database. If you're using a provider that supports Always Encrypted to connect to the destination database, make sure Always Encrypted is disabled for the database connection. For more information, see [Enable and disable Always Encrypted for a database connection](#enable-and-disable-always-encrypted-for-a-database-connection).

You also need to ensure the database principal (user) that the SQL Server Import and Export Wizard uses to connect to the destination database is configured with the `ALLOW_ENCRYPTED_VALUE_MODIFICATIONS` option set to `ON`. This option suppresses cryptographic metadata checks on the server in bulk copy operations, which lets the wizard bulk insert the encrypted data to the destination database without decrypting it. For more information, see [Bulk load encrypted data to columns protected by Always Encrypted](migrate-sensitive-data-protected-by-always-encrypted.md).

## Enable and disable Always Encrypted for a database connection

If your migration scenario requires the SQL Server Import and Export Wizard to encrypt or decrypt data, configure the source SQL Server database connection, the destination SQL Server database connection, or both, by using a data provider that supports Always Encrypted. You also need to enable Always Encrypted for those connections.

Use any data provider for a connection if you don't need the wizard to encrypt or decrypt data on that connection.

The following data providers in the SQL Server Import and Export Wizard support Always Encrypted.

- .NET Framework Data Provider for SQL Server
  - Make sure the machine on which the wizard runs uses .NET Framework 4.6.1 or later.
  - To enable Always Encrypted for a connection, set `Column Encryption Setting` to `Enabled` in the connection properties. To disable Always Encrypted, set `Column Encryption Setting` to `Disabled`. For more information, see [Connect to SQL Server with the .NET Framework Data Provider for SQL Server](../../../integration-services/import-export-data/connect-to-a-sql-server-data-source-sql-server-import-and-export-wizard.md#connect-to-sql-server-with-the-net-framework-data-provider-for-sql-server) and [Enabling Always Encrypted for Application Queries](develop-using-always-encrypted-with-net-framework-data-provider.md#enabling-always-encrypted-for-application-queries).
- .NET Framework Data Provider for ODBC.
  - Install Microsoft ODBC Driver 13.1 or later.
    - To enable Always Encrypted for a connection, set `Column Encryption` to `Enabled` in the connection properties. To disable Always Encrypted, set `Column Encryption` to `Disabled`. For more information, see [Connect to SQL Server with the ODBC driver for SQL Server](../../../integration-services/import-export-data/connect-to-a-sql-server-data-source-sql-server-import-and-export-wizard.md#connect-to-sql-server-with-the-odbc-driver-for-sql-server) and [Enabling Always Encrypted in an ODBC Application](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-in-an-odbc-application).

## Permissions for encrypting or decrypting data during migration

To encrypt or decrypt data stored in a SQL Server source or destination database, you need the `VIEW ANY COLUMN MASTER KEY DEFINITION` and `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` permissions in the source database. You also need key store permissions to access and use your column master key. For detailed information on the key store permissions required for key management operations, see [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) and find the section relevant to your key store.

## Related content

- [Always Encrypted](always-encrypted-database-engine.md)
- [Export and import databases using Always Encrypted](always-encrypted-migrate-using-bacpac.md)
- [Backup and restore databases using Always Encrypted](always-encrypted-migrate-using-backup-restore.md)
- [Bulk load encrypted data to columns using Always Encrypted](migrate-sensitive-data-protected-by-always-encrypted.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
