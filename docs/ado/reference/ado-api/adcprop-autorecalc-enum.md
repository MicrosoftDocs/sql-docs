---
title: "ADCPROP_AUTORECALC_ENUM"
description: "ADCPROP_AUTORECALC_ENUM"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADCPROP_AUTORECALC_ENUM"
helpviewer_keywords:
  - "ADCPROP_AUTORECALC_ENUM [ADO]"
apitype: "COM"
---
# ADCPROP_AUTORECALC_ENUM
Specifies when the [MSDataShape](../../guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) provider re-calculates aggregate and calculated columns in a hierarchical Recordset.  
  
 These constants are only used with the **MSDataShape** provider and the **Recordset** "**Auto Recalc**" dynamic property, which is referenced in the [ADO Dynamic Property Index](./ado-dynamic-property-index.md) and documented in the [Microsoft Cursor Service for OLE DB](../../guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) or [Microsoft Data Shaping Service for OLE DB](../../guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) documentation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRecalcAlways**|1|Default. Recalculates whenever the **MSDataShape** provider determines values that the calculated columns depend upon have changed.|  
|**adRecalcUpFront**|0|Calculates only when initially building the hierarchical **Recordset**.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.