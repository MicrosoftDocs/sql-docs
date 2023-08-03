---
title: "ObjectProxy (ADO - WFC Syntax)"
description: "ObjectProxy (ADO - WFC Syntax)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ObjectProxy collection [ADO]"
apitype: "COM"
---
# ObjectProxy (ADO - WFC Syntax)
An **ObjectProxy** object represents a server, and is returned by the **createObject** method of the [DataSpace](../rds-api/dataspace-object-rds.md) object. The ObjectProxy class has one method, **call**, which can invoke a method on the server and return an object resulting from that invocation.  
  
 **package com.ms.wfc.data**  
  
## Methods  
  
### Call Method (ADO/WFC Syntax)  
 Invokes a method on the server represented by the ObjectProxy. Optionally, method arguments may be passed as an array of objects.  
  
#### Syntax  
  
```  
public Object ObjectProxy.( String method )  
public Object ObjectProxy.( String method, Object[] args)  
```  
  
#### Returns  
 Object  
 An object resulting from invoking the method.  
  
#### Parameters  
 *ObjectProxy*  
 An **ObjectProxy** object that represents the server.  
  
 *method*  
 A String, containing the name of the method to invoke on the server.  
  
 *args*  
 Optional. An array of objects that are arguments to the method on the server. Java data types are automatically converted to data types suitable for use on the server.