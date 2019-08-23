---
title: "Using the Setting Class for a Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "delivery extensions [Reporting Services], settings"
  - "Setting class"
ms.assetid: 50746639-da7c-46a5-ac7b-e87dd6b91880
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Using the Setting Class for a Delivery Extension
  The <xref:Microsoft.ReportingServices.Interfaces.Setting> class is located in the <xref:Microsoft.ReportingServices.Interfaces> namespace and represents information about extension settings for a delivery extension. The <xref:Microsoft.ReportingServices.Interfaces.Setting> class provides infrastructure for storing information about the settings that are required in order for a delivery extension to function properly. For example, in Report Server E-Mail delivery, a user is required to supply settings specific to e-mail delivery, such as the recipient's address, the sender's address, the subject line of the e-mail, and more. Undoubtedly, your custom delivery providers will require the user to supply specific settings in order for the delivery extension to deliver notifications and reports.  
  
 The <xref:Microsoft.ReportingServices.Interfaces.Setting> class is used when implementing the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ExtensionSettings%2A> property of the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> interface. The <xref:Microsoft.ReportingServices.Interfaces.Setting> class is also used for processing the extension setting data that is supplied by a user when a subscription or notification is created.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.Setting> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Implementing a Delivery Extension](implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
