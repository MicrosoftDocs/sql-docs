---
title: "Custom extensions were detected on the report server (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "rendering extensions [Reporting Services], custom extensions"
  - "security extensions [Reporting Services]"
  - "custom extensions [Reporting Services]"
  - "data processing extensions [Reporting Services], custom extensions"
  - "delivery extensions [Reporting Services]"
ms.assetid: fa184bd7-11d6-4ea3-9249-bc1b13db49e5
author: markingmyname
ms.author: maghan
manager: craigg
---
# Custom extensions were detected on the report server (Upgrade Advisor)
  Upgrade Advisor detected custom extension settings in the configuration files, indicating that your installation includes one or more custom extensions for data processing, delivery, rendering, security, or authentication. Upgrade will move the extension configuration settings with the upgraded report server. However, if the custom extensions are installed in the existing report server installation folder, the assembly files for those custom extensions will not be moved to the new installation folder during the upgrade process. After upgrade completes, you must move the assembly files to the new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation folder.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides an extensible architecture that allows developers to create custom extensions for data processing, delivery, rendering, security, and authentication.  
  
 If custom extensions or assemblies are used in your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, you can use Setup to perform an upgrade, but you may need to move extensions to the new installation location after upgrade completes, or you may need to perform steps prior to upgrade.  
  
> [!NOTE]  
>  Upgrade Advisor does not detect whether custom code assemblies are configured for use in reports to calculate item values, styles, and formatting. For more information, see [Other Reporting Services Upgrade Issues](../../../2014/sql-server/install/other-reporting-services-upgrade-issues.md).  
  
 If you purchased custom extensions from a software vendor, check with the vendor for additional information about upgrading your custom functionality.  
  
## Corrective Action  
 Use the following sections to determine steps to perform in addition to or prior to performing an upgrade of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]:  
  
 [Custom data processing or delivery extensions](#dataprocdeliver)  
  
 [Custom rendering extensions](#render)  
  
 [Custom security or authentication extensions on a SQL Server 2000 report server](#secauth2000)  
  
 [Custom security or authentication extensions on a SQL Server 2005 report server](#secauth2005)  
  
 After upgrade completes, move the extension assemblies to the new installation folder and then verify that the custom extensions work as expected. If your extension does not work, you might have to recompile it.  
  
#### To recompile an extension  
  
1.  Copy the Microsoft.ReportingServices.Interfaces.dll file to the folder that contains your source code.  
  
2.  Open the project that contains your source files and add a reference to the Microsoft.ReportingServices.Interfaces.dll file.  
  
3.  Rebuild the solution to bind the extension.  
  
 If you decide not to continue with upgrade, you might decide to migrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instead. For steps on migrating custom extensions, see [Migrating custom extensions](#migrcustext) in this topic.  
  
###  <a name="dataprocdeliver"></a> Custom data processing or delivery extensions  
 If Upgrade Advisor detects custom data processing or delivery extensions, the upgrade process is not blocked. However, after upgrade completes, you might need to perform additional steps before the custom functionality provided by these extensions will work. For example, you must perform additional steps when the custom extension files are installed in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation folder.  
  
##### Post-upgrade steps for custom data processing or delivery extensions  
  
1.  Move the extension file or files to the new program folder for the report server. By default, the report server program folder is in \Program Files\Microsoft SQL Server\MSRS10_50.\<*instance_name*>\report server.  
  
 For more information, see "Deploying a Data Processing Extension" and "Implementing a Delivery Extension" in SQL Server Books Online.  
  
###  <a name="render"></a> Custom rendering extensions  
 If Upgrade Advisor detects custom rendering extensions, the upgrade process is blocked. You can continue with the upgrade process by removing the custom extension configuration entries from the configuration file. However, this will cause the custom extensions to be unavailable to users after upgrade completes. If you need custom rendering extensions after upgrade, you must build updated rendering extensions or obtain updated rendering extensions from a custom extension vendor.  
  
 You must perform steps to enable an upgrade or you may chose to migrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instead.  
  
> [!IMPORTANT]  
>  Do not upgrade or migrate your report server until you have tested and verified that the updated rendering extension works as expected.  
  
##### To upgrade custom rendering extensions  
  
1.  Obtain rendering extensions with the latest interfaces.  
  
2.  Remove the old custom rendering extension entry or entries from RSReportServer.config.  
  
3.  Upgrade the report server.  
  
4.  After upgrade completes, install the updated extensions on the report server.  
  
 For more information, see "Implementing a Rendering Extension" in SQL Server Books Online.  
  
###  <a name="secauth2000"></a> Custom security or authentication extensions on a SQL Server 2000 report server  
 If Upgrade Advisor detects custom security or authentication extensions on a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] report server, the upgrade process is blocked. You must perform steps to enable an upgrade or you may chose to migrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instead. In either case, you must update and recompile the extensions with the latest interfaces in Microsoft.ReportingServices.Interfaces.dll, because the interfaces have changed between [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
> [!IMPORTANT]  
>  Do not upgrade or migrate your report server until you have tested and verified that the updated security or authentication extension works as expected.  
  
 If you are using a custom authentication extension that you created for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2000 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must modify the source code to support new classes and members introduced for model-driven reporting.  
  
##### To upgrade custom security or authentication extensions from a SQL Server 2000 report server  
  
1.  Update and recompile any security or authentication extensions with the latest interfaces.  
  
2.  Remove the security or authentication extension entry or entries from RSReportServer.config.  
  
3.  Upgrade the report server.  
  
4.  After upgrade completes, install the updated extensions on the report server.  
  
 For more information, see "Implementing a Security Extension" in SQL Server Books Online.  
  
###  <a name="secauth2005"></a> Custom security or authentication extensions on a SQL Server 2005 report server  
 If Upgrade Advisor detects custom security or authentication extensions on a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] report server, the upgrade process is blocked. You must perform steps to enable an upgrade or you may chose to migrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instead.  
  
##### To upgrade custom security or authentication extensions from a SQL Server 2005 report server  
  
1.  Remove the security or authentication extension configuration entries from RSReportServer.config.  
  
2.  Upgrade the report server.  
  
3.  After upgrade completes, add the configuration entries back in RSReportServer.config.  
  
4.  If the extension assemblies were installed in the old [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation folder, move then to the new installation folder.  
  
 For more information, see "Implementing a Security Extension" in SQL Server Books Online.  
  
###  <a name="migrcustext"></a> Migrating custom extensions  
 If you decide to migrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instead performing an upgrade, use the steps to migrate custom extensions to the new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance.  
  
##### To migrate custom extensions to a new Reporting Services instance  
  
1.  Build or obtain updated extensions with the latest [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] interfaces.  
  
2.  Migrate the report server to a new instance.  
  
3.  Configure the extensions on the new instance.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
