---
title: "Deploy a data processing extension"
description: Learn how to make your Reporting Services data processing extension discoverable by the report server and by Report Designer.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - intro-deployment
  - updatefrequency5
helpviewer_keywords:
  - "data processing extensions [Reporting Services], deploying"
  - "Extension element"
  - "deploying [Reporting Services], extensions"
---
# Deploy a data processing extension
  Once you write and compile your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension into a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] library, you need to make it discoverable by the report server and by Report Designer. This process is as easy as copying the extension to the appropriate directories and adding entries to the appropriate [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration files.  
  
## Configuration file Extension element
 Data processing extensions that you deploy to the report server or Report Designer need to be entered as **Extension** elements in the configuration files. These files are RSReportServer.config for the report server and RSReportDesigner.config for Report Designer.  
  
 The following table describes the attributes for the **Extension** element for data processing extensions.  
  
|Attribute|Description|  
|---------------|-----------------|  
|**Name**|A unique name for the extension, for example, "SQL" for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data processing extension or "OLEDB" for the OLE DB data processing extension. The maximum length for the **Name** attribute is 255 characters. The name must be unique among all entries within the **Extension** element of a configuration file.|  
|**Type**|A comma-separated list that includes the fully qualified namespace along with the name of the assembly.|  
|**Visible**|A value of **false** indicates that the data processing extension shouldn't be visible in user interfaces. If the attribute isn't included, the default value is **true**.|  
  
 For more information about the RSReportServer.config or RSReportDesigner.config files, see [Reporting Services configuration files](../../../reporting-services/report-server/reporting-services-configuration-files.md).  
  
## In this section  
  
|Article|Description|  
|-----------|-----------------|  
|[Deploy a data processing extension to a report server](../../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension-to-a-report-server.md)|Describes how to deploy your data processing extension to a report server.|  
|[Deploy a data processing extension to Report Designer](../../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension-to-report-designer.md)|Describes how to deploy your data processing extension to Report Designer.|  
  
## Related content

- [Reporting Services extensions](../../../reporting-services/extensions/reporting-services-extensions.md)
- [Implement a data processing extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
