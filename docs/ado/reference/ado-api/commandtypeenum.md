---
title: "CommandTypeEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "CommandTypeEnum"
helpviewer_keywords: 
  - "CommandTypeEnum enumeration [ADO]"
ms.assetid: 4b1feb9c-a855-40fe-a906-efe688687e9f
author: MightyPen
ms.author: genemi
manager: craigg
---
# CommandTypeEnum
Specifies how a command argument should be interpreted.  
  
 It is important to validate user-supplied *CommandString* values to avoid giving application users the opportunity to inject potentially dangerous commands for ADO to execute.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCmdUnspecified**|-1|Does not specify the command type argument.|  
|**adCmdText**|1|Evaluates [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) as a textual definition of a command or stored procedure call.|  
|**adCmdTable**|2|Evaluates **CommandText** as a table name whose columns are all returned by an internally generated SQL query.|  
|**adCmdStoredProc**|4|Evaluates **CommandText** as a stored procedure name.|  
|**adCmdUnknown**|8|Default. Indicates that the type of command in the **CommandText** property is not known.<br /><br /> When the type of command is not known, ADO will make several attempts to interpret the **CommandText**.<br /><br /> -   **CommandText** is interpreted as a textual definition of a command or stored procedure call. This is the same behavior as **adCmdText**.<br />-   **CommandText** is the name of a stored procedure. This is the same behavior as **adCmdStoredProc**.<br />-   **CommandText** is interpreted as the name of a table. All columns are returned by an internally generated SQL query. This is the same behavior as **adCmdTable**.|  
|**adCmdFile**|256|Evaluates **CommandText** as the file name of a persistently stored [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). Used with **Recordset.**[Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) or [Requery](../../../ado/reference/ado-api/requery-method.md) only.|  
|**adCmdTableDirect**|512|Evaluates **CommandText** as a table name whose columns are all returned. Used with **Recordset.Open** or **Requery** only. To use the [Seek](../../../ado/reference/ado-api/seek-method.md) method, the **Recordset** must be opened with **adCmdTableDirect**.<br /><br /> This value cannot be combined with the [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) value **adAsyncExecute**.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.CommandType.UNSPECIFIED|  
|AdoEnums.CommandType.TEXT|  
|AdoEnums.CommandType.TABLE|  
|AdoEnums.CommandType.STOREDPROC|  
|AdoEnums.CommandType.UNKNOWN|  
|AdoEnums.CommandType.FILE|  
|AdoEnums.CommandType.TABLEDIRECT|  
  
## Applies To  
  
|||  
|-|-|  
|[CommandType Property (ADO)](../../../ado/reference/ado-api/commandtype-property-ado.md)|[Execute Method (ADO Command)](../../../ado/reference/ado-api/execute-method-ado-command.md)|  
|[Execute Method (ADO Connection)](../../../ado/reference/ado-api/execute-method-ado-connection.md)|[Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)|  
|[Requery Method](../../../ado/reference/ado-api/requery-method.md)||
