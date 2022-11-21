---
title: "EXPORT (DMX)"
description: "EXPORT (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# EXPORT (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Extracts a mining model or mining structure object from the server to an Analysis Services Backup File (.abf).  
  
## Syntax  
  
```  
  
EXPORT <object type> <object name>[, <object name>] [<object type> <object name>[, <object name] ] TO <filename> [WITH DEPENDENCIES]  
```  
  
## Arguments  
 *object type*  
 Optional.The type of the object to export (either mining model or mining structure).  
  
 *object name*  
 Optional. The name of the object to export.  
  
 *filename*  
 The name and location of the file to export as a string.  
  
## Remarks  
 If the statement specifies a mining model, the resultant file will also contain an associated mining structure. If the statement specifies **WITH DEPENDENCIES**, all objects required to process the object (for example, the data source and data source view) are included in the .abf file.  
  
 You must be a database or server administrator to export or import objects from a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
## Export Mining Structure Example  
 The following example exports the Targeted Mailing and Forecasting mining structures, and the Association mining model to a specific file location. Because the Association model is part of the Market Basket mining structure, the example also exports the Market Basket structure. Any other mining models that may exist as part of the Market Basket mining structure will not be exported because the Association model was exported using **MINING MODEL**, not **MINING STRUCTURE**.  
  
```  
EXPORT MINING STRUCTURE [Targeted Mailing], [Forecasting] MINING MODEL Association TO 'C:\TM_NEW.abf'  
```  
  
## Export Mining Model Example  
 The following example exports the Association mining model to a specified file location. Because the statement specifies **WITH DEPENDENCIES**, the data source and data source view objects are also included in the .abf file.  
  
```  
EXPORT MINING MODEL [Association] TO 'C:\Association_NEW.abf' WITH DEPENDENCIES  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [IMPORT &#40;DMX&#41;](../dmx/import-dmx.md)   
 [Export and Import Data Mining Objects](/analysis-services/data-mining/export-and-import-data-mining-objects)  
  
