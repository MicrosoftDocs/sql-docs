---
description: "Proxy Account Properties - New Proxy Account (General Page)"
title: Proxy Account Properties - New Proxy Account (General Page)
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.proxy.general.f1"
ms.assetid: 5cd81265-bf59-413b-8397-150ddc70d0c7
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Proxy Account Properties - New Proxy Account (General Page)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view or change the properties of a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account.  
  
## Options  
**Proxy name**  
Type the name of the proxy.  
  
**Credential name**  
Type the name of the credential for the proxy.  
  
> [!NOTE]  
> The credential name provided must be the name of an existing credential. For information on creating credentials, see [How to: Create a Proxy](https://msdn.microsoft.com/c1e77e91-2a69-40d9-b8b3-97cffc710586)  
  
**...**  
Launches the **Select Credential** dialog.  
  
**Description**  
Type the description for the proxy.  
  
**Active to the following subsystems**  
Select the subsystems that the proxy account has access to.  
  
**Reassign job steps to**  
Select the proxy to reassign job steps to. This list is enabled when you revoke access to a subsystem that the proxy previously had access to.  
  
## See Also  
[Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md)  
  
