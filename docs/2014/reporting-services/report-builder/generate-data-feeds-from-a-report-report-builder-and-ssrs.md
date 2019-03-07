---
title: "Generate Data Feeds from a Report (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e68baae2-9f2a-4f13-9179-9ac7f29111c5
author: markingmyname
ms.author: maghan
manager: kfile
---
# Generate Data Feeds from a Report (Report Builder and SSRS)
  You can generate Atom-compliant data feeds from reports, and then use the data feeds in applications, such as the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] client, that can consume data feeds.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Atom rendering extension generates an Atom service document that lists the data feeds available from a report. The document lists at least one data feed for each data region in the report. Depending on the type of data region and the data that the data region displays, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might generate multiple data feeds from a data region.  
  
 Atom service document contains a unique identifier for each the data feed and you use the identifier in a URL to view the content of the data feed.  
  
 For more information, see [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](generating-data-feeds-from-reports-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To generate an Atom service document  
  
1.  From the Report Manager **Home** page, navigate to the report from which you want to generate data feeds.  
  
2.  Click the report.  
  
     The report is run.  
  
3.  On the toolbar, click the Export to Data Feed icon.  
  
     A message appears asking you if you want to save or open the atom document that contains the data feed.  
  
4.  Click **Save** to save the document to the file system, or click **Open** to view the document content before saving. **By default, the document opens in a browser.**  
  
5.  Browse to the location to save the document.  
  
6.  Optionally, change the name of the document.  
  
    > [!NOTE]  
    >  By default, the document name is the report name.  
  
7.  Verify the document type is **ATOMSVC File**, and then click **Save**.  
  
8.  Optionally, open the .atomsvc file in a browser or text or XML editor.  
  
### To view an Atom-compliant data feed  
  
1.  If the Atom service document is not already open, locate it and open it in a browser such as Internet Explorer.  
  
2.  Copy the URL of the data feed that you want to view from the Atom service document to the browser.  
  
     The format of the URL is the following:  
  
 `http://<server name>/ReportServer?%2f<ReportName>rs%3aCommand=Render&rs%3aFormat=ATOM&rc%3aDataFeed=<Identifier>`  
  
1.  Press ENTER.  
  
     A message appears asking you if you want to save or open the atom document that contains the data feed.  
  
2.  Click **Save** to save the document to the file system, or click **Open** to view the data feed before saving.  
  
3.  Browse to the location to save the document.  
  
4.  Optionally, change the name of the document.  
  
    > [!NOTE]  
    >  By default the document name is the report name. If the Atom service document has multiple feeds, by default all use the same name, the report name. To differentiate them, rename them to use meaningful names.  
  
5.  Verify the document type is **ATOM File**, and then click **Save**.  
  
6.  Optionally, open the .atom file in a browser or text editor or XML editor.  
  
## See Also  
 [Exporting Reports &#40;Report Builder and SSRS&#41;](export-reports-report-builder-and-ssrs.md)  
  
  
