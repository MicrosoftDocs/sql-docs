---
description: "OLE Automation Stored Procedures (Transact-SQL)"
title: "OLE Automation Stored Procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "system stored procedures [SQL Server], OLE Automation"
  - "OLE Automation [SQL Server], stored procedures"
ms.assetid: ff16a833-01fe-4877-8aa6-55b72603ec2e
author: markingmyname
ms.author: maghan
---
# OLE Automation Stored Procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that allow OLE Automation objects to be used within a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] blocks access to OLE Automation stored procedures because this component is turned off as part of the security configuration for this server. A system administrator can enable access to OLE Automation procedures by using sp_configure. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).  

:::row:::
    :::column:::
        [sp_OACreate](../../relational-databases/system-stored-procedures/sp-oacreate-transact-sql.md)

        [sp_OADestroy](../../relational-databases/system-stored-procedures/sp-oadestroy-transact-sql.md)

        [sp_OAGetErrorInfo](../../relational-databases/system-stored-procedures/sp-oageterrorinfo-transact-sql.md)

        [sp_OAGetProperty](../../relational-databases/system-stored-procedures/sp-oagetproperty-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_OAMethod](../../relational-databases/system-stored-procedures/sp-oamethod-transact-sql.md)

        [sp_OASetProperty](../../relational-databases/system-stored-procedures/sp-oasetproperty-transact-sql.md)

        [sp_OAStop](../../relational-databases/system-stored-procedures/sp-oastop-transact-sql.md)

        [Object Hierarchy Syntax &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/object-hierarchy-syntax-transact-sql.md)
    :::column-end:::
:::row-end:::

> [!NOTE]
> Do not directly or indirectly call Automation procedures from any SQL Server common language runtime (CLR) objects. Doing so can cause SQL Server to crash unexpectedly. Additionally, an error message that resembles the following is logged in the application event log:
>
> Information \<Time stamp>Windows Error Reporting 1001 None  
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

## See Also

- [System Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)

- [Ole Automation Procedures Server Configuration Option](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md)
