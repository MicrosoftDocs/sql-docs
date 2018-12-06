---
title: "SaveOptionsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "SaveOptionsEnum"
helpviewer_keywords: 
  - "SaveOptionsEnum enumeration [ADO]"
ms.assetid: 59339100-6e29-48d1-aea3-6873796d186b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SaveOptionsEnum
Specifies whether a file should be created or overwritten when saving from a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object. The values can be **adSaveCreateNotExist** or **adSaveCreateOverWrite**..  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSaveCreateNotExist**|1|Default. Creates a new file if the file specified by the *FileName* parameter does not already exist.|  
|**adSaveCreateOverWrite**|2|Overwrites the file with the data from the currently open **Stream** object, if the file specified by the *Filename* parameter already exists. If the file specified by the *Filename* parameter does not exist, a new file is created.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [SaveToFile Method](../../../ado/reference/ado-api/savetofile-method.md)
