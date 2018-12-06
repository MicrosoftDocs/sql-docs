---
title: "Database Element for Workload (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Database element"
ms.assetid: 112fca2a-37e5-4162-b2e7-b56eb8ab0c6f
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Element for Workload (DTA)
  Specifies the database where the workload trace table is located.  
  
## Syntax  
  
```  
  
<Workload>  
  <Database>  
   ...code removed here...  
  </Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once if no other type of workload is specified. You must specify an `EventString`, a `File`, or a `Database` child element for the `Workload` parent, but only one type can be used. For example, if you specify a workload with the `Database` element, you cannot also specify a workload with the `File` element in the same XML input file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Workload Element &#40;DTA&#41;](workload-element-dta.md)|  
|**Child elements**|[Name Element for Database &#40;DTA&#41;](name-element-for-database-dta.md)<br /><br /> [Schema Element for Database &#40;DTA&#41;](schema-element-for-database-dta.md)|  
  
## Remarks  
 This element is of the **DatabaseDetailsTypecomplexType** name in the Database Engine Tuning Advisor XML schema. Do not confuse this `Database` element with the one whose root parent is the `Configuration` element. (See [Database Element for Configuration &#40;DTA&#41;](database-element-for-configuration-dta.md).)  
  
## Example  
 For a usage example of this `Database` element, see the code example in [Workload Element &#40;DTA&#41;](workload-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
