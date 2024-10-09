---
title: "Prepare to implement a delivery extension"
description: Discover how to implement a delivery extension in Reporting Services. Learn about available interfaces and classes and required and optional functionality.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "interfaces [Reporting Services]"
  - "delivery extensions [Reporting Services], implementing"
---
# Prepare to implement a delivery extension
  Before you implement your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension, you should define the interfaces to implement. You first need to decide how to use your delivery extension, what settings your delivery extension requires, and the specific functionality you need to implement in order to deliver report notifications.  
  
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
|<xref:Microsoft.ReportingServices.Interfaces.IDeliveryReportServerInformation> Interface|Contains information about the report server that delivery extensions require (for example, a list of the available rendering extensions).|  
|<xref:Microsoft.ReportingServices.Interfaces.Setting> Class|Represents a setting for an extension.|  
|<xref:Microsoft.ReportingServices.Interfaces.Notification> Class|Contains subscription information that delivery extensions use to deliver reports.|  
|<xref:Microsoft.ReportingServices.Interfaces.Report> Class|Represents report-specific information and methods that enable delivery extensions to deliver reports to users.|  
|<xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> Class|Represents the output from a rendering extension. A <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object contains the associated file name and type information the delivery extension requires in order to process the stream returned by the rendering extension.|  
|<xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> Interface|A user control that represents the means to retrieve delivery extension-specific subscription information from the user in Report Manager (for example, an e-mail address or the path to a file share).|  
  
## Related content

- [Reporting Services extensions](../../../reporting-services/extensions/reporting-services-extensions.md)
- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
