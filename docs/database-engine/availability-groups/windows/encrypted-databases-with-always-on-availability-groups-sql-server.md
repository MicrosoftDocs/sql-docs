---
title: "Add an encrypted database to an availability group"
description: "Learn the steps to add an encrypted (or recently decrypted) database to an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/19/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "Transparent Data Encryption, AlwaysOn Availability Groups"
  - "Transparent Data Encryption, Always On Availability Groups"
  - "TDE, AlwaysOn Availability Groups"
  - "TDE, Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
---
# Add an encrypted database to an Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article contains information about the using currently encrypted or recently decrypted databases with [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)].

## <a id="Restrictions"></a> Limitations

If a database is encrypted or even contains a database encryption key (DEK), you can't use the [!INCLUDE [ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)] or [!INCLUDE [ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] to add the database to an availability group. Even if an encrypted database has been decrypted, its log backups might contain encrypted data. In this case, full initial data synchronization could fail on the database. This is because the restore log operation might require the certificate that was used by the database encryption keys (DEKs), and that certificate might be unavailable.

To make a decrypted database eligible to add to an availability group using the wizard:

1. Create a full database backup of the primary database.
1. Create a log backup of the primary database.
1. Restore the database backup on the server instance that hosts the secondary replica.
1. Restore the log backup on the secondary database.

## Related content

- [Prepare a secondary database for an Always On availability group](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)
- [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)
- [Add a database to an Always On availability group with the 'Availability Group Wizard'](availability-group-add-database-to-group-wizard.md)
- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Transparent data encryption (TDE)](../../../relational-databases/security/encryption/transparent-data-encryption.md)
