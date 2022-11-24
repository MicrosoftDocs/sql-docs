---
title: "Persisting Hierarchical Recordsets"
description: "Persisting Hierarchical Recordsets"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "hierarchical Recordsets [ADO], persisting"
  - "persisting hierarchical Recordsets [ADO]"
  - "data shaping [ADO], hierarchical Recordsets"
---
# Persisting Hierarchical Recordsets
You can save a hierarchical **Recordset** to a file in either ADTG or XML format by calling the [Save](../../../ado/reference/ado-api/save-method.md) method. However, two limitations apply when saving hierarchical **Recordset**s in XML format: You cannot save in XML if the hierarchical **Recordset** contains pending updates, and you cannot save a parameterized hierarchical **Recordset**.  
  
 For more information about the Data Shaping provider, see [Microsoft Data Shaping Service for OLE DB](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) (ADO) and [Overview of the Data Shaping Service for OLE DB](/previous-versions/windows/desktop/ms719615(v=vs.85)).  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)