---
title: "Reporting Services Extensions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Reporting Services, extending"
  - "extensions [Reporting Services], about extensions"
  - "SSIS, extending"
  - "Reporting Services, extending"
  - "extensions [Reporting Services]"
ms.assetid: 2bf17ae4-2292-4a58-a1f0-56e99abd9b69
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Reporting Services Extensions
  The modular architecture of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is designed for extensibility. A managed code API is available so that you can easily develop, install, and manage extensions consumed by many [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components. You can create private or shared assemblies using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] and add new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] functionality to meet your evolving business needs.  
  
 The unique extensibility architecture of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] enables developers to extend specific features of the product and its components. Currently, broad support exists for extending the data processing capabilities of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The data processing API includes familiar, [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider constructs and conventions that enable developers to build additional data processing into [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. These data processing extensions add functionality to both the Report Server and Report Designer, enabling seamless integration of custom data into reports.  
  
 Another supported extension is the delivery extension. The delivery API is fully integrated with the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] architecture, enabling a wide variety of delivery mechanisms to be used when sending report notifications to users. You can extend the Report Server to provide custom delivery to users and you can extend the subscription management pages of Report Manager to enable subscriptions that use custom delivery extensions.  
  
 Another report server extension, Report Definition Customization Extension (RDCE), can dynamically customize a report definition before it is passed to the processing engine. You might customize reports based on factors such as users or languages. For example, you might want to implement different views for various users such as managers or members of a department, or you might want to customize a report to have a different layout when it is rendered in French or Arabic.  
  
## In This Section  
 [Security Considerations for Extensions](security-considerations-for-extensions.md)  
 Describes security issues related to developing and deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extensions.  
  
 [Implementing a Data Processing Extension](data-processing/implementing-a-data-processing-extension.md)  
 Describes the requirements and steps for implementing a data processing extension for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 [Implementing a Delivery Extension](delivery-extension/implementing-a-delivery-extension.md)  
 Describes the requirements and steps for implementing a delivery extension for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 [Implementing a Rendering Extension](rendering-extension/implementing-a-rendering-extension.md)  
 Contains an introduction to developing rendering extensions.  
  
 [Implementing a Security Extension](security-extension/implementing-a-security-extension.md)  
 Describes the requirements and steps for implementing a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] security extension.  
  
 [Reporting Services Extension Library](reporting-services-extension-library.md)  
 Contains the programming reference for the extension API library for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extensibility features.  
  
  
