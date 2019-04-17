---
title: "Level Properties - user hierarchies | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# User Hierarchies - Level Properties
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The following table lists and describes the properties of a level in a user-defined hierarchy.  
  
|Property|Description|  
|--------------|-----------------|  
|Description|Contains the description of the level.|  
|HideMemberIf|Indicates whether and when a member in a level should be hidden from client applications. This property can have the following values:<br /><br /> Never<br /> Members are never hidden. This is the default value.<br /><br /> OnlyChildWithNoName<br /> A member is hidden when the member is the only child of its parent and the member's name is empty.<br /><br /> OnlyChildWithParentName<br /> A member is hidden when the member is the only child of its parent and the member's name is identical to that of its parent.<br /><br /> NoName<br /> A member is hidden when the member's name is empty.<br /><br /> ParentName<br /> A member is hidden when the member's name is identical to that of its parent.|  
|ID|Contains the unique identifier (ID) of the level.|  
|Name|Contains the friendly name of the level. By default, the name of a level is the same as the name of the source attribute.|  
|SourceAttribute|Contains the name of the source attribute on which the level is based.|  
  
## See Also  
 [User Hierarchy Properties](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md)  
  
  
