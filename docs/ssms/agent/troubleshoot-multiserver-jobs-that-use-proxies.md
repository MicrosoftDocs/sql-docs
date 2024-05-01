---
title: "Troubleshoot Multiserver Jobs That Use Proxies"
description: "Troubleshoot Multiserver Jobs That Use Proxies"
author: markingmyname
ms.author: maghan
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "proxies [SQL Server Agent], multiserver jobs"
  - "jobs [SQL Server Agent], multiserver jobs using proxies"
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Troubleshoot Multiserver Jobs That Use Proxies
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Distributed jobs whose steps are associated with a proxy run under the context of the proxy account on the target server. If job steps that use proxy accounts fail when downloaded from the master server, check the **error_message** column in the **sysdownloadlist** table in the **msdb** database for the following error messages:  
  
-   "The job step requires a proxy account, however proxy matching is disabled on the target server."  
  
    To resolve this error, set the **\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL.**_\<n\>_**\SQLServerAgent\AllowDownloadedJobsToMatchProxyName** registry subkey to **1 (true)**. By default, this subkey is set to **0** (**false**). The value of **MSSQL.**\<*n*> is the instance name; for example, **MSSQL.1** or **MSSQL.3**.  
  
-   "Proxy not found."  
  
    To resolve this error, make sure a proxy account exists on the target server with the same name as the master server proxy account under which the job step runs.  
  
> [!CAUTION]  
> [!INCLUDE[ssNoteRegistry](../../includes/ssnoteregistry-md.md)]  
  
## See Also  
[Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md)  
