---
title: "DROP MINING MODEL (DMX) | Microsoft Docs"
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
  - "DROP_MINING_MODEL"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "DROP MINING MODEL statement"
  - "deleting mining models"
  - "removing mining models"
  - "dropping mining models"
  - "mining models [Analysis Services], deleting"
ms.assetid: 8ff561d0-a526-4712-9fff-11df9f8c45a1
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DROP MINING MODEL (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a mining model from the database.  
  
## Syntax  
  
```  
  
DROP MINING MODEL <model >  
```  
  
## Arguments  
 *model*  
 A model identifier.  
  
## Examples  
 The following sample code drops the mining model NBSample.  
  
```  
DROP MINING MODEL [NBSample]  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
