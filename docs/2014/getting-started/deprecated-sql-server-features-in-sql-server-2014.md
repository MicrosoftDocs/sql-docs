---
title: "Deprecated SQL Server Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "database-engine"
ms.topic: conceptual
ms.assetid: fdc0c778-cc8d-42ab-8833-4deb4329f37a
author: mightypen
ms.author: genemi
manager: craigg
---
# Deprecated SQL Server Features in SQL Server 2014
  This topic describes the deprecated features that are still available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. These features are scheduled to be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Deprecated features should not be used in new applications.  
  
## Features Not Supported in the Next Version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  
 The following [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] features will not be supported in the next version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Do not use these features in new development work, and modify applications that currently use these features as soon as possible. The Feature name column appears in trace events as the ObjectName, in performance counters and sys.dm_os_performance_counters as the instance_name. The Feature ID appears in trace events as the ObjectId.  
  
|Category|Deprecated feature|Replacement|Feature name|Feature ID|  
|--------------|------------------------|-----------------|------------------|----------------|  
|Data Programmability|[sys.soap_endpoints &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-soap-endpoints-transact-sql)|Windows Communications Foundation (WCF) or ASP.NET|Native XML Web Services|22|  
|Data Programmability|[sys.endpoint_webmethods &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-endpoint-webmethods-transact-sql)|Windows Communications Foundation (WCF) or ASP.NET|Native XML Web Services|23|  
  
### Slipstream Functionality  
 The [Product Update feature](/previous-versions/sql/sql-server-2012/hh231670(v=sql.110)?redirectedfrom=MSDN) was introduced in SQL Server 2012 as an extension to the  Slipstream functionality that was available in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] PCU1. In SQL Server 2014, the Product Update feature is the recommended method to use for slipstreaming SQL Server. Therefore the command-line parameters, /*PCUSource* and /*CUSource*, associated with original Slipstream functionality, should no longer be used. These parameters will continue to work, but may be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup. The recommended parameter to use is /*UpdateSource* which combines the functionality of the original Slipstream parameters, /*PCUSource* and /*CUSource*.  
  
 For more information about Slipstream functionality that was available in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] PCU1, see [Slipstream a SQL Server Update](https://go.microsoft.com/fwlink/?LinkId=219945) (https://go.microsoft.com/fwlink/?LinkId=219945).  
 For information on how to use /*UpdateSource* to slipstream SQL Server builds, refer to the following:
 
 - [How to patch SQL Server 2012 Setup with an updated setup package (using UpdateSource to get a smart setup)](https://blogs.msdn.microsoft.com/jason_howell/2012/08/28/how-to-patch-sql-server-2012-setup-with-an-updated-setup-package-using-updatesource-to-get-a-smart-setup/)
 
 - [SQL Server 2012 Setup just got smarter… ](https://techcommunity.microsoft.com/t5/SQL-Server-Support/SQL-Server-2012-Setup-just-got-smarter-8230/ba-p/317440)
 
## See Also  
 [Backward Compatibility](../../2014/getting-started/backward-compatibility.md)  
  
  
