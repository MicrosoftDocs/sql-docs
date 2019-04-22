---
title: "Report Server Command Prompt Utilities (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "rsconfig utility"
  - "components [Reporting Services], command line utilities"
  - "rs utility"
  - "command prompt utilities [Reporting Services]"
  - "rskeymgmt utility"
ms.assetid: 68f2f9f4-f894-40ff-a71c-f9756bf4b68c
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Report Server Command Prompt Utilities (SSRS)
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes several command line utilities that you can use to administer a report server. These utilities are installed automatically when you install a report server.  
  
|Name|Command file|Supported Deployment mode|Description|  
|----------|------------------|-------------------------------|-----------------|  
|RSS utility|rs.exe|Native mode and SharePoint mode. The [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] release introduced SharePoint mode support.|The [rs utility](rs-exe-utility-ssrs.md) is a script host that you can use to perform scripted operations. Use this tool to run [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] scripts that copy data between report server databases, publish reports, create items in a report server database, and more. To learn more about using scripts to administer a server, see [Script Deployment and Administrative Tasks](script-deployment-and-administrative-tasks.md).|  
|Powershell cmdlets||SharePoint only|For a list of the of the powershell cmdlets, see [PowerShell cmdlets for Reporting Services SharePoint Mode](../powershell-cmdlets-for-reporting-services-sharepoint-mode.md).|  
|Rsconfig utility|rsconfig.exe|Native only|The [rsconfig utility](rsconfig-utility-ssrs.md) is used to configure and manage a report server connection to the report server database. You can also use it to specify a user account to use for unattended report processing. For more information, see [Reporting Services Report Server &#40;Native Mode&#41;](../report-server/reporting-services-report-server-native-mode.md). To learn more about connection configuration, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md).|  
|Rskeymgmt Utility|rskeymgmt.exe|Native only|The [rskeymgmt utility](rskeymgmt-utility-ssrs.md) is an encryption key management tool. You can use it to back up, apply, recreate, and delete symmetric keys. You can also use this tool to attach a report server instance to a shared report server database. Rskeymgmt can be used in database recovery operations. You can reuse an existing database in a new installation by applying a back up copy of the symmetric key. If the keys cannot be recovered, this tool provides a way to delete encrypted content that you no longer use. To learn more about key management and storage of sensitive data, see [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](../install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md) and [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../install-windows/ssrs-encryption-keys-manage-encryption-keys.md).|  
  
> [!NOTE]  
>  If you prefer to use a tool that has a graphical user interface, you can use the Reporting Services Configuration manager instead of `rsconfig` and `rskeymgmt`.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [Reporting Services Tools](reporting-services-tools.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](../report-server/reporting-services-report-server-native-mode.md)  
  
  
