---
description: "Install Reporting Services 2016 at the Command Prompt - SSRS"
title: "Install Reporting Services 2016 at the Command Prompt - SSRS | Microsoft Docs"
ms.date: 01/09/2018
ms.service: reporting-services
ms.topic: conceptual
helpviewer_keywords:
  - "command line"
ms.assetid: 048169b3-512c-41e4-895a-0416eff41268
author: maggiesMSFT
ms.author: maggies
monikerRange: "= sql-server-2016"
ms.custom:
  - intro-installation
---
# Install Reporting Services 2016 at the Command Prompt

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a command-line installation from the SQL Server setup program. This topic contains several examples of command-line installations that are specific to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For a complete description of the command-line options available for all SQL Server components, see [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md). This topic does not describe command-line options for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products. For information on command installation of the add-in, see [Install the add-in using the installation file rsSharePoint.msi](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md#bkmk_install_rssharepoint).

##  <a name="bkmk_native_mode"></a> Native mode Reporting Services

### RSINSTALLMODE (Native Mode)
 The primary input setting for installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is the **/RSINSTALLMODE** input setting. The setting has two options: **DefaultNativeMode** and **FilesOnlyMode**  
  
 If the installation includes the SQL Server Database engine, the default RSINSTALLMODE is DefaultNativeMode.If the installation does not include the SQL Server Database engine, the default RSINSTALLMODE is FilesOnlyMode.If you choose DefaultNativeMode but the installation does not include the SQL Server Database engine, the installation automatically changes the RSINSTALLMODE to FilesOnlyMode. For more information on the input settings, see [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

### Examples of Native Mode Installation

 The following example installs the following and configures the accounts for :  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in native mode.  
  
-   The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
-   SQL Server Agent, which is needed for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions features.  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
```console
Setup.exe /q /IACCEPTSQLSERVERLICENSETERMS /ACTION="install" /ERRORREPORTING=1 /UPDATEENABLED="False" /INSTANCENAME="MSSQLSERVER" /FEATURES="SQLEngine,Adv_SSMS,RS" /RSINSTALLMODE="DefaultNativeMode" /SQLSVCACCOUNT="[DOMAIN\ACCOUNT]" /SQLSVCPASSWORD="[PASSWORD]" /AGTSVCACCOUNT="[DOMAIN\ACCOUNT]" /AGTSVCPASSWORD="[PASSWORD]" /SQLSYSADMINACCOUNTS="[DOMAIN\ACCOUNT]"  
```  

[!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]
  
##  <a name="bkmk_sharepoint_mode"></a> SharePoint mode Reporting Services  
  
### RSSHPINSTALLMODE (SharePoint Mode)  
 The input setting to install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode is **/RSSHPINSTALLMODE**. The input setting has one option: SharePointFilesOnlyMode. The option installs all the files needed for SharePoint mode but, configuration is required following installation. The additional configuration steps are completed using SharePoint Central Administration. For more information, see [Install the first Report Server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md).  
  
### Examples of SharePoint Mode Installation  
 The following example installs SQL Server the database engine service and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode as well as the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint (RS_SHPWFE).  
  
```  
setup /q /ACTION=install /FEATURES=SQL, RS_SHP, RS_SHPWFE,TOOLS /INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="BUILTIN\ADMINISTRATORS" /RSSVCACCOUNT="NT AUTHORITY\NETWORK SERVICE" /SQLSVCACCOUNT="NT AUTHORITY\NETWORK SERVICE" /AGTSVCACCOUNT="NT AUTHORITY\NETWORK SERVICE"  
```  
  
 The following example installs only [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.  
  
```  
Setup.exe /q /ACTION="Install" /IACCEPTSQLSERVERLICENSETERMS /FEATURES="RS_SHP" /INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /INSTALLSHAREDDIR="C:\Program Files\Microsoft SQL Server" /INSTALLSHAREDWOWDIR="C:\Program Files (x86)\Microsoft SQL Server" /INSTALLSQLDATADIR="C:\Program Files\Microsoft SQL Server" /SECURITYMODE="SQL" /SAPWD="[PASSWORD]" /PID="[Your PID Value]" /SQLSYSADMINACCOUNTS="[Account Name]" "AutoSql Admin Group" /ASSYSADMINACCOUNTS="[Account Name]" /UPDATEENABLED="False"  
  
```  
  
### Examples of SharePoint Mode Upgrade  
 The following example upgrades [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode. **RSUPGRADEPASSWORD** is the password of the existing Report Server service account. RSUPGRADEPASSWORD is a required field in an upgrade scenario unless the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service account is a built-in account.  
  
```  
Setup.exe /q /ACTION="Upgrade" /INSTANCENAME="MSSQLSERVER" /PID="[PID value]" /FTSVCACCOUNT="[DOMAIN\ACCOUNT]" /FTSVCPASSWORD="[PASSWORD]" /UPDATEENABLED="False" /IACCEPTSQLSERVERLICENSETERMS /RSUPGRADEPASSWORD="[PASSWORD]"  
```  
  
 The following example can be used to upgrade a SharePoint Mode installation that is based on the SharePoint shared service architecture. The example uses switch ALLOWUPGRADEFORSSRSSHAREPOINTMODE. The switch is not needed for upgrading older versions that are not based on the shared service architecture:  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
```  
Setup.exe /q /ACTION="Upgrade" /INSTANCENAME="MSSQLSERVER" /PID="[Your PID Value]" /FTSVCACCOUNT="[ACCOUNT Name]" /FTSVCPASSWORD="[PASSWORD]" /UPDATEENABLED="False" /IACCEPTSQLSERVERLICENSETERMS /ALLOWUPGRADEFORSSRSSHAREPOINTMODE="True"  
```

## Next steps

[Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)   
[SysPrep Parameters](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#SysPrep)   
[Install Power Pivot from the Command Prompt](/analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013#bkmk_install)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
