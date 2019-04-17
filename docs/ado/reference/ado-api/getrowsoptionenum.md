---
title: "GetRowsOptionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "GetRowsOptionEnum"
helpviewer_keywords: 
  - "GetRowsOptionEnum enumeration [ADO]"
ms.assetid: adc109b9-79f4-4946-a5eb-658e22e9a8a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetRowsOptionEnum
Specifies how many records to retrieve from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adGetRowsRest**|-1|Retrieves the rest of the records in the **Recordset**, from either the current position or a bookmark specified by the *Start* parameter of the [GetRows](../../../ado/reference/ado-api/getrows-method-ado.md) method.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.GetRowsOption.REST|  
  
## Applies To  
 [GetRows Method (ADO)](../../../ado/reference/ado-api/getrows-method-ado.md)
