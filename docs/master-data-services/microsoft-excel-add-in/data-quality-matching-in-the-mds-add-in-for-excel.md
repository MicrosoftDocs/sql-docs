---
title: "Data Quality Matching in the MDS Add-in for Excel | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: be78d051-0d56-46d3-bb89-327e218dadd6
author: leolimsft
ms.author: lle
manager: craigg
---
# Data Quality Matching in the MDS Add-in for Excel

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Over time, you will want to add more data to the MDS repository. Before adding data, it can be useful to compare the new data to the data that's already managed in MDS, to ensure you are not adding duplicate or inaccurate data.  
  
 The MDS [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] uses the Data Quality Services (DQS) feature of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to match data that's similar. When you use the matching functionality in the Add-in, similar records are grouped together and a score that represents the accuracy of the result is displayed. For more information about the matching functionality provided by DQS, see [Data Matching](../../data-quality-services/data-matching.md).  
  
## Workflow for Data Quality Matching  
 When using DQS with the MDS [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], use the following workflow:  
  
1.  Retrieve a list of MDS-managed data and combine it with a list that is not managed in MDS. For more information, see [Combine Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/combine-data-mds-add-in-for-excel.md).  
  
2.  Use DQS knowledge to compare the data in the combined list. For more information, see [Match Similar Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md).  
  
3.  To view more details about the similarities found by DQS, show the detail columns.  
  
4.  Go through the results and determine which data should be added to the MDS repository and which data is duplicated.  
  
5.  Publish the new and/or updated data to the MDS repository.  
  
## Knowledge Bases  
 The matching results provided in the Add-in are based on a DQS knowledge base.  
  
-   The default knowledge base (DQS Data) is created when DQS is installed. If you choose to use the default knowledge base (without adding a matching policy to the default knowledge base in the Data Quality Client), you must map columns in the worksheet to domains in the knowledge base, and then assign a weight value to the domains you choose.  
  
-   You can use the Data Quality Client to create a new knowledge base with a matching policy, or to add a matching policy to the default knowledge base. In this case, the weight values are determined by the matching policy you already created and you need only to map the columns to the domains. For more information, see [Create a Matching Policy](../../data-quality-services/create-a-matching-policy.md).  
  
 For more information about knowledge bases, see [DQS Knowledge Bases and Domains](../../data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Combine external data with MDS-managed data in preparation to compare it.|[Combine Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/combine-data-mds-add-in-for-excel.md)|  
|Use DQS knowledge to find similarities in your data.|[Match Similar Data &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/match-similar-data-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Overview: Importing Data from Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
-   [Data Matching](../../data-quality-services/data-matching.md)  
  
  
