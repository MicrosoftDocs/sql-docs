---
title: "Data Quality Matching Columns (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: f683fdc6-0d4c-4793-8143-567616cb2094
author: leolimsft
ms.author: lle
manager: craigg
---
# Data Quality Matching Columns (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], after you match data, in the **Data Quality** group on the ribbon, you can click **Show Details** to display columns that provide matching details.  
  
 The following table shows the columns that are displayed when matching data.  
  
|Name|Description|  
|----------|-----------------|  
|**CLUSTER_ID**|A unique identifier used to group similar records. All rows that are similar have the same **CLUSTER_ID**. If no **CLUSTER_ID** is displayed for a row, then no similar records were found.|  
|**RECORD_ID**|A unique identifier used to identify records. Similar to the Code value stored in the MDS repository, it is a value used to identify a record. It is generated automatically each time matching takes place.|  
|**PIVOT_MARK**|An arbitrary record that other records are compared to; it does not have a score value.|  
|**SCORE**|Represents how similar the records in the group are to the pivot record. This score is determined by DQS. If no score is displayed, either the record is the pivot for other records, or no matches were found.|  
  
## See Also  
 [Data Quality Matching in the MDS Add-in for Excel](../../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md)   
 [Match Similar Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md)   
 [Data Matching](../../data-quality-services/data-matching.md)  
  
  
