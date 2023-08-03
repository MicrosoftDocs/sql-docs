---
title: "DataSpace (ADO - WFC Syntax)"
description: "DataSpace (ADO - WFC Syntax)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "DataSpace collection [ADO], ADO/WFC syntax"
apitype: "COM"
---
# DataSpace (ADO - WFC Syntax)
The **createObject** method of the **DataSpace** class specifies both a business object to process client application requests (*progid*) and the communications protocol and server (*connection*). **createObject** returns an [ObjectProxy](../../../ado/reference/ado-api/objectproxy-ado-wfc-syntax.md) object that represents the server.  
  
## package com.ms.wfc.data  
  
### Constructor  
  
```  
public DataSpace()  
```  
  
### Methods  
  
```  
public static ObjectProxy DataSpace.createObject(String  
    progid, String connection)  
```  
  
### Properties  
  
```  
public static int getInternetTimeout()  
public static void setInternetTimeout(int plInetTimeout)  
```  
  
## See Also  
 [DataSpace Object (RDS)](../../../ado/reference/rds-api/dataspace-object-rds.md)
