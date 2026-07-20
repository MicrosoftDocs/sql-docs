---
title: "Export and Import Databases Using Always Encrypted"
description: "Export and import databases using Always Encrypted"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 03/11/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "Always Encrypted, configure with SSMS"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Export and import databases using Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to export and import databases containing columns protected with [Always Encrypted](always-encrypted-database-engine.md).

When you export a database, all data stored in encrypted columns is retrieved from the database in the encrypted form (ciphertext) and put into the resulting [BACPAC](../../../tools/sql-database-projects/concepts/data-tier-applications/overview.md). The resulting BACPAC also contains the metadata for Always Encrypted keys.

When you import the BACPAC into a database, the encrypted data from the BACPAC is loaded into the database and Always Encrypted key metadata is re-created.

If you have an application that is configured to query encrypted columns stored in the source database (the one you exported), you don't need to do anything special to enable the application to query the encrypted data in the target database, as the keys in both databases are the same.

For detailed information about how to export and import a database, see:

- [Export a BACPAC file](../../../tools/sql-database-projects/concepts/data-tier-applications/export-bacpac-file.md)
- [Import a BACPAC file to create a new database](../../../tools/sql-database-projects/concepts/data-tier-applications/import-bacpac-file-create-new-database.md)
- [Export an Azure SQL database to a BACPAC file](/azure/sql-database/sql-database-export)
- [Import a BACPAC file to a database in Azure SQL Database](/azure/sql-database/sql-database-import)
- [SqlPackage](../../../tools/sqlpackage/sqlpackage.md)

## Permissions for migrating databases with encrypted columns

You need the following permissions:

- `ALTER ANY COLUMN MASTER KEY` and `ALTER ANY COLUMN ENCRYPTION KEY` on the source database. 

- `ALTER ANY COLUMN MASTER KEY`, `ALTER ANY COLUMN ENCRYPTION KEY`, `VIEW ANY COLUMN MASTER KEY DEFINITION`, and `VIEW ANY COLUMN ENCRYPTION DEFINITION` on the target database.

You don't need to have access to column master keys configured for the encrypted columns, as the data stays encrypted during the export and import operations.

## Related content

- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
- [Always Encrypted](always-encrypted-database-engine.md)
- [Backup and restore databases using Always Encrypted](always-encrypted-migrate-using-backup-restore.md)
- [Migrate data to or from columns using Always Encrypted with SQL Server Import and Export Wizard](always-encrypted-migrate-using-import-export-wizard.md)
- [Bulk load encrypted data to columns using Always Encrypted](migrate-sensitive-data-protected-by-always-encrypted.md)
