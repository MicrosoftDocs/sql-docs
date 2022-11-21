---
description: "Filter Data before Exporting (MDS Add-in for Excel)"
title: Filter Data before Exporting
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 9e30eae0-776b-4a09-aac3-0c0249d92ca5
author: CordeliaGrey
ms.author: jiwang6
---
# Filter Data before Exporting (MDS Add-in for Excel)

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], filter data when you want to limit the size or scope of data that you are exporting to Excel.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
### To filter data before exporting  
  
1.  Open Excel and on the **Master Data** tab, connect to an MDS repository. For more information, see [Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connect-to-an-mds-repository-mds-add-in-for-excel.md).  
  
2.  In the **Master Data Explorer** pane, select a model and version. The list of entities is populated.  
  
    -   If the **Master Data Explorer** pane is not visible, in the **Connect and Load** group, click **Show Explorer**.  
  
    -   If the **Master Data Explorer** pane is disabled, it is because the existing sheet already contains MDS-managed data. To enable the pane, open a new worksheet.  
  
3.  In the **Master Data Explorer** pane, in the list of entities, click the entity you want to filter.  
  
4.  On the ribbon, in the **Connect and Load** group, click **Filter**.  
  
5.  Complete the **Filter** dialog box by selecting the attributes (columns) to display, setting the order of the columns, and if needed, filtering the data so fewer rows are returned. View the **Summary** pane for how much data will be returned. For more information, see [Filter Dialog Box &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/filter-dialog-box-mds-add-in-for-excel.md).  
  
6.  Click **Load Data**. The sheet is populated with MDS-managed data.  
  
    > [!NOTE]  
    >  -   Only the first one million members are loaded into Excel.  
    > -   In columns that are constrained lists (domain-based attributes), by default only the first 25000 values are loaded.  
  
## Next Steps  
 [Import Data from Excel to Master Data Services &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/import-data-from-excel-to-master-data-services-mds-add-in-for-excel.md)  
  
## See Also  
 [Overview: Exporting Data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)   
 [Filter Dialog Box &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/filter-dialog-box-mds-add-in-for-excel.md)   
 [Reorder Columns &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/reorder-columns-mds-add-in-for-excel.md)  
  
  
