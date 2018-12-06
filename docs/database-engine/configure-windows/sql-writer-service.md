---
title: "SQL Writer Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "VDI [SQL Server]"
  - "restoring [SQL Server], SQL Writer Service"
  - "backups [SQL Server], while SQL Server running"
  - "Volume Shadow Copy Service"
  - "volume backups while running [SQL Server]"
  - "Virtual Backup Device Interface [SQL Server]"
  - "SQL Writer Service"
  - "services [SQL Server], SQL Writer"
  - "MSDE Writer"
  - "VSS"
ms.assetid: 0f299867-f499-4c2a-ad6f-b2ef1869381d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Writer Service
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The SQL Writer Service provides added functionality for backup and restore of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through the Volume Shadow Copy Service framework.  
  
 The SQL Writer Service is installed automatically. It must be running when the Volume Shadow Copy Service (VSS) application requests a backup or restore. To configure the service, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Services applet. The SQL Writer Service installs on all operating systems.  
  
## Purpose  
 When running, [!INCLUDE[ssDE](../../includes/ssde-md.md)] locks and has exclusive access to the data files. When the SQL Writer Service is not running, backup programs running in Windows do not have access to the data files, and backups must be performed using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup.  
  
 Use the SQL Writer Service to permit Windows backup programs to copy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files while [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
## Volume Shadow Copy Service  
 The VSS is a set of COM APIs that implements a framework to allow volume backups to be performed while applications on a system continue to write to the volumes. The VSS provides a consistent interface that allows coordination between user applications that update data on disk (writers) and those that back up applications (requestors).  
  
 The VSS captures and copies stable images for backup on running systems, particularly servers, without unduly degrading the performance and stability of the services they provide. For more information on the VSS, see your Windows documentation.  

> [!NOTE]
> When using VSS to backup a virtual machine that is hosting a Basic Availability Group, if the virtual machine is currently hosting databases that are in a secondary state, starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 CU2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU9 those databases *will not* be backed up with the virtual machine.  This is because Basic Availability Groups do not support backing up databases on the secondary replica.  Prior to these versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the backup would fail with an error.
  
## Virtual Backup Device Interface (VDI)  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides an API called Virtual Backup Device Interface (VDI) that enables independent software vendors to integrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into their products for providing support for backup and restore operations. These APIs are engineered to provide maximum reliability and performance, and support the full range of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore functionality, including the full range of hot and snapshot backup capabilities.  
  
## Permissions  
 The SQL Writer service must run under the **Local System** account. The SQL Writer service uses the **NT Service\SQLWriter** login to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Using the **NT Service\SQLWriter** login allows the SQL Writer process to run at a lower privilege level in an account designated as **no login**, which limits vulnerability. If the SQL Writer service is disabled, then any utility which in relies on VSS snapshots, such as System Center Data Protection Manager, as well as some other 3rd-party products, would be broken, or worse, at risk of taking backups of databases which were not consistent. If neither [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the system it runs on, nor the host system (in the event of a virtual machine), need to use anything besides [!INCLUDE[tsql](../../includes/tsql-md.md)] backup, then the SQL Writer service can be safely disabled and the login removed.  Note that the SQL Writer service may be invoked by a system or volume level backup, whether the backup is directly snapshot-based or not. Some system backup products use VSS to avoid being blocked by open or locked files. The SQL Writer service needs elevated permissions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] because in the course of its activities it briefly freezes all I/O for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Features  
 SQL Writer supports:  
  
-   Full database backup and restore including full-text catalogs  
  
-   Differential backup and restore  
  
-   Restore with move  
  
-   Database rename  
  
-   Copy-only backup  
  
-   Auto-recovery of database snapshot  
  
 SQL Writer does not support:  
  
-   Log backups  
  
-   File and filegroup backup  
  
-   Page restore  
  
## Remarks
The SQL Writer service is a separate service from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine and is shared across different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and across different instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same server.  The SQL Writer service file ships as part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation package and will be marked with the same version number as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine it ships with.  When a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a server or an existing instance is upgraded, if the version number of the instance being installed or upgraded is higher than the version number of the SQL Writer service that is currently on the server, that file will be replaced with the one from the installation package.  Note that if the SQL Writer service was updated by a Service Pack or Cumulative Update and a RTM version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is being installed, it is possible to replace a newer version of the SQL Writer service with an older one, provided that the installation has a higher major version number.  For example, the SQL Writer service was updated in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 CU2.  If that instance is upgraded to [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] RTM, the updated SQL Writer service will be replaced with an older version.  In this case, you would need to apply the latest CU to the new instance in order to get the newer version of the SQL Writer service.

