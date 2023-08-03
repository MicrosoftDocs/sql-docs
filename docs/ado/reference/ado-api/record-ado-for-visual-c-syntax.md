---
title: "Record (ADO for Visual C++ Syntax)"
description: "Record (ADO for Visual C++ Syntax)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Record collection [ADO], ADO for Visual C++ syntax"
dev_langs:
  - "C++"
apitype: "COM"
---
# Record (ADO for Visual C++ Syntax)
## Methods  
  
```  
Cancel(void)  
Close(void)  
CopyRecord(BSTR Source, BSTR Destination, BSTR UserName, BSTR Password, CopyRecordOptionsEnum Options, VARIANT_BOOL Async, BSTR *pbstrNewURL)  
DeleteRecord(BSTR Source, VARIANT_BOOL Async)  
GetChildren(_ADORecordset **ppRSet)  
MoveRecord(BSTR Source, BSTR Destination, BSTR UserName, BSTR Password, MoveRecordOptionsEnum Options, VARIANT_BOOL Async, BSTR *pbstrNewURL)  
Open(VARIANT Source, VARIANT ActiveConnection, ConnectModeEnum Mode, RecordCreateOptionsEnum CreateOptions, RecordOpenOptionsEnum Options, BSTR UserName, BSTR Password)  
```  
  
## Properties  
  
```  
get_ActiveConnection(VARIANT *pvar)  
put_ActiveConnection(BSTR bstrConn)  
putref_ActiveConnection(_ADOConnection *Con)  
get_Fields(ADOFields **ppFlds)  
get_Mode(ConnectModeEnum *pMode)  
put_Mode(ConnectModeEnum Mode)  
get_ParentURL(BSTR *pbstrParentURL)  
get_RecordType(RecordTypeEnum *pType)  
get_Source(VARIANT *pvar)  
put_Source(BSTR Source)  
putref_Source(IDispatch *Source)  
get_State(ObjectStateEnum *pState)  
```  
  
## See Also  
 [Record Object (ADO)](./record-object-ado.md)