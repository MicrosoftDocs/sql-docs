---
title: "Creating a Connection String | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/20/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [ADO]"
  - "connection strings [ADO]"
ms.assetid: 14eae122-2d1e-40c8-b88e-b7cb8dfbc93b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Creating a Connection String
A connection string consists of a list of argument/value pairs (that is, parameters), separated by semi-colons. For example:  
  
```syntax
"arg1=val1; arg2=val2; ... argN=valN;"  
```  
  
 All the parameters must be recognized by either ADO or the specified provider.  
  
 ADO recognizes the following five arguments in a connection string.  
  
|Argument|Description|  
|--------------|-----------------|  
|*Provider*|Specifies the name of a provider to use for the connection.|  
|*File Name*|Specifies the name of a provider-specific file (for example, a persisted data source object) containing preset connection information.|  
|*URL*|Specifies the connection string as an absolute URL identifying a resource, such as a file or directory.|  
|*Remote Provider*|Specifies the name of a provider to use when opening a client-side connection. (Remote Data Service only.)|  
|*Remote Server*|Specifies the path name of the server to use when opening a client-side connection. (Remote Data Service only.)|  
  
 Other arguments are passed to the provider named in the *Provider* argument, without any processing by ADO.  
  
 The HelloData application in [HelloData: A Simple ADO Application](../../../ado/guide/data/hellodata-a-simple-ado-application.md) used the following connection string:  
  
```vb
m_sConnStr = "Provider=SQLOLEDB;Data Source=MySqlServer;" & _  
             "Initial Catalog=Northwind;Integrated Security='SSPI';"  
```  
  
 In this connection string, ADO recognizes only the `"Provider=SQLOLEDB"` parameter, which specifies the Microsoft OLE DB Provider for SQL Server as the ADO data source. The rest of the argument/value pairs, `"Data Source=MySqlServer; Initial Catalog=Northwind;Integrated Security='SSPI';"`, are passed verbatim to this provider. The type and validity of such parameters are provider-specific. For information about valid parameters that can be passed in the connection string, consult the individual provider's documentation.  
  
 According to the OLE DB Provider for SQL Server documentation, you can substitute "Server" for the *Data Source* parameter and "Database" for the *Initial Catalog* parameter. Thus, the following connection string would produce results identical to the one above:  
  
```vb
m_sConnStr = "Provider=SQLOLEDB;Server=MySqlServer;" & _  
             "Database=Northwind;Integrated Security='SSPI';"  
```
