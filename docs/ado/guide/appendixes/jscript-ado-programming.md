---
title: "JScript ADO Programming"
description: "JScript ADO Programming"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "JScript programming in ADO"
  - "ADO, JScript programming"
dev_langs:
  - "JScript"
---
# JScript ADO Programming
## Creating an ADO Project  
 Microsoft JScript does not support type libraries, so you do not need to reference ADO in your project. Consequently, no associated features such as command line completion are supported. Also, by default, ADO enumerated constants are not defined in JScript.  
  
 However, ADO provides you with two include files containing the following definitions to be used with JScript:  
  
-   For server-side scripting use Adojavas.inc, which is installed in the ADO library folders.  
  
-   For client-side scripting use Adcjavas.inc, which is installed in the ADO library folders.  
  
 You can either copy and paste constant definitions from these files into your ASP pages, or, if you are doing server-side scripting, copy Adojavas.inc file to a folder on your Web site and references it from your ASP page like this:  
  
```javascript
<!--#include File="adojavas.inc"-->  
```  
  
## Creating ADO Objects in JScript  
 You must instead use the **CreateObject** function call:  
  
```javascript
var Rs1;  
Rs1 = Server.CreateObject("ADODB.Recordset");  
```  
  
## JScript Example  
 The following code is a generic example of JScript server-side programming in an Active Server Page (ASP) file that opens a **Recordset** object:  
  
```javascript
<%  @LANGUAGE="JScript" %>  
<!--#include File="adojavas.inc"-->  
<HTML>  
<BODY BGCOLOR="White" topmargin="10" leftmargin="10">  
<%  
var Source = "SELECT * FROM Authors";  
var Connect =  "Provider=sqloledb;Data Source=srv;" +  
    "Initial Catalog=Pubs;Integrated Security=SSPI;"  
var Rs1 = Server.CreateObject( "ADODB.Recordset.2.5" );  
Rs1.Open(Source,Connect,adOpenForwardOnly);  
Response.Write("Success!");  
%>  
</BODY>  
</HTML>  
```
