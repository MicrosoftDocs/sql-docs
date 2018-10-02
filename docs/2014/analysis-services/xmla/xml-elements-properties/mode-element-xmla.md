---
title: "Mode Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Mode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Mode"
  - "microsoft.xml.analysis.mode"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Mode"
helpviewer_keywords: 
  - "Mode element"
ms.assetid: 43a54181-6494-48c3-b14b-376d8939fa9f
author: minewiskan
ms.author: owend
manager: craigg
---
# Mode Element (XMLA)
  Identifies the mode to be used by the parent [Lock](../xml-elements-commands/lock-element-xmla.md) element when creating a lock on a specified object.  
  
## Syntax  
  
```xml  
  
<Lock>  
   ...  
   <Mode>...</Mode>  
   ...  
</Lock>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Lock](../xml-elements-commands/lock-element-xmla.md), [Unlock](../xml-elements-commands/unlock-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The parent `Lock` element uses the `Mode` element to determine the type of lock to create on an object. The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*CommitShared*|A shared lock is established on the specified object. Other shared locks can be created for the same object.<br /><br /> A shared lock prevents transactions containing write operations, such as an [Execute](../xml-elements-methods-execute.md) method call running an [Alter](../xml-elements-commands/alter-element-xmla.md) command, on a specified object, from committing until the shared lock is removed. A shared lock does not prevent transactions containing read operations, such as a [Discover](../xml-elements-methods-discover.md) method call or an `Execute` method call running a [Statement](../xml-elements-commands/statement-element-xmla.md) command, from committing.|  
|*CommitExclusive*|An exclusive lock is established on the specified object. Other shared or exclusive locks cannot be created for the same object.<br /><br /> An exclusive lock prevents transactions containing either read or write operations on a specified object from committing until the exclusive lock is removed.|  
  
## See Also  
 [ID Element &#40;XMLA&#41;](id-element-xmla.md)   
 [Object Element &#40;XMLA&#41;](object-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
