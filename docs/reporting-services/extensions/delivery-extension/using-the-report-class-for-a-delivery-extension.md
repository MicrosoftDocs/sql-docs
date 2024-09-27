---
title: "Use the Report class for a delivery extension"
description: Find out how delivery extensions can use the Report class, which stores the report URL on the report server, the report name, and other properties.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "delivery extensions [Reporting Services], report information"
  - "Report class"
---
# Use the Report class for a delivery extension
  The <xref:Microsoft.ReportingServices.Interfaces.Report> class represents a report in the report server database. Any subscription is associated with a specific report. The report is contained in the notification. Your delivery extension can use the <xref:Microsoft.ReportingServices.Interfaces.Report> object that is part of the notification to render the report. The <xref:Microsoft.ReportingServices.Interfaces.Report> object also contains report-specific properties, such as the URL to the report on the report server and the name of the report. These properties can all be used as part of your delivery provider.  
  
 The <xref:Microsoft.ReportingServices.Interfaces.Report.Render%2A> method of the <xref:Microsoft.ReportingServices.Interfaces.Report> class can be used to render a report. The <xref:Microsoft.ReportingServices.Interfaces.Report.Render%2A> method returns an array of one or more <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects that together comprise a single rendered report. The first <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object is the rendered report. Any other <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> objects are resources that must be delivered along with the report data (for example, an HTML file and associated images). Rendering extensions that are single-stream rendering extensions (IMAGE, PDF, MHTML, and Excel) return only one <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object in the array.  
  
 The <xref:Microsoft.ReportingServices.Interfaces.RenderedOutputFile> object, which contains the report stream, can be included as part of a delivery.  
  
 For an example of how to use the <xref:Microsoft.ReportingServices.Interfaces.Report> class, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
- [Use the RenderedOutputFile class for a delivery extension](../../../reporting-services/extensions/delivery-extension/using-the-renderedoutputfile-class-for-a-delivery-extension.md)
