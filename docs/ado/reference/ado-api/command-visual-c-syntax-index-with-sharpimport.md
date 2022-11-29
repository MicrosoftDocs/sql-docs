---
title: "Command (Visual C++ Syntax Index with #import)"
description: "Command (Visual C++ Syntax Index with #import)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Command collection [ADO], Visual C++ syntax index with #import"
dev_langs:
  - "C++"
apitype: "COM"
---
# Command (Visual C++ Syntax Index with #import)
## Methods  
  
```  
HRESULT Cancel( );  
_RecordsetPtr Execute( VARIANT * RecordsAffected, VARIANT * Parameters, long Options );  
_ParameterPtr CreateParameter( _bstr_t Name, enum DataTypeEnum Type, enum ParameterDirectionEnum Direction, long Size, const _variant_t & Value = vtMissing );  
```  
  
## Properties  
  
```  
_ConnectionPtr GetActiveConnection( );  
void PutRefActiveConnection( struct _Connection * ppvObject );  
void PutActiveConnection( const _variant_t & ppvObject ); __declspec(property(get=GetActiveConnection,put=PutRefActiveConnection)) _ConnectionPtr ActiveConnection;  
_bstr_t GetCommandText( );  
void PutCommandText( _bstr_t pbstr );  
__declspec(property(get=GetCommandText,put=PutCommandText)) _bstr_t  
    CommandText;  
long GetCommandTimeout( );  
void PutCommandTimeout( long pl );  
__declspec(property(get=GetCommandTimeout,put=PutCommandTimeout)) long CommandTimeout;  
void PutCommandType( enum CommandTypeEnum plCmdType );  
enum CommandTypeEnum GetCommandType( );  
__declspec(property(get=GetCommandType,put=PutCommandType)) enum CommandTypeEnum CommandType;  
VARIANT_BOOL GetPrepared( );  
void PutPrepared( VARIANT_BOOL pfPrepared );  
__declspec(property(get=GetPrepared,put=PutPrepared)) VARIANT_BOOL Prepared;  
ParametersPtr GetParameters( );  
__declspec(property(get=GetParameters)) ParametersPtr Parameters;  
_bstr_t GetName( );  
void PutName( _bstr_t pbstrName );  
__declspec(property(get=GetName,put=PutName)) _bstr_t Name;  
long GetState( );  
__declspec(property(get=GetState)) long State;  
```  
  
## See Also  
 [Command Object (ADO)](./command-object-ado.md)