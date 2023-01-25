---
title: "Issuing Commands to the Underlying Data Provider"
description: "Issuing Commands to the Underlying Data Provider"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "shape commands [ADO]"
  - "underlying providers [ADO]"
  - "data shaping [ADO], commands"
---
# Issuing Commands to the Underlying Data Provider
Any command that does not begin with SHAPE is passed through to the data provider. This is equivalent to issuing a shape command in the form "SHAPE {provider command}". These commands do *not* have to produce a **Recordset**. For instance, "SHAPE {DROP TABLE MyTable} is a perfectly valid shape command, assuming the data provider supports DROP TABLE.  
  
 This capability allows both normal provider commands and shape commands to share the same connection and transaction.  
  
## See Also  
 [Data Shaping Example](./data-shaping-example.md)   
 [Formal Shape Grammar](./formal-shape-grammar.md)   
 [Shape Commands in General](./shape-commands-in-general.md)