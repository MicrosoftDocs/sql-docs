---
title: "Combine Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: a867dc15-5a0d-457c-8304-ac323bcf9377
author: leolimsft
ms.author: lle
manager: craigg
---
# Combine Data (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], combine data from two worksheets when you want to compare data before publishing. In this procedure, you combine data from a two worksheets into one. Then you can perform further comparisons and determine which data, if any, to publish to the MDS repository.  
  
## Prerequisites  
  
-   You must have a worksheet that contains MDS-managed data. For more information, see [Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md).  
  
-   You must have a worksheet that contains data you want to combine with MDS-managed data. This sheet must have a header row.  
  
### To combine non-managed data into an MDS-managed sheet  
  
1.  On the sheet that contains MDS-managed data, in the **Publish and Validate** group, click **Combine Data**.  
  
2.  In the **Combine Data** dialog box, next to the **Range to combine with MDS data** text box, click the icon. The dialog box contracts.  
  
3.  Click the sheet that contains the data you want to combine.  
  
4.  Highlight all cells on the sheet that you want to combine, including the header row.  
  
5.  In the **Combine Data** dialog box, click the icon. The dialog box expands.  
  
6.  For a column listed for the MDS entity, select a column under **Corresponding Column**. All MDS columns do not need corresponding columns.  
  
7.  Click **Combine**. A **SOURCE** column is displayed, indicating whether the data is from MDS or an external source.  
  
## Next Steps  
  
-   To find similarities between the MDS-managed and external data, see [Match Similar Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md).  
  
## See Also  
 [Overview: Exporting Data to Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-exporting-data-to-excel-mds-add-in-for-excel.md)   
 [Data Quality Matching in the MDS Add-in for Excel](../../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md)  
  
  
