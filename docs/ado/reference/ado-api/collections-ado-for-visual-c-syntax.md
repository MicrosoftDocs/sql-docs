---
title: "Collections (ADO for Visual C++ Syntax) | Microsoft Docs"
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
  - "ADO for Visual C++ syntax [ADO]"
  - "syntax indexes [ADO], ADO for Visual C++ syntax"
  - "collections [ADO], ADO for Visual C++ syntax"
ms.assetid: 6a0109a0-f2d9-4f7c-8e1e-42763f9acaea
author: MightyPen
ms.author: genemi
manager: craigg
---
# Collections (ADO for Visual C++ Syntax)
## Parameters  
  
### Methods  
  
```  
Append(IDispatch *Object);  
Delete(VARIANT Index);  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Append Method (ADO)](../../../ado/reference/ado-api/append-method-ado.md)  
  
-   [Delete Method (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)  
  
-   [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, _ADOParameter **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)  
  
-   [Item Property (ADO)](../../../ado/reference/ado-api/item-property-ado.md)  
  
## Fields  
  
### Methods  
  
```  
Append(BSTR bstrName, DataTypeEnum Type, long DefinedSize, FieldAttributeEnum Attrib);  
Delete(VARIANT Index);  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Append Method (ADO)](../../../ado/reference/ado-api/append-method-ado.md)  
  
-   [Delete Method (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)  
  
-   [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOField **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)  
  
-   [Item Property (ADO)](../../../ado/reference/ado-api/item-property-ado.md)  
  
## Errors  
  
### Methods  
  
```  
Clear(void);  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Clear Method (ADO)](../../../ado/reference/ado-api/clear-method-ado.md)  
  
-   [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOError **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)  
  
-   [Item Property (ADO)](../../../ado/reference/ado-api/item-property-ado.md)  
  
## Properties  
  
### Methods  
  
```  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOProperty **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)  
  
-   [Item Property (ADO)](../../../ado/reference/ado-api/item-property-ado.md)  
  
## See Also  
 [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)   
 [Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)   
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
