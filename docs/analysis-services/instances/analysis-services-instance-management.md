---
title: "SQL Server Analysis Services Server Management | Microsoft Docs"
ms.date: 11/15/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# SQL Server Analysis Services server management
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]

For Azure Analysis Services, see [Manage Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-manage).

## Instances

  A server instance of Analysis Services is a copy of the **msmdsrv.exe** executable that runs as an operating system service. Each instance is fully independent of other instances on the same server, having its own configuration settings, permissions, ports, startup accounts, file storage, and server mode properties.  
  
 Each instance runs as Windows service, Msmdsrv.exe, in the security context of a defined logon account.  
  
-   The service name of default instance is MSSQLServerOLAPService.  
  
-   The service name of each named instance of is MSOLAP$InstanceName.  
  
> [!NOTE]  
>  If multiple instances are installed, Setup also installs a redirector service, which is integrated with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service. The redirector service is responsible for directing clients to the appropriate named instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service always runs in the security context of the Local Service account, a limited user account used by Windows for services that do not access resources outside the local computer.  
  
 Multi-instance means that you can scale-up by installing multiple server instances on the same hardware. For Analysis Services in particular, it also means that you can support different server modes by having multiple instances on the same server, each one configured to run in a specific mode.  

## Server mode
  
 Server mode is a server property that determines which storage and memory architecture is used for that instance. A server that runs in Multidimensional mode uses the resource management layer that was built for multidimensional cube databases and data mining models. In contrast, Tabular server mode uses the VertiPaq in-memory analytics engine and data compression to aggregate data as it is requested.  
  
 Differences in storage and memory architecture mean that a single instance of Analysis Services will run either tabular databases or multidimensional databases, but not both. The server mode property determines which type of database runs on the instance.  
  
 Server mode is set during installation when you specify the type of database that will run on the server. To support all available modes, you can install multiple instances of Analysis Services, each deployed in a server mode that corresponds to the projects you are building.  
  
 As a general rule, most of the administrative tasks you must perform do not vary by mode. As an Analysis Services system administrator, you can use the same procedures and scripts to manage any Analysis Services instance on your network regardless of how it was installed.  
  
> [!NOTE]  
>  The exception is [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint. Server administration of a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] deployment is always within the context of a SharePoint farm. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] differs from other server modes in that it is always single-instance, and always managed through SharePoint Central Administration or the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool. Although it is possible to connect to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint in SQL Server Management Studio or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], it is not desirable. A SharePoint farm includes infrastructure that synchronizes server state and oversees server availability. Using other tools can interfere with these operations. For more information about [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server administration, see [Power Pivot for SharePoint ](../../analysis-services/power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md).  
  
  
  
## See also  
 [Comparing Tabular and Multidimensional Solutions ](../../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md)   
 [Determine the Server Mode of an Analysis Services Instance](../../analysis-services/instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
