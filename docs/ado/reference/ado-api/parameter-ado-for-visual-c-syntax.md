---
title: "Parameter (ADO for Visual C++ Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "Parameter collection [ADO], ADO for Visual C++ syntax"
ms.assetid: 74801dc1-cf0f-4a6e-960b-5990fe55e30d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Parameter (ADO for Visual C++ Syntax)
## Methods  
  
```  
AppendChunk(VARIANT Val)  
```  
  
## Properties  
  
```  
get_Attributes(LONG *plParmAttribs)  
put_Attributes(LONG lParmAttribs)  
get_Direction(ParameterDirectionEnum *plParmDirection)  
put_Direction(ParameterDirectionEnum lParmDirection)  
get_Name(BSTR *pbstr)  
put_Name(BSTR bstr)  
get_NumericScale(BYTE *pbScale)  
put_NumericScale(BYTE bScale)  
get_Precision(BYTE *pbPrecision)  
put_Precision(BYTE bPrecision)  
get_Size(long *pl)  
put_Size(long l)  
get_Type(DataTypeEnum *psDataType)  
put_Type(DataTypeEnum sDataType)  
get_Value(VARIANT *pvar)  
put_Value(VARIANT val)  
```  
  
## See Also  
 [Parameter Object](../../../ado/reference/ado-api/parameter-object.md)
