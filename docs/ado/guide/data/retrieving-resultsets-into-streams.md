---
title: "Retrieving Resultsets into Streams"
description: "Retrieving Resultsets into Streams"
author: rothja
ms.author: jroth
ms.date: "01/20/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "streams [ADO], retrieving query results"
  - "query results into stream [ADO]"
  - "retrieving results into stream [ADO]"
---
# Retrieving Resultsets into Streams
Instead of receiving results in the traditional **Recordset** object, ADO can instead retrieve query results into a stream. The ADO **Stream** object (or other objects that support the COM **IStream** interface, such as the ASP **Request** and **Response** objects) can be used to contain these results. One use for this feature is to retrieve results in XML format. With SQL Server, for example, XML results can be returned in multiple ways, such as using the FOR XML clause with a SQL SELECT query or using an XPath query.  
  
 To receive query results in stream format instead of in a **Recordset**, you must specify the **adExecuteStream** constant from **ExecuteOptionEnum** as a parameter of the **Execute** method of a **Command** object. If your provider supports this feature, the results will be returned in a stream upon execution. You might be required to specify additional provider-specific properties before the code executes. For example, with the Microsoft OLE DB Provider for SQL Server, properties such as **Output Stream** in the **Properties** collection of the **Command** object must be specified. For more information about SQL Server-specific dynamic properties related to this feature, see XML-Related Properties in the SQL Server Books Online.  
  
## FOR XML Query Example  
 The following example is written in VBScript to the Northwind database:  
  
```html
<!-- BeginRecordAndStreamVBS -->  
<%@ LANGUAGE = VBScript %>  
<%  Option Explicit      %>  
  
<HTML>  
<HEAD>  
<META NAME="GENERATOR" Content="Microsoft Developer Studio"/>  
<META HTTP-EQUIV="Content-Type" content="text/html"; charset="iso-8859-1">  
<TITLE>FOR XML Query Example</TITLE>  
  
<STYLE>  
   BODY  
   {  
      FONT-FAMILY: Tahoma;  
      FONT-SIZE: 8pt;  
      OVERFLOW: auto  
   }  
  
   H3  
   {  
      FONT-FAMILY: Tahoma;  
      FONT-SIZE: 8pt;  
      OVERFLOW: auto  
   }  
</STYLE>  
  
<!-- #include file="adovbs.inc" -->  
<%  
   Response.Write "<H3>Server-side processing</H3>"  
  
   Response.Write "Page Generated @ " & Now() & "<BR/>"  
  
   Dim adoConn  
   Set adoConn = Server.CreateObject("ADODB.Connection")  
  
   Dim sConn  
   sConn = "Provider=SQLOLEDB;Data Source=" & _  
      Request.ServerVariables("SERVER_NAME") & ";" & _  
      Initial Catalog=Northwind;Integrated Security=SSPI;"  
  
   Response.write "Connect String = " & sConn & "<BR/>"  
  
   adoConn.ConnectionString = sConn  
   adoConn.CursorLocation = adUseClient  
  
   adoConn.Open  
  
   Response.write "ADO Version = " & adoConn.Version & "<BR/>"  
   Response.write "adoConn.State = " & adoConn.State & "<BR/>"  
  
   Dim adoCmd  
   Set adoCmd = Server.CreateObject("ADODB.Command")  
   Set adoCmd.ActiveConnection = adoConn  
  
   Dim sQuery  
   sQuery = "<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'><sql:query>SELECT * FROM PRODUCTS WHERE ProductName='Gumbr Gummibrchen' FOR XML AUTO</sql:query></ROOT>"  
  
   Response.write "Query String = " & sQuery & "<BR/>"  
  
   Dim adoStreamQuery  
   Set adoStreamQuery = Server.CreateObject("ADODB.Stream")  
   adoStreamQuery.Open  
   adoStreamQuery.WriteText sQuery, adWriteChar  
   adoStreamQuery.Position = 0  
  
   adoCmd.CommandStream = adoStreamQuery  
   adoCmd.Dialect = "{5D531CB2-E6Ed-11D2-B252-00C04F681B71}"  
  
   Response.write "Pushing XML to client for processing "  & "<BR/>"  
  
   adoCmd.Properties("Output Stream") = Response  
   Response.write "<XML ID='MyDataIsle'>"  
   adoCmd.Execute , , 1024  
   Response.write "</XML>"  
  
%>  
  
<SCRIPT language="VBScript" For="window" Event="onload">  
   Dim xmlDoc  
   Set xmlDoc = MyDataIsle.XMLDocument  
   xmlDoc.resolveExternals=false  
   xmlDoc.async=false  
  
   If xmlDoc.parseError.Reason <> "" then  
      Msgbox "parseError.Reason = " & xmlDoc.parseError.Reason  
   End If  
  
   Dim root, child  
   Set root = xmlDoc.documentElement  
   For each child in root.childNodes  
      dim OutputXML  
      OutputXML = document.all("log").innerHTML  
      document.all("log").innerHTML = OutputXML & "<LI>" & child.getAttribute("ProductName") & "</LI>"  
   Next  
</SCRIPT>  
  
</HEAD>  
  
<BODY>  
  
   <H3>Client-side processing of XML Document MyDataIsle</H3>  
   <UL id=log>  
   </UL>  
  
</BODY>  
</HTML>  
<!-- EndRecordAndStreamVBS -->  
  
```  
  
 The FOR XML clause instructs SQL Server to return data in the form of an XML document.  
  
### FOR XML Syntax  
  
```syntax
FOR XML [RAW|AUTO|EXPLICIT]  
```  
  
 FOR XML RAW generates generic row elements that have column values as attributes. FOR XML AUTO uses heuristics to generate a hierarchical tree with element names based on table names. FOR XML EXPLICIT generates a universal table with relationships fully described by metadata.  
  
 An example SQL SELECT FOR XML statement follows:  
  
```sql
SELECT * FROM PRODUCTS ORDER BY PRODUCTNAME FOR XML AUTO  
```  
  
 The command can be specified in a string as shown earlier, assigned to **CommandText**, or in the form of an XML template query assigned to **CommandStream**. For more information about XML template queries, see [Command Streams](../../../ado/guide/data/command-streams.md) in ADO or Using Streams for Command Input in the SQL Server Books Online.  
  
 As an XML template query, the FOR XML query appears as follows:  
  
```xml
<sql:query> SELECT * FROM PRODUCTS ORDER BY PRODUCTNAME FOR XML AUTO </sql:query>  
```  
  
 This example specifies the ASP **Response** object for the **Output Stream** property:  
  
```vb
adoCmd.Properties("Output Stream") = Response  
```  
  
 Next, specify **adExecuteStream** parameter of **Execute**. This example wraps the stream in XML tags to create an XML data island:  
  
```vb
Response.write "<XML ID=MyDataIsle>"  
adoCmd.Execute , , adExecuteStream  
Response.write "</XML>"  
```  
  
### Remarks  
 At this point, XML has been streamed to the client browser and it is ready to be displayed. This is done by using client-side VBScript to bind the XML document to an instance of the DOM and looping through each child node to build a list of Products in HTML.
