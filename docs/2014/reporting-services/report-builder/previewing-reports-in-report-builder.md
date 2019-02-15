---
title: "Previewing Reports in Report Builder | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: ba6b5bdd-d8c6-4aa8-ba32-3a10b11969d4
author: markingmyname
ms.author: maghan
manager: kfile
---
# Previewing Reports in Report Builder
  While you create a report, it is helpful to preview the report often to verify that the report displays what you want. To preview your report, click **Run**. The report renders in preview mode.  
  
 Report Builder improves the preview experience by using edit sessions when connected to a report server. The edit session creates a data cache and makes the datasets in the cache available for repeated report previews. An edit session is not a feature that you interact with directly, but understanding when the cached dataset is refreshed will help you improve performance when you preview a report and understand why the report renders faster or slower.  
  
 Other benefits of edit sessions are the abilities to edit reports that use embedded data sources or reference items such as images or subreports that are stored on the report server.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Improving Preview Performance  
 How you create and update reports affects how fast the report renders in preview. The first time that you preview a report that relies on a server reference, an edit session is created for you and the data used when the report is run is added to a data cache that is stored on the report server. When you make changes to the report that does not affect the data, the cached copy of the data is used by the report. This means that you will not see data change each time you preview the report. If you want new data, click the **Refresh** button on the ribbon.  
  
 The following actions cause the cache to be refreshed and slow down report rendering the next time you preview the report:  
  
-   Add, change, or delete a dataset. The cached dataset contains all the datasets that a report uses and modification to any dataset invalidates the cached dataset. This includes changing the name, query, or fields in the dataset.  
  
    > [!NOTE]  
    >  If the dataset has a large number of fields that you do not expect to use, you should consider updating the dataset to omit those fields. Although this creates a new edit session and the first preview of the report is slower, there smaller cached dataset is overall beneficial to the performance of the report server.  
  
-   Add, change, or delete a data source. This includes changing the name or properties of the data source, the data extension of the data source, or the properties of the connection to the data source.  
  
-   Change the shared data source that the report uses to a different data source.  
  
-   Change the language of the report.  
  
-   Change the assemblies or custom code that the report uses.  
  
-   Add, change, or delete the query parameters in the report or parameter values.  
  
 Changes to the report layout and data formatting do not affect the cached dataset. You can do the following actions without refreshing the cached dataset:  
  
-   Add or remove data regions such as tables, matrices or charts.  
  
-   Add or delete columns from the report. All the fields in the dataset are available to use in the report. Adding or removing fields in the report has no effect on the dataset.  
  
-   Change the order of fields in tables and matrices.  
  
-   Add, change, or delete row and column groups.  
  
-   Add, change, or delete formatting of data values in fields.  
  
-   Add, change, or delete images, lines, or text boxes.  
  
-   Change page breaks.  
  
 The edit session is created the first time that you preview a report. By default, an edit session lasts 7200 seconds (2 hours). The session is reset to two hours every time you run the report. When the edit session expires, the data cache is deleted. If the edit session expires, one is automatically created again the next time that you preview the report. The expiration time for edit sessions is configurable. If you find that two hours is too long or too short, contact the administrator of the report server.  
  
 By default, the data cache can hold up to five datasets. If you use many different combinations of parameter values, the report might need more data. This requires the cache be refreshed and the report renders more slowly the next time that you preview it. The number of entries in the cache is configurable by the administrator of the report server.  
  
## Concurrency of Report Updates  
 Frequently, you preview a report as a step in updating and then saving a report to a report server. When you are updating a report, it is possible that someone else is updating and then saving the report at the same time. The report that is saved last is the version of report that is available for future viewing and updating. This means that the version of the report that you previewed might not be the version you reopen. You have the option to save the report with a new name by using the **Save As** option on the Report Builder menu.  
  
## External Report Items  
 Your report might include items such as shared data sources, external images, and subreports that are stored separately from the report. Because the items are stored separately is possible that they can be moved to a different location on the report server or deleted. If this happens, your report could fail to preview. You can either update the report to indicate the updated location of the item or if the item was deleted, replace it with an existing item, or remove the reference to the item it from the report.  
  
 If a subreport used by your report is changed after your edit session was created, the report will not render in preview. To successfully preview the report, you should save the report or click **Refresh** to get fresh data.  
  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](../report-data/report-datasets-ssrs.md)   
 [Formatting Report Items &#40;Report Builder and SSRS&#41;](../report-design/formatting-report-items-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../report-design/charts-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Saving Reports &#40;Report Builder&#41;](saving-reports-report-builder.md)  
  
  
