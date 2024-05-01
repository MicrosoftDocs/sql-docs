---
title: Create a multiserver environment
description: Multiserver administration requires that you set up a master server (MSX) and one or more target servers (TSX).
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/03/2023
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "SQL Server Agent, multiserver environments"
  - "master servers [SQL Server], about master servers"
  - "target servers [SQL Server], about target servers"
  - "multiserver environments [SQL Server]"
monikerRange: ">= sql-server-2016"
---
# Create a multiserver environment

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Multiserver administration requires that you set up a master server (MSX) and one or more target servers (TSX). Jobs that are processed on all the target servers are first defined on the master server and then downloaded to the target servers.

> [!IMPORTANT]  
> Most, but not all, SQL Server Agent features are currently supported on [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. The [Multi Server Administration feature is not supported on Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent).

By default, full Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), encryption and certificate validation are enabled for connections between master servers and target servers. For more information, see [Set Encryption Options on Target Servers](../../ssms/agent/set-encryption-options-on-target-servers.md).

If you have a large number of target servers, avoid defining your master server on a production server that has significant performance requirements from other [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] functionality, because target server traffic can slow performance on your production server. If you also forward events to a dedicated master server, you can centralize administration on one server. For more information, see [Manage Events](../../ssms/agent/manage-events.md).

> [!NOTE]  
> By default, the SQL Server Agent service account is mapped to the default SQL Server Agent service SID (`NT SERVICE\SQLSERVERAGENT`), which is a member of the **sysadmin** fixed server role. The account must also be a member of the `msdb` database role **TargetServersRole** on the master server if multiserver job processing is used. The Master Server Wizard automatically adds the service account to this role as part of the enlistment process.

## Considerations for multiserver environments

Consider the following issues when creating a multiserver environment:

- Use the most recent version as the master server. The current and two previous versions are supported.

- Each target server reports to only one master server. You must defect a target server from one master server before you can enlist it into a different one.

- When changing the name of a target server, you must defect it before changing the name and re-enlist it after the change.

- If you want to dismantle a multiserver configuration, you must defect all the target servers from the master server.

- SQL Server Integration Services only supports target servers that are the same version or higher than the master server version.

## Related tasks

- [Make a Master Server](make-a-master-server.md)
- [Make a Target Server](make-a-target-server.md)
- [Enlist a Target Server to a Master Server](enlist-a-target-server-to-a-master-server.md)
- [Defect a Target Server from a Master Server](defect-a-target-server-from-a-master-server.md)
- [Defect Multiple Target Servers from a Master Server](defect-multiple-target-servers-from-a-master-server.md)
- [sp_help_targetserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-targetserver-transact-sql.md)
- [sp_help_targetservergroup (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-targetservergroup-transact-sql.md)

## Related content

- [Troubleshoot Multiserver Jobs That Use Proxies](troubleshoot-multiserver-jobs-that-use-proxies.md)
