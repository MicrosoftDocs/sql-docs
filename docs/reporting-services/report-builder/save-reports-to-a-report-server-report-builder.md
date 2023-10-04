---
title: "Save Reports to a Report Server (Report Builder)"
description: In Report Builder, you can publish a report to a report server. Others can view it. Each time you run the published report, you see the most current data.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Save Reports to a Report Server (Report Builder)

  In Report Builder, you can save a report definition to a report server (also known as publishing a report). When the report is saved to a report server, other users can view the report. Each time you run the published report, you will retrieve the most current data. To save a static copy of a rendered report, export the report to a different file format and save it or use the report history feature to save versions of rendered reports.

> [!NOTE]  
> The location of the saved report definition does not affect whether the report is processed on the server or processed locally when you preview the report.

### Save a report to a report server

1. From the Report Builder button, select **Save**. The **Save As**_\<Report Item>_ dialog box opens.

    > [!NOTE]  
    >  If you are resaving a report, it is automatically resaved to its previous location. Use the Save As option to change location.

1. Optionally, select **Recent Sites and Servers** to show a list of recently used report servers and SharePoint sites.

1. Browse to the report server location where you want to save the report.

1. In **Name**, type the name of the report.

1. In **Items of type**, select the type of report item you are saving. The type for reports is Reports(*.rdl).

### Save a report as a different name

1. From the Report Builder button, select **Save As**. The **Save As**_\<Report Item>_ dialog box opens.

1. Browse to the report server location or to the file share where you want to save the report.

1. In **Name**, type the name of the report.

1. In **Items of type**, select the type of report item you are saving. The type for reports is Reports(*.rdl).

## See also

- [Finding, Viewing, and Managing Reports (Report Builder and SSRS )](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
- [Export Reports (Report Builder and SSRS)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Saving Reports (Report Builder)](../../reporting-services/report-builder/saving-reports-report-builder.md)
- [Export a Report as Another File Type (Report Builder and SSRS)](/previous-versions/sql/)
