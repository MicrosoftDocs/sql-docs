---
title: "Reporting Services SoapException class"
description: Learn how to address specific Reporting Services SoapException class errors that you know might happen.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "exceptions [Reporting Services], SoapException class"
  - "SoapException class"
---
# Reporting Services SoapException class
  You should address specific [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] errors that you know might happen. For example, in an application where you ask the user to create a folder, it might be possible for the user to try to create a folder that already exists. As the developer, you don't have control over what the user enters in the folder name and path fields of your application. But, you do have control over what the user experience is when someone incidentally tries to create an item that already exists.  
  
 To make it easier for you to catch specific error conditions, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] classifies an error code for the exception and returns the classification of the error using properties from the **SoapException** class. For more information, see "SoapException Class" in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK documentation.  
  
 The following table lists the public properties of the **SoapException** class.  
  
|Public property|Description|  
|---------------------|-----------------|  
|**Actor**|The code that caused the exception. The value is the URL to the Web service method.|  
|**Detail**|Application-specific error information. The report server sets the value and is in XML format. For more information, see [Detail property](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/detail-property.md) and [Use the Detail property to handle specific errors](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/using-the-detail-property-to-handle-specific-errors.md).|  
|**HelpLink**|A URL or URN to a Help file associated with the error. The Web Service usually sets the value and it sets a URL to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Help and Support. Because [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] supports multiple help links for errors that occur, the report server sets help link information as part of the **Detail** property. For more information, see [HelpLink element](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/helplink-element.md).|  
|**Message**|A descriptive, localized message that describes the error. This text might appear in the application UI.|  
  
## Related content

- [Introduction to exception management in Reporting Services](../../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)
- [SoapException Errors table](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/soapexception-errors-table.md)
