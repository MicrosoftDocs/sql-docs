---
title: "Implementing a Rendering Extension | Microsoft Docs"
ms.date: 03/16/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "rendering extensions [Reporting Services]"
  - "custom rendering extensions [Reporting Services]"
  - "transformations [Reporting Services]"
  - "extensions [Reporting Services], rendering"
  - "rendering extensions [Reporting Services], implementing"
ms.assetid: 4a5c64f5-2391-4597-ba3f-81d265b23703
author: markingmyname
ms.author: maghan
---
# Implementing a Rendering Extension
  A rendering extension is a component or module of a report server that transforms report data and layout information into a device-specific format. SQL Server Reporting Services includes six rendering extensions: HTML, Excel, Word, CSV or Text, XML, Image, and PDF. You can create additional rendering extensions to generate reports in other formats.  
  
> [!NOTE]  
>  To determine which rendering extensions are available, you can view the list of installed extensions in the RSReportServer.config file.  
  
## In This Section  
 [Rendering Extensions Overview](../../../reporting-services/extensions/rendering-extension/rendering-extensions-overview.md)  
 Introduces how to write a custom rendering extension for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
 [Implementing the IRenderingExtension Interface](../../../reporting-services/extensions/rendering-extension/implementing-the-irenderingextension-interface.md)  
 Describes the attributes of a rendering extension.  
  
 [Deploying a Rendering Extension](../../../reporting-services/extensions/rendering-extension/deploying-a-rendering-extension.md)  
 Describes how to deploy a rendering extension on a report server.  
  
 [Removing a Rendering Extension](../../../reporting-services/extensions/rendering-extension/removing-a-rendering-extension.md)  
 Describes how to remove a rendering extension from a report server.  
  
## See Also  
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
