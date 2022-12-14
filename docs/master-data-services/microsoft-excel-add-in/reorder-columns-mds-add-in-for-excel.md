---
description: "Reorder Columns (MDS Add-in for Excel)"
title: Reorder Columns
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: ac00462e-c0f7-4b8d-86f2-d9eda2598a15
author: CordeliaGrey
ms.author: jiwang6
---
# Reorder Columns (MDS Add-in for Excel)

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you can reorder columns by filtering the list before loading.  
  
 When you reorder attributes in the **Filter** dialog box, the data is loaded into Excel with the new order. However, the next time that you filter the attribute data, the order will revert to the order in the original design. To change the order permanently, an administrator should change the order in the **System Administration** area of Master Data Manager. For more information, see [Change the Order of Attributes](../../master-data-services/change-the-order-of-attributes.md).  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
### To reorder MDS-managed columns  
  
1.  Open Excel and on the **Master Data** tab, connect to an MDS repository. For more information, see [Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connect-to-an-mds-repository-mds-add-in-for-excel.md).  
  
2.  In the **Master Data Explorer** pane, select a model and version. The list of entities is populated.  
  
    -   If the **Master Data Explorer** pane is not visible, in the **Connect and Load** group, click **Show Explorer**.  
  
    -   If the **Master Data Explorer** pane is disabled, it is because the existing sheet already contains MDS-managed data. To enable the pane, open a new worksheet.  
  
3.  In the **Master Data Explorer** pane, click an entity.  
  
4.  In the **Connect and Load** group, click **Filter**.  
  
5.  In the **Filter** dialog box, in the **Columns** section, in the list of attributes, click the attribute you want to move.  
  
6.  To the right of the list, click the **Up** or **Down** arrow to move the attribute left and right in the worksheet.  
  
7.  Repeat step 7 for each attribute until the top-to-bottom order represents the left-to-right order you want in the worksheet.  
  
8.  Click **Load Data**. The sheet is populated with MDS-managed data and the columns are displayed in the order you specified.  
  
## See Also  
 [Overview: Exporting Data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)  
  
  
