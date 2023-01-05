---
description: "Consolidated Member Staging Table (Master Data Services)"
title: Consolidated Member Staging Table
ms.custom: ""
ms.date: "04/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "database [Master Data Services], attributes staging table"
  - "attributes staging table [Master Data Services]"
ms.assetid: 070681ed-be99-49ae-93bd-6402f2134ace
author: CordeliaGrey
ms.author: jiwang6
---
# Consolidated Member Staging Table (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Use the consolidated members staging table (stg.name_Consolidated) in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database to create, update, deactivate, and delete consolidated members. You can also use it to update attribute values for consolidated members.  
  
##  <a name="TableColumns"></a> Table Columns  
 The following table explains what each of the fields in the Consolidated staging table are used for.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|**ID**|An automatically assigned identifier. Do not enter a value in this field. If the batch has not been processed, this field is blank.|  
|**ImportType**<br /><br /> Required|Determines what to do when staged data matches data that already exists in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.<br /><br /> **0**: Create new members. Replace existing MDS data with staged data, but only if the staged data isn't NULL. NULL values are ignored. To change an attribute value to NULL, use **~NULL~**.<br /><br /> **1**: Create new members only. Any updates to existing MDS data fail.<br /><br /> **2**: Create new members. Replace existing MDS data with staged data. If you import NULL values, they will overwrite existing MDS values.<br /><br /> **3**: Deactivate the member, based on the Code value. All attributes, hierarchy and collection memberships, and transactions are maintained but no longer available in the UI. If the member is used as a domain-based attribute value of another member, the deactivation will fail.<br /><br /> **4**: Permanently delete the member, based on the Code value. All attributes, hierarchy and collection memberships, and transactions are permanently deleted. If the member is used as a domain-based attribute value of another member, the deletion will fail.|  
|**ImportStatus_ID**<br /><br /> Required|The status of the import process. Possible values are:<br /><br /> **0**, which you specify to indicate that the record is ready for staging.<br /><br /> **1**, which is automatically assigned and indicates that the staging process for the record has succeeded.<br /><br /> **2**, which is automatically assigned and indicates that the staging process for the record has failed.|  
|**Batch_ID**<br /><br /> Required by web service only|An automatically assigned identifier that groups records for staging. All members in the batch are assigned this identifier, which is displayed in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface in the **ID** column.<br /><br /> If the batch has not been processed, this field is blank.|  
|**BatchTag**<br /><br /> Required, except by web service|A unique name for the batch, up to 50 characters.|  
|**HierarchyName**<br /><br /> Required|The explicit hierarchy name. Each consolidated member can belong to one hierarchy only.|  
|**ErrorCode**|Displays an error code. For all records with a **ImportStatus_ID** of **2**, see [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md).|  
|**Code**<br /><br /> Required, except when codes are generated automatically for **ImportType1** or **2**; see [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md) for more information|A unique code for the member.|  
|**Name**<br /><br /> Optional|A name for the member.|  
|**NewCode**|Use only if you are changing the member code.|  
|\<Attribute name>|A column exists for each attribute in the entity. Use this with an **ImportType** of **0** or **2**. For free-form attributes, specify the new text or string value for the attribute. For domain-based attributes, specify the code for the member that will be the attribute. For link attributes, the URL must start with **https://**.<br /><br /> <br /><br /> Note: You cannot stage file attributes.|  
  
## See Also  
 [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)   
 [View Errors that Occur During Staging &#40;Master Data Services&#41;](../master-data-services/view-errors-that-occur-during-staging-master-data-services.md)   
 [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md)  
  
  
