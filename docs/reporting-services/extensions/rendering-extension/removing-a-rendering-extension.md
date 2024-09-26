---
title: "Remove a rendering extension"
description: Find out how to remove a rendering extension from Reporting Services so that it's no longer available to the report server and Report Designer.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "deleting rendering extensions"
  - "removing rendering extensions"
  - "rendering extensions [Reporting Services], removing"
---
# Remove a rendering extension
  To remove a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] rendering extension, remove the **Extension** element for your rendering extension from the rsreportserver.config file, located in the ``%ProgramFiles%\Microsoft SQL Server\MSRS10_50.\<Instance Name>\Reporting Services\ReportServer`` folder. If you made entries for a Report Designer and a report server, remove the **Extension** element from the [RSReportDesigner configuration file](../../../reporting-services/report-server/rsreportdesigner-configuration-file.md) as well. After the configuration information is removed, the rendering extension is no longer available to the component.  
  
## Related content

- [Reporting Services configuration files](../../../reporting-services/report-server/reporting-services-configuration-files.md)
- [Implement a rendering extension](../../../reporting-services/extensions/rendering-extension/implementing-a-rendering-extension.md)
- [Rendering extensions overview](../../../reporting-services/extensions/rendering-extension/rendering-extensions-overview.md)
- [Implement the IRenderingExtension interface](../../../reporting-services/extensions/rendering-extension/implementing-the-irenderingextension-interface.md)
- [Security considerations for extensions](../../../reporting-services/extensions/security-considerations-for-extensions.md)
- [Deploy a rendering extension](../../../reporting-services/extensions/rendering-extension/deploying-a-rendering-extension.md)
