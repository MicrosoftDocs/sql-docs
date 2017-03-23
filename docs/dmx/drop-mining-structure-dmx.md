---
title: "DROP MINING STRUCTURE (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DROP MINING STRUCTURE"
  - "DROP_MINING_STRUCTURE"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "removing mining structures"
  - "dropping mining structures"
  - "DROP MINING STRUCTURE statement"
  - "deleting mining structures"
  - "mining structures [DMX], deleting"
ms.assetid: 30df8c36-3a15-4d8c-98f3-0f8917be9fc8
caps.latest.revision: 15
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DROP MINING STRUCTURE (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops the specified mining structure from the database. All the mining models that are associated with the structure are also dropped from the database.  
  
## Syntax  
  
```  
  
DROP MINING STRUCTURE <structure>  
```  
  
## Arguments  
 *structure*  
 A structure identifier.  
  
## Examples  
 The following example drops the New Mailing mining structure from the database.  
  
```  
DROP MINING STRUCTURE [New Mailing]  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
