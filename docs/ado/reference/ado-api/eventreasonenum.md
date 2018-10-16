---
title: "EventReasonEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "EventReasonEnum"
helpviewer_keywords: 
  - "EventReasonEnum enumeration [ADO]"
ms.assetid: 7d4a5496-ec2d-4936-b36a-7049a82be4b4
author: MightyPen
ms.author: genemi
manager: craigg
---
# EventReasonEnum
Specifies the reason that caused an event to occur.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adRsnAddNew**|1|An operation added a new record.|  
|**adRsnClose**|9|An operation closed the **Recordset**.|  
|**adRsnDelete**|2|An operation deleted a record.|  
|**adRsnFirstChange**|11|An operation made the first change to a record.|  
|**adRsnMove**|10|An operation moved the record pointer within the **Recordset**.|  
|**adRsnMoveFirst**|12|An operation moved the record pointer to the first record in the **Recordset**.|  
|**adRsnMoveLast**|15|An operation moved the record pointer to the last record in the **Recordset**.|  
|**adRsnMoveNext**|13|An operation moved the record pointer to the next record in the **Recordset**.|  
|**adRsnMovePrevious**|14|An operation moved the record pointer to the previous record in the **Recordset**.|  
|**adRsnRequery**|7|An operation requeried the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).|  
|**adRsnResynch**|8|An operation resynchronized the **Recordset** with the database.|  
|**adRsnUndoAddNew**|5|An operation reversed the addition of a new record.|  
|**adRsnUndoDelete**|6|An operation reversed the deletion of a record.|  
|**adRsnUndoUpdate**|4|An operation reversed the update of a record.|  
|**adRsnUpdate**|3|An operation updated an existing record.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.EventReason.ADDNEW|  
|AdoEnums.EventReason.CLOSE|  
|AdoEnums.EventReason.DELETE|  
|AdoEnums.EventReason.FIRSTCHANGE|  
|AdoEnums.EventReason.MOVE|  
|AdoEnums.EventReason.MOVEFIRST|  
|AdoEnums.EventReason.MOVELAST|  
|AdoEnums.EventReason.MOVENEXT|  
|AdoEnums.EventReason.MOVEPREVIOUS|  
|AdoEnums.EventReason.REQUERY|  
|AdoEnums.EventReason.RESYNCH|  
|AdoEnums.EventReason.UNDOADDNEW|  
|AdoEnums.EventReason.UNDODELETE|  
|AdoEnums.EventReason.UNDOUPDATE|  
|AdoEnums.EventReason.UPDATE|  
  
## Applies To  
  
|||  
|-|-|  
|[WillChangeRecord and RecordChangeComplete Events (ADO)](../../../ado/reference/ado-api/willchangerecord-and-recordchangecomplete-events-ado.md)|[WillChangeRecordset and RecordsetChangeComplete Events (ADO)](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md)|  
|[WillMove and MoveComplete Events (ADO)](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md)||
