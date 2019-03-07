---
title: "Persisting Hierarchical Recordsets | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchical Recordsets [ADO], persisting"
  - "persisting hierarchical Recordsets [ADO]"
  - "data shaping [ADO], hierarchical Recordsets"
ms.assetid: 43798bb5-98a6-4ad6-9bf8-78154b3a1827
author: MightyPen
ms.author: genemi
manager: craigg
---
# Persisting Hierarchical Recordsets
You can save a hierarchical **Recordset** to a file in either ADTG or XML format by calling the [Save](../../../ado/reference/ado-api/save-method.md) method. However, two limitations apply when saving hierarchical **Recordset**s in XML format: You cannot save in XML if the hierarchical **Recordset** contains pending updates, and you cannot save a parameterized hierarchical **Recordset**.  
  
 For more information about the Data Shaping provider, see [Microsoft Data Shaping Service for OLE DB](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) (ADO) and [Overview of the Data Shaping Service for OLE DB](https://msdn.microsoft.com/9f51e471-8e85-448e-9fb8-b64bbf767bf3).  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)
