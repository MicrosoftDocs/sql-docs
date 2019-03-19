---
title: "Overview: Exporting Data to Excel (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: b628548b-982b-4e45-abf4-c8e83e3ab1c2
author: leolimsft
ms.author: lle
manager: craigg
---
# Overview: Exporting Data to Excel (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must export data from the MDS repository into an active Excel worksheet before you can work with it. When you are done working with the data, import it to the MDS repository so other users can share it.  
  
 The data you can export  is limited to the data you have permission to access. Permission to access data is set in the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application or set programmatically.  
  
 When you export  large amounts of data, you can set warnings that are displayed when the data that might take a long time to load. To do this, in the **Options** group, click **Settings**. On the **Data** tab, select the **Display filter warning for large data sets**.  
  
> [!WARNING]  
>  An MDS-enabled workbook must be opened and updated only in Excel with the MDS Add-in for Excel. Opening an MDS-enabled workbook in Excel on a computer in which the MDS Excel Add-in is not installed is not supported, and could cause corruption of the workbook file. If you want to share data with someone else, email a shortcut query file to them, rather than saving the worksheet and emailing it. For more information on the query, see [Email a Shortcut Query File &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/email-a-shortcut-query-file-mds-add-in-for-excel.md).  
  
## Filtering Data  
 You can filter data before exporting to limit the amount of data that you're going to download. This includes choosing which attributes (columns) you want to load, the order you want to display the attributes, and the members (rows of data) you want to work with. For more info see [Filter Data before Exporting &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/filter-data-before-exporting-mds-add-in-for-excel.md).  
  
## Connect Automatically and Load Frequently-Used Data  
 If you want to always connect to the same server and export the same set of data, you can create shortcut query files, which contain connection and filter information. For more information about query files, see [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/shortcut-query-files-mds-add-in-for-excel.md).  
  
## Refreshing Data  
 Data in the MDS repository may be updated by other users after you export it. You can retrieve this data without losing changes you've made to non-MDS data. For more information, see [Refreshing Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/refreshing-data-mds-add-in-for-excel.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Filter MDS data before you load it into Excel.|[Filter Data before Exporting &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/filter-data-before-exporting-mds-add-in-for-excel.md)|  
|Load MDS data into Excel.|[Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md)|  
|Change the order of columns before you download data.|[Reorder Columns &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/reorder-columns-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Connections &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connections-mds-add-in-for-excel.md)  
  
-   [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/shortcut-query-files-mds-add-in-for-excel.md)  
  
-   [Master Data Services Add-in for Microsoft Excel](../../master-data-services/microsoft-excel-add-in/master-data-services-add-in-for-microsoft-excel.md)  
  
-   [Security &#40;Master Data Services&#41;](../../master-data-services/security-master-data-services.md)  
  
  
