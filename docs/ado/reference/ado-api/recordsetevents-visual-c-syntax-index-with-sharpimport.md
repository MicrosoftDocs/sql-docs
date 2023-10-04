---
title: "RecordsetEvents (Visual C++ Syntax Index with #import)"
description: "RecordsetEvents (Visual C++ Syntax Index with #import)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "RecordsetEvents collection [ADO]"
dev_langs:
  - "C++"
apitype: "COM"
---
# RecordsetEvents (Visual C++ Syntax Index with #import)
## Events  
  
```  
HRESULT WillChangeField( long cFields, const  
    _variant_t & Fields, enum     EventStatusEnum * adStatus, struct  
    _Recordset * pRecordset );  
  
HRESULT FieldChangeComplete( long cFields, const  
    _variant_t & Fields,     struct Error * pError, enum EventStatusEnum  
    * adStatus, struct     _Recordset * pRecordset );  
  
HRESULT WillChangeRecord( enum EventReasonEnum  
    adReason, long cRecords,     enum EventStatusEnum * adStatus, struct  
    _Recordset * pRecordset );  
  
HRESULT RecordChangeComplete( enum EventReasonEnum  
    adReason, long     cRecords, struct Error * pError, enum  
    EventStatusEnum * adStatus,     struct _Recordset * pRecordset );  
  
HRESULT WillChangeRecordset( enum EventReasonEnum  
    adReason, enum     EventStatusEnum * adStatus, struct _Recordset *  
    pRecordset );  
  
HRESULT RecordsetChangeComplete( enum  
    EventReasonEnum adReason, struct     Error * pError, enum  
    EventStatusEnum * adStatus, struct _Recordset *     pRecordset );  
  
HRESULT WillMove( enum EventReasonEnum adReason, enum  
    EventStatusEnum *     adStatus, struct _Recordset * pRecordset );  
  
HRESULT MoveComplete( enum EventReasonEnum adReason, struct  
    Error *     pError, enum EventStatusEnum * adStatus, struct  
    _Recordset * pRecordset );  
  
HRESULT EndOfRecordset( VARIANT_BOOL * fMoreData, enum  
    EventStatusEnum *     adStatus, struct _Recordset * pRecordset );  
  
HRESULT FetchProgress( long Progress, long MaxProgress,  
    enum     EventStatusEnum * adStatus, struct _Recordset * pRecordset );  
  
HRESULT FetchComplete( struct Error * pError, enum  
    EventStatusEnum *     adStatus, struct _Recordset * pRecordset );  
```
