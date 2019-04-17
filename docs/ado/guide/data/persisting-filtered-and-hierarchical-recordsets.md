---
title: "Persisting Filtered and Hierarchical Recordsets | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "filtered Recordset persistence [ADO]"
  - "persisting data [ADO]"
  - "data updates [ADO], persisting data"
  - "data persistence [ADO]"
  - "updating data [ADO], persisting data"
ms.assetid: d01aeb4d-4e43-450b-b3f2-0c27eaaf9f86
author: MightyPen
ms.author: genemi
manager: craigg
---
# Persisting Filtered and Hierarchical Recordsets
If the [Filter](../../../ado/reference/ado-api/filter-property.md) property is in effect for the **Recordset**, only the rows accessible under the filter are saved. If the **Recordset** is hierarchical, the current child **Recordset** and its children are saved, including the parent **Recordset**. If the **Save** method of a child **Recordset** is called, the child and all its children are saved, but the parent is not. For more information about hierarchical **Recordsets**, see [Data Shaping](../../../ado/guide/data/data-shaping.md).  
  
> [!NOTE]
>  Some limitations apply when saving hierarchical **Recordsets** (data shapes) in XML format. For more information, see [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md).
