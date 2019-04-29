---
title: "ADCPROP_AUTORECALC_ENUM | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADCPROP_AUTORECALC_ENUM"
helpviewer_keywords: 
  - "ADCPROP_AUTORECALC_ENUM [ADO]"
ms.assetid: ded4f087-87b9-4efa-8026-bde53d3e9e8a
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADCPROP_AUTORECALC_ENUM
Specifies when the [MSDataShape](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) provider re-calculates aggregate and calculated columns in a hierarchical Recordset.  
  
 These constants are only used with the **MSDataShape** provider and the **Recordset** "**Auto Recalc**" dynamic property, which is referenced in the [ADO Dynamic Property Index](../../../ado/reference/ado-api/ado-dynamic-property-index.md) and documented in the [Microsoft Cursor Service for OLE DB](../../../ado/guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) or [Microsoft Data Shaping Service for OLE DB](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) documentation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRecalcAlways**|1|Default. Recalculates whenever the **MSDataShape** provider determines values that the calculated columns depend upon have changed.|  
|**adRecalcUpFront**|0|Calculates only when initially building the hierarchical **Recordset**.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.
