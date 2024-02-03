---
title: "Delivery extensions overview"
description: Read an overview of delivery extensions, which you can use to deliver Reporting Services reports in various ways, such as through email or file sharing.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], delivery extensions"
  - "delivery extensions [Reporting Services], about extensions"
---
# Delivery extensions overview
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] enables users to create and publish reports that, once created and published, can be delivered to various locations. In addition, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes several delivery extensions and a delivery API that enable developers to create more delivery extensions to further extend the functionality of delivery in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
 The following table lists the delivery extensions included with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
|Delivery extension|Description|  
|------------------------|-----------------|  
|Report Server E-Mail|Uses an SMTP server to e-mail reports to individual users or groups.|  
|Report Server File Share|Used to distribute reports within your organization to network file shares. Lets you automatically copy a report to a file share on a designated schedule.|  

:::image type="content" source="../../../reporting-services/extensions/delivery-extension/media/bk-reportservicedelivery.gif" alt-text="Screenshot of the Reporting Services delivery extension architecture.":::

Reporting Services delivery extension architecture.
  
 Delivery extensions are paired with subscriptions. When a user creates a subscription, they can choose one of the available delivery extensions to determine how the report is delivered. In [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], subscriptions are located in the report server database. When an event occurs, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] matches the event against subscriptions contained in the report server database. For each subscription tied to the event, the report server creates a notification. For data-driven subscriptions, a notification is created for each recipient. Once a notification is created, the report server invokes a particular delivery extension and passes in values for the extensions settings specified in the notification. The delivery extension sends the notification to the user as specified by the selected delivery extension.  
  
 Delivery extensions implement the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension API. Supporting the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension API enables delivery extensions to receive notifications from the report server and provide status of the notification.  
  
 The report server doesn't manage delivery destinations for notifications and reports. Gathering destination information is accomplished through the code you write in your delivery extension.  
  
## Subscriptions and delivery extensions  
 Client applications create subscriptions that use delivery extensions using two methods of the Report Server Web service: <xref:ReportService2010.ReportingService2010.CreateSubscription%2A> and <xref:ReportService2010.ReportingService2010.CreateDataDrivenSubscription%2A>. For modifying subscriptions that already exist, the <xref:ReportService2010.ReportingService2010.SetSubscriptionProperties%2A> and <xref:ReportService2010.ReportingService2010.SetDataDrivenSubscriptionProperties%2A> methods are used. When a user creates a subscription, they also select a delivery extension for the subscription and enters values for the required extension settings. When a user saves a subscription, it's stored in the report server database. Subscriptions create notifications based on a schedule or an event. When a delivery begins, the selected delivery extension first loads any configuration data from the configuration file. Next, the extension settings for the subscription are retrieved, and values are set. Finally, the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method is called, and the notification is sent.  
  
## Developer requirements  
 Developing a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension requires you to have:  
  
-   A deployment computer with a report server installed.  
  
-   A development computer with [!INCLUDE[vsprvs2008](../../../includes/vsprvs2008-md.md)] or the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] Software Development Kit (SDK) installed.  
  
-   An in-depth understanding of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] features and capabilities, specifically subscription and delivery.  
  
-   An in-depth understanding of [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] and Web controls if you're planning to implement your own subscription user interface for Report Manager.  
  
-   Development experience in a [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] language such as [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual C# or [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../../includes/visual-basic-md.md)] .NET.  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
