---
title: "Business Rule Conditions (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: d2e0a8c3-4c2e-407c-856e-68d95ebda9ed
author: leolimsft
ms.author: lle
manager: craigg
---
# Business Rule Conditions (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], business rule conditions determine the conditions that must be true for one or more actions to be taken.  
  
> [!NOTE]  
>  Conditions are optional. If you do not specify a condition, then actions are taken any time data is validated against business rules.  
  
## Business Rule Conditions  
  
|Condition Name|Description|  
|--------------------|-----------------|  
|**is equal to**|The selected attribute **is equal to** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, date, and link values.|  
|**is not equal to**|The selected attribute **is not equal to** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, date, and link values.|  
|**is greater than**|The selected attribute **is greater than** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, and date values.|  
|**is greater than or equal to**|The selected attribute **is greater than or equal to** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, and date values.|  
|**is less than**|The selected attribute **is less than** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, and date values.|  
|**is less than or equal to**|The selected attribute **is less than or equal to** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text, number, and date values.|  
|**starts with**|The selected attribute **starts with** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**does not start with**|The selected attribute **does not start with** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**ends with**|The selected attribute **ends with** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**does not end with**|The selected attribute **does not end with** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**contains**|The selected attribute **contains** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**does not contain**|The selected attribute **does not contain** a specific attribute, a specific attribute value, or is blank.<br /><br /> This condition is valid for text and link values.|  
|**contains the pattern**|The selected attribute **contains the pattern** of a specific attribute, a specific attribute value, or is blank. Use .NET Framework regular expressions to specify the pattern.<br /><br /> For more information about regular expressions, see [Regular Expression Language Elements](https://go.microsoft.com/fwlink/?LinkId=164401) in the MSDN Library.<br /><br /> This condition is valid for text and link values.|  
|**does not contain the pattern**|The selected attribute **does not contain the pattern** of a specific attribute, a specific attribute value, or is blank. Use .NET Framework regular expressions to specify the pattern.<br /><br /> For more information about regular expressions, see [Regular Expression Language Elements](https://go.microsoft.com/fwlink/?LinkId=164401) in the MSDN Library.<br /><br /> This condition is valid for text and link values.|  
|**contains the subset**|The selected attribute **contains the subset** of a specific attribute or a specific attribute value. You must specify the starting position for the search (for example, 1 means start searching at the first character).<br /><br /> This condition is valid for text and link values.|  
|**does not contain the subset**|The selected attribute **does not contain the subset** of a specific attribute or a specific attribute value. You must specify the starting position for the search (for example, 1 means start searching at the first character).<br /><br /> This condition is valid for text and link values.|  
|**has changed**|The selected attribute **has changed** since the last time business rules were applied to the member. You must specify the change group the attribute is a member of.<br /><br /> For more information on change tracking groups, see [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md).<br /><br /> This condition is valid for text, number, date, and link values.|  
|**has not changed**|The selected attribute **has not changed** since the last time business rules were applied to the member. You must specify the change group the attribute is a member of.<br /><br /> For more information on change tracking groups, see [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md).<br /><br /> This condition is valid for text, number, date, and link values.|  
|**is between**|The selected attribute **is between** two specific attribute values.<br /><br /> This condition is valid for text, number, and date values.|  
|**is not between**|The selected attribute **is not between** two specific attribute values.<br /><br /> This condition is valid for text, number, and date values.|  
  
> [!NOTE]  
>  When a business rule contains a condition that compares two values, and the rule is applied to a member for which both values are NULL, that member will fail validation.  
  
## See Also  
 [Business Rule Actions &#40;Master Data Services&#41;](../master-data-services/business-rule-actions-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md)  
  
  
