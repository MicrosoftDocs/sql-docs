---
title: "Hybrid Commands"
description: "Hybrid Commands"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "shape commands [ADO]"
  - "hybrid commands [ADO]"
  - "data shaping [ADO], hybrid commands"
---
# Hybrid Commands
Hybrid commands are partially parameterized commands. For example:  
  
```  
SHAPE {select * from plants}   
   APPEND( {select * from customers where country = ?}   
           RELATE PlantCountry TO PARAMETER 0,   
             PlantRegion TO CustomerRegion )   
```  
  
 The caching behavior for a hybrid command is the same as that of regular parameterized commands.  
  
## See Also  
 [Data Shaping Example](./data-shaping-example.md)   
 [Formal Shape Grammar](./formal-shape-grammar.md)   
 [Shape Commands in General](./shape-commands-in-general.md)