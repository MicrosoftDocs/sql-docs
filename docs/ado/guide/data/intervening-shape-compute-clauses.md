---
title: "Intervening Shape COMPUTE Clauses | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "shape commands [ADO]"
  - "COMPUTE clause [ADO]"
  - "data shaping [ADO], COMPUTE clause"
ms.assetid: a576bf81-8f3c-4ba1-817b-87e89a8da684
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)