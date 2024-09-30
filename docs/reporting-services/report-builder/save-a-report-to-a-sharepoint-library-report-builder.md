---
title: "Save a report to a SharePoint library (Report Builder)"
description: This article describes the requirements and process necessary to save reports to a report server configured for SharePoint integration.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Save a report to a SharePoint library (Report Builder)

  To save a report to a report server configured for SharePoint integration, you must browse to the SharePoint server and establish a connection to the report server. In the report definition, all references to items related to the report must use values that are specific to a SharePoint report server. Related items include subreports, drillthrough reports, and resources such as Web-based images. For more information, see [Specify paths to external items (Report Builder)](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).

You must have **Member** or **Owner** permission on the SharePoint site to set the properties on the project.

### Save a report to a SharePoint site

1. From the Report Builder button, select **Save**. The **Save As**_\<Report Item>_ dialog opens.

    > [!NOTE]  
    >  If you are resaving a report, the report is automatically resaved to its previous location. Use the **Save As** option to change location.

1. Optionally, select **Recent Sites and Servers** to show a list of recently used report servers and SharePoint sites.

1. Browse to the SharePoint site, and then select **Save**.

    > [!NOTE]  
    >  If you leave a changed report for more than 10 hours without saving it, it is disconnected from the server without being saved. If that happens, in the lower-right status bar, select **Disconnect**, and then select **Connect**. The most recent server is in the list of available servers. Select the server and the report reconnects.

## Related content

- [Find, view, and manage reports (Report Builder)](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
