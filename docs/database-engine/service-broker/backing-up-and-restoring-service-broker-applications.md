---
title: Backing Up and Restoring Service Broker Applications
description: "Backup and restore procedures for a Service Broker service are integrated with the database in which the service runs."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Backing Up and Restoring Service Broker Applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Backup and restore procedures for a Service Broker service are integrated with the database in which the service runs. If the service contains components outside the database, such as an external application, you must back up and restore those components separately.

The **msdb** database contains routes for incoming messages. Therefore, these routes are not backed up with the database that contains the service. Service Broker endpoints and configuration for transport security are stored in the **master** database, so these objects also are not backed up with the database that contains the service.

Service Broker routing relies on a unique identifier in each database to correctly deliver messages. When restoring a backup that is intended to replace the original database, ensure that this identifier is not changed. When restoring a copy of a database to a different location, take care to change this identifier. For more information on Service Broker database identities, see [Managing Service Broker Identities](managing-service-broker-identities.md).

## See also

- [Migration (Service Broker)](migration.md)
- [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)
