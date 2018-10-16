---
title: "AllowOverwrite Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AllowOverwrite Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.allowoverwrite"
  - "urn:schemas-microsoft-com:xml-analysis#EndSession"
helpviewer_keywords: 
  - "AllowOverwrite element"
ms.assetid: e7e92481-5f29-47f2-9efd-4e5e60c002bb
author: minewiskan
ms.author: owend
manager: craigg
---
# AllowOverwrite Element (XMLA)
  Determines whether the parent [Backup](../xml-elements-commands/backup-element-xmla.md) or [Restore](../xml-elements-commands/restore-element-xmla.md) command attempts to overwrite the target file or database.  
  
## Syntax  
  
```xml  
  
<Backup> <!-- or Restore -->  
   ...  
   <AllowOverwrite>...</AllowOverwrite>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../xml-elements-commands/backup-element-xmla.md), [Restore](../xml-elements-commands/restore-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For `Backup` commands, the `AllowOverwrite` element determines whether the command can overwrite the backup file specified in the `File` element.  
  
 For `Restore` elements, the `AllowOverwrite` element determines whether the command can overwrite the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database specified in the `DatabaseName` element.  
  
## See Also  
 [DatabaseName Element &#40;XMLA&#41;](name-element-xmla.md)   
 [File Element &#40;XMLA&#41;](file-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
