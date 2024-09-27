---
title: "Save reports to a report server (Report Builder)"
description: In Report Builder, you can publish a report to a report server. Others can view it. Each time you run the published report, you see the most current data.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Save reports to a report server (Report Builder)

  In Report Builder, you can save a report definition to a report server, also known as publishing a report. When the report is saved to a report server, other users can view the report. Each time you run the published report, you retrieve the most current data. To save a static copy of a rendered report, export the report to a different file format and save it or use the report history feature to save versions of rendered reports.

> [!NOTE]  
> The location of the saved report definition does not affect whether the report is processed on the server or processed locally when you preview the report.

### Save a report to a report server

1. From the Report Builder button, select **Save**. The **Save As**_\<Report Item>_ dialog opens.

    > [!NOTE]  
    >  If you are resaving a report, it is automatically resaved to its previous location. Use the Save As option to change location.

1. Optionally, select **Recent Sites and Servers** to show a list of recently used report servers and SharePoint sites.

1. Browse to the report server location where you want to save the report.

1. In **Name**, enter the name of the report.

1. In **Items of type**, select the type of report item you're saving. The type for reports is Reports (*.rdl).

### Save a report as a different name

1. From the **Report Builder** button, select **Save As**. The **Save As**_\<Report Item>_ dialog opens.

1. Browse to the report server location or to the file share where you want to save the report.

1. In **Name**, enter the name of the report.

1. In **Items of type**, select the type of report item you're saving. The type for reports is Reports (*.rdl).

## Related content

- [Find, view, and manage reports (Report Builder)](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
- [Export reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Save reports (Report Builder)](../../reporting-services/report-builder/saving-reports-report-builder.md)
- [Export a report as another file type (Report Builder)](/previous-versions/sql/)
