---
title: "Security Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Security Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.security"
  - "urn:schemas-microsoft-com:xml-analysis#Security"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Security"
helpviewer_keywords: 
  - "Security element"
ms.assetid: 0b601f69-d16d-4d10-9361-b8afaa6ed357
author: minewiskan
ms.author: owend
manager: craigg
---
# Security Element (XMLA)
  Specifies how to back up or restore security definitions, such as roles and permissions, during a [Backup](../xml-elements-commands/backup-element-xmla.md) or [Restore](../xml-elements-commands/restore-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Backup> <!-- or Restore -->  
   ...  
   <Security>...</Security>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*SkipMembership*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../xml-elements-commands/backup-element-xmla.md), [Restore](../xml-elements-commands/restore-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Security` element determines whether the security definitions, such as roles and permissions, defined on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database are backed up or restored during, respectively, a `Backup` or `Restore` command. This element also determines if the Windows user accounts and groups defined as members of the security definitions are included as part of the `Backup` or `Restore` command.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*SkipMembership*|Include security definitions, but exclude membership information, during `Backup` or `Restore` commands.|  
|*CopyAll*|Include security definitions and membership information during `Backup` or `Restore` commands.|  
|*IgnoreSecurity*|Exclude security definitions during `Backup` or `Restore` commands.|  
  
## See Also  
 [SynchronizeSecurity Element &#40;XMLA&#41;](security-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
