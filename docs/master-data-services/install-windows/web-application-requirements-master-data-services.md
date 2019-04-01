---
title: "Web Application Requirements (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "02/13/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
keywords: 
  - "master data services"
ms.assetid: 9455d3cf-c1b7-4d48-8aff-7dc636ed5dc3
author: leolimsft
ms.author: lle
manager: craigg
---
# Web Application Requirements (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] is a web application hosted by Internet Information Services (IIS). [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] works only in Internet Explorer (IE) 9 or later. IE 8  and earlier versions, Microsoft Edge and Chrome are not supported.  

**For instructions on how to install and configure IIS**, see [Installing and Configuring IIS](../../master-data-services/master-data-services-installation-and-configuration.md#InstallIIS).
  
 Use [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] to create and configure the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application. [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] configures IIS on the local computer, so it is best for initial web configuration tasks. For example, configure a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] environment with a single [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, or configure the first web application in a scale-out deployment of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]. Use IIS tools to perform more complex tasks, such as configuring multiple web servers in a scale-out deployment.  
  
> [!NOTE]  
>  Any computer on which you install components of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] must be licensed. For more information, refer to the End User License Agreement (EULA).  
  
## Requirements  
  
### Operating System  
 Before you install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)], review the following requirements:    
    
-   [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)    
  
### Microsoft Silverlight  
 To work in the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, Silverlight 5 must be installed on the client computer. If you do not have the required version of Silverlight, you will be prompted to install it when you navigate to an area of the web application that requires it. You can install Silverlight 5 from [here](https://go.microsoft.com/fwlink/?LinkId=243096).  
  
### Role and Role Services  
 On Windows Server 2012 or Windows Server 2012 R2, you can use **Server Manager**, which is available in the Microsoft Management Console (MMC), to install the **Web Server (IIS)** role, and required role services.  
 
 
> [!IMPORTANT]  
>**Dynamic Content Compression** is enabled by default. This significantly reduces the size of the xml response and saves the network I/O, though CPU usage is increased.  For more information, see **[CTP 2.0] Improved Performance** in [What's New in Master Data Services &#40;MDS&#41;](../../master-data-services/what-s-new-in-master-data-services-mds.md).  
  
||  
|-|  
|Internet Information Services<br /><br /> Web Management Tools<br /><br /> IIS Management Console<br /><br /> World Wide Web Services<br /><br /> Application Development<br /><br /> .NET Extensibility 3.5<br /><br /> .NET Extensibility 4.5<br /><br /> ASP.NET 3.5<br /><br /> ASP.NET 4.5<br /><br /> ISAPI Extensions<br /><br /> ISAPI Filters<br /><br /> Common HTTP Features<br /><br /> Default Document<br /><br /> Directory Browsing<br /><br /> HTTP Errors<br /><br /> Static Content<br /><br /> [Note: Do not install WebDAV Publishing]<br /><br /> Health and Diagnostics<br /><br /> HTTP Logging<br /><br /> Request Monitor<br /><br /> Performance<br /><br /> Static Content Compression<br /><br /> Security<br /><br /> Request Filtering<br /><br /> Windows Authentication|  
  
### Features 
 On Windows Server 2012 and Windows Server 2012 R2, you can use **Server Manager** to install the following required features.  
  
||  
|-|  
|.NET Framework 3.5 (includes .NET 2.0 and 3.0)<br /><br /> .NET Framework 4.5 Advanced Services<br /><br /> ASP.NET 4.5<br /><br /> WCF Services<br /><br /> HTTP Activation [Note: This is required.]<br /><br /> TCP Port Sharing<br /><br /> Windows Process Activation Service<br /><br /> Process Model<br /><br /> .NET Environment<br /><br /> Configuration APIs<br/><br/>Dynamic Content Compression|  
  
 Following is an example PowerShell script to add prerequisite server roles and features. The prerequisite server roles and features varies depending on the environment.  
  
```powershell  
Install-WindowsFeature Web-Mgmt-Console, AS-NET-Framework, Web-Asp-Net, Web-Asp-Net45, Web-Default-Doc, Web-Dir-Browsing, Web-Http-Errors, Web-Static-Content, Web-Http-Logging, Web-Request-Monitor, Web-Stat-Compression, Web-Filtering, Web-Windows-Auth, NET-Framework-Core, WAS-Process-Model, WAS-NET-Environment, WAS-Config-APIs  
  
Install-WindowsFeature Web-App-Dev, NET-Framework-45-Features -IncludeAllSubFeature -Restart  
```  
  
 For more information about PowerShell command, see [Install-WindowsFeature](/powershell/module/servermanager/install-windowsfeature).  
  
### Accounts and Permissions  
  
|Type|Description|  
|----------|-----------------|  
|Windows account|You must log on to the web server computer with a Windows account that has permission to configure Windows roles, role services, and features, and to create and manage application pools, web sites, and web applications in IIS on the local computer.|  
|Service account|When you create the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application in [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)], you must specify an identity for the application pool that the application runs in. This account can be different from the service account that was specified when the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database was created.<br /><br /> This identity must be a domain user account, and it is added to the mds_exec database role in the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database for database access. For more information, see [Database Logins, Users, and Roles](../../master-data-services/database-logins-users-and-roles-master-data-services.md). This account is also added to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] Windows group, **MDS_ServiceAccounts**, which is granted permission to the temporary compilation directory, **MDSTempDir**, in the file system. For more information, see [Folder and File Permissions &#40;Master Data Services&#41;](../../master-data-services/folder-and-file-permissions-master-data-services.md).|  
  
## See Also  
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)   
      
 [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](../../master-data-services/install-windows/create-a-master-data-manager-web-application-master-data-services.md)   
 [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/web-configuration-page-master-data-services-configuration-manager.md)  
  
  
