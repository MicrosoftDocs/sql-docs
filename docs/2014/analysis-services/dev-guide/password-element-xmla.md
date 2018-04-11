---
title: "Password Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Password Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Password"
  - "urn:schemas-microsoft-com:xml-analysis#Password"
  - "microsoft.xml.analysis.password"
helpviewer_keywords: 
  - "Password element"
ms.assetid: 8a0603bd-f6a1-4b86-84f1-c83d0b03951b
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Password Element (XMLA)
  Determines the password to be used by the parent [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md) or [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md) command for encrypting or decrypting a backup file.  
  
## Syntax  
  
```xml  
  
<Backup> <!-- or Restore -->  
   ...  
   <Password>...</Password>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For `Backup` commands, if the `Password` element is not included or contains an empty string, the backup file is not encrypted.  
  
 For `Restore` commands, if the `Password` element is not included or contains an empty string while attempting to restore an encrypted backup file, an error occurs.  
  
 If `Location` elements are included in either a `Backup` or a `Restore` command, the same `Password` element is used for both backup and remote backup files. For more information about remote backup files, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Location Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/location-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  