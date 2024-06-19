---
title: Virtual device interface reference
titleSuffix: SQL Server VDI reference
description: This article provides an overview of the virtual device interface reference for SQL Server backup.
author: MashaMSFT
ms.author: mathoma
ms.date: 07/03/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: reference
---

# Virtual device interface (VDI) reference

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This section contains the specifications for SQL Server application programming interfaces intended for use by third-party backup software vendors.

## Overview

The virtual device interface (VDI) provides the highest online backup throughput with minimal degradation to the transaction workload, as well as the fastest possible restore times. It enables third party vendors to achieve the same performance characteristics as the SQL Server native backup/restore, and makes the full range of backup/restore functionality available. VDI was introduced in SQL Server 7.0 and is supported and enhanced in later versions.

VDI supports two primary types of backup technologies:

- Conventional online backups where the entire contents of the backup set is read and transferred to the backup media.

- Snapshot backups using underlying split-mirror or copy-on-write technology.

Conventional online backups done through VDI can take advantage of the full range of features of backup and restore in SQL Server. Snapshot backups are limited to full database and file/filegroup backups only. However, snapshot backups may be rolled forward with conventional database differential, file differential, and transaction log backups.

Backup or restore operations that use the Microsoft SQL Server Virtual Device Interface (VDI) require that the server connection for SQL Server that is used to issue the `BACKUP` or `RESTORE` commands must be logged on as the sysadmin fixed server role. The VDI connection requires the Sysadmin permission because the connection itself is capable of executing more than simple backup operations and involves shared resources between the client and server. This may not be desirable in all environments and may cause third-party backup applications that use VDI to fail if the service accounts of the applications are configured to only have the `db_backupoperator` fixed database role.

## Next steps

Review the VDI reference documentation in this section.

Download the full specification and the supporting source files:

[GitHub: sql-server-samples/samples/features/sqlvdi](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sqlvdi)
