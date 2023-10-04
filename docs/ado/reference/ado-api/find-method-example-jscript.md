---
title: "Find Method Example (JScript)"
description: "Find Method Example (JScript)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Find method [ADO], JScript example"
dev_langs:
  - "JScript"
---
# Find Method Example (JScript)
This example uses the [Recordset](./recordset-object-ado.md) object's [Find](./find-method-ado.md) method to locate and display the companies in the ***Northwind*** database whose name begins with the letter G. Cut and paste the following code to Notepad or another text editor, and save it as **FindJS.asp**.  
  
```  
<!-- BeginFindJS -->  
<%@  Language=JavaScript %>  
<%// use this meta tag instead of adojavas.inc%>  
<!--METADATA TYPE="typelib" uuid="00000205-0000-0010-8000-00AA006D2EA4" -->  
  
<html>  
  
<head>  
<title>ADO Recordset.Find Example</title>  
<style>  
<!--  
BODY {  
   font-family: 'Verdana','Arial','Helvetica',sans-serif;  
   BACKGROUND-COLOR:white;  
   COLOR:black;  
    }  
.thead {  
   background-color: #008080;   
   font-family: 'Verdana','Arial','Helvetica',sans-serif;   
   font-size: x-small;  
   color: white;  
   }  
.thead2 {  
   background-color: #800000;   
   font-family: 'Verdana','Arial','Helvetica',sans-serif;   
   font-size: x-small;  
   color: white;  
   }  
.tbody {   
   text-align: center;  
   background-color: #f7efde;  
   font-family: 'Verdana','Arial','Helvetica',sans-serif;   
   font-size: x-small;  
    }  
-->  
</style>  
</head>  
  
<body bgcolor="white">  
  
<h1>ADO Recordset.Find Example</h1>  
<%  
    // connection and recordset variables  
    var Cnxn = Server.CreateObject("ADODB.Connection");  
    var strCnxn = "Provider='sqloledb';Data Source=" + Request.ServerVariables("SERVER_NAME") + ";" +  
            "Initial Catalog='Northwind';Integrated Security='SSPI';";  
    var rsCustomers = Server.CreateObject("ADODB.Recordset");  
        // display string  
    var strMessage;      
    var strFind;  
  
    try  
    {  
            // open connection  
        Cnxn.Open(strCnxn);  
  
            //create recordset using object refs  
        SQLCustomers = "select * from Customers;";  
  
        rsCustomers.ActiveConnection = Cnxn;  
        rsCustomers.CursorLocation = adUseClient;  
        rsCustomers.CursorType = adOpenKeyset;  
        rsCustomers.LockType = adLockOptimistic;  
        rsCustomers.Source = SQLCustomers;  
  
        rsCustomers.Open();  
        rsCustomers.MoveFirst();  
  
            //find criteria  
        strFind = "CompanyName like 'g%'"  
        rsCustomers.Find(strFind);  
  
        if (rsCustomers.EOF) {  
            Response.Write("No records matched ");  
            Response.Write(SQLCustomers & "So cannot make table...");  
            Cnxn.Close();  
            Response.End();  
        }   
        else {  
            Response.Write('<table width="100%" border="2">');  
            Response.Write('<tr class="thead2">');  
            // Put Headings On The Table for each Field Name  
            for (thisField = 0; thisField < rsCustomers.Fields.Count; thisField++) {  
                fieldObject = rsCustomers.Fields(thisField);  
                Response.Write('<th width="' + Math.floor(100 / rsCustomers.Fields.Count) + '%">' + fieldObject.Name + "</th>");  
            }  
            Response.Write("</tr>");  
  
            while (!rsCustomers.EOF) {  
                Response.Write('<tr class="tbody">');  
                for(thisField=0; thisField<rsCustomers.Fields.Count; thisField++) {  
                    fieldObject = rsCustomers.Fields(thisField);  
                    strField = fieldObject.Value;  
                    if (strField == null)  
                        strField = "-Null-";  
                    if (strField == "")  
                        strField = "";  
                    Response.Write("<td>" + strField + "</td>");  
                }  
                rsCustomers.Find(strFind, 1, adSearchForward)  
                Response.Write("</tr>");  
            }  
            Response.Write("</table>");  
        }  
    }  
    catch (e)  
    {  
        Response.Write(e.message);  
    }  
    finally  
    {  
        // clean up  
        if (rsCustomers.State == adStateOpen)  
            rsCustomers.Close;  
        if (Cnxn.State == adStateOpen)  
            Cnxn.Close;  
        rsCustomers = null;  
        Cnxn = null;  
    }  
%>  
  
</body>  
  
</html>  
<!-- EndFindJS -->  
```  
  
## See Also  
 [Find Method (ADO)](./find-method-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)