---
title: "Implementing the ISubscriptionBaseUIUserControl Interface for a Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "user controls [Reporting Services]"
  - "ISubscriptionBaseUIUserControl interface"
  - "delivery extensions [Reporting Services], user controls"
ms.assetid: a1e9122c-aa0b-45de-b536-4f1202875ab1
author: markingmyname
ms.author: maghan
manager: craigg
---
# Implementing the ISubscriptionBaseUIUserControl Interface for a Delivery Extension
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extensions can contain an implementation of a subscription user interface (UI) for gathering extension-specific information in Report Manager. The UI is invoked when a user creates a new subscription or modifies an existing one. When a new subscription is being created, the UI displays suitable default values and enables users to interact with the delivery provider. When a subscription is being modified, the UI is pre-populated with the information in the current subscription.  
  
 Delivery extensions provide subscription UI as an ASP.NET user control. The report server incorporates the user control defined by the delivery extension when displaying the subscriptions UI. The base interface that provides abstract methods enabling this functionality is the <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface. This interface ensures that common operations, such as validation of input values, are correctly performed. Additionally, the base user control supplies a set of default properties that are used by the report server for consistency across subscriptions. These properties are required by delivery extensions that are integrated with Report Manager.  
  
 You can implement the <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface in a delivery provider in order to build a subscription UI for Report Manager. The <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface provides infrastructure for enabling users to enter values for subscription settings, for processing the settings needed for the delivery extension, and for validating the settings.  
  
> [!NOTE]  
>  You are not required to implement the <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface as part of your delivery extension. Subscriptions that use your delivery extension can always be created through the SOAP API methods <xref:ReportService2010.ReportingService2010.CreateSubscription%2A> and <xref:ReportService2010.ReportingService2010.CreateDataDrivenSubscription%2A> instead. For more information about the SOAP API features for managing subscription and delivery, see [Subscription and Delivery Methods](../../report-server-web-service/methods/subscription-and-delivery-methods.md).  
  
 The <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface extends <xref:Microsoft.ReportingServices.Interfaces.IExtension>. Your user control that implements <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> must also inherit from **System.Web.UI.WebControls.WebControl**. For more information about the **WebControl** class, see your [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] Developer's Guide.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.ISubscriptionBaseUIUserControl> interface, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Implementing a Delivery Extension](implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
