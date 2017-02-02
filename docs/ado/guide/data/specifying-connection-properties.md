---
title: "Specifying Connection Properties | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connection properties [ADO]"
  - "connections [ADO]"
ms.assetid: 49456201-b085-4851-9686-e814136b07be
caps.latest.revision: 15
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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