---
title: Web Application Requirements
description: Find out about the requirements to install and run the Master Data Services web application hosted by Internet Information Services.
author: meetdeepak
ms.author: dkhare
ms.reviewer: randolphwest, mikeray
ms.date: 03/05/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: checklist
ms.custom:
  - build-2025
keywords: master data services
ai-usage: ai-assisted
---
# Web application requirements (Master Data Services)

[!INCLUDE [SQL Server Windows Only - ASDBMI](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](../includes/support-notice.md)]

[!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] is a web application hosted by Internet Information Services (IIS). [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] works only in Internet Explorer (IE) 9 or later. IE 8 and earlier versions, Microsoft Edge, and Chrome aren't supported.

**For instructions on how to install and configure IIS**, see [Installing and Configuring IIS](../master-data-services-installation-and-configuration.md#InstallIIS).

Use [!INCLUDE [ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] to create and configure the [!INCLUDE [ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application. [!INCLUDE [ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)] configures IIS on the local computer, so it's best for initial web configuration tasks. For example, configure a [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] environment with a single [!INCLUDE [ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, or configure the first web application in a scale-out deployment of [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)]. Use IIS tools to perform more complex tasks, such as configuring multiple web servers in a scale-out deployment.

> [!NOTE]  
> Any computer on which you install components of [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] must be licensed. For more information, see the End User License Agreement (EULA).

## Requirements

### Operating system

Before you install [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)], review the following requirements:

- [Hardware and software requirements for SQL Server 2016 and SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
- [Hardware and software requirements for SQL Server 2019](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)

::: moniker range="=sql-server-2016 || =sql-server-2017"

### Microsoft Silverlight

To work in the [!INCLUDE [ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, Silverlight 5 must be installed on the client computer. If you don't have the required version of Silverlight, you're prompted to install it when you navigate to an area of the web application that requires it.  
:::moniker-end

### Role and role services

You can use Windows **Server Manager**, which is available in the Microsoft Management Console (MMC), to install the **Web Server (IIS)** role and required role services.

> [!IMPORTANT]  
> **Dynamic Content Compression** is enabled by default. This significantly reduces the size of the xml response and saves the network I/O, though CPU usage is increased. For more information, see **Improved Performance** in [What's New in Master Data Services (MDS)](../what-s-new-in-master-data-services-mds.md).

- Internet Information Services
- Web Management Tools
- IIS Management Console
- World Wide Web Services
- Application Development
- .NET Extensibility 3.5
- .NET Extensibility 4.5
- ASP.NET 3.5
- ASP.NET 4.5
- ISAPI Extensions
- ISAPI Filters
- Common HTTP Features
- Default Document
- Directory Browsing
- HTTP Errors
- Static Content [Note: Don't install WebDAV Publishing.]
- Health and Diagnostics
- HTTP Logging
- Request Monitor
- Performance
- Static Content Compression
- Security
- Request Filtering
- Windows Authentication

### Features

You can use Windows **Server Manager** to install the following required features.

- .NET Framework 3.5 (includes .NET 2.0 and 3.0)
- .NET Framework 4.5 Advanced Services
- ASP.NET 4.5
- WCF Services
- HTTP Activation [Note: This is required.]
- TCP Port Sharing
- Windows Process Activation Service
- Process Model
- .NET Environment
- Configuration APIs
- Dynamic Content Compression

Following is an example PowerShell script to add prerequisite server roles and features. The prerequisite server roles and features vary depending on the environment.

```powershell
Install-WindowsFeature Web-Mgmt-Console, AS-NET-Framework, Web-Asp-Net, Web-Asp-Net45, Web-Default-Doc, Web-Dir-Browsing, Web-Http-Errors, Web-Static-Content, Web-Http-Logging, Web-Request-Monitor, Web-Stat-Compression, Web-Filtering, Web-Windows-Auth, NET-Framework-Core, WAS-Process-Model, WAS-NET-Environment, WAS-Config-APIs

Install-WindowsFeature Web-App-Dev, NET-Framework-45-Features -IncludeAllSubFeature -Restart
```

For more information about the PowerShell command, see [Install-WindowsFeature](/powershell/module/servermanager/install-windowsfeature).

### Accounts and permissions

| Type | Description |
| --- | --- |
| Windows account | You must connect to the web server computer with a Windows account that has permission to configure Windows roles, role services, and features, and to create and manage application pools, web sites, and web applications in IIS on the local computer. |
| Service account | When you create the [!INCLUDE [ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application in [!INCLUDE [ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)], you must specify an identity for the application pool that the application runs in. This account can be different from the service account that was specified when the [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] database was created.<br /><br />This identity must be a domain user account, and it's added to the **mds_exec** database role in the [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] database for database access. For more information, see [Database Logins, Users, and Roles (Master Data Services)](../database-logins-users-and-roles-master-data-services.md). This account is also added to a [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] Windows group, `MDS_ServiceAccounts`, which is granted permission to the temporary compilation directory, `MDSTempDir`, in the file system. For more information, see [Folder and File Permissions (Master Data Services)](../folder-and-file-permissions-master-data-services.md). |

## Related content

- [Installation Tasks for Master Data Services](install-master-data-services.md)
- [Create a master data manager web application (Master Data Services)](create-a-master-data-manager-web-application-master-data-services.md)
- [Web Configuration Page (Master Data Services Configuration Manager)](../web-configuration-page-master-data-services-configuration-manager.md)
