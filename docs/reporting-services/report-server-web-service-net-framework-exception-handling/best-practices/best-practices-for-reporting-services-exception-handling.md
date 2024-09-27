---
title: "Best practices for Reporting Services exception management"
description: Learn about best practices for Reporting Services exception handling, such as how to deal with error cases that don't throw exceptions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "exceptions [Reporting Services], best practices"
---
# Best practices for Reporting Services exception management
  When you develop [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] applications, there are several methodologies you can use to eliminate or reduce the occurrence of exceptions. When exceptions do occur, provide clear and concise error messages to the user, and add adequate exception handling to prevent your applications from ending unexpectedly.  
  
 An application that sends requests to the Report Server Web service should:  
  
-   Avoid causing exceptions by preventing as many invalid requests as possible.  
  
-   Catch exceptions and provide specific error-handling code whenever possible.  
  
-   Deal with error cases that don't throw exceptions.  
  
## In this section  
  
|Article|Description|  
|-----------|-----------------|  
|[Prevent invalid requests](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/preventing-invalid-requests.md)|Describes techniques for preventing requests that aren't valid from being sent to the report server.|  
|[Use try and catch blocks](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/using-try-and-catch-blocks.md)|Describes how to further enhance the reliability of your application with try/catch blocks.|  
|[Handle warnings and cases that do not cause exceptions](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/handling-warnings-and-cases-that-do-not-cause-exceptions.md)|Explains how to handle errors that don't result in an exception occurring [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
|[Use the Detail property to handle specific errors](../../../reporting-services/report-server-web-service-net-framework-exception-handling/best-practices/using-the-detail-property-to-handle-specific-errors.md)|Explains how to programmatically handle specific errors by using the **Detail** property of the **SoapException** object.|  
  
## Related content

- [Detail property](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/detail-property.md)
- [Introduction to exception management in Reporting Services](../../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)
- [Reporting Services SoapException class](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/reporting-services-soapexception-class.md)
