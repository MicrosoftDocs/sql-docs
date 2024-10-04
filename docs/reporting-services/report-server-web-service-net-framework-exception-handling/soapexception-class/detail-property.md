---
title: "Detail property"
description: Learn about the Detail property of the Reporting Services SoapException class, specifically about the XML elements that define the property.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Detail property"
  - "SoapException class"
---
# Detail property
  The **Detail** property of the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] **SoapException** class has the following XML structure:  
  
## Elements  
 **Detail**  
 The top-level element that contains all other error detail elements.  
  
 **ErrorCode**  
 The [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]-specific error code.  
  
 **HttpStatus**  
 The HTTP status code.  
  
 **Message**  
 The error message and the error code assigned by the report server.  
  
 **HelpLink**  
 The Help link URL to a Web site at which further information about the error can be found. For more information, see [HelpLink element](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/helplink-element.md).  
  
 **LinkID**  
 The ID assigned to the link.  
  
 **ProductName**  
 The name of the product. The default value is **Microsoft SQL Server Reporting Services**.  
  
 **ProductVersion**  
 The version of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. The maximum length is 15 characters. The format of the version number should be as follows: 8.00.0xxx.00.  
  
 **ProductLocaleId**  
 The locale ID or language ID of the application's INTL DLL (for example, 0x41A).  
  
 **OperatingSystem**  
 The operating system [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] is installed on. Valid values include **0** for operating system independent, **1** for [!INCLUDE[win2000](../../../includes/win2000-md.md)], and **16** for Windows XP.  
  
 **CountryLocaleId**  
 The locale ID or language ID of the operating system. For example, the value for the French version of Windows is 0x040c.  
  
 **MoreInformation**  
 An XML string that contains nested exceptions that occurred while the method ran.  
  
 **Source**  
 A child element of **MoreInformation**. The source of the error.  
  
 **Message**  
 A child element of **MoreInformation**. The error message of a nested exception. This element includes XML attributes for **ErrorCode** and **HelpLink**.  
  
 **Warnings**  
 An XML string that contains the warnings returned from report processing.  
  
## Related content

- [Introduction to exception management in Reporting Services](../../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)
- [Reporting Services SoapException class](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/reporting-services-soapexception-class.md)
- [Use the Detail property to handle specific errors](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/using-the-detail-property-to-handle-specific-errors.md)
