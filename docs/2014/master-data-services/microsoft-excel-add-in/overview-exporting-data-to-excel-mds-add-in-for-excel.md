---
title: "Loading Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: b628548b-982b-4e45-abf4-c8e83e3ab1c2
author: leolimsft
ms.author: lle
manager: craigg
---
# Loading Data (MDS Add-in for Excel)
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must load data from the MDS repository into an active Excel worksheet before you can work with it. When you are done working with the data, publish it to the MDS repository so other users can share it.  
  
 The data you can load is limited to the data you have permission to access. Permission to access data is set in the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application or set programmatically.  
  
 When you load large amounts of data, you can set warnings that are displayed when the data that might take a long time to load. To do this, in the **Options** group, click **Settings**. On the **Data** tab, select the **Display filter warning for large data sets**.  
  
> [!WARNING]  
>  An MDS-enabled workbook must be opened and updated only in Excel with the MDS Add-in for Excel. Opening an MDS-enabled workbook in Excel on a computer in which the MDS Excel Add-in is not installed is not supported, and could cause corruption of the workbook file. If you want to share data with someone else, email a shortcut query file to them, rather than saving the worksheet and emailing it. For more information on the query, see [Email a Shortcut Query File &#40;MDS Add-in for Excel&#41;](email-a-shortcut-query-file-mds-add-in-for-excel.md).  
  
## Filtering Data  
 You can filter data before loading to limit the amount of data that you're going to download. This includes choosing which attributes (columns) you want to load, the order you want to display the attributes, and the members (rows of data) you want to work with. For more info see [Filter Data before Loading &#40;MDS Add-in for Excel&#41;](filter-data-before-exporting-mds-add-in-for-excel.md).  
  
## Connect Automatically and Load Frequently-Used Data  
 If you want to always connect to the same server and load the same set of data, you can create shortcut query files, which contain connection and filter information. For more information about query files, see [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](shortcut-query-files-mds-add-in-for-excel.md).  
  
## Refreshing Data  
 Data in the MDS repository may be updated by other users after you load it. You can retrieve this data without losing changes you've made to non-MDS data. For more information, see [Refreshing Data &#40;MDS Add-in for Excel&#41;](refreshing-data-mds-add-in-for-excel.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Filter MDS data before you load it into Excel.|[Filter Data before Loading &#40;MDS Add-in for Excel&#41;](filter-data-before-exporting-mds-add-in-for-excel.md)|  
|Load MDS data into Excel.|[Load Data from MDS into Excel](export-data-to-excel-from-master-data-services.md)|  
|Change the order of columns before you download data.|[Reorder Columns &#40;MDS Add-in for Excel&#41;](reorder-columns-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Connections &#40;MDS Add-in for Excel&#41;](connections-mds-add-in-for-excel.md)  
  
-   [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](shortcut-query-files-mds-add-in-for-excel.md)  
  
-   [Master Data Services Add-in for Microsoft Excel](master-data-services-add-in-for-microsoft-excel.md)  
  
-   [Security &#40;Master Data Services&#41;](../security-master-data-services.md)  
  
  
