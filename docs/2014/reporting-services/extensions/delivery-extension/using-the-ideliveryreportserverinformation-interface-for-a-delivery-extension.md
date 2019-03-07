---
title: "Using the IDeliveryReportServerInformation Interface for a Delivery Extension | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "IDeliveryReportServerInformation interface"
  - "delivery extensions [Reporting Services], retrieving report server information"
ms.assetid: adbce647-18f3-470c-8114-42f8bcc95dc2
author: markingmyname
ms.author: maghan
manager: kfile
---
# Using the IDeliveryReportServerInformation Interface for a Delivery Extension
  The <xref:Microsoft.ReportingServices.Interfaces.IDeliveryReportServerInformation> interface exposes several properties that you can use to retrieve information about a report server. You can use this information to deliver notifications and reports. When implementing your delivery extension class, you implement the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ReportServerInformation%2A> property as required by the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> interface. The <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension.ReportServerInformation%2A> property returns an object that implements the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryReportServerInformation> interface. From this object you can get a list of rendering extensions currently supported by the report server.  
  
 The following `for` loop could be used to store a list of rendering extensions currently available on the report server in an **ArrayList** object.  
  
```vb  
Dim renderFormats As New ArrayList()  
Dim e As Microsoft.ReportingServices.Interfaces.Extension  
For Each e In  ReportServerInformation.RenderingExtension  
   If e.Visible Then  
      renderFormats.Add(e.Name)  
   End If  
Next e  
```  
  
```csharp  
ArrayList renderFormats = new ArrayList();  
foreach (Microsoft.ReportingServices.Interfaces.Extension e in ReportServerInformation.RenderingExtension)  
{   
   if (e.Visible)  
   {  
      renderFormats.Add(e.Name);  
   }  
}  
```  
  
 For more information about the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryReportServerInformation> interface, see [Using the IDeliveryReportServerInformation Interface for a Delivery Extension](using-the-ideliveryreportserverinformation-interface-for-a-delivery-extension.md).  
  
## See Also  
 <xref:Microsoft.ReportingServices.Interfaces>   
 [Implementing a Delivery Extension](implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
