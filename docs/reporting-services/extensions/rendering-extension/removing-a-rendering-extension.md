---
title: "Removing a Rendering Extension | Microsoft Docs"
ms.date: 03/18/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "deleting rendering extensions"
  - "removing rendering extensions"
  - "rendering extensions [Reporting Services], removing"
ms.assetid: 2abfebfb-065f-45cc-a904-c914394cf900
author: maggiesMSFT
ms.author: maggies
---
# Removing a Rendering Extension
  To remove a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] rendering extension, simply remove the **Extension** element for your rendering extension from the rsreportserver.config file, located in **%ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<Instance Name>\Reporting Services\ReportServer** folder. If you made entries for a Report Designer as well as a report server, remove the **Extension** element from the [RSReportDesigner Configuration File](../../../reporting-services/report-server/rsreportdesigner-configuration-file.md) as well. After the configuration information is removed, the rendering extension is no longer available to the component.  
  
## See Also  
 [Reporting Services Configuration Files](../../../reporting-services/report-server/reporting-services-configuration-files.md)   
 [Implementing a Rendering Extension](../../../reporting-services/extensions/rendering-extension/implementing-a-rendering-extension.md)   
 [Rendering Extensions Overview](../../../reporting-services/extensions/rendering-extension/rendering-extensions-overview.md)   
 [Implementing the IRenderingExtension Interface](../../../reporting-services/extensions/rendering-extension/implementing-the-irenderingextension-interface.md)   
 [Security Considerations for Extensions](../../../reporting-services/extensions/security-considerations-for-extensions.md)   
 [Deploying a Rendering Extension](../../../reporting-services/extensions/rendering-extension/deploying-a-rendering-extension.md)  
  
  
