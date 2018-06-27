---
title: "DROP MINING STRUCTURE (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DROP MINING STRUCTURE (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

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
  
  
