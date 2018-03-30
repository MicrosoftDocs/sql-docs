---
title: "Removing a Rendering Extension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "deleting rendering extensions"
  - "removing rendering extensions"
  - "rendering extensions [Reporting Services], removing"
ms.assetid: 2abfebfb-065f-45cc-a904-c914394cf900
caps.latest.revision: 37
author: "douglaslM"
ms.author: "carlasab"
manager: "jhubbard"
---
# Removing a Rendering Extension
  To remove a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] rendering extension, simply remove the `Extension` element for your rendering extension from the rsreportserver.config file, located in **%ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<Instance Name>\Reporting Services\ReportServer** folder. If you made entries for a Report Designer as well as a report server, remove the `Extension` element from the [RSReportDesigner Configuration File](../../../2014/reporting-services/rsreportdesigner-configuration-file.md) as well. After the configuration information is removed, the rendering extension is no longer available to the component.  
  
## See Also  
 [Reporting Services Configuration Files](../../../2014/reporting-services/reporting-services-configuration-files.md)   
 [Implementing a Rendering Extension](../../../2014/reporting-services/dev-guide/implementing-a-rendering-extension.md)   
 [Rendering Extensions Overview](../../../2014/reporting-services/dev-guide/rendering-extensions-overview.md)   
 [Implementing the IRenderingExtension Interface](../../../2014/reporting-services/dev-guide/implementing-the-irenderingextension-interface.md)   
 [Security Considerations for Extensions](../../../2014/reporting-services/dev-guide/security-considerations-for-extensions.md)   
 [Deploying a Rendering Extension](../../../2014/reporting-services/dev-guide/deploying-a-rendering-extension.md)  
  
  