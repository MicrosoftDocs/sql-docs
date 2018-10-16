---
title: "DataSpace (ADO - WFC Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "DataSpace collection [ADO], ADO/WFC syntax"
ms.assetid: 950d45d8-07de-467b-b255-f9a7b997204c
author: MightyPen
ms.author: genemi
manager: craigg
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
