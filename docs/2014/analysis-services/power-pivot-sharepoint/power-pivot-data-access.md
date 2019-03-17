---
title: "PowerPivot Data Access | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 83dc82da-91fb-4e47-91a8-0e0db67339b8
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Data Access
  This topic describes the ways in which data is retrieved from a PowerPivot workbook that is published to a SharePoint library.  
  
 PowerPivot data is stored inside an Excel workbook. The connection string is a URL to a workbook on a SharePoint site.  
  
 PowerPivot data is most often used by the workbook that contains it, as the data behind PivotTables and PivotCharts. Alternatively, PowerPivot data can also be used as an external data source, where a workbook, dashboard, or report connects to a separate Excel (.xlsx) file in SharePoint and retrieves the data for subsequent use. Client tools that typically use PowerPivot data are Excel, [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], other Reporting Services reports, and PerformancePoint.  
  
 On the desktop, the PowerPivot add-in uses AMO and ADOMD.NET to create, process, and query the PowerPivot data in the client workspace.  
  
 On a SharePoint farm, Excel Services uses the local MSOLAP OLE DB provider to connect to PowerPivot data. The provider sends the connection request to a PowerPivot for SharePoint server in the farm. That server loads the data, runs the query, and returns the result set.  
  
##  <a name="queryproc"></a> Querying PowerPivot Data in SharePoint  
 When you view a PowerPivot workbook from a SharePoint library, the PowerPivot data that is inside the workbook is detected, extracted, and processed separately on Analysis Services server instances within the farm, while Excel Services renders the presentation layer. You can view the fully-processed workbook in a browser window or in an Excel 2010 desktop application that has the PowerPivot add-in.  
  
 The following diagram shows how a request for query processing moves through the farm. Because PowerPivot data is part of an Excel 2010 workbook, a request for query processing occurs when a user opens an Excel workbook from a SharePoint library and interacts with a PivotTable or PivotChart that contains PowerPivot data.  
  
 ![GMNI_DataProcReq](../media/gmni-dataprocreq.gif "GMNI_DataProcReq")  
  
 Excel Services and PowerPivot for SharePoint components process different parts of the same workbook (.xlsx) file. Excel Services detects PowerPivot data and requests processing from a PowerPivot server in the farm. The PowerPivot server allocates the request to an [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance, which extracts the data from the workbook in the content library and loads the data. Data that is stored in memory is merged back into the rendered workbook, and passed back to Excel Web Access for presentation in a browser window.  
  
 Not all data in a PowerPivot workbook is handled by PowerPivot for SharePoint. Excel Services processes tables and cell data in a worksheet. Only PivotTables, PivotCharts, and Slicers that go against PowerPivot data are handled by the PowerPivot for SharePoint.  
  
## See Also  
 [Connect to Analysis Services](../instances/connect-to-analysis-services.md)   
 [Tabular Model Data Access](../tabular-models/tabular-model-data-access.md)  
  
  
