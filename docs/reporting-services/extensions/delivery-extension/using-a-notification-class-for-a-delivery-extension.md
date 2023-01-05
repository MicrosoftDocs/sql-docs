---
title: "Using a Notification Class for a Delivery Extension | Microsoft Docs"
description: Find out how delivery extensions can use the Notification class. This class stores subscription information that is used when delivering reports.
ms.date: 03/06/2017
ms.prod: reporting-services
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "delivery extensions [Reporting Services], notifications"
  - "notifications [Reporting Services]"
  - "retry queues"
  - "Nofication class"
ms.assetid: 549c40c4-d33d-46c2-9d6a-7bbb671ac67a
author: maggiesMSFT
ms.author: maggies
---
# Using a Notification Class for a Delivery Extension
  The <xref:Microsoft.ReportingServices.Interfaces.Notification> class is located in the <xref:Microsoft.ReportingServices.Interfaces> namespace and represents subscription information that delivery extensions use for delivering reports. The <xref:Microsoft.ReportingServices.Interfaces.Notification> class provides a number of properties that can be used to render the reports for delivery, determine the status of the notification, and set user data.  
  
 ![Report notification process](../../../reporting-services/extensions/delivery-extension/media/bk-ext-03.gif "Report notification process")  
The notification is the central object of any delivery  
  
 When an event fires that is associated with a subscription that uses your custom delivery extension, a notification is created that contains a <xref:Microsoft.ReportingServices.Interfaces.Report> object. The <xref:Microsoft.ReportingServices.Interfaces.Report> object encapsulates the functionality needed to render a given report to a supported rendering format and contains report-specific properties, such as the URL to the report on the server and the name of the report. For more information about the <xref:Microsoft.ReportingServices.Interfaces.Report> class, see [Using the Report Class for a Delivery Extension](../../../reporting-services/extensions/delivery-extension/using-the-report-class-for-a-delivery-extension.md).  
  
 You pass the <xref:Microsoft.ReportingServices.Interfaces.Notification> object to the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method of your delivery extension. Your <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method should contain specific code to process the notification and to deliver the report.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.Notification> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Retry Functionality  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] allows you to create a retry queue for notifications that cannot immediately be delivered. After the report server invokes the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method of a delivery extension, the delivery extension can request that the report server retry the delivery at a later point in time. If this occurs, the report server places the notification in an internal queue and retries the delivery after a specific period of time has elapsed. Administrators can configure the maximum number of retry attempts that the report server performs and the period between retries in the delivery extension section of the RSReportServer.config file using the **MaxNumberOfRetries** XML element and the **PeriodBetweenRetries** XML element. Notifications are removed from the retry queue if delivery later succeeds or if the maximum number of retry attempts is reached. If delivery fails after the maximum number of retries, the notification is discarded.  
  
## See Also  
 [Implementing a Delivery Extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
