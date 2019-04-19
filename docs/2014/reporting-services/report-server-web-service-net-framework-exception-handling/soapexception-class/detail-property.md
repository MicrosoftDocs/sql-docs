---
title: "Detail Property | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "Detail property"
  - "SoapException class"
ms.assetid: c1ddaeb6-c540-49fa-b06e-b6359d377ee8
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Detail Property
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
 The Help link URL to a Web site at which further information about the error can be found. For more information, see [HelpLink Element](helplink-element.md).  
  
 **LinkID**  
 The ID assigned to the link.  
  
 **ProductName**  
 The name of the product. The default value is **Microsoft SQL Server Reporting Services**.  
  
 **ProductVersion**  
 The version of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. The maximum length is 15 characters. The format of the version number should be as follows: 8.00.0xxx.00.  
  
 **ProductLocaleId**  
 The locale ID or language ID of the application's INTL DLL (for example, 0x41A).  
  
 **OperatingSystem**  
 The operating system [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] is installed on. Valid values include **0** for operating system independent, **1** for [!INCLUDE[win2kfamily](../../../includes/win2kfamily-md.md)], and **16** for Windows XP.  
  
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
  
## See Also  
 [Introducing Exception Handling in Reporting Services](../introducing-exception-handling-in-reporting-services.md)   
 [Reporting Services SoapException Class](reporting-services-soapexception-class.md)   
 [Using the Detail Property to Handle Specific Errors](../best-practices/using-the-detail-property-to-handle-specific-errors.md)  
  
  
