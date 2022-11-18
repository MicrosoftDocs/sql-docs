---
description: "SET UNIQUE Command"
title: "SET UNIQUE Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SET UNIQUE command [ODBC]"
ms.assetid: 1f69e31e-4599-47cc-ac89-b86fba8703c5
author: David-Engel
ms.author: v-davidengel
---
# SET UNIQUE Command
Specifies whether records with duplicate index key values are maintained in an index file.  
  
## Syntax  
  
```  
  
SET UNIQUE ON | OFF  
```  
  
## Arguments  
 ON  
 Specifies that any record with a duplicate index key value not be included in the index file. Only the first record with the original index key value is included in the index file.  
  
 OFF  
 (Default.) Specifies that records with duplicate index key values be included in the index file.  
  
## Remarks  
 An index file retains its SET UNIQUE setting when you issue REINDEX. For more information, see [INDEX](../../odbc/microsoft/index-command.md).
