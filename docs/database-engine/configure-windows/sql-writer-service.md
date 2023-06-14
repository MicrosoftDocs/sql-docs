---
title: SQL Writer service
description: The SQL Writer service provides added backup and restore functionality in SQL Server through the Volume Shadow Copy Service framework.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/03/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "VDI [SQL Server]"
  - "restoring [SQL Server], SQL Writer service"
  - "backups [SQL Server], while SQL Server running"
  - "Volume Shadow Copy Service"
  - "volume backups while running [SQL Server]"
  - "Virtual Backup Device Interface [SQL Server]"
  - "SQL Writer service"
  - "services [SQL Server], SQL Writer"
  - "MSDE Writer"
  - "VSS"
---
# SQL Writer service

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

The SQL Writer service provides added functionality for backup and restore of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through the Volume Shadow Copy Service framework.

The SQL Writer service is installed automatically. It must be running when the Volume Shadow Copy Service (VSS) application requests a backup or restore. To configure the service, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Services applet. The SQL Writer service installs on all Windows operating systems.

## Purpose

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] locks and has exclusive access to the database files. When the SQL Writer service isn't running, backup programs running in Windows don't have access to the data files, and backups must be performed using native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup. Use the SQL Writer service to permit Windows backup programs to copy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database files while [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running and is maintaining exclusive locks on those files.

## Volume Shadow Copy Service

The VSS is a set of COM APIs that implements a framework to allow volume backups to be performed while applications continue to write to those volumes. The VSS helps coordination between user applications that write data to disk (writers) and applications that back up that data (requestors).

The VSS captures and copies stable images for backup on running systems, particularly servers, without unduly degrading the performance and stability of the services they provide. For more information on the VSS, see your Windows documentation.

> [!NOTE]  
> When using VSS to backup a virtual machine that is hosting a Basic availability group, if the virtual machine is currently hosting databases that are in a secondary state, starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 CU2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU9 those databases *will not* be backed up with the virtual machine. This is because Basic availability groups do not support backing up databases on the secondary replica. Prior to these versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the backup would fail with an error.

## Virtual Backup Device Interface (VDI)

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides an API called Virtual Backup Device Interface (VDI) that enables independent software vendors to integrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into their backup and restore products. These APIs are engineered to provide maximum reliability and performance, and support the full range of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore functionality, including the full range of hot and snapshot backup capabilities. If a third-party vendor application requests a snapshot (VSS) backup, the SQL Writer service calls into the VDI API functions to perform the actual backups. The VDI API is independent of VSS and is frequently used in software solutions that don't employ VSS APIs.

## Permissions

The SQL Writer service must run under the **Local System** account. The SQL Writer service uses the **NT Service\SQLWriter** login to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Using the **NT Service\SQLWriter** login allows the SQL Writer process to run at a lower privilege level in an account designated as **no login**, which limits vulnerability. If the SQL Writer service is disabled, then any utility that relies on VSS snapshots can break, and may risk taking backups of databases that aren't consistent. Examples include System Center Data Protection Manager, as well as some other third-party products.

If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the system it runs on, and the host system (in the case of a virtual machine) don't need anything besides [!INCLUDE[tsql](../../includes/tsql-md.md)] backup, then the SQL Writer service can be safely disabled and the login removed. The SQL Writer service may be invoked by a system or volume level backup, whether the backup is directly snapshot-based or not. Some system backup products use VSS to avoid being blocked by open or locked files. The SQL Writer service needs **sysadmin** permissions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] because in the course of its activities it briefly freezes all I/O for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Features

SQL Writer supports:

- Full database backup and restore including full-text catalogs
- Differential backup and restore
- Restore with move
- Database rename
- Copy-only backup
- Autorecovery of database snapshot

SQL Writer doesn't support:

- Log backups
- File and filegroup backup
- Page restore

## Service upgrade and maintenance

The SQL Writer service is a separate service from the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] and is shared across different versions and instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same server. The SQL Writer service file ships as part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation package and is marked with the same version number as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine it ships with.

When a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a server or an existing instance is upgraded, if the version number of the instance being installed or upgraded is higher than the version number of the SQL Writer service that is currently on the server, that file is replaced with the one from the installation package.

If the SQL Writer service was updated by a Service Pack or Cumulative Update and newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is being installed, you can replace a newer version of the SQL Writer service with an older one, as long as the installation has a higher major version number. For example, the SQL Writer service was updated in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP2 CU2. If that instance is upgraded to [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] RTM, the updated SQL Writer service is replaced with an older version. In this case, you would need to apply the latest CU to the new instance in order to get the newer version of the SQL Writer service.

## Next steps

- [SQL Server Services](../../tools/configuration-manager/sql-server-services.md)
- [SQL Server Browser service](../../tools/configuration-manager/sql-server-browser-service.md)
- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
