---
title: "Grandchild Aggregates | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "grandchild aggregates [ADO]"
  - "data shaping [ADO], grandchild aggregates"
ms.assetid: 4162d35f-2ce1-4218-80a5-b6933348837e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Grandchild Aggregates
The chapter column created in a clause of a shape command may be given a *chapter-alias name* (typically with the AS keyword). You can identify any column in any chapter of the shaped **Recordset** with a fully qualified name that identifies the child containing the column. For example, if the parent chapter, chap1, contains a child chapter, chap2, that has an amount column, amt, then the qualified name would be chap1.chap2.amt. The qualified name can then be used as an argument to one of the aggregate functions (SUM, AVG, MAX, MIN, COUNT, STDEV, or ANY).  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)
