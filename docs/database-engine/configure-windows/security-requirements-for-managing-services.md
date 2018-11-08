---
title: "Security Requirements for Managing Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent service, security"
  - "services [SQL Server], security"
  - "SQL Server services, security"
  - "WMI Providers [SQL Server]"
  - "server configuration [SQL Server]"
  - "security [SQL Server], services"
  - "services [SQL Server], WMI"
ms.assetid: 1874a317-ddb2-425d-98d9-b53e1be6fc6a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Security Requirements for Managing Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To manage the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Services, use either SQL Server Configuration Manager or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Manage the services on clustered servers with the Cluster Administrator.  
  
 To manage the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service and set the server configuration options, you must be a member of the **serveradmin** fixed server role or the **sysadmin** fixed server role. Members of the Windows **Administrators** group can start and stop services and configure the server options that Windows provides.  
  
> [!NOTE]  
>  To operate properly, the accounts used for the services must be configured with the correct domain, file system, and registry permissions. For information about the required permissions, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
## Windows Management Instrumentation  
 SQL Server Configuration Manager and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] use Windows Management Instrumentation (WMI) to display and modify some of the server properties. To manage services and obtain the status of the services, the user must have rights to access the WMI object. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the following server property pages use WMI:  
  
-   Autostart Services  
  
-   Startup Parameters  
  
-   Security  
  
-   Misc Server Settings  
  
## Related Tasks  
 [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)  
  
## Related Content  
 [WMI Provider for Configuration Management Concepts](../../relational-databases/wmi-provider-configuration/wmi-provider-for-configuration-management.md)  
  
  
