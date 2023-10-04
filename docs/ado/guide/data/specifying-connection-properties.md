---
title: "Specifying Connection Properties"
description: "Specifying Connection Properties"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "connection properties [ADO]"
  - "connections [ADO]"
---
# Specifying Connection Properties
You can supply much of the information specified by a [connection string](../../../ado/guide/data/creating-a-connection-string.md) by setting properties of the **Connection** object before opening the connection. For example, you could achieve the same effect as the connection string discussed in [Creating a Connection String](../../../ado/guide/data/creating-a-connection-string.md) by using the following code.  
  
```  
With objConn  
.Provider = "SQLOLEDB"  
.Properties("Data Source") = "MySqlServer"  
   .Properties("Integrated Security") = "SSPI"  
.Open  
.DefaultDatabase = "Northwind"  
End With  
```  
  
 DefaultDatabase is set only after you open the connection.  
  
> [!NOTE]
>  In ADO you must not use a password containing semicolons (";") unless the password is enclosed in single quotation marks.
