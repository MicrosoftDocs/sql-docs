---
title: "Files-only installation (Reporting Services)"
description: "Files-Only Installation (Reporting Services)"
author: maggiesMSFT
ms.author: maggies
ms.date: 05/24/2018
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "files-only installation [Reporting Services]"
  - "installation options [Reporting Services]"
---
# Files-only installation (Reporting Services)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

  *Files-only installation* refers to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation where Setup creates the folder structure for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] program files, copies the files to disk, registers the Report Server service on the local computer, configures the service account, grants files permissions to the service account, and registers the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI provider.  
  
 A files-only installation includes the following [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features: Report Server service (which hosts the Report Server Web service and background processing application), Report Builder, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] command line utilities (rsconfig.exe, rskeymgmt.exe and rs.exe). It doesn't apply to shared features such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], which must be specified as separate items if you want to install them.  
  
 In contrast with other installation modes, a report server that is installed in files-only mode isn't operational when Setup is finished. Extra configuration is required to bring the report server online by using the [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).  
  
## When to select files-only installation mode  

 A files-only installation must be performed when:  
  
- You want to connect the report server to a remote report server database.  
  
- You want to install the report server as a named instance.  
  
- You have deployment requirements that include using custom settings or functionality, and you want full control over when and how the server is configured.  
  
- Installing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster that includes [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
## How to perform a files-only installation  

 Files-only installation is the default for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 You can specify a files-only installation through the command line or in the Installation wizard. The following articles provide step-by-step instructions:  
  
- [Install SQL Server from the installation wizard &#40;setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).  
  
- [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
### Example command line script  

 For clarity, the example includes ```/RSINSTALLMODE="FilesOnlyMode"```. However, because files-only mode is the default, you can omit this argument and still get a files-only mode installation.  
  
```  
setup /q /ACTION=install /FEATURES=RS /InstanceName=MSSQLSERVER /RSSVCACCOUNT="NT AUTHORITY\NETWORK SERVICE" /RSINSTALLMODE="FilesOnlyMode"  
```  
  
### Installation wizard  

 When you select [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the Feature Selection page, Setup provides a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration page that enables you to specify the installation mode. To specify a files-only installation, select **Install but do not configure the report server** on the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration page.  
  
## Related content 

- [Verify a Reporting Services installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md)
- [Configure the Report Server Service Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Configure report server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
- [Configure a report server database connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)
::: moniker range="=sql-server-2016"

- [Install Reporting Services SharePoint mode](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)

::: moniker-end

- [Install Reporting Services native mode report server](~/reporting-services/install-windows/install-reporting-services-native-mode-report-server.md)
- [Reporting Services tools](../../reporting-services/tools/reporting-services-tools.md)  
  
