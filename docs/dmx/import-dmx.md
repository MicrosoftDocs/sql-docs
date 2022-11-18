---
title: "IMPORT (DMX)"
description: "IMPORT (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# IMPORT (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Loads a mining model or mining structure from an Analysis Services Backup File (.abf) file onto the server.  
  
## Syntax  
  
```  
  
IMPORT FROM <filename>  
```  
  
## Arguments  
 *filename*  
 A string containing the name and location of the  file to import.  
  
## Remarks  
 If no objects are specified, the entire contents of the .dmb file will be loaded. If the .dmb file includes a database that does not exist on the server, the database will be created.  
  
 You must be a database or server administrator to export or import objects.  
  
## Import from File Example  
 The following example imports the entire contents of the file containing the association model and structure onto the current server.  
  
```  
IMPORT FROM 'C:\TEMP\Association_NEW.dmb'  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [EXPORT &#40;DMX&#41;](../dmx/export-dmx.md)   
 [Export and Import Data Mining Objects](/analysis-services/data-mining/export-and-import-data-mining-objects)  
  
