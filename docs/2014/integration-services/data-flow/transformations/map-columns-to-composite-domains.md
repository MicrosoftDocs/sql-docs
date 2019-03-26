---
title: "Map Columns to Composite Domains | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: d9422412-8a3d-45ae-af7f-072c902a09ba
author: janinezhang
ms.author: janinez
manager: craigg
---
# Map Columns to Composite Domains
  A composite domain consists of two or more single domains. You can map multiple columns to the domain, or you can map a single column with delimited values to the domain.  
  
 When you have multiple columns, you must map a column to each single domain in the composite domain to apply the composite domain rules to data cleansing. You select the single domains contained in the composite domain, in the Data Quality Client. For more information, see [Create a Composite Domain](../../../data-quality-services/create-a-composite-domain.md).  
  
 When you have a single column with delimited values, you must map the single column to the composite domain. The values must appear in the same order as the single domains appear in the composite domain. The delimiter in the data source must match the delimiter that is used to parse the composite domain values. You select the delimiter for the composite domain, as well as set other properties, in the Data Quality Client. For more information, see [Create a Composite Domain](../../../data-quality-services/create-a-composite-domain.md).  
  
### To map multiple columns to a composite domain  
  
1.  Right-click the DQS Cleansing transformation, and then click **Edit**.  
  
2.  On the **Connection Manager** tab, confirm that the composite domain appears in the list of available domains.  
  
3.  On the **Mapping** tab, select the columns in the **Available Input Columns** area.  
  
4.  For each column listed in the **Input Column** field, select a single domain in the **Domain** field. Select only single domains that are in the composite domain.  
  
5.  As needed, modify the names that appear in the **Source Alias**, **Output Alias**, and **Status Alias** fields.  
  
6.  As needed, set properties on the **Advanced** tab. For more information about the properties, see [DQS Cleansing Transformation Editor Dialog Box](../../dqs-cleansing-transformation-editor-dialog-box.md).  
  
### To map a column with delimited values to a composite domain  
  
1.  Right-click the DQS Cleansing transformation, and then click **Edit**.  
  
2.  On the **Connection Manager** tab, confirm that the composite domain appears in the list of available domains.  
  
3.  On the **Mapping** tab, select the column in the **Available Input Columns** area.  
  
4.  For the column listed in the **Input Column** field, select the composite domain in the **Domain** field.  
  
5.  As needed, modify the names that appear in the **Source Alias**, **Output Alias**, and **Status Alias** fields.  
  
6.  As needed, set properties on the **Advanced** tab. For more information about the properties, see [DQS Cleansing Transformation Editor Dialog Box](../../dqs-cleansing-transformation-editor-dialog-box.md).  
  
## See Also  
 [DQS Cleansing Transformation](dqs-cleansing-transformation.md)  
  
  
