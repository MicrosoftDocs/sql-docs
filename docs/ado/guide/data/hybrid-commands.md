---
description: "Hybrid Commands"
title: "Hybrid Commands | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "shape commands [ADO]"
  - "hybrid commands [ADO]"
  - "data shaping [ADO], hybrid commands"
ms.assetid: e8ca40e8-459c-40e2-8dd3-3ec6d5ee7b51
author: rothja
ms.author: jroth
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