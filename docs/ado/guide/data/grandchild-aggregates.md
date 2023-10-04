---
title: "Grandchild Aggregates"
description: "Grandchild Aggregates"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "grandchild aggregates [ADO]"
  - "data shaping [ADO], grandchild aggregates"
---
# Grandchild Aggregates
The chapter column created in a clause of a shape command may be given a *chapter-alias name* (typically with the AS keyword). You can identify any column in any chapter of the shaped **Recordset** with a fully qualified name that identifies the child containing the column. For example, if the parent chapter, chap1, contains a child chapter, chap2, that has an amount column, amt, then the qualified name would be chap1.chap2.amt. The qualified name can then be used as an argument to one of the aggregate functions (SUM, AVG, MAX, MIN, COUNT, STDEV, or ANY).  
  
## See Also  
 [Data Shaping Example](./data-shaping-example.md)