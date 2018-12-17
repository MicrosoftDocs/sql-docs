---
title: "Implementing the IDeliveryExtension Interface for a Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "delivery extensions [Reporting Services], attributes"
  - "delivery extensions [Reporting Services], class creation"
  - "IDeliveryExtension interface"
ms.assetid: ab0344db-510b-403f-8dbf-b9831553765d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Implementing the IDeliveryExtension Interface for a Delivery Extension
  Your delivery extension class is used to deliver report notifications to users based on the contents of the notifications. The delivery extension class also provides infrastructure for validating user settings that are passed to the delivery extension. In addition, your delivery extension class should contain specific properties that clients can use to gain information about the name of the extension, the settings that the extension supports, and the rendering formats that are available to the delivery extension.  
  
 ![IDeliveryExtension interface process](../../media/bk-ext-02.gif "IDeliveryExtension interface process")  
The IDeliveryExtension interface allows validation of user data as well as for clients to learn about the required delivery settings  
  
 To create a delivery extension class, implement <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> and <xref:Microsoft.ReportingServices.Interfaces.IExtension>. The **IDeliveryExtension** interface enables your delivery extension to deliver report notifications using the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method and to validate incoming extension settings using the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ValidateUserData%2A> method. The **IExtension** interface enables your delivery extension to implement a localized extension name and to process extension-specific configuration information stored in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] configuration file. By implementing **IExtension**, your delivery extension contains the <xref:Microsoft.ReportingServices.Interfaces.Extension.LocalizedName%2A> property. It is strongly recommended that [!INCLUDE[ssRS](../../../includes/ssrs.md)] delivery extensions support the **LocalizedName** property, so that users encounter a familiar name for the extension in a user interface, such as Report Manager.  
  
 Your delivery extension must also implement the **ExtensionSettings** property of the **IDeliveryExtension** interface. The report server uses the value returned by the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ExtensionSettings%2A> property to evaluate the settings that a delivery extension requires. Clients that interact with delivery extensions use the <xref:ReportService2010.ReportingService2010.GetExtensionSettings%2A> method of the Report Server Web service to return a list of settings for the delivery extension.  
  
 You can also use your delivery extension class to retrieve and process custom configuration data stored in the RSReportServer.config file. For more information about processing custom configuration data, see the <xref:Microsoft.ReportingServices.Interfaces.IExtension.SetConfiguration%2A> method.  
  
 For a sample **IDeliveryExtension** class implementation, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Implementing a Delivery Extension](../delivery-extension/implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
