---
title: "Configure Database Engine Instances (SQL Server)"
description: Get acquainted with SQL Server configuration tasks so that you can configure a Database Engine instance to meet performance and availability requirements.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---
# Configure Database Engine Instances (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Each instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be configured to meet the performance and availability requirements defined for the databases hosted by the instance. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] includes configuration options that control behaviors such as resource usage and the availability of features such as auditing or trigger recursion.  
  
## Instance Configuration  
 When a database is deployed into production there is often a service level agreement (SLA) defining areas such as the levels of performance required from the database and the required availability level of the database. The terms of the SLA typically drive configuration requirements for the instance.  
  
 An instance is usually configured immediately after it has been installed. The initial configuration is usually determined by the SLA requirements of the types of databases planned to be deployed to the instance. After the databases have been deployed, the database administrators monitor the performance of the instance and adjust the configuration settings as needed if the performance metrics show the instance is not meeting the SLA requirements.  
  
## Configuration Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes the various instance configuration options and how to view or change these options.|[Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)|  
|Describes how to view and configure the default locations of new data and log files in the instance.|[View or Change the Default Locations for Data and Log Files &#40;SQL Server Management Studio&#41;](../../database-engine/configure-windows/view-or-change-the-default-locations-for-data-and-log-files.md)|  
|Describes how to configure SQL Server to use soft-NUMA.|[Soft-NUMA &#40;SQL Server&#41;](../../database-engine/configure-windows/soft-numa-sql-server.md)|  
|Describes how to map a TCP/IP port to non-uniform memory access (NUMA) node affinity.|[Map TCP IP Ports to NUMA Nodes &#40;SQL Server&#41;](../../database-engine/configure-windows/map-tcp-ip-ports-to-numa-nodes-sql-server.md)|  
|Describes how to enable the Windows Lock Pages In Memory policy. This policy determines which accounts can use a process to keep data in physical memory, preventing the system from paging the data to virtual memory on disk.|[Enable the Lock Pages in Memory Option &#40;Windows&#41;](../../database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows.md)|  
  
## See Also  
 [Database Engine Instances &#40;SQL Server&#41;](../../database-engine/configure-windows/database-engine-instances-sql-server.md)  
  
  
