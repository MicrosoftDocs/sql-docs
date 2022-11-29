---
description: "Export and import databases using Always Encrypted"
title: "Export and import databases using Always Encrypted  | Microsoft Docs"
ms.custom: ""
ms.date: 10/30/2019
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Always Encrypted, configure with SSMS"
ms.assetid: 29816a41-f105-4414-8be1-070675d62e84
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current" 
---
# Export and import databases using Always Encrypted 
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to export and import databases containing columns protected with [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md).

When you export a database, all data stored in encrypted columns is retrieved from the database in the encrypted form (ciphertext) and put into the resulting [BACPAC](../../data-tier-applications/data-tier-applications.md). The resulting BACPAC also contains the metadata for Always Encrypted keys.

When you import the BACPAC into a database, the encrypted data from the BACPAC is loaded into the database and Always Encrypted key metadata is re-created. 

If you have an application that is configured to query encrypted columns stored in the source database (the one you exported), you don't need to do anything special to enable the application to query the encrypted data in the target database, as the keys in both databases are the same.

For detailed information about how to export and import a database, see:
- [Export a Data-tier Application](../../data-tier-applications/export-a-data-tier-application.md)
- [Import a BACPAC File to Create a New User Database](../../data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)
- [Export an Azure SQL database to a BACPAC file](/azure/sql-database/sql-database-export)
- [Import a BACPAC file to a database in Azure SQL Database](/azure/sql-database/sql-database-import)
- [SqlPackage.exe](../../../tools/sqlpackage/sqlpackage.md)

## Permissions for migrating databases with encrypted columns

You need *ALTER ANY COLUMN MASTER KEY* and *ALTER ANY COLUMN ENCRYPTION KEY* on the source database. You need *ALTER ANY COLUMN MASTER KEY*, *ALTER ANY COLUMN ENCRYPTION KEY*, *VIEW ANY COLUMN MASTER KEY DEFINITION*, and *VIEW ANY COLUMN ENCRYPTION DEFINITION* on the target database.

You do not need to have access to column master keys configured for the encrypted columns, as the data stays encrypted during the export and import operations.

## Next Steps
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See Also
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Back up and restore databases using Always Encrypted ](always-encrypted-migrate-using-backup-restore.md)
- [Migrate data to or from columns using Always Encrypted with SQL Server Import and Export Wizard](always-encrypted-migrate-using-import-export-wizard.md)
- [Bulk load encrypted data to columns using Always Encrypted](migrate-sensitive-data-protected-by-always-encrypted.md)