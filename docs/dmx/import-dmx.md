---
title: "IMPORT (DMX) | Microsoft Docs"
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
  - "IMPORT"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "IMPORT statement"
  - "mining structures [DMX], importing"
ms.assetid: c053bc88-2daf-4ebb-81d7-5a330250536d
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IMPORT (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
 [Export and Import Data Mining Objects](../analysis-services/data-mining/export-and-import-data-mining-objects.md)  
  
  
