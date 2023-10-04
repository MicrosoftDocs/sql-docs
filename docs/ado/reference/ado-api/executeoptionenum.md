---
title: "ExecuteOptionEnum"
description: "ExecuteOptionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ExecuteOptionEnum"
helpviewer_keywords:
  - "ExecuteOptionEnum enumeration [ADO]"
apitype: "COM"
---
# ExecuteOptionEnum
Specifies how a provider should execute a command.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAsyncExecute**|0x10|Indicates that the command should execute asynchronously.<br /><br /> This value cannot be combined with the [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) value **adCmdTableDirect**.|  
|**adAsyncFetch**|0x20|Indicates that the remaining rows after the initial quantity specified in the [CacheSize](../../../ado/reference/ado-api/cachesize-property-ado.md) property should be retrieved asynchronously.|  
|**adAsyncFetchNonBlocking**|0x40|Indicates that the main thread never blocks while retrieving. If the requested row has not been retrieved, the current row automatically moves to the end of the file.<br /><br /> If you open a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) from a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) containing a persistently stored **Recordset**, **adAsyncFetchNonBlocking** will not have an effect; the operation will be synchronous and blocking.<br /><br /> **adAsynchFetchNonBlocking** has no effect when the [adCmdTableDirect](../../../ado/reference/ado-api/commandtypeenum.md) option is used to open the **Recordset**.|  
|**adExecuteNoRecords**|0x80|Indicates that the command text is a command or stored procedure that does not return rows (for example, a command that only inserts data). If any rows are retrieved, they are discarded and not returned.<br /><br /> **adExecuteNoRecords** can only be passed as an optional parameter to the **Command** or **Connection Execute** method.|  
|**adExecuteStream**|0x400|Indicates that the results of a command execution should be returned as a stream.<br /><br /> **adExecuteStream** can only be passed as an optional parameter to the **Command Execute** method.|  
|**adExecuteRecord**|0x800|Indicates that the **CommandText** is a command or stored procedure that returns a single row which should be returned as a **Record** object.|  
|**adOptionUnspecified**|-1|Indicates that the command is unspecified.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.ExecuteOption.ASYNCEXECUTE|  
|AdoEnums.ExecuteOption.ASYNCFETCH|  
|AdoEnums.ExecuteOption.ASYNCFETCHNONBLOCKING|  
|AdoEnums.ExecuteOption.NORECORDS|  
|AdoEnums.ExecuteOption.UNSPECIFIED|  
  
## Applies To  

:::row:::
    :::column:::
        [Execute Method (ADO Command)](../../../ado/reference/ado-api/execute-method-ado-command.md)  
        [Execute Method (ADO Connection)](../../../ado/reference/ado-api/execute-method-ado-connection.md)  
    :::column-end:::
    :::column:::
        [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)  
        [Requery Method](../../../ado/reference/ado-api/requery-method.md)  
    :::column-end:::
:::row-end:::
