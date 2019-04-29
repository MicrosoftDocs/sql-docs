---
title: "Refreshing Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 58dbe99a-288d-4f1c-9cd5-704d6836c945
author: leolimsft
ms.author: lle
manager: craigg
---
# Refreshing Data (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], refresh data when you want to get the latest information from the MDS repository without opening a new worksheet. You can refresh either all cells or a selection of cells. This can be useful when you have inserted columns with custom formulas or other data that is not managed in MDS and you want to preserve it.  
  
## When You Can Refresh MDS-Managed Data  
 You can refresh MDS-managed data in an active worksheet if the active worksheet already contains MDS-managed data. If you have changed attribute values or added members to the worksheet, you must publish your changes before you can refresh.  
  
## Refreshing a Selection  
 You have the choice of refreshing all cells or refreshing only selected cells. The selected cells must be contiguous. If a set of contiguous cells is selected, all MDS managed cells in that set will be updated to display the values currently stored on the server. Unchanged rows and columns that are not managed by MDS are not affected in any way.  
  
## What Happens When You Refresh MDS-Managed Data  
 When you refresh data in the [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], what happens to the MDS-managed data in the sheet depends on what has changed in the MDS repository since you last loaded the data.  
  
-   If new members have been added to repository, they are added to the end of the worksheet and are highlighted in green.  
  
-   If members were deleted from the repository, they are deleted from the worksheet.  
  
-   If an attribute value has changed in the MDS repository, the value in the worksheet is updated with value from the MDS repository. The cell color does not change.  
  
> [!WARNING]
>  -   In the active worksheet, if non-managed data exists in rows beneath the MDS-managed data, the non-managed data may be overwritten. This occurs when you refresh the sheet and new rows of MDS-managed data, which overlap with the non-managed data, are added.  
> -   When you refresh, comments on MDS-managed cells are deleted.  
  
## How to Refresh MDS-Managed Data  
 In the **Connect and Load** group on the ribbon, the **Refresh** button has two options, **Refresh All** and **Refresh Selection**. The default action of the ribbon button is **Refresh All**. To refresh the entire sheet with values from the server, click the **Refresh** button or choose the **Refresh All** option. To refresh only some of the cells in a sheet, select the cells (must be one contiguous selection) and choose the **Refresh Selection** option.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a connection to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database.|[Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connect-to-an-mds-repository-mds-add-in-for-excel.md)|  
|Load MDS data into Excel.|[Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md)|  
  
## Related Content  
  
-   [Connections &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connections-mds-add-in-for-excel.md)  
  
-   [Overview: Exporting Data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)  
  
-   [Master Data Services Add-in for Microsoft Excel](../../master-data-services/microsoft-excel-add-in/master-data-services-add-in-for-microsoft-excel.md)  
  
  
