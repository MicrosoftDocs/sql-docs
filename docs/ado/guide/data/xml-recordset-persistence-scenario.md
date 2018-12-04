---
title: "XML Recordset Persistence Scenario | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "XML persistence [ADO], persistence scenario"
ms.assetid: 353d569a-043a-4397-9ee6-564c4af8d5f6
author: MightyPen
ms.author: genemi
manager: craigg
---
# XML Recordset Persistence Scenario
In this scenario, you will create an Active Server Pages (ASP) application that saves the contents of a Recordset object directly to the ASP Response object.  
  
> [!NOTE]
>  This scenario requires that your server have Internet Information Server 5.0 (IIS) or later installed.  
  
 The returned Recordset is displayed in Internet Explorer using a [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md).  
  
 The following steps are necessary to create this scenario:  
  
-   Set Up the Application  
  
-   Get the Data  
  
-   Send the Data  
  
-   Receive and Display the Data  
  
## Step 1: Set Up the Application  
 Create an IIS virtual directory named "XMLPersist" with script permissions. Create two new text files in the folder to which the virtual directory points, one named "XMLResponse.asp," the other named "Default.htm."  
  
## Step 2: Get the Data  
 In this step, you will write the code to open an ADO Recordset and prepare to send it to the client. Open the file XMLResponse.asp with a text editor, such as Notepad, and insert the following code.  
  
```  
<%@ language="VBScript" %>  
  
<!-- #include file='adovbs.inc' -->  
  
<%  
  Dim strSQL, strCon  
  Dim adoRec   
  Dim adoCon   
  Dim xmlDoc   
  
  ' You will need to change "MySQLServer" below to the name of the SQL   
  ' server machine to which you want to connect.  
  strCon = "Provider=sqloledb;Data Source=MySQLServer;Initial Catalog=Pubs;Integrated Security=SSPI;"  
  Set adoCon = server.createObject("ADODB.Connection")  
  adoCon.Open strCon  
  
  strSQL = "SELECT Title, Price FROM Titles ORDER BY Price"  
  Set adoRec = Server.CreateObject("ADODB.Recordset")  
  adoRec.Open strSQL, adoCon, adOpenStatic, adLockOptimistic, adCmdText  
```  
  
 Be sure to change the value of the `Data Source` parameter in `strCon` to the name of your Microsoft SQL Server computer.  
  
 Keep the file open and go on to the next step.  
  
## Step 3: Send the Data  
 Now that you have a Recordset, you must send it to the client by saving it as XML to the ASP Response object. Add the following code to the bottom of XMLResponse.asp.  
  
```  
  Response.ContentType = "text/xml"  
  Response.Expires = 0  
  Response.Buffer = False  
  
  Response.Write "<?xml version='1.0'?>" & vbNewLine  
  adoRec.save Response, adPersistXML  
  adoRec.Close  
  Set adoRec=Nothing  
%>  
```  
  
 Notice that the ASP Response object is specified as the destination for the Recordset [Save Method](../../../ado/reference/ado-api/save-method.md). The destination of the Save method can be any object that supports the IStream interface, such as an ADO [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md), or a file name that includes the complete path to which the Recordset is to be saved.  
  
 Save and close XMLResponse.asp before going to the next step. Also copy the adovbs.inc file from the default ADO library installation folder to the same folder where you saved the XMLResponse.asp file.  
  
## Step 4: Receive and Display the Data  
 In this step you will create an HTML file with an embedded [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md) object that points at the XMLResponse.asp file to get the Recordset. Open default.htm with a text editor, such as Notepad, and add the following code. Replace "sqlserver" in the URL with the name of your server.  
  
```  
<HTML>  
<HEAD><TITLE>ADO Recordset Persistence Sample</TITLE></HEAD>  
<BODY>  
  
<TABLE DATASRC="#RDC1" border="1">  
  <TR>  
<TD><SPAN DATAFLD="title"></SPAN></TD>  
<TD><SPAN DATAFLD="price"></SPAN></TD>  
  </TR>  
</TABLE>  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID="RDC1">  
   <PARAM NAME="URL" VALUE="XMLResponse.asp">  
</OBJECT>  
  
</BODY>  
</HTML>  
```  
  
 Close the default.htm file and save it to the same folder where you saved XMLResponse.asp. Using Internet Explorer 4.0 or later, open the URL https://*sqlserver*/XMLPersist/default.htm and observe the results. The data is displayed in a bound DHTML table. Now open the URL https:// *sqlserver* /XMLPersist/XMLResponse.asp and observe the results. The XML is displayed.  
  
## See Also  
 [Save Method](../../../ado/reference/ado-api/save-method.md)   
 [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md)
