---
title: "Web Application Requirements (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 9455d3cf-c1b7-4d48-8aff-7dc636ed5dc3
author: leolimsft
ms.author: lle
manager: craigg
---
# Web Application Requirements (Master Data Services)
  [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] is a web application hosted by Internet Information Services (IIS). [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] works only in Internet Explorer (IE) 7 or later. IE 7 and earlier versions, Microsoft Edge and Chrome are not supported.  
  
 Use [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] to create and configure the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application. [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] configures IIS on the local computer, so it is best for initial web configuration tasks. For example, configure a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] environment with a single [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, or configure the first web application in a scale-out deployment of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]. Use IIS tools to perform more complex tasks, such as configuring multiple web servers in a scale-out deployment.  
  
> [!NOTE]  
>  Any computer on which you install components of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] must be licensed. For more information, refer to the End User License Agreement (EULA).  
  
## Requirements  
  
### Operating System  
 The following Windows operating systems include the Internet Information Services (IIS) functionality required for the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] web application and web service.  
  
|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Developer (64-bit) x64|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Enterprise (64-bit) x64|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Business Intelligence (64-bit) x64|  
|-------------------------------------------------------|--------------------------------------------------------|-------------------------------------------------------------------|  
|[!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] SP2<br /><br /> Windows Server 2008 R2 SP1<br /><br /> Windows 7 Professional, Enterprise, and Ultimate<br /><br /> Windows 8.0 Professional, Enterprise, and Ultimate|[!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] SP2<br /><br /> Windows Server 2008 R2 SP1<br /><br /> Windows Server 2012|[!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] SP2<br /><br /> Windows Server 2008 R2 SP1<br /><br /> Windows Server 2012|  
  
 For a full list of the Windows operating systems that are supported for your edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
### Microsoft Silverlight  
 To work in the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, Silverlight 5 must be installed on the client computer. If you do not have the required version of Silverlight, you will be prompted to install it when you navigate to an area of the web application that requires it. You can install Silverlight 5 from [here](https://go.microsoft.com/fwlink/?LinkId=243096).  
  
### Role and Role Services (Windows Server 2008 or Windows Server 2008 R2, Windows 7 operating systems)  
 On Windows Server 2008 R2, you can use **Server Manager**, which is available in the Microsoft Management Console (MMC), to install the **Web Server (IIS)** role and the following required role services.  
  
> [!NOTE]  
>  On [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)] and Windows 7 operating systems, use **Programs and Features** in Control Panel to enable these options in the **Windows Features** dialog box.  
  
||  
|-|  
|Web Server<br /><br /> Common HTTP Features<br /><br /> Static Content<br /><br /> Default Document<br /><br /> Directory Browsing<br /><br /> HTTP Errors<br /><br /> Application Development<br /><br /> ASP.NET<br /><br /> .NET Extensibility<br /><br /> ISAPI Extensions<br /><br /> ISAPI Filters<br /><br /> Health and Diagnostics<br /><br /> HTTP Logging<br /><br /> Request Monitor<br /><br /> Security<br /><br /> Windows Authentication<br /><br /> Request Filtering<br /><br /> Performance<br /><br /> Static Content Compression<br /><br /> Management Tools<br /><br /> IIS Management Console|  
  
### Role and Role Services (Windows Server 2012 or Windows 8 operating systems)  
 On Windows Server 2012, you can use **Server Manager**, which is available in the Microsoft Management Console (MMC), to install the **Web Server (IIS)** role and the following required role services.  
  
> [!NOTE]  
>  On the Windows 8 operating system, use **Programs and Features** in Control Panel to enable these options in the **Windows Features** dialog box.  
  
||  
|-|  
|Internet Information Services<br /><br /> Web Management Tools<br /><br /> IIS Management Console<br /><br /> World Wide Web Services<br /><br /> Application Development<br /><br /> .NET Extensibility 3.5<br /><br /> .NET Extensibility 4.5<br /><br /> ASP.NET 3.5<br /><br /> ASP.NET 4.5<br /><br /> ISAPI Extensions<br /><br /> ISAPI Filters<br /><br /> Common HTTP Features<br /><br /> Default Document<br /><br /> Directory Browsing<br /><br /> HTTP Errors<br /><br /> Static Content<br /><br /> [Note: Do not install WebDAV Publishing]<br /><br /> Health and Diagnostics<br /><br /> HTTP Logging<br /><br /> Request Monitor<br /><br /> Performance<br /><br /> Static Content Compression<br /><br /> Security<br /><br /> Request Filtering<br /><br /> Windows Authentication|  
  
### Features (Windows Server 2008 or Windows Server 2008 R2, Windows 7 operating systems)  
 On [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] or Windows Server 2008 R2, you can use **Server Manager** to install the following required features.  
  
> [!NOTE]  
>  On [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)] and Windows 7 operating systems, use **Programs and Features** in Control Panel to enable these options in the **Windows Features** dialog box.  
  
||  
|-|  
|.NET Framework 3.0 Features<br /><br /> WCF Activation<br /><br /> HTTP Activation<br /><br /> Non-HTTP Activation<br /><br /> Windows Process Activation Service<br /><br /> Process Model<br /><br /> .NET Environment<br /><br /> Configuration APIs|  
  
### Features (Windows Server 2012 or Windows 8 operating systems)  
 On Windows Server 2012, you can use **Server Manager** to install the following required features.  
  
> [!NOTE]  
>  On the Windows 8 operating system, use **Programs and Features** in Control Panel to enable these options in the **Windows Features** dialog box.  
  
||  
|-|  
|.NET Framework 3.5 (includes .NET 2.0 and 3.0)<br /><br /> .NET Framework 4.5 Advanced Services<br /><br /> ASP.NET 4.5<br /><br /> WCF Services<br /><br /> HTTP Activation [Note: This is required.]<br /><br /> TCP Port Sharing<br /><br /> Windows Process Activation Service<br /><br /> Process Model<br /><br /> .NET Environment<br /><br /> Configuration APIs|  
  
### Accounts and Permissions  
  
|Type|Description|  
|----------|-----------------|  
|Windows account|You must log on to the web server computer with a Windows account that has permission to configure Windows roles, role services, and features, and to create and manage application pools, web sites, and web applications in IIS on the local computer.|  
|Service account|When you create the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application in [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)], you must specify an identity for the application pool that the application runs in. This account can be different from the service account that was specified when the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database was created.<br /><br /> This identity must be a domain user account, and it is added to the mds_exec database role in the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database for database access. For more information, see [Database Logins, Users, and Roles &#40;Master Data Services&#41;](../database-logins-users-and-roles-master-data-services.md). This account is also added to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] Windows group, **MDS_ServiceAccounts**, which is granted permission to the temporary compilation directory, **MDSTempDir**, in the file system. For more information, see [Folder and File Permissions &#40;Master Data Services&#41;](../folder-and-file-permissions-master-data-services.md).<br /><br /> The application pool account needs the VIEW SERVER STATE permission, to avoid server errors. For example, the MDS Validate Version command fails with a server error. For more information, see [MDS Validate Version command fails with a server error in SQL Server 2012 and SQL Server 2014](https://go.microsoft.com/fwlink/p/?LinkId=526304)|  
  
## See Also  
 [Install Master Data Services](install-master-data-services.md)   
 [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](create-a-master-data-manager-web-application-master-data-services.md)   
 [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../web-configuration-page-master-data-services-configuration-manager.md)  
  
  
