---
title: "Specify device information settings in a URL"
description: Learn how to specify device information settings in a URL, specifically with the DeviceInfo XML element.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/16/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "device information settings [Reporting Services], URLs"
  - "URL access [Reporting Services], device information settings"
---
# Specify device information settings in a URL
  Device information settings are parameters that are passed to a rendering extension. If you use the methods of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Report Server Web service to render a report, a **DeviceInfo** XML element is passed as an input parameter. Child elements of the **DeviceInfo** element are specific to the device information settings of different rendering extensions. You can include device information settings in a URL by using the *rc:tag=value* parameter string, where *tag* is the name of the device information settings element being accessed. For more information about device information settings in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], see [Pass device information settings to rendering extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md).  
  
## Example  
 The following example sets the format of the specified report to JPEG by using the *OutputFormat* device information setting of the image rendering extension (the line breaks in this example are for legibility):  
  
```  
https://servername/reportserver?/SampleReports  
/Employee Sales Summary&EmployeeID=38&rs:  
Command=Render&rs:Format=IMAGE&rc:OutputFormat=JPEG  
```  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)  
  
  
