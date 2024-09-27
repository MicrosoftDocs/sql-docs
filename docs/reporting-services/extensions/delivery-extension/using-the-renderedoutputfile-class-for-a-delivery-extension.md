---
title: "Use the RenderedOutputFile class for a delivery extension"
description: Learn how delivery extensions can use the RenderedOutputFile class, which stores a rendered report or report resources.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "RenderedOutputFile class"
  - "data streams [Reporting Services]"
  - "delivery extensions [Reporting Services], data streams"
---
# Use the RenderedOutputFile class for a delivery extension
  The <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class represents a data stream and information about the data stream's associated properties. The **Data** property of the <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class is used to represent a rendered report or report resource as a **Stream** object.  
  
 The <xref:Microsoft.ReportingServices.Interfaces.Report.Render%2A> method of the **Report** object returns an array of one or more <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects that together constitute a single rendered report. The first <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object is the rendered report. Any other <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects are resources that must be delivered along with the report data (for example, an HTML file and associated images). Rendering extensions that are single-stream rendering extensions (IMAGE, PDF, MHTML, and EXCEL) return only one <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object in the array.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
