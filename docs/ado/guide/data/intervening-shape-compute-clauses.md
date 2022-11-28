---
title: "Intervening Shape COMPUTE Clauses"
description: "Intervening Shape COMPUTE Clauses"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "shape commands [ADO]"
  - "COMPUTE clause [ADO]"
  - "data shaping [ADO], COMPUTE clause"
---
# Intervening Shape COMPUTE Clauses
It is valid to embed one or more COMPUTE clauses between the parent and child in a parameterized shape command, as in the following example:  
  
```  
SHAPE {select au_lname, state from authors} APPEND   
   ((SHAPE   
      (SHAPE   
         {select * from authors where state = ?} rs   
      COMPUTE rs, ANY(rs.state) state, ANY(rs.au_lname) au_lname   
      BY au_id) rs2   
   COMPUTE rs2, ANY(rs2.state) BY au_lname)   
RELATE state TO PARAMETER 0)  
```  
  
## See Also  
 [Data Shaping Example](./data-shaping-example.md)   
 [Formal Shape Grammar](./formal-shape-grammar.md)   
 [Shape Commands in General](./shape-commands-in-general.md)