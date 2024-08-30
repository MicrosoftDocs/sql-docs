---
title: "OLE Automation stored procedures (Transact-SQL)"
description: "OLE Automation stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system stored procedures [SQL Server], OLE Automation"
  - "OLE Automation [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# OLE Automation stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that allow OLE Automation objects to be used within a [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. By default, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] blocks access to OLE Automation stored procedures because this component is turned off as part of the security configuration for this server. A system administrator can enable access to OLE Automation procedures by using `sp_configure`. For more information, see [Surface area configuration](../security/surface-area-configuration.md).

- [sp_OACreate](sp-oacreate-transact-sql.md)
- [sp_OADestroy](sp-oadestroy-transact-sql.md)
- [sp_OAGetErrorInfo](sp-oageterrorinfo-transact-sql.md)
- [sp_OAGetProperty](sp-oagetproperty-transact-sql.md)
- [sp_OAMethod](sp-oamethod-transact-sql.md)
- [sp_OASetProperty](sp-oasetproperty-transact-sql.md)
- [sp_OAStop](sp-oastop-transact-sql.md)
- [Object hierarchy syntax (Transact-SQL)](object-hierarchy-syntax-transact-sql.md)

> [!NOTE]  
> Don't directly or indirectly call Automation procedures from any SQL Server common language runtime (CLR) objects. Doing so can cause SQL Server to crash unexpectedly. Additionally, an error message that resembles the following is logged in the application event log:
>  
> Information \<Time stamp> Windows Error Reporting 1001 None  
Fault bucket , type 0  
Event Name: APPCRASH  
Response: Not available  
Cab Id: 0  
>  
> Problem signature:  
P1: sqlservr.exe  
P2: 2009.100.4000.0  
P3: 4fecc5ba  
P4: StackHash_b620  
P5: 6.1.7601.17725  
P6: 4ec4aa8e  
P7: c0000374  
P8: 00000000000c40f2  
P9:  
P10:

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Ole Automation Procedures (server configuration option)](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md)
