---
title: "Implement a rendering extension"
description: Find out how to transform Reporting Services report data and layout information into device-specific formats by implementing rendering extensions.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/16/2017
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rendering extensions [Reporting Services]"
  - "custom rendering extensions [Reporting Services]"
  - "transformations [Reporting Services]"
  - "extensions [Reporting Services], rendering"
  - "rendering extensions [Reporting Services], implementing"
---
# Implement a rendering extension
  A rendering extension is a component or module of a report server that transforms report data and layout information into a device-specific format. SQL Server Reporting Services includes six rendering extensions: HTML, Excel, Word, CSV or Text, XML, Image, and PDF. You can create other rendering extensions to generate reports in other formats.  
  
> [!NOTE]  
>  To determine which rendering extensions are available, you can view the list of installed extensions in the RSReportServer.config file.  
  
## In this section  
 [Rendering extensions overview](../../../reporting-services/extensions/rendering-extension/rendering-extensions-overview.md)  
 Introduces how to write a custom rendering extension for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
 [Implement the IRenderingExtension interface](../../../reporting-services/extensions/rendering-extension/implementing-the-irenderingextension-interface.md)  
 Describes the attributes of a rendering extension.  
  
 [Deploy a rendering extension](../../../reporting-services/extensions/rendering-extension/deploying-a-rendering-extension.md)  
 Describes how to deploy a rendering extension on a report server.  
  
 [Remove a rendering extension](../../../reporting-services/extensions/rendering-extension/removing-a-rendering-extension.md)  
 Describes how to remove a rendering extension from a report server.  
  
## Related content

- [Reporting Services extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
