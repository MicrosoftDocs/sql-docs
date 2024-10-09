---
title: "Preview reports in Report Builder"
description: While you create a Reporting Services paginated report, you can preview the report to verify that the report displays what you want.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Preview reports in Report Builder

  While you create a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report, you should preview the report often to verify that the report displays what you want. To preview your report, select **Run**. The report renders in preview mode.

Report Builder improves the preview experience by using edit sessions when connected to a report server. The edit session creates a data cache and makes the datasets in the cache available for repeated report previews. An edit session isn't a feature that you interact with directly. Rather, when the cached dataset is refreshed, the report helps you improve performance when you preview a report and understand why the report renders faster or slower.

Other benefits of edit sessions are the abilities to edit reports that use embedded data sources. You can also edit reports that use reference items, such as images or subreports that are stored on the report server.

> [!NOTE]  
> There are some differences between previewing in Report Builder and viewing in a browser. For example, a calendar control, which is added to a report when you specify a `Date/Time` type parameter, is different in Report Builder and in a browser.

## Improve preview performance

How you create and update reports affects how fast the report renders in preview. The first time that you preview a report that relies on a server reference, an edit session is created for you. The data used when the report is run is added to a data cache that is stored on the report server. When you make changes to the report that doesn't affect the data, the cached copy of the data is used by the report. This means that you don't see data change each time you preview the report. If you want new data, select the **Refresh** button on the ribbon.

The following actions cause the cache to be refreshed and slow down report rendering the next time you preview the report:

- Add, change, or delete a dataset. The cached dataset contains all the datasets that a report uses and modification to any dataset invalidates the cached dataset. This modification includes changing the name, query, or fields in the dataset.

    > [!NOTE]  
    >  If the dataset has a large number of fields that you do not expect to use, you should consider updating the dataset to omit those fields. Although this creates a new edit session and the first preview of the report is slower, there smaller cached dataset is overall beneficial to the performance of the report server.

- Add, change, or delete a data source. This modification includes changing the name or properties of the data source, the data extension of the data source, or the properties of the connection to the data source.

- Change the shared data source that the report uses to a different data source.

- Change the language of the report.

- Change the assemblies or custom code that the report uses.

- Add, change, or delete the query parameters in the report or parameter values.

Changes to the report layout and data formatting don't affect the cached dataset. You can do the following actions without refreshing the cached dataset:

- Add or remove data regions such as tables, matrices, or charts.

- Add or delete columns from the report. All the fields in the dataset are available to use in the report. Adding or removing fields in the report has no effect on the dataset.

- Change the order of fields in tables and matrices.

- Add, change, or delete row and column groups.

- Add, change, or delete formatting of data values in fields.

- Add, change, or delete images, lines, or text boxes.

- Change page breaks.

The edit session is created the first time that you preview a report. By default, an edit session lasts 7,200 seconds (2 hours). The session is reset to two hours every time you run the report. When the edit session expires, the data cache is deleted. If the edit session expires, one is automatically created again the next time that you preview the report. The expiration time for edit sessions is configurable. If you find that two hours is too long or too short, contact the administrator of the report server.

By default, the data cache can hold up to five datasets. If you use many different combinations of parameter values, the report might need more data. This need requires the cache be refreshed and the report renders more slowly the next time that you preview it. The number of entries in the cache is configurable by the administrator of the report server.

## Concurrency of report updates

Frequently, you preview a report as a step in updating and then saving a report to a report server. When you're updating a report, it someone else might update and then save the report at the same time. The report that is saved last is the version of report that is available for future viewing and updating. This result means that the version of the report that you previewed might not be the version you reopen. You can save the report with a new name by using the **Save As** option on the Report Builder menu.

## External report items

Your report might include items such as shared data sources, external images, and subreports that are stored separately from the report. Because the items are stored separately is possible that they can be moved to a different location on the report server or deleted. If you update at the same time as someone else, your report could fail to preview. You can update the report to indicate the updated location of the item. Or, if the item was deleted, you can replace it with an existing item, or remove the reference to the item it from the report.

If a subreport used by your report is changed after your edit session was created, the report doesn't render in preview. To successfully preview the report, you should save the report or select **Refresh** to get fresh data.

## Related content

- [Report datasets](../../reporting-services/report-data/report-datasets-ssrs.md)
- [Format report items (Report Builder)](../../reporting-services/report-design/formatting-report-items-report-builder-and-ssrs.md)
- [Tables, matrices, and lists (Report Builder)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Charts (Report Builder)](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
- [Save reports (Report Builder)](../../reporting-services/report-builder/saving-reports-report-builder.md)
