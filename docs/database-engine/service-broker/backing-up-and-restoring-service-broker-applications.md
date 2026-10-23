---
title: Back Up and Restore Service Broker Applications
description: "Backup and restore procedures for a Service Broker service are integrated with the database in which the service runs."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Back up and restore Service Broker applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Backup and restore procedures for a Service Broker service are integrated with the database in which the service runs. If the service contains components outside the database such as an external application, you must back up and restore those components separately.

The `msdb` database contains routes for incoming messages. Therefore, these routes aren't backed up with the database that contains the service. Service Broker endpoints and configuration for transport security are stored in the `master` database, so these objects also aren't backed up with the database that contains the service.

Service Broker routing relies on a unique identifier in each database to correctly deliver messages. When restoring a backup that is intended to replace the original database, ensure that this identifier isn't changed. When restoring a copy of a database to a different location, take care to change this identifier. For more information on Service Broker database identities, see [Manage Service Broker identities](managing-service-broker-identities.md).

## Related content

- [Migration (Service Broker)](migration.md)
- [Back up and restore of SQL Server databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)
