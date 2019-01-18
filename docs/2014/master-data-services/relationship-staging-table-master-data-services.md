---
title: "Relationship Staging Table (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "relationships staging table [Master Data Services]"
  - "database [Master Data Services], relationships table"
ms.assetid: e19b6002-67bd-4e7d-9f19-ecb455522b1a
author: leolimsft
ms.author: lle
manager: craigg
---
# Relationship Staging Table (Master Data Services)
  Use the relationship staging table (stg.name_Relationship) in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database to change the location of members in an explicit hierarchy, based on the relationship the members have to one another.  
  
##  <a name="TableColumns"></a> Table Columns  
 The following table explains what each of the fields in the Relationship staging table are used for.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|**ID**|An automatically assigned identifier. Do not enter a value in this field. If the batch has not been processed, this field is blank.|  
|**RelationshipType**<br /><br /> Required|The type of relationship that's being set. Possible values are:<br /><br /> **1**:Parent<br /><br /> **2**: Sibling (at the same level)|  
|**ImportStatus_ID**<br /><br /> Required|The status of the import process. Possible values are:<br /><br /> **0**, which you specify to indicate that the record is ready for staging.<br /><br /> **1**, which is automatically assigned and indicates that the staging process for the record has succeeded.<br /><br /> **2**, which is automatically assigned and indicates that the staging process for the record has failed.|  
|**Batch_ID**<br /><br /> Required by web service only|An automatically assigned identifier that groups records for staging. All members in the batch are assigned this identifier, which is displayed in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface in the **ID** column.<br /><br /> If the batch has not been processed, this field is blank.|  
|**BatchTag**<br /><br /> Required, except by web service|A unique name for the batch, up to 50 characters.|  
|**HierarchyName**<br /><br /> Required|The explicit hierarchy name. Each consolidated member can belong to one hierarchy only.|  
|**ParentCode**<br /><br /> Required|For parent-child relationships, the code of the consolidated member that will be the parent of the child leaf or consolidated member.<br /><br /> For sibling relationships, the code of one of the siblings.|  
|**ChildCode**<br /><br /> Required|For parent-child relationships, the code of the consolidated or leaf member that will be the child.<br /><br /> For sibling relationships, the code of one of the siblings.|  
|**Sort Order**<br /><br /> Optional|An integer that indicates the order of the member in relation to the other members under the parent. Each child member should have a unique identifier.|  
|**ErrorCode**|Displays an error code. For all records with a **ImportStatus_ID** of **2**, see [Staging Process Errors &#40;Master Data Services&#41;](staging-process-errors-master-data-services.md).|  
  
## See Also  
 [Move Explicit Hierarchy Members by Using the Staging Process &#40;Master Data Services&#41;](/sql/2014/master-data-services/add-update-and-delete-data-master-data-services)   
 [Data Import &#40;Master Data Services&#41;](overview-importing-data-from-tables-master-data-services.md)   
 [View Errors that Occur During the Staging Process &#40;Master Data Services&#41;](view-errors-that-occur-during-staging-master-data-services.md)   
 [Staging Process Errors &#40;Master Data Services&#41;](staging-process-errors-master-data-services.md)  
  
  
