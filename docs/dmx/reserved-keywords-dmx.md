---
title: "Reserved Keywords (DMX)"
description: "Reserved Keywords (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Reserved Keywords (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASversion2005](../includes/ssasversion2005-md.md)] reserves certain keywords for its exclusive use. These keywords cannot be used anywhere in Data Mining Extensions (DMX) statements except in the positions that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] defines in the DMX language reference. These restricted DMX keywords include the following members:  
  
-   All data definition statements listed in the topic, [DMX Data Definition Statements](../dmx/dmx-statements-data-definition.md).  
  
-   All data mining query functions listed in the topic, [DMX Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md).  
  
-   All operators listed in the topic, [DMX Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md).  
  
-   Keywords that are defined in the Multidimensional Expressions (MDX) query language and are included as part of a DMX statement.  
  
-   Keywords that are defined in the OLE DB for Data Mining specification and are included as part of a DMX statement.  
  
 When you name objects in a database, we recommend that you use a naming convention that avoids using reserved keywords.  
  
 If your database does contain names that match reserved keywords, you must use delimited identifiers when you refer to those objects. For more information, see [Identifiers &#40;DMX&#41;](../dmx/identifiers-dmx.md).  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
