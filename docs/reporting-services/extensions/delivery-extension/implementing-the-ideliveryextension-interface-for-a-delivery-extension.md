---
title: "Implement the IDeliveryExtension interface for a delivery extension"
description: Find out how to implement the IDeliveryExtension interface in a delivery extension so that clients can validate user data and retrieve delivery settings.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "delivery extensions [Reporting Services], attributes"
  - "delivery extensions [Reporting Services], class creation"
  - "IDeliveryExtension interface"
---
# Implement the IDeliveryExtension interface for a delivery extension
  Your delivery extension class is used to deliver report notifications to users based on the contents of the notifications. The delivery extension class also provides infrastructure for validating user settings that are passed to the delivery extension. Your delivery extension class should contain specific properties that clients can use to gain information about the name of the extension. It should also have the settings that the extension supports and the rendering formats that are available to the delivery extension.  

:::image type="content" source="../../../reporting-services/extensions/delivery-extension/media/bk-ext-02.gif" alt-text="Screenshot of the IDeliveryExtension interface process.":::

The IDeliveryExtension interface allows validation of user data and for clients to learn about the required delivery settings.
  
 To create a delivery extension class, implement <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> and <xref:Microsoft.ReportingServices.Interfaces.IExtension>. The **IDeliveryExtension** interface enables your delivery extension to deliver report notifications using the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.Deliver%2A> method and to validate incoming extension settings using the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ValidateUserData%2A> method. The **IExtension** interface enables your delivery extension to implement a localized extension name and to process extension-specific configuration information stored in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] configuration file. By implementing **IExtension**, your delivery extension contains the <xref:Microsoft.ReportingServices.Interfaces.Extension.LocalizedName%2A> property. [!INCLUDE[ssRS](../../../includes/ssrs.md)] delivery extensions should support the **LocalizedName** property, so that users encounter a familiar name for the extension in a user interface, such as Report Manager.  
  
 Your delivery extension must also implement the **ExtensionSettings** property of the **IDeliveryExtension** interface. The report server uses the value returned by the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ExtensionSettings%2A> property to evaluate the settings that a delivery extension requires. Clients that interact with delivery extensions use the <xref:ReportService2010.ReportingService2010.GetExtensionSettings%2A> method of the Report Server Web service to return a list of settings for the delivery extension.  
  
 You can also use your delivery extension class to retrieve and process custom configuration data stored in the RSReportServer.config file. For more information about processing custom configuration data, see the <xref:Microsoft.ReportingServices.Interfaces.IExtension.SetConfiguration%2A> method.  
  
 For a sample **IDeliveryExtension** class implementation, see [Reporting Services Samples on CodePlex (SQL Server Reporting Services SSRS)](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
