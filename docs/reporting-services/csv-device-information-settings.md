---
title: "CSV Device Information Settings | Microsoft Docs"
ms.date: 03/16/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "CSV [Reporting Services]"
  - "device information settings [Reporting Services], CSV rendering"
ms.assetid: f96f83a6-50bc-48ce-9fcd-fd9e1952d40a
author: markingmyname
ms.author: maghan
---
# CSV Device Information Settings
  The device information settings for the CSV rendering extension allow delimiters and qualifiers to be changed and line break handling to be specified. The extension of the file can also be submitted, as well as the encoding and inclusion of header rows in the output. Because delimiters are likely to be special characters, you should encode them in a CDATA section, if the settings are written as XML.  
  
 The following table lists the device information settings for rendering in Text format.  
  
|Setting|Value|  
|-------------|-----------|  
|**Encoding**|The Internet Assigned Numbers Authority (IANA) name of a character encoding that is supported by the .NET Framework. The default value is **UTF-8**. Examples of other values include ASCII, UTF-7, and UTF-16.|  
|**ExcelMode**|Specifies that the target output is for Excel. The default value is **true**.|  
|**FieldDelimiter**|The delimiter string to put in the result. The default value is a comma (,). You should URL encode the value of this device information when passing it on a URL. For example, a tab character as a delimiter should be "%09".<br /><br /> You can change the default field delimiter to any character that you want, including TAB, by changing the device information settings in the configuration file. For example, to use TAB, update the FieldDelimiter setting to \<FieldDelimiter xml:space="preserve">[TAB]\</FieldDelimiter><br /><br /> In the example [TAB] is an actual tab character, which means that whitespace appears in the configuration file. The "xml:space" attribute tells parsers to preserve whitespace.|  
|**FileExtension**|The file extension to put on the result. The default value is **.CSV**. If both **FileExtension** and **Extension** are specified then **FileExtension** will take precedence.|  
|**NoHeader**|Indicates whether the header row is excluded from the output. The default value is **false**.|  
|**Qualifier**|The qualifier string to put around results that contain the field delimiter or record delimiter. If the results contain the qualifier, the qualifier is repeated. The **Qualifier** setting must be different from the **FieldDelimiter** and **RecordDelimiter** settings. The default value is a quotation mark (").|  
|**RecordDelimiter**|The record delimiter to put at the end of each record. The default value is \<cr>\<lf>.|  
|**SuppressLineBreaks**|Indicates whether line breaks are removed from the data included in the output. The default value is **false**. If the value is **true**, the **FieldDelimiter**, **RecordDelimiter**, and **Qualifier** settings cannot be a space character.|  
|**UseFormattedValues**|Indicates whether formatted strings are put into the CSV output. The default value is **true** when **ExcelMode** is **true**; otherwise it is **false**.|  
  
## See Also  
 <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
 [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
