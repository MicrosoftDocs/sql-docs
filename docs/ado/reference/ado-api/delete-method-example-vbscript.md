---
title: "Delete Method Example (VBScript) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Delete method [ADO], VBScript example"
ms.assetid: 78935d6d-1c1a-4306-a83a-1763210c69f9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete Method Example (VBScript)
This example uses the [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) method to remove a specified record from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
 Use the following example in an Active Server Page (ASP). To view this fully functional example, you must either have the data source AdvWorks.mdb (installed with the SDK) located at C:\Program Files\Microsoft Platform SDK\Samples\DataAccess\Rds\RDSTest\advworks.mdb or edit the path in the example code to reflect the actual location of this file. This is a Microsoft Access database file.  
  
 Use **Find** to locate the file Adovbs.inc and place it in the directory you plan to use. Cut and paste the following code into Notepad or another text editor, and save it as **DeleteVBS.asp**. You can view the result in any client browser.  
  
 To exercise the example, try using the [AddNew](../../../ado/reference/ado-api/addnew-method-example-vbscript.md) example first to add some records. Then you can try to delete them. View the result in any client browser.  
  
```  
<!-- BeginDeleteVBS -->  
<%@ Language=VBScript %>  
<% ' use this meta tag instead of ADOVBS.inc%>  
<!-- METADATA TYPE="typelib" uuid="00000205-0000-0010-8000-00AA006D2EA4"  -->  
<HTML>  
<HEAD>  
<TITLE>ADO Delete Method</TITLE>  
</HEAD>  
<STYLE>  
<!--  
TH {  
   background-color: #008080;   
   font-family: 'Arial Narrow','Arial',sans-serif;   
   font-size: xx-small;  
   color: white;  
   }  
TD {   
   text-align: center;  
   background-color: #f7efde;  
   font-family: 'Arial Narrow','Arial',sans-serif;   
   font-size: xx-small;  
    }  
-->  
</STYLE>  
  
<BODY>   
<H3>ADO Delete Method</H3>  
  
<%  
    ' to integrate this code replace the DataSource value in the connection string  
  
    ' connection and recordset variables  
   Dim Cnxn, strCnxn  
   Dim rsCustomers, strSQLCustomers  
  
    ' create and open connection  
   Set Cnxn = Server.CreateObject("ADODB.Connection")   
   strCnxn="Provider='sqloledb';Data Source=" & _  
            Request.ServerVariables("SERVER_NAME") & ";" & _  
            "Integrated Security='SSPI';Initial Catalog='Northwind';"  
   Cnxn.Open  strCnxn  
    ' create and open recordset  
   Set rsCustomers = Server.CreateObject("ADODB.Recordset")  
   strSQLCustomers = "Customers"  
   rsCustomers.Open strSQLCustomers, Cnxn, adOpenKeyset, adLockOptimistic, adCmdTable  
  
   ' Move to designated record and delete it  
   If Not IsEmpty(Request.Form("WhichRecord")) Then  
      'Get value to move from Form Post method  
      Moves = Request.Form("WhichRecord")  
  
      rsCustomers.Move CInt(Moves)  
      If Not rsCustomers.EOF or rsCustomers.BOF Then  
          ' handle any db errors  
         On Error Resume Next  
         rsCustomers.Delete 1  
         If Cnxn.Errors.Count <> 0 Then  
            Response.Write "Cannot delete since there are related records in other tables."  
            Response.End  
         End If  
         rsCustomers.MoveFirst  
         On Error GoTo 0  
      Else  
         Response.Write "Not a Valid Record Number"  
         rsCustomers.MoveFirst  
      End If  
   End If  
%>  
  
<!-- BEGIN column header row for Customer Table-->  
<TABLE COLSPAN=8 CELLPADDING=5 BORDER=0>  
<TR>  
   <TH>Rec. #</TH>  
   <TH>Company Name</TH>  
   <TH>Contact Name</TH>  
   <TH>City</TH>  
</TR>  
  
   <%   
   ' Display ADO Data from Customer Table   
   ' Loop through Recordset adding one row to HTML Table each pass  
   Dim iCount  
   iCount = 0  
   Do Until rsCustomers.EOF %>  
   <TR>  
     <TD> <%= CStr(iCount) %>  
     <TD> <%= rsCustomers("CompanyName")%> </TD>  
     <TD> <%= rsCustomers("ContactName")%> </TD>  
     <TD> <%= rsCustomers("City")%> </TD>  
   </TR>  
   <%   
     iCount = iCount + 1  
     rsCustomers.MoveNext   
   Loop   
   %>  
</TABLE>  
  
<!-- Do Client side Input Data Validation Move to named record and Delete it -->  
<Center>  
<H4>Clicking Button Will Remove Designated Record</H4>  
<H5>There are <%=rsCustomers.RecordCount%> Records in this Set</H5>  
  
<Form Method=Post Action="Deletevbs.asp" Name=Form>  
   <Input Type=Text Name="WhichRecord" Size=3>   
   <Input Type=Button Name=cmdDelete Value="Delete Record">  
</Form>  
  
</BODY>  
  
<Script Language = "VBScript">  
Sub cmdDelete_OnClick  
   If IsNumeric(Document.Form.WhichRecord.Value) Then  
      Document.Form.WhichRecord.Value = CInt(Document.Form.WhichRecord.Value)  
      Dim Response  
      Response = MsgBox("Are You Sure About Deleting This Record?", vbYesNo,  "ADO-ASP Example")  
  
      If Response = vbYes Then  
         Document.Form.Submit  
      End If  
   Else  
      MsgBox "You Must Enter a Valid Record Number",,"ADO-ASP Example"  
   End If  
End Sub  
</Script>  
  
<%  
    ' clean up  
    If rsCustomers.State = adStateOpen then  
        rsCustomers.Close  
    End If  
    If Cnxn.State = adStateOpen then  
        Cnxn.Close  
    End If  
%>  
</HTML>  
<!-- EndDeleteVBS -->  
```  
  
## See Also  
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
