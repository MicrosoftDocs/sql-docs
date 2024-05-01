---
title: "Use a Notification class for a delivery extension"
description: Find out how delivery extensions can use the Notification class. This class stores subscription information that is used when delivering reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/06/2017
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "delivery extensions [Reporting Services], notifications"
  - "notifications [Reporting Services]"
  - "retry queues"
  - "Nofication class"
---
# Use a Notification class for a delivery extension
  The <xref:Microsoft.ReportingServices.Interfaces.Notification> class is located in the <xref:Microsoft.ReportingServices.Interfaces> namespace and represents subscription information that delivery extensions use for delivering reports. The <xref:Microsoft.ReportingServices.Interfaces.Notification> class provides many properties that can be used to render the reports for delivery, determine the status of the notification, and set user data.  

:::image type="content" source="../../../reporting-services/extensions/delivery-extension/media/bk-ext-03.gif" alt-text="Screenshot of the Report notification process.":::

The notification is the central object of any delivery. 
  
 When an event fires that is associated with a subscription that uses your custom delivery extension, a notification is created that contains a <xref:Microsoft.ReportingServices.Interfaces.Report> object. The <xref:Microsoft.ReportingServices.Interfaces.Report> object encapsulates the functionality needed to render a given report to a supported rendering format and contains report-specific properties, such as the URL to the report on the server and the name of the report. For more information about the <xref:Microsoft.ReportingServices.Interfaces.Report> class, see [Use the Report class for a delivery extension](../../../reporting-services/extensions/delivery-extension/using-the-report-class-for-a-delivery-extension.md).  
  
 You pass the <xref:Microsoft.ReportingServices.Interfaces.Notification> object to the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method of your delivery extension. Your <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method should contain specific code to process the notification and to deliver the report.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.Notification> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Retry functionality  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] allows you to create a retry queue for notifications that can't immediately be delivered. After the report server invokes the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method of a delivery extension, the delivery extension can request that the report server retry the delivery at a later point in time. If this event occurs, the report server places the notification in an internal queue and retries the delivery after a specific period of time elapses. Administrators can configure the maximum number of retry attempts that the report server performs and the period between retries in the delivery extension section of the RSReportServer.config file using the **MaxNumberOfRetries** XML element and the **PeriodBetweenRetries** XML element. Notifications are removed from the retry queue if delivery later succeeds or if the maximum number of retry attempts is reached. If delivery fails after the maximum number of retries, the notification is discarded.  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
