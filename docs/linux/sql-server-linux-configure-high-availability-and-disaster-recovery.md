---
# required metadata

title: Configure high availability and disaster recovery for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 11-4-2016
ms.topic: article
ms.prod: sql-linux 
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Configure high availability and disaster recovery for SQL Server on Linux

SQL Server on Linux supports high-availability and disaster recovery. On the most basic level, you can backup and restore databases in the same way you backup and restore SQL Server on Windows. In more advanced scenarios SQL Server also integrates with various clustering solutions. Future releases of SQL Server on Linux may include additional high-availability capabilities.

## Backup and restore databases

You can restore SQL Server databases to vNextCTP1 from SQL Server 2012 and newer, as long as the database comes from a SQL Server build that is less than or equal to the version of SQL Server where you are restoring the database. The restore commands are the same as the restore commands for SQL Server on Windows. 

SQL Server on Linux can restore databases from backups SQL Server on Windows or Linux as long as the SQL Server version is supported. 

SQL Server on Windows can restore databases from backups on either operating system as long as the SQL Server version is supported. 

## Shared-disk failover clusters

SQL Server can integrate with various clustering solutions. You can create a shared disk failover cluster for SQL Server. On this type of cluster, storage is presented to cluster nodes. The instance of SQL Server can be active on any one node in the cluster. If the component responsible for monitoring the SQL cluster detects that SQL Server is not responding, it can move the service to another node. For details see [Configure shared disk cluster for SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure.md)

## Additional capabilities

Microsoft is continuing to enable high availability and disaster recovery capabilities that are available for SQL Server on Windows.

## See also