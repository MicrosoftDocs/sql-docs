---
title: "XML Device Information Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "XML [Reporting Services], rendering"
  - "device information settings [Reporting Services], PDF rendering"
ms.assetid: a32e83fe-c10e-4ebd-8975-5be7dcc422e7
author: markingmyname
ms.author: maghan
manager: kfile
---
# XML Device Information Settings
  The following table lists the device information settings for rendering in XML format.  
  
|Setting|Value|  
|-------------|-----------|  
|`XSLT`|The path in the report server namespace of an XSLT to apply to the XML file, for example `/Transforms/myxslt`. The xsl file must be a published resource on the report server and you must access it through a report server item path. The value of this setting is applied after any XSLT that is specified in the report. If the `XSLT` setting is applied, the `OmitSchema` setting is ignored.|  
|**MIMEType**|The Multipurpose Internet Mail Extensions (MIME) type of the XML file.|  
|**UseFormattedValues**|Indicates whether to render the formatted value of a text box when generating the XML data. A value of false indicates that the underlying value of the text box is used.|  
|**Indented**|Indicates whether to generate indented XML. The default value of `false` generates non-indented, compressed XML.|  
|`OmitNamespace`|Indicates whether to omit the default namespace from the XML.<br /><br /> If true, the XML does not specify a default namespace.<br /><br /> If false, the XML specifies a default namespace with the value of the report's DataSchema property. The DataSchema property defaults to the report name.<br /><br /> The default value is `false`.|  
|`OmitSchema`|Indicates whether to omit the schema location from the XML. The location is the SchemaLocation attribute. The default value of OmitSchema depends on the value of OmitNamespace:<br /><br /> If OmitNamespace = False, then OmitSchema = `False` by default. The user can override the default by setting OmitSchema = True.<br /><br /> If OmitNamespace = True, then OmitSchema will function as `True` regardless of the value explicitly configured for OmitShema.|  
|**Encoding**|The Internet Assigned Numbers Authority (IANA) name of a character encoding that is supported by the .NET Framework. The default value is `UTF-8`. Examples of other values include ASCII, UTF-7, and UTF-16.|  
|**FileExtension**|The file extension to use for the generated file.|  
|**Schema**|Indicates whether the XML schema definition (XSD) is rendered or whether the actual XML data is rendered. A value of `true` indicates that an XML schema is rendered. The default value is `false`.|  
  
## See Also  
 <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
 [Passing Device Information Settings to Rendering Extensions](report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../../2014/reporting-services/technical-reference-ssrs.md)  
  
  
