---
title: "Collections (ADO - WFC Syntax)"
description: "Collections (ADO - WFC Syntax)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "syntax indexes [ADO], ADO/WFC"
  - "collections [ADO], ADO/WFC syntax"
  - "ADO/WFC syntax index [ADO]"
apitype: "COM"
---
# Collections (ADO - WFC Syntax)
**package com.ms.wfc.data**  
  
## Parameters  
  
### Methods  
  
```  
public void append(com.ms.wfc.data.Parameter param)  
public void delete(int n)  
public void delete(String s)  
public void refresh()  
public Parameter getItem(int n)  
public Parameter getItem(String s)  
```  
  
### Properties  
  
```  
public int getCount()  
```  
  
## Fields  
  
### Methods  
  
```  
public void append(String name, int type)  
public void append(String name, int type, int definedSize)  
public void append(String name, int type, int definedSize, int attrib)  
public void delete(int n)  
public void delete(String s)  
public void refresh()  
public com.ms.wfc.data.Field getItem(int n)  
public com.ms.wfc.data.Field getItem(String s)  
```  
  
### Properties  
  
```  
public int getCount()  
```  
  
## Errors  
  
### Methods  
  
```  
public void clear()  
public void refresh()  
public com.ms.wfc.data.Error getItem(int n)  
public com.ms.wfc.data.Error getItem(String s)  
```  
  
### Properties  
  
```  
public int getCount()  
```  
  
## See Also  
 [Errors Collection (ADO)](./errors-collection-ado.md)   
 [Fields Collection (ADO)](./fields-collection-ado.md)   
 [Parameters Collection (ADO)](./parameters-collection-ado.md)