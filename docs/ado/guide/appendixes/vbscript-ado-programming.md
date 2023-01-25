---
title: "VBScript ADO Programming"
description: "VBScript ADO Programming"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, VBScript"
  - "VBScript [ADO]"
dev_langs:
  - "VB"
---
# VBScript ADO Programming
## Creating an ADO Project  
 Microsoft Visual Basic, Scripting Edition does not support type libraries, so you do not need to reference ADO in your project. Consequently, no associated features such as command line completion are supported. Also, by default, ADO enumerated constants are not defined in VBScript.  
  
 However, ADO provides you with two include files containing the following definitions to be used with VBScript:  
  
-   For server-side scripting use Adovbs.inc, which is installed in the c:\Program Files\Common Files\System\ado\ folder by default.  
  
-   For client-side scripting use Adcvbs.inc, which is installed in the c:\Program Files\Common Files\System\msdac\ folder by default.  
  
 You can either copy and paste constant definitions from these files into your ASP pages, or, if you are doing server-side scripting, copy Adovbs.inc file to a folder on your Web site and referencing it from your ASP page like this:  
  
```vb
<!--#include File="adovbs.inc"-->  
```  
  
## Creating ADO Objects in VBScript  
 You cannot use the **Dim** statement to assign objects to a specific type in VBScript. Also, VBScript does not support the **New** syntax used with the **Dim** statement in Visual Basic for Applications. You must instead use the **CreateObject** function call:  
  
```vb
Dim Rs1  
Set Rs1 = Server.CreateObject( "ADODB.Recordset" )  
```  
  
## VBScript Examples  
 The following code is a generic example of VBScript server-side programming in an Active Server Page (ASP) file:  
  
```vb
<%  @LANGUAGE="VBSCRIPT" %>  
<%  Option Explicit %>  
<!--#include File="adovbs.inc"-->  
<HTML>  
    <BODY BGCOLOR="White" topmargin="10" leftmargin="10">  
  
    <!-- Your ASP Code goes here -->  
<%  
Dim Source  
Dim Connect  
Dim Rs1  
  
Source = "SELECT * FROM Authors"  
Connect = "Provider=sqloledb;Data Source=srv;" & _  
    "Initial Catalog=Pubs;Integrated Security=SSPI;"  
  
Set Rs1 = Server.CreateObject( "ADODB.Recordset" )  
Rs1.Open Source, Connect, adOpenForwardOnly  
Response.Write("Success!")  
%>  
    </BODY>  
</HTML>  
```  
  
 More specific VBScript examples are included with the ADO documentation. For more information, see [ADO Code Examples in Microsoft Visual Basic Scripting Edition](../../reference/ado-api/ado-code-examples-vbscript.md).  
  
## Differences Between VBScript and Visual Basic  
 Using ADO with VBScript is similar to using ADO with Visual Basic in many ways, including how syntax is used. However, some significant differences exist:  
  
-   VBScript supports only the Variant data type, which can hold different types of data. You can store the data you need in a Variant data type, and the data will function appropriately due to casting performed by VBScript. It recognizes the type required by ADO, and converts the value in the Variant accordingly.  
  
-   You cannot use **on error goto \<label>** within VBScript.  
  
-   VBScript supports some of the built-in Visual Basic functions such as **Msgbox**, **Date**, and **IsNumeric**. However, because VBScript is a subset of Visual Basic, not all built-in functions are supported. For example, VBScript does not support the **Format** function and the file I/O functions.