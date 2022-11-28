---
title: "ActiveCommand Property Example (JScript)"
description: "ActiveCommand Property Example (JScript)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ActiveCommand property [ADO], JScript example"
dev_langs:
  - "JScript"
---
# ActiveCommand Property Example (JScript)
This example demonstrates the [ActiveCommand](./activecommand-property-ado.md) property. Cut and paste the following code to Notepad or another text editor, and save it as **ActiveCommandJS.asp**.  
  
```  
<!-- BeginActiveCommandJS -->  
<%@LANGUAGE="JScript" %>  
<%// use this meta tag instead of adojavas.inc%>  
<!--METADATA TYPE="typelib" uuid="00000205-0000-0010-8000-00AA006D2EA4" -->  
  
<%  
    // user input  
    strName = new String(Request.Form("ContactName"))  
%>  
  
<html>  
  
<head>  
<title>ActiveCommand Property Example (JScript)</title>  
<style>  
<!--  
BODY {  
   font-family: 'Verdana','Arial','Helvetica',sans-serif;  
   BACKGROUND-COLOR:white;  
   COLOR:black;  
    }  
-->  
</style>  
</head>  
  
<body bgcolor="White">  
  
<h1>ActiveCommand Property Example (JScript)</h1>  
  
<%  
if (strName.length > 0)  
    {  
        // connection and recordset variables  
        var Cnxn = Server.CreateObject("ADODB.Connection")  
        var strCnxn = "Provider='sqloledb';Data Source=" + Request.ServerVariables("SERVER_NAME") + ";" +  
            "Initial Catalog='Northwind';Integrated Security='SSPI';";  
        var cmdContact = Server.CreateObject("ADODB.Command");  
        var rsContact = Server.CreateObject("ADODB.Recordset");  
        // display variables  
        var strMessage;  
  
        try  
        {  
            // open connection  
            Cnxn.Open(strCnxn);  
  
            // Open a recordset using a command object  
            cmdContact.CommandText = "SELECT ContactName FROM Customers WHERE City = ?";  
            cmdContact.ActiveConnection = Cnxn;  
            // create parameter and insert variable value  
            cmdContact.Parameters.Append(cmdContact.CreateParameter("ContactName", adChar, adParamInput, 30, strName));  
  
            rsContact = cmdContact.Execute();  
  
            while(!rsContact.EOF){  
                // start new line  
                strMessage = "<P>";  
  
                // get data  
                strMessage += rsContact("ContactName")  
  
                // end the line  
                strMessage += "</P>";  
  
                // show data  
                Response.Write(strMessage);  
  
                // get next record  
                rsContact.MoveNext;  
            }  
        }  
        catch (e)  
        {  
            Response.Write(e.message);  
        }  
        finally  
        {  
            // 'clean up  
            if (rsContact.State == adStateOpen)  
                rsContact.Close;  
            if (Cnxn.State == adStateOpen)  
                Cnxn.Close;  
            rsContact = null;  
            Cnxn = null;  
        }  
    }  
%>  
  
<hr>  
  
<form method="POST" action="ActiveCommandJS.asp">  
  <p align="left">Enter city of customer to find (e.g., Paris): <input type="text" name="ContactName" size="40" value=""></p>  
  <p align="left"><input type="submit" value="Submit" name="B1"><input type="reset" value="Reset" name="B2"></p>  
</form>  
</body>  
  
</html>  
<!-- EndActiveCommandJS -->  
```  
  
## See Also  
 [ActiveCommand Property (ADO)](./activecommand-property-ado.md)   
 [Command Object (ADO)](./command-object-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)