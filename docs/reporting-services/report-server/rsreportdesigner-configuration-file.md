---
title: "RSReportDesigner configuration file"
description: Learn about the configuration file that stores settings about the rendering and data processing extensions available to Report Designer.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Report Designer [Reporting Services], configuration file"
  - "RSReportDesigner configuration file"
---
# RSReportDesigner configuration file
  The `RSReportDesigner.config` file stores settings about the rendering and data processing extensions available to Report Designer. Data processing extension information is stored in the `Data` element. Rendering extension information is stored in the `Render` element. The `Designer` element enumerates the query builders that are used in Report Designer.  
  
 Report Designer uses embedded report server functionality to preview reports. Server-related settings can be specified to support local server-side processing for preview operations. For more information about report server configuration settings, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
## File location  
 This file is located in the `\Program Files\Microsoft Visual Studio 8\Common7\IDE\PrivateAssemblies`.  
  
## Edit guidelines  
 Don't modify the settings in this file unless you're doing one of the following actions:

- Deploying or removing a custom extension
- Disabling caching during preview
- Registering a new data processing extension after a Service Pack upgrade 
  
 Specific instructions for editing configuration files are available if you're customizing rendering extension settings. For more information, see [Customize rendering extension parameters in RSReportServer.Config](../../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md).  
  
 For general instructions on how to edit configuration files, see [Modify a Reporting Services configuration file &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md).  
  
## Example configuration file  
 The following example illustrates the format of the `RSReportDesigner.config` file.  
  
```  
<Configuration>  
  <Add Key="SecureConnectionLevel" Value="0" />  
  <Add Key="InstanceName" Value="Microsoft.ReportingServices.PreviewServer" />  
  <Add Key="SessionCookies" Value="true" />  
  <Add Key="SessionTimeoutMinutes" Value="3" />  
  <Add Key="PolicyLevel" Value="rspreviewpolicy.config" />  
  <Add Key="CacheDataForPreview" Value="true" />  
  <Extensions>  
    <Render> . . . </Render>  
    <Data> . . . </Data>  
    <Designer> . . . </Designer>  
```  
  
## Configuration settings  
  
|Setting|Description|  
|-------------|-----------------|  
|**SecureConnectionLevel**|Specifies the degree of security of the Web service connection. Valid values range from 0 through 3, where 0 is least secure. For more information, see [Use secure web service methods](../../reporting-services/report-server-web-service/net-framework/using-secure-web-service-methods.md).|  
|**InstanceName**|An identifier for the preview server. Don't modify this value.|  
|**SessionCookies**|Specifies whether the report server uses browser cookies to maintain session information. Valid values include **True** and **False**. The default is **True**. If you set this value to false, session data is stored in the **reportservertempdb** database.|  
|**SessionTimeoutMinutes**|Specifies the period for which a session cookie is valid. The default is 3 minutes.|  
|**PolicyLevel**|Specifies the security policy configuration file. The valid value is `Rspreviewpolicy.config`. For more information, see [Use Reporting Services security policy files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).|  
|**CacheDataForPreview**|When set to **True**, the Report Designer stores data in a cache file on the local computer. Valid values are **True** (default) and **False**. For more information, see [Preview reports](../../reporting-services/reports/previewing-reports.md).|  
|**Render**|Enumerates the rendering extensions that are available to Report Designer for preview purposes. The set of rendering extensions that are used for preview should be identical to those extensions installed with the report server.<br /><br /> **Name** specifies the rendering extension. If you're invoking a rendering extension through code, use this value to call a specific extension.<br /><br /> **Type** specifies the fully qualified class name of the extension class, plus the library name, comma separated.<br /><br /> **Visible** specifies whether the name appears in any user interface. This value can be **True** (default) or **False**. If **True**, it appears in user interfaces.|  
|**Data**|Enumerates the data processing extensions that are available to Report Designer for connecting to data sources that provide data to reports. The set of data processing extensions used in Report Designer might be identical to those extensions installed with the report server. If you're adding or removing custom extensions, see [Deploy a data processing extension](../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md).<br /><br /> **Name** specifies the data processing extension.<br /><br /> **Type** specifies the fully qualified class name of the extension class, plus the library name, comma separated.|  
|**Designer**|Enumerates the query builders that are available to Report Designer. Query builders provide a user interface for constructing queries that retrieve data used in reports. Query builders might vary for different data processing extensions. By default, Reporting Services provides one visual data tool user interface for all data processing extensions that are included in the product. However, if you're building or using non-Microsoft data processing extensions, other query builder interfaces might apply.|  
|**PreviewProcessingServiceStartupTimeoutSeconds**|Specifies the period to wait for the preview processing service to start up before showing an error message. The default is 15 seconds.|  
  
## Related content

- [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md)
- [Query design tools &#40;SSRS&#41;](../../reporting-services/report-data/query-design-tools-ssrs.md)
