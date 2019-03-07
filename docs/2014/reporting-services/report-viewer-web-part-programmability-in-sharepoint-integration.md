---
title: "Report Viewer Web Part Programmability in SharePoint Integration | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
ms.assetid: 714017b7-1bd6-4950-a3c6-d0df8450a877
author: markingmyname
ms.author: maghan
manager: kfile
---
# Report Viewer Web Part Programmability in SharePoint Integration
  The Report Viewer Web Part is a `T:Microsoft.ReportingServices.SharePoint.UI.WebParts.ReportViewerWebPart` server control, which contains a set of public application programming interfaces (API) that enables developers to create custom SharePoint applications. You can create custom Web Parts that supply report path and parameters to Report Viewer Web Part using Web Part connections. You can also embed the Web Part in a custom SharePoint Web Part page and customize it using the public API.  
  
## Connecting to Report Viewer Web Part with Custom Web Parts  
 The Report Viewer Web Part is a connection consumer to SharePoint Web Parts that implement <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> or `T:Microsoft.SharePoint.WebPartPages.IFilterValues`. An <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> Web Part, such as the **Documents** Web Part can supply a report path to a Report Viewer Web Part when placed on the same Web Part page as the Report Viewer Web Part. Likewise, an `T:Microsoft.SharePoint.WebPartPages.IFilterValues` Web Part, such as the **Text Filter** or the **Choice Filter**, can supply a report parameter to a Report Viewer Web Part when placed on the same Web Part page as the Report Viewer Web Part.  
  
### Implementing a Report Path Provider with IWebPartRow  
 To supply a report path to the Report Viewer Web Part through Web Part connections, do the following:  
  
1.  Create a Web Part that implements the <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> interface.  
  
2.  Add the Web Part to the same Web Part page as the Report Viewer Web Part.  
  
3.  Connect your Web Part to the Report Viewer Web Part in the Web-based Web Part design user interface.  
  
    > [!NOTE]  
    >  You can only connect one <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> Web Part to the Report Viewer Web Part at a time, and you cannot connect both an <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> Web Part and an `T:Microsoft.SharePoint.WebPartPages.IFilterValues` Web Part to the Report Viewer Web Part at the same time.  
  
 For your <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> Web Part to work properly with the `T:Microsoft.ReportingServices.SharePoint.UI.WebParts.ReportViewerWebPart`, you must do the following in the <xref:System.Web.UI.WebControls.WebParts.IWebPartRow.GetRowData%2A> method:  
  
-   Invoke the callback method with a <xref:System.Data.DataRowView> object as the input parameter.  
  
-   Make sure that the <xref:System.Data.DataRowView> object contains a column called "DocUrl" that contains the report path.  
  
    > [!NOTE]  
    >  The Report Viewer Web Part in the add-in for [!INCLUDE[offSPServ](../includes/offspserv-md.md)] 2010 also supports receiving the report path using the "FileRef" column.  
  
### Implementing a Report Parameter Provider with IFilterValues  
 A Web Part that implements `T:Microsoft.SharePoint.WebPartPages.IFilterValues` can provide one parameter value to the Report Viewer Web Part. The parameter value sent to the Report Viewer Web Part is subject to the same restrictions placed on the report parameter as specified in the report definition, such as data type, valid values, and so on  
  
 To supply a report parameter to the Report Viewer Web Part, do the following:  
  
1.  Create a Web Part that implements the `T:Microsoft.SharePoint.WebPartPages.IFilterValues` interface.  
  
2.  Add the Web Part to the same page as the `T:Microsoft.ReportingServices.SharePoint.UI.WebParts.ReportViewerWebPart`.  
  
3.  Connect your `T:Microsoft.SharePoint.WebPartPages.IFilterValues` Web Part to the Report Viewer Web Part in the Web-based Web Part design user interface.  
  
    > [!NOTE]  
    >  You can connect multiple `T:Microsoft.SharePoint.WebPartPages.IFilterValues` Web Parts to the Report Viewer Web Part at a time. However, you cannot connect both an <xref:System.Web.UI.WebControls.WebParts.IWebPartRow> Web Part and an `T:Microsoft.SharePoint.WebPartPages.IFilterValues` Web Part to the Report Viewer Web Part at the same time.  
  
## See Also  
 [IFilterValues interface](https://msdn.microsoft.com/library/office/microsoft.sharepoint.webpartpages.ifiltervalues\(v=office.15\).aspx)  
  
  
