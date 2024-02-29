---
title: "Report Server command prompt utilities"
description: Learn about the SQL Server Reporting Services command line utilities that are used to administer a report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rsconfig utility"
  - "components [Reporting Services], command line utilities"
  - "rs utility"
  - "command prompt utilities [Reporting Services]"
  - "rskeymgmt utility"
---
# Report Server command prompt utilities (SSRS)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes several command line utilities that you can use to administer a report server. These utilities are installed automatically when you install a report server.  
  
|Name|Command file|Supported Deployment mode|Description|  
|----------|------------------|-------------------------------|-----------------|  
|RSS utility|rs.exe|Native mode and SharePoint mode. The [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)] release introduced SharePoint mode support.|The [rs utility](../../reporting-services/tools/rs-exe-utility-ssrs.md) is a script host that you can use to perform scripted operations. Use this tool to run [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] scripts that copy data between report server databases, publish reports, create items in a report server database, and more. To learn more about using scripts to administer a server, see [Script deployment and administrative tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md).|  
|PowerShell cmdlets||SharePoint only|For a list of the PowerShell cmdlets, see [PowerShell cmdlets for Reporting Services SharePoint mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).|  
|Rsconfig utility|rsconfig.exe|Native only|The [rsconfig utility](../../reporting-services/tools/rsconfig-utility-ssrs.md) is used to configure and manage a report server connection to the report server database. You can also use it to specify a user account to use for unattended report processing. For more information, see [Reporting Services report server &#40;Native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md). To learn more about connection configuration, see [Configure a report server database connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).|  
|Rskeymgmt Utility|rskeymgmt.exe|Native only|The [rskeymgmt utility](../../reporting-services/tools/rskeymgmt-utility-ssrs.md) is an encryption key management tool. You can use it to back up, apply, recreate, and delete symmetric keys. You can also use this tool to attach a report server instance to a shared report server database. Rskeymgmt can be used in database recovery operations. You can reuse an existing database in a new installation by applying a backup copy of the symmetric key. If the keys can't be recovered, this tool provides a way to delete encrypted content that you no longer use. To learn more about key management and storage of sensitive data, see [Store encrypted report server data &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md) and [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
  
> [!NOTE]  
>  If you prefer to use a tool that has a graphical user interface, you can use the Report Server Configuration Manager instead of **rsconfig** and **rskeymgmt**.  
  
## Related content 
 [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Reporting Services tools](../../reporting-services/tools/reporting-services-tools.md)   
 [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)  
  
  
