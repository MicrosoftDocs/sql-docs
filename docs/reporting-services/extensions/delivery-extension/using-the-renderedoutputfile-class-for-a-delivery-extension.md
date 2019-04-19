---
title: "Using the RenderedOutputFile Class for a Delivery Extension | Microsoft Docs"
ms.date: 03/06/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "RenderedOutputFile class"
  - "data streams [Reporting Services]"
  - "delivery extensions [Reporting Services], data streams"
ms.assetid: 8b591801-42d5-49fa-b710-bf7e6917accf
author: maggiesMSFT
ms.author: maggies
---
# Using the RenderedOutputFile Class for a Delivery Extension
  The <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class represents a data stream and information about the data stream's associated properties. The **Data** property of the <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class is used to represent a rendered report or report resource as a **Stream** object.  
  
 The <xref:Microsoft.ReportingServices.Interfaces.Report.Render%2A> method of the **Report** object returns an array of one or more <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects that together constitute a single rendered report. The first <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object is the rendered report. Any other <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects are resources that must be delivered along with the report data (for example, an HTML file and associated images). Rendering extensions that are single-stream rendering extensions (IMAGE, PDF, MHTML, and EXCEL) return only one <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object in the array.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Implementing a Delivery Extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
