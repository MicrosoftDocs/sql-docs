---
title: "Leaf Member Staging Table (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "04/01/2016"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "members staging table [Master Data Services]"
  - "database [Master Data Services], members staging table"
ms.assetid: a8c953da-ec20-47dc-8656-ed5f0dfed89b
author: leolimsft
ms.author: lle
manager: craigg
---
# Leaf Member Staging Table (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Use the leaf members staging table (stg.name_Leaf) in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database to create, update, deactivate, and delete leaf members. You can also use it to update attribute values for leaf members.  
  
##  <a name="TableColumns"></a> Table Columns  
 The following table explains what each of the fields in the Leaf staging table are used for.  
  
|Column Name|Description|Values|  
|-----------------|-----------------|------------|  
|**ID**|An automatically assigned identifier.|Do not enter a value in this field. If the batch has not been processed, this field is blank.|  
|**ImportType**<br /><br /> Required|Determines what to do when staged data matches data that already exists in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|**0**: Create new members. Replace existing MDS data with staged data, but only if the staged data is not NULL. NULL values are ignored. To change a string attribute value to NULL, set it **~NULL~**. To change a number attribute value to NULL, set it to **-98765432101234567890**. To change a datetime attribute value to NULL, set it to **5555-11-22T12:34:56**.<br /><br /> **1**: Create new members only. Any updates to existing MDS data fail.<br /><br /> **2**: Create new members. Replace existing MDS data with staged data. If you import NULL values, they will overwrite existing MDS values.<br /><br /> **3**: Deactivate the member, based on the Code value. All attributes, hierarchy and collection memberships, and transactions are maintained but no longer available in the UI. If the member is used as a domain-based attribute value of another member, the deactivation will fail. See **ImportType5** for an alternative.<br /><br /> **4**: Permanently delete the member, based on the Code value. All attributes, hierarchy and collection memberships, and transactions are permanently deleted. If the member is used as a domain-based attribute value of another member, the deletion will fail. See **ImportType6** for an alternative.<br /><br /> **5**: Deactivate the member, based on the **Code** value. All attributes, hierarchy and collection memberships, and transactions are maintained but no longer available in the UI. If the member is used as a domain-based attribute value of other members, the related values will be set to NULL. ImportType 5 is for leaf members only.<br /><br /> **6**: Permanently delete the member, based on the **Code** value. All attributes, hierarchy and collection memberships, and transactions are permanently deleted. If the member is used as a domain-based attribute value of other members, the related values will be set to NULL. ImportType 6 is for leaf members only.|  
|**ImportStatus_ID**<br /><br /> Required|The status of the import process.|**0**, which you specify to indicate that the record is ready for staging.<br /><br /> **1**, which is automatically assigned and indicates that the staging process for the record has succeeded.<br /><br /> **2**, which is automatically assigned and indicates that the staging process for the record has failed.|  
|**Batch_ID**<br /><br /> Required by web service only|An automatically assigned identifier that groups records for staging. All members in the batch are assigned this identifier, which is displayed in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] user interface in the **ID** column.|If the batch has not been processed, this field is blank.|  
|**BatchTag**<br /><br /> Required, except by web service|A unique name for the batch, up to 50 characters.||  
|**ErrorCode**|Displays an error code. For all records with a **ImportStatus_ID** of **2**, see [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md).||  
|**Code**<br /><br /> Required, except when codes are generated automatically for **ImportType1** or **2**; see [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md) for more information|A unique code for the member.||  
|**Name**<br /><br /> Optional|A name for the member.||  
|**NewCode**|Use only if you are changing the member code.||  
|\<Attribute name>|A column exists for each attribute in the entity. Use this with an **ImportType** of **0** or **2**. For free-form attributes, specify the new text or string value for the attribute. For domain-based attributes, specify the code for the member that will be the attribute. For link attributes, the URL must start with **https://**.<br /><br /> Note: You cannot stage file attributes.||  
  
## See Also  
 [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)   
 [View Errors that Occur During Staging &#40;Master Data Services&#41;](../master-data-services/view-errors-that-occur-during-staging-master-data-services.md)   
 [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md)  
  
  
