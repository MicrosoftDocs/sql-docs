---
title: "Preparing to Implement a Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "interfaces [Reporting Services]"
  - "delivery extensions [Reporting Services], implementing"
ms.assetid: aee1608d-374f-4ad3-bc23-fe07fdaa52b7
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Preparing to Implement a Delivery Extension
  Before you implement your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension, you should define the interfaces to implement. You first need to decide how your delivery extension will be used, what settings your delivery extension will require, and the specific functionality you will need to implement in order to deliver report notifications.  
  
 Each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension must provide the following functionality:  
  
-   An <xref:Microsoft.ReportingServices.Interfaces.IExtension> interface implementation that represents the extension and a localized extension name.  
  
-   An <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> implementation that creates a delivery extension that can be used to deliver report notifications to end users.  
  
-   The ability to process specific user data for a subscription.  
  
 Each delivery extension can be enhanced to include the following functionality:  
  
-   An [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] user control implementation that enables end users to use Report Manager to create report subscriptions that use the delivery extension.  
  
 The following table describes the available interfaces and classes for delivery extensions.  
  
|Interface or class|Description|  
|------------------------|-----------------|  
|<xref:Microsoft.ReportingServices.Interfaces.IExtension> Interface|Represents an extension in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
|<xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> Interface|Represents a delivery extension in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
|<xref:Microsoft.ReportingServices.Interfaces.IDeliveryReportServerInformation> Interface|Contains information about the report server that is required by delivery extensions (for example, a list of the available rendering extensions).|  
|<xref:Microsoft.ReportingServices.Interfaces.Setting> Class|Represents a setting for an extension.|  
|<xref:Microsoft.ReportingServices.Interfaces.Notification> Class|Contains subscription information that delivery extensions use to deliver reports.|  
|<xref:Microsoft.ReportingServices.Interfaces.Report> Class|Represents report-specific information and methods that enable delivery extensions to deliver reports to users.|  
|<xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> Class|Represents the output from a rendering extension. A <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object contains the associated file name and type information that is required by the delivery extension in order to process the stream returned by the rendering extension.|  
|<xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> Interface|A user control that represents the means to retrieve delivery extension-specific subscription information from the user in Report Manager (for example, an e-mail address or the path to a file share).|  
  
## See Also  
 [Reporting Services Extensions](../reporting-services-extensions.md)   
 [Implementing a Delivery Extension](implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
