---
title: "Recordset and SourceRecordset Properties Example (VBScript)"
description: "Recordset and SourceRecordset Properties Example (VBScript)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Source property [ADO], VBScript example"
  - "Recordset property [ADO], VBScript example"
dev_langs:
  - "VB"
---
# Recordset and SourceRecordset Properties Example (VBScript)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 The following example shows how to set the necessary parameters of the [RDSServer.DataFactory](./datafactory-object-rdsserver.md) default business object at run time.  
  
 To test this example, cut and paste this code between the \<Body> and \</Body> tags in a normal HTML document and name it **RecordsetVBS.asp**. ASP script will identify your server.  
  
```  
<!-- BeginRecordSetVBS -->  
<%@ Language=VBScript %>  
<html>  
<head>  
    <meta name="VI60_DefaultClientScript"  content=VBScript>  
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">  
    <title>Recordset and SourceRecordset Properties Example (VBScript)</title>  
<style>  
<!--  
body {  
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
  
<body>  
<h1>Recordset and SourceRecordset Properties Example (VBScript)</h1>  
  
<Center>  
<H2>RDS API Code Examples</H2>  
<HR>  
<H3>Using SourceRecordset and Recordset with RDSServer.DataFactory</H3>  
<!-- RDS.DataSpace ID RDS1 -->  
<OBJECT ID="RDS1" WIDTH=1 HEIGHT=1   
CLASSID="CLSID:BD96C556-65A3-11D0-983A-00C04FC29E36">  
</OBJECT>  
  
<!-- RDS.DataControl with parameters set at Run Time -->  
<OBJECT classid="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33"  
   ID=RDC WIDTH=1 HEIGHT=1>  
</OBJECT>  
  
<TABLE DATASRC=#RDC>  
   <TR>  
      <TD> <INPUT DATAFLD="FirstName" SIZE=15> </TD>  
      <TD> <INPUT DATAFLD="LastName" SIZE=15></TD>  
   </TR>  
</TABLE>  
<HR>  
<Input Size=70 Name="txtServer" Value="https://<%=Request.ServerVariables("SERVER_NAME")%>"><BR>  
<Input Size=70 Name="txtConnect" Value="Provider='sqloledb';Integrated Security='SSPI';Initial Catalog='Northwind'"><BR>  
<Input Size=70 Name="txtSQL" Value="SELECT FirstName, LastName FROM Employees">  
<HR>  
<INPUT TYPE=BUTTON NAME="Run" VALUE="Run"><BR>  
  
</Center>  
<Script Language="VBScript">  
  
   Dim rdsDF  
   Dim strServer  
   strServer = "https://<%=Request.ServerVariables("SERVER_NAME")%>"  
  
   Sub Run_OnClick()  
  
      Dim rs           
      ' Create RDSServer.DataFactory Object  
      Set rdsDF = RDS1.CreateObject("RDSServer.DataFactory", strServer)                 
      ' Get Recordset  
      Set rs = rdsDF.Query(txtConnect.Value,txtSQL.Value)  
  
      ' Set parameters of RDS.DataControl at run time  
      RDC.Server = txtServer.Value  
      RDC.SQL = txtSQL.Value  
      RDC.Connect = txtConnect.Value  
      RDC.Refresh  
  
   End Sub  
  
</Script>  
  
</body>  
</html>  
<!-- EndRecordsetVBS -->  
```  
  
## See Also  
 [DataFactory Object (RDSServer)](./datafactory-object-rdsserver.md)   
 [Recordset, SourceRecordset Properties (RDS)](./recordset-sourcerecordset-properties-rds.md)