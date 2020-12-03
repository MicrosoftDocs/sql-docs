---
description: "Collections (ADO for Visual C++ Syntax)"
title: "Collections (ADO for Visual C++ Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
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
  
-   [Append Method (ADO)](./append-method-ado.md)  
  
-   [Delete Method (ADO Parameters Collection)](./delete-method-ado-parameters-collection.md)  
  
-   [Refresh Method (ADO)](./refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, _ADOParameter **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](./count-property-ado.md)  
  
-   [Item Property (ADO)](./item-property-ado.md)  
  
## Fields  
  
### Methods  
  
```  
Append(BSTR bstrName, DataTypeEnum Type, long DefinedSize, FieldAttributeEnum Attrib);  
Delete(VARIANT Index);  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Append Method (ADO)](./append-method-ado.md)  
  
-   [Delete Method (ADO Parameters Collection)](./delete-method-ado-parameters-collection.md)  
  
-   [Refresh Method (ADO)](./refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOField **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](./count-property-ado.md)  
  
-   [Item Property (ADO)](./item-property-ado.md)  
  
## Errors  
  
### Methods  
  
```  
Clear(void);  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Clear Method (ADO)](./clear-method-ado.md)  
  
-   [Refresh Method (ADO)](./refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOError **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](./count-property-ado.md)  
  
-   [Item Property (ADO)](./item-property-ado.md)  
  
## Properties  
  
### Methods  
  
```  
Refresh(void);  
```  
  
 For more information, see  
  
-   [Refresh Method (ADO)](./refresh-method-ado.md)  
  
### Properties  
  
```  
get_Count(long *c);  
get_Item(VARIANT Index, ADOProperty **ppvObject);  
```  
  
 For more information, see  
  
-   [Count Property (ADO)](./count-property-ado.md)  
  
-   [Item Property (ADO)](./item-property-ado.md)  
  
## See Also  
 [Errors Collection (ADO)](./errors-collection-ado.md)   
 [Fields Collection (ADO)](./fields-collection-ado.md)   
 [Parameters Collection (ADO)](./parameters-collection-ado.md)   
 [Properties Collection (ADO)](./properties-collection-ado.md)