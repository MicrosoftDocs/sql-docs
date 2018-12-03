---
title: "Match Similar Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: f6fd6fc1-3569-42a5-b6cb-87a921c88f3b
author: leolimsft
ms.author: lle
manager: craigg
---
# Match Similar Data (MDS Add-in for Excel)
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], use Data Quality Services (DQS) functionality to find similarities in your data.  
  
 To perform this procedure, you can:  
  
-   Use the default Data Quality Services knowledge base, or  
  
-   Create your own custom DQS knowledge base and matching policy. For more information, see [Create a Matching Policy](../../data-quality-services/create-a-matching-policy.md).  
  
## Prerequisites  
  
-   You must have a worksheet that contains MDS-managed data. For more information, see [Load Data from MDS into Excel](export-data-to-excel-from-master-data-services.md).  
  
-   Optional. You can combine other data with the MDS-managed data before checking for similarities. For more information, see [Combine Data &#40;MDS Add-in for Excel&#41;](combine-data-mds-add-in-for-excel.md).  
  
### To find similarities by using the default knowledge base  
  
1.  From the worksheet that contains MDS-managed data, in the **Data Quality** group, click **Match Data**.  
  
2.  In the **Match Data** dialog box, from the **DQS Knowledge Base** list, select **DQS Data (default)**.  
  
3.  For each column that contains data you want to match, add a row in the dialog box. For information about the fields in this dialog box, see [How to Set Matching Rule Parameters](../../data-quality-services/create-a-matching-policy.md#MatchingRules).  
  
4.  When the total of all weight values equals 100 percent, click **OK**.  
  
### To find similarities by using a custom knowledge base  
  
1.  From the worksheet that contains MDS-managed data, in the **Data Quality** group, click **Match Data**.  
  
2.  From the **DQS Knowledge Base** list, select the name of your custom knowledge base.  
  
3.  For each column in the worksheet, select a DQS domain.  
  
4.  When all DQS domains are mapped to columns in the worksheet, click **OK**.  
  
## Next Steps  
  
-   View additional information to determine which data is similar. For more information, see [Data Quality Matching Columns &#40;MDS Add-in for Excel&#41;](data-quality-matching-columns-mds-add-in-for-excel.md).  
  
## See Also  
 [Data Quality Matching in the MDS Add-in for Excel](data-quality-matching-in-the-mds-add-in-for-excel.md)   
 [Data Matching](../../data-quality-services/data-matching.md)  
  
  
