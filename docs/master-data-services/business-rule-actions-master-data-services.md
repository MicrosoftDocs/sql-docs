---
title: Business Rule Actions
description: In Master Data Services, business rules cause actions. Learn about default value actions, change value actions, validation actions, and external actions.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: cdc4daca-3dff-46d8-b7f0-57f7826dd61a
author: CordeliaGrey
ms.author: jiwang6
---
# Business Rule Actions (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], business rule actions are the consequence of business rule condition evaluations. If a condition is true, the action is initiated.  
  
> [!NOTE]  
>  For default value and change value actions, if the generated value exceeds the maximum length of the attribute, the generated value is truncated.  
  
## Default Value Actions  
 **Default value** actions set the default value of a specified attribute. Users with permission can change these default values.  
  
|Value Name|Description|  
|----------------|-----------------|  
|**defaults to**|The selected attribute **defaults to** a specific attribute, a specific attribute value, or is blank.<br /><br /> This action is valid for text, number, date, and link values.|  
|**defaults to a generated value**|The selected attribute **defaults to a generated value** that is determined by entering a starting and incremental value.<br /><br /> This action is valid for text and number values.|  
|**defaults to a concatenated value**|The selected attribute **defaults to a concatenated value** that is determined by specifying multiple attributes.<br /><br /> This action is valid for text and link values.|  
  
## Change Value Actions  
 **Change value** actions update the value of a specified attribute or attribute value. Users can change these values only if the new value causes the action to be true.  
  
|Value Name|Description|  
|----------------|-----------------|  
|**Equals**|The selected attribute is changed to a defined attribute value, another attribute, or blank.<br /><br /> This action is valid for text, number, date, and link values.|  
|**equals a concatenated value**|The selected attribute is changed to a concatenated value, which is determined by specifying multiple attributes.<br /><br /> This action is valid for text and link values.|  
  
## Validation Actions  
 **Validation** actions, when they do not evaluate to true, send email to a specified user or group. To commit a version, all validation actions must evaluate to true.  
  
 The only exceptions are the **is mandatory** and **is not valid** actions. These actions must be combined with a change value action, so that the data can successfully be validated and the version committed.  
  
|Validation Name|Description|  
|---------------------|-----------------|  
|**is required**|The selected attribute **is required**, which means it cannot be null or blank.<br /><br /> This action is valid for text, number, date, and link values.|  
|**is not valid**|The selected attribute **is not valid**.<br /><br /> This action is valid for text, number, date, and link values.|  
|**must contain the pattern**|The selected attribute **must contain the pattern** that is specified. Use .NET Framework regular expressions to specify the pattern.<br /><br /> For more information about regular expressions, see [Regular Expression Language Elements](/dotnet/standard/base-types/regular-expression-language-quick-reference) in the MSDN Library.<br /><br /> This action is valid for text and link values.|  
|**must be unique**|The selected attribute **must be unique** independently or in combination with defined attributes.<br /><br /> **Best Practice:** Combine this action with a mandatory condition to ensure validity of index fields in subscribing systems.<br /><br /> This action is valid for text, number, date, and link values.<br /><br /> **NOTE**: If the first attribute is of type DateTime, then it cannot be used in combination with an attribute of type Numeric or Text attribute. If the first attribute is of type Numeric, then it cannot be used in combination with an attribute of type DateTime.|  
|**must have one of the following values**|The selected attribute **must have one of the values** specified in a list.<br /><br /> This action is valid for text values.|  
|**must be greater than**|The selected attribute **must be greater than** a specific attribute, a specific attribute value, or blank.<br /><br /> This action is valid for text, number, and date values.|  
|**must be equal to**|The selected attribute **must be equal to** a defined attribute value, another attribute, or blank.<br /><br /> This action is valid for text, number, date, and link values.|  
|**must be greater than or equal to**|The selected attribute **must be greater than or equal to** a specific attribute, a specific attribute value, or blank.<br /><br /> This action is valid for text, number, and date values.|  
|**must be less than**|The selected attribute **must be less than** a specific attribute, a specific attribute value, or blank.<br /><br /> This action is valid for text, number, and date values.|  
|**must be less than or equal to**|The selected attribute **must be less than or equal to** a specific attribute, a specific attribute value, or blank.<br /><br /> This action is valid for text, number, and date values.|  
|**must be between**|The selected attribute **must be between** two specific attribute or attribute values.<br /><br /> This action is valid for text, number, and date values.|  
|**must have a minimum length of**|The selected attribute **must have a minimum length of** the specified value.<br /><br /> This action is valid for text and link values.|  
|**must have a maximum length of**|The selected attribute **must have a maximum length of** the specified value.<br /><br /> This action is valid for text and link values.|  
  
## External Action  
 **External** actions interact with applications outside of [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)].  
  
|Action Name|Description|  
|-----------------|-----------------|  
|**start workflow**|Initiates an external workflow. The data that caused this action to occur is passed to the workflow. For more information, see [SharePoint Workflow Integration with Master Data Services](/previous-versions/sql/sql-server-2008-r2/gg690195(v=msdn.10)).<br /><br /> This action is valid for text, number, date, and link values.|  
  
## See Also  
 [Business Rule Conditions &#40;Master Data Services&#41;](../master-data-services/business-rule-conditions-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md)  
  
