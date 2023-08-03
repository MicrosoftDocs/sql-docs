---
title: "Parameter (Visual C++ Syntax Index with #import)"
description: "Parameter (Visual C++ Syntax Index with #import)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Parameter collection [ADO], Visual C++ syntax index with #import"
dev_langs:
  - "C++"
apitype: "COM"
---
# Parameter (Visual C++ Syntax Index with #import)
## Methods  
  
```  
HRESULT AppendChunk( const _variant_t & Val );  
```  
  
## Properties  
  
```  
long GetAttributes( );  
void PutAttributes( long plParmAttribs );  
__declspec(property(get=GetAttributes,put=PutAttributes)) long  
    Attributes;  
  
enum ParameterDirectionEnum GetDirection( );  
void PutDirection( enum ParameterDirectionEnum plParmDirection );  
__declspec(property(get=GetDirection,put=PutDirection)) enum  
    ParameterDirectionEnum Direction;  
  
_bstr_t GetName( );  
void PutName( _bstr_t pbstr );  
__declspec(property(get=GetName,put=PutName)) _bstr_t Name;  
  
unsigned char GetNumericScale( );  
void PutNumericScale( unsigned char pbScale );  
__declspec(property(get=GetNumericScale,put=PutNumericScale)) unsigned  
    char NumericScale;  
  
unsigned char GetPrecision( );  
void PutPrecision( unsigned char pbPrecision );  
__declspec(property(get=GetPrecision,put=PutPrecision)) unsigned char  
    Precision;  
  
long GetSize( );  
void PutSize( long pl );  
__declspec(property(get=GetSize,put=PutSize)) long Size;  
  
enum DataTypeEnum GetType( );  
void PutType( enum DataTypeEnum psDataType );  
__declspec(property(get=GetType,put=PutType)) enum DataTypeEnum Type;  
  
_variant_t GetValue( );  
void PutValue( const _variant_t & pvar );  
__declspec(property(get=GetValue,put=PutValue)) _variant_t Value;  
```  
  
## See Also  
 [Parameter Object](./parameter-object.md)