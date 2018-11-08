---
title: "Analysis Services Instance Management | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 0455fa4f-b92d-4a8b-a8f0-f2a268a5c84e
author: minewiskan
ms.author: owend
manager: craigg
---
# Analysis Services Instance Management
  An instance of Analysis Services is a copy of the `msmdsrv.exe` executable that runs as an operating system service. Each instance is fully independent of other instances on the same server, having its own configuration settings, permissions, ports, startup accounts, file storage, and server mode properties.  
  
 Each instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] runs as Windows service, Msmdsrv.exe, in the security context of a defined logon account.  
  
-   The service name of default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is MSSQLServerOLAPService.  
  
-   The service name of each named instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is MSOLAP$InstanceName.  
  
> [!NOTE]  
>  If multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] are installed, Setup also installs a redirector service, which is integrated with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service. The redirector service is responsible for directing clients to the appropriate named instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service always runs in the security context of the Local Service account, a limited user account used by Windows for services that do not access resources outside the local computer.  
  
 Multi-instance means that you can scale-up by installing multiple server instances on the same hardware. For Analysis Services in particular, it also means that you can support different server modes by having multiple instances on the same server, each one configured to run in a specific mode.  
  
 Server mode is a server property that determines which storage and memory architecture is used for that instance. A server that runs in Multidimensional mode uses the resource management layer that was built for multidimensional cube databases and data mining models. In contrast, Tabular server mode uses the xVelocity in-memory analytics engine (VertiPaq) and data compression to aggregate data as it is requested.  
  
 Differences in storage and memory architecture mean that a single instance of Analysis Services will run either tabular databases or multidimensional databases, but not both. The server mode property determines which type of database runs on the instance.  
  
 Server mode is set during installation when you specify the type of database that will run on the server. To support all available modes, you can install multiple instances of Analysis Services, each deployed in a server mode that corresponds to the projects you are building.  
  
 As a general rule, most of the administrative tasks you must perform do not vary by mode. As an Analysis Services system administrator, you can use the same procedures and scripts to manage any Analysis Services instance on your network regardless of how it was installed.  
  
> [!NOTE]  
>  The exception is PowerPivot for SharePoint. Server administration of a PowerPivot deployment is always within the context of a SharePoint farm. PowerPivot differs from other server modes in that it is always single-instance, and always managed through SharePoint Central Administration or the PowerPivot Configuration Tool. Although it is possible to connect to PowerPivot for SharePoint in SQL Server Management Studio or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], it is not desirable. A SharePoint farm includes infrastructure that synchronizes server state and oversees server availability. Using other tools can interfere with these operations. For more information about PowerPivot server administration, see [PowerPivot for SharePoint &#40;SSAS&#41;](../power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md).  
  
## In This Section  
  
|Link|Task Description|  
|----------|----------------------|  
|[Post-install Configuration &#40;Analysis Services&#41;](post-install-configuration-analysis-services.md)|Describes the required and optional tasks that complete or modify an installation of Analysis.|  
|[Connect to Analysis Services](connect-to-analysis-services.md)|Describes connection string properties, client libraries, authentication methodologies, and steps for establishing or clearing connections.|  
|[Monitor an Analysis Services Instance](monitor-an-analysis-services-instance.md)|Describes tools and techniques for monitoring a server instance, including how to use Performance Monitor and SQL Server Profiler.|  
|[Script Administrative Tasks in Analysis Services](../script-administrative-tasks-in-analysis-services.md)|Explains how to automate many administrative tasks, including processing.|  
|[Globalization scenarios for Analysis Services Multiidimensional](../globalization-scenarios-for-analysis-services-multiidimensional.md)|Explains language and collation support, steps for changing both properties, and tips for setting and testing language and collation behaviors.|  
|[Log operations in Analysis Services](log-operations-in-analysis-services.md)|Describes the logs and explains how to configure them.|  
  
## See Also  
 [Comparing Tabular and Multidimensional Solutions &#40;SSAS&#41;](../comparing-tabular-and-multidimensional-solutions-ssas.md)   
 [PowerPivot Configuration Tools](../power-pivot-sharepoint/power-pivot-configuration-tools.md)   
 [PowerPivot Server Administration and Configuration in Central Administration](../power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md)   
 [Determine the Server Mode of an Analysis Services Instance](determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
