---
title: "Generate data feeds from a report (Report Builder)"
description: You can generate Atom-compliant data feeds from paginated reports. Use the feeds in applications such as Power Pivot or Power BI that can consume data feeds.
author: maggiesMSFT
ms.author: maggies
ms.date: 11/21/2022
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Generate data feeds from a report (Report Builder and SSRS)

You can generate Atom-compliant data feeds from paginated reports, and then use the data feeds in applications such as Power Pivot or Power BI that can consume data feeds.

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Atom rendering extension generates an Atom service document that lists the data feeds available from a report. The document lists at least one data feed for each data region in the report. Depending on the type of data region and the data that the data region displays, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might generate multiple data feeds from a data region.

Atom service document contains a unique identifier for each the data feed and you use the identifier in a URL to view the content of the data feed.

For more information, see [Generate data feeds from reports (Report Builder and SSRS)](../../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

### Generate an Atom service document

1. On the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, navigate to the report for which you want to generate data feeds.

1. Select the report so the report is run.

1. On the toolbar, select the **Export** button and choose the **Data Feed** option.

     A message appears asking you if you want to save or open the `atomsvc` document that contains the data feed.

1. Select **Save** to save the document to the file system, or select **Open** to view the document content before saving. **By default, the document opens in a browser.**

1. Browse to the location to save the document.

1. Optionally, change the name of the document.

    > [!NOTE]  
    >  By default, the document name is the report name.

1. Verify the document type is **ATOMSVC File**, and then select **Save**.

1. Optionally, open the `.atomsvc` file in a browser or text or XML editor.

### View an Atom-compliant data feed

1. If the Atom service document isn't already open, locate it and open it in a browser such as Microsoft Edge.

1. Copy the URL of the data feed that you want to view from the Atom service document to the browser.

     The format of the URL is the following example:

     `https://<server name>/ReportServer?/<ReportName>&rs:Command=Render&rs:Format=ATOM&rc:ItemPath=Tablix1`

1. Press ENTER.

     A message appears asking you if you want to save or open the atom document that contains the data feed.

1. Select **Save** to save the document to the file system, or select **Open** to view the data feed before saving.

1. Browse to the location to save the document.

1. Optionally, change the name of the document.

    > [!NOTE]  
    >  By default the document name is the report name. If the Atom service document has multiple feeds, by default all use the same name, the report name. To differentiate them, rename them to use meaningful names.

1. Verify the document type is **ATOM File**, and then select **Save**.

1. Optionally, open the `.atom` file in a browser or text editor or XML editor.

## Related content

[Export reports](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
