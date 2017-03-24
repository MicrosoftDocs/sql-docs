---
title: "What&#39;s New in SQL Server vNext | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0b57f375-9242-4bb2-9d4b-c560d5a93524
caps.latest.revision: 71
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# What&#39;s New in SQL Server vNext
SQL Server vNext represents a major step towards making SQL Server a platform that gives you choices of development languages, data types, on-premises and in the cloud, and across operating systems by bringing the power of SQL Server to Linux, Linux-based Docker containers, and Windows.

This topic is a summary of what is new in the most recent Community Technical Preview (CTP) release, links to more detailed what's new information for specific feature areas.

![info_tip](../sql-server/media/info-tip.png) Run SQL Server on Linux! For more information, see:
-  [What's new for SQL Server vNext on Linux](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-whats-new)
-  [SQL Server on Linux Documentation](https://docs.microsoft.com/en-us/sql/linux/)


**Try it out:**    
   -   [![Download from Evaluation Center](../analysis-services/media/download.png)](http://go.microsoft.com/fwlink/?LinkID=829477) **[Download the SQL Server vNext Community Technology Preview](http://go.microsoft.com/fwlink/?LinkID=829477)**

## What's New in SQL Server vNext CTP 1.4 (March 2017)
### SQL Server Database Engine
- There are no new Database Engine features in this CTP.
- This CTP contains bug fixes for the Database Engine.
- For a detailed list of vNext CTP enhancements in previous CTP releases, see [What's New in SQL Server vNext (Database Engine)](../database-engine/configure-windows/what-s-new-in-sql-server-vnext-database-engine.md).

### SQL Server R Services
- There are no new R Services features in this CTP.
- For more detailed R Services what's new information, including details from previous CTPs, see [What's New in SQL Server R Services](../advanced-analytics/r-services/what-s-new-in-sql-server-r-services.md).  

### SQL Server Reporting Services (SSRS)
- There are no new SSRS features in this CTP.
- For more detailed SSRS what's new information, including details from previous releases, see [What's new in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md). 

### SQL Server Analysis Services (SSAS)
- There are no new SSAS features in this CTP.  
- For more details, including what's new for Analysis Services in the latest preview releases of SSDT and SSMS, see [What's New in Analysis Services vNext](../analysis-services/what-s-new-in-sql-server-analysis-services-vnext.md).  

### SQL Server Integration Services (SSIS)
- There are no new SSIS features in this CTP.
- For more detailed SSIS what's new information, including details from previous CTPs, see [What's New in Integration Services vNext](../integration-services/what-s-new-in-integration-services-in-sql-server-vnext.md).  

### Master Data Services (MDS)
- There are no new Master Data Services features in this CTP.

![horizontal_bar](../sql-server/media/horizontal-bar.png)

## What's New in SQL Server vNext CTP 1.3 (February 2017)
### SQL Server Database Engine
- Indirect checkpoint performance improvements.
- Cluster-less Availability Groups support added.
- Minimum Replica Commit Availability Groups setting added.
- Availability Groups can now work across Windows-Linux to enable cross-OS migrations and testing.
- Temporal Tables Retention Policy support added,
- New DMV SYS.DM_DB_STATS_HISTOGRAM
- Online non-clustered columnstore index buill and rebuild support added
- 5 new dynamic management views to return information about Linux process. For more information, see [Linux Process Dynamic Management Views](../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md).   
- [sys.dm_db_stats_histogram (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) is added for examining statistics.

### SQL Server Analysis Services (SSAS) (CTP 1.3)
- Encoding hints - an advanced feature used to optimize processing (data refresh) of large in-memory tabular models. To learn more, see [What's New in Analysis Services vNext](../analysis-services/what-s-new-in-sql-server-analysis-services-vnext.md). 


![horizontal_bar](../sql-server/media/horizontal-bar.png)

##  ![info_tip](../sql-server/media/info-tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server)](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - general discussion about R](https://www.reddit.com/r/SQLServer/)

## See also    
 + [![Release Notes](../analysis-services/instances/install-windows/media/ssrs-fyi-note.png)] [SQL Server VNext Release Notes](../sql-server/sql-server-vnext-release-notes.md). 
+ [Features supported by Edition](https://msdn.microsoft.com/library/cc645993.aspx)
 + [Installation hardware and software requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
 + [Installation Wizard](../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)
 
 + [Setup and Servicing Installation](http://msdn.microsoft.com/library/6df72a78-6b36-4bc1-948e-04b4ebe46094)
 
 ![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)


