---
title: Staging Process Errors
description: This article explains error codes added to all processed records during the staging process in Master Data Services.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "staging process [Master Data Services], error messages"
ms.assetid: 0d9be0dd-638f-4dd4-92b2-253fda655455
author: CordeliaGrey
ms.author: jiwang6
---
# Staging Process Errors (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  When the staging process is complete, all processed records in the staging tables have a value in the ErrorCode column. These values are listed in the following table.  
  
|Code|Error|Occurs When/Details|Applies to Table|  
|----------|-----------|--------------------------|----------------------|  
|210001|The same member code exists multiple times in the staging table.|Your staging batch includes the same member code multiple times. Neither member is created or updated.|Leaf<br /><br /> Consolidated<br /><br /> Relationship|  
|210003|The attribute values references a member that does not exist or is inactive.|When you stage domain-based attributes, you must use the code, rather than the name. Applies to **ImportType0**, **1**, and **2**.|Leaf<br /><br /> Consolidated|  
|210006|The member code is inactive.|**ImportType** = **1** and you specified a member code that doesn't exist.|Leaf<br /><br /> Consolidated<br /><br /> Relationship|  
|210032|The hierarchy name is missing or is not valid.|The explicit hierarchy was not found or the **HierarchyName** value was blank.|Consolidated<br /><br /> Relationship|  
|210035|Because a code generation business rule does not exist, the **MemberCode** is required.|When creating or updating members, a **MemberCode** is always required, unless you are using automatic code generation. For more information, see [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md).|Leaf<br /><br /> Consolidated|  
|210036|Because a code generation business rule exists, the **MemberCode** is not required.|When creating or updating members, a **MemberCode** is not required when you are using automatic code generation. However, you can specify a code if you choose. For more information, see [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md).|Leaf<br /><br /> Consolidated|  
|210041|"ROOT" is not a valid member code.|The **MemberCode** value contains the word "ROOT."|Leaf<br /><br /> Consolidated<br /><br /> Relationship|  
|210042|"MDMUNUSED" is not a valid member code.|The **MemberCode** value contains the word "MDMUNUSED."|Leaf<br /><br /> Consolidated<br /><br /> Relationship|  
|210052|The MemberCode cannot be deactivated because it is used as a domain-based attribute value.|When **ImportType** = **3** or **4**, staging fails if the member is used as an attribute value for other members. Either use **ImportType5** or **6** to set the value to NULL, or change the values before running the staging process.|Leaf<br /><br /> Consolidated|  
|300002|The member code is not valid.|Relationships: Either the parent or child member code does not exist.<br /><br /> Leaf or Consolidated: **ImportType** = **3** or **4** and the member code does not exist.|Leaf<br /><br /> Consolidated<br /><br /> Relationship|  
|300004|The member code already exists.|**ImportType** = **1** and you used a member code that already exists in the entity.|Leaf<br /><br /> Consolidated|  
|210011|When **RelationshipType** is **1**, the **ParentCode** cannot be a leaf member.|Ensure that the **ParentCode** value is a consolidated member code.|Relationship|  
|210015|The member code exists multiple times in the staging table for a hierarchy and a batch.|For an explicit hierarchy, you specified the location of the same member multiple times in the same batch.|Relationship|  
|210016|The relationship could not be created because it would cause a circular reference.|This occurs when you try to assign a child as a parent.|Relationship|  
|210046|The member cannot be a sibling of Root.|This occurs when **RelationshipType** = **2** (sibling) and either the **ParentCode** or **ChildCode** is **Root**. Members cannot be at the same level as the Root node; they can only be children.|Relationship|  
|210047|The member cannot be a sibling of Unused.|This occurs when **RelationshipType** = **2** (sibling) and either the **ParentCode** or **ChildCode** is **Unused**. Members can only be children of the Unused node.|Relationship|  
|210048|**ParentCode** and **ChildCode** cannot be the same.|The **ParentCode** value is the same as the **ChildCode** value. These values must be different.|Relationship|  
  
## See Also  
 [View Errors that Occur During Staging &#40;Master Data Services&#41;](../master-data-services/view-errors-that-occur-during-staging-master-data-services.md)   
 [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)  
  
  
