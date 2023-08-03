---
title: "Command Streams"
description: "Command Streams"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "command streams [ADO]"
  - "streams [ADO], command"
---
# Command Streams
ADO has always supported command input in string format specified by the **CommandText** property. As an alternative, with ADO 2.7 or later, you can also use a stream of information for command input by assigning the stream to the **CommandStream** property. You can assign an ADO **Stream** object, or any object that supports the COM **IStream** interface.  
  
 The content of the command stream is simply passed from ADO to your provider, so your provider must support command input by stream for this feature to work. For example, SQL Server supports queries in the form of XML templates or OpenXML extensions to Transact-SQL.  
  
 Because the details of the stream must be interpreted by the provider, you must specify the command dialect by setting the **Dialect** property. The value of **Dialect** is a string containing a GUID, which is defined by your provider. For information about valid values for **Dialect** supported by your provider, see your provider documentation.  
  
## XML Template Query Example  
 The following example is written in VBScript to the Northwind database.  
  
 First, initialize and open the **Stream** object that will be used to contain the query stream:  
  
```  
Dim adoStreamQuery  
Set adoStreamQuery = Server.CreateObject("ADODB.Stream")  
adoStreamQuery.Open  
```  
  
 The content of the query stream will be an XML template query.  
  
 The template query requires a reference to the XML namespace identified by the sql: prefix of the \<sql:query> tag. A SQL SELECT statement is included as the content of the XML template and assigned to a string variable as follows:  
  
```  
sQuery = "<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'>  
<sql:query> SELECT * FROM PRODUCTS ORDER BY PRODUCTNAME </sql:query>  
</ROOT>"  
```  
  
 Next, write the string to the stream:  
  
```  
adoStreamQuery.WriteText sQuery, adWriteChar  
adoStreamQuery.Position = 0  
```  
  
 Assign adoStreamQuery to the **CommandStream** property of an ADO **Command** object:  
  
```  
Dim adoCmd  
Set adoCmd  = Server.CreateObject("ADODB.Command"")  
adoCmd.CommandStream = adoStreamQuery  
```  
  
 Specify the command language **Dialect**, which indicates how the SQL Server OLE DB Provider should interpret the command stream. The dialect specified by a provider-specific GUID:  
  
```  
adoCmd.Dialect = "{5D531CB2-E6Ed-11D2-B252-00C04F681B71}"  
```  
  
 Finally, execute the query and return the results to a **Recordset** object:  
  
```  
Set objRS = adoCmd.Execute  
```
