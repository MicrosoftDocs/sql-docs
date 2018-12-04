---
title: "ExecuteOptions and FetchOptions Properties Example (VBScript) | Microsoft Docs"
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
  - "FetchOptions property [ADO], VBScript example"
  - "ExecuteOptions property [ADO]"
ms.assetid: 753a4a3d-0fba-40b8-86e7-50b34182ca69
author: MightyPen
ms.author: genemi
manager: craigg
---
# ExecuteOptions and FetchOptions Properties Example (VBScript)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 The following code shows how to set the [ExecuteOptions](../../../ado/reference/rds-api/executeoptions-property-rds.md) and [FetchOptions](../../../ado/reference/rds-api/fetchoptions-property-rds.md) properties at design time. If left unset, **ExecuteOptions** defaults to **adcExecSync**. This setting indicates that when the **RDS.Refresh** method is called, it will be executed on the current calling thread-that is, synchronously. Cut and paste the following code to Notepad or another text editor and save it as **ExecuteOptionsDesignVBS.asp**.  
  
```  
<!-- BeginExecuteOptionsDesignVBS -->  
<%@ Language=VBScript %>  
<html>  
<head>  
    <meta name="VI60_DefaultClientScript"  content=VBScript>  
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">  
    <title>Design-time ExecuteOptions and FetchOptions Properties Example</title>  
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
<h2>Design-time <br> ExecuteOptions and FetchOptions Properties Example</h2>  
  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID=RDS height=1 width=1>  
<PARAM NAME="SQL" VALUE="SELECT FirstName, LastName FROM Employees ORDER BY LastName">  
<PARAM NAME="Connect" VALUE="Provider='sqloledb';Data Source=<%=Request.ServerVariables("SERVER_NAME")%>;Integrated Security='SSPI';Initial Catalog='Northwind'">  
<PARAM NAME="Server" VALUE="https://<%=Request.ServerVariables("SERVER_NAME")%>">  
<PARAM NAME="ExecuteOptions" VALUE="1">  
<PARAM NAME="FetchOptions" VALUE="3">  
</OBJECT>  
  
<TABLE DATASRC=#RDS>  
<TBODY>  
   <TR class="thead2">  
      <TH>First Name</TH>  
      <TH>Last Name</TH>  
   </TR>  
   <TR class="tbody">  
     <TD><SPAN DATAFLD="FirstName"></SPAN></TD>  
     <TD><SPAN DATAFLD="LastName"></SPAN></TD>  
   </TR>  
</TBODY>  
</TABLE>  
  
</body>  
</html>  
<!-- EndExecuteOptionsDesignVBS -->  
```  
  
 The following example shows how to set the **ExecuteOptions** and **FetchOptions** properties at run time in VBScript code. See the [Refresh](../../../ado/reference/rds-api/refresh-method-rds.md) method for a working example of these properties. Cut and paste the following code to Notepad or another text editor and save it as **ExecuteOptionsRuntimeVBS.asp**.  
  
```  
<!-- BeginExecuteOptionsRuntimeVBS -->  
<%@ Language=VBScript %>  
<html>  
<head>  
    <meta name="VI60_DefaultClientScript"  content=VBScript>  
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">  
    <title>Run-time ExecuteOptions and FetchOptions Properties Example</title>  
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
<h2>Run-time <br> ExecuteOptions and FetchOptions Properties Example</h2>  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID=RDS height=1 width=1>  
<PARAM NAME="SQL" VALUE="SELECT FirstName, LastName FROM Employees ORDER BY LastName">  
<PARAM NAME="Connect" VALUE="Provider='sqloledb';Data Source=<%=Request.ServerVariables("SERVER_NAME")%>;Integrated Security='SSPI';Initial Catalog='Northwind'">  
<PARAM NAME="Server" VALUE="https://<%=Request.ServerVariables("SERVER_NAME")%>">  
</OBJECT>  
  
<TABLE DATASRC=#RDS>  
<TBODY>  
   <TR class="thead2">  
      <TH>First Name</TH>  
      <TH>Last Name</TH>  
   </TR>  
   <TR class="tbody">  
     <TD><SPAN DATAFLD="FirstName"></SPAN></TD>  
     <TD><SPAN DATAFLD="LastName"></SPAN></TD>  
   </TR>  
</TBODY>  
</TABLE>  
<Script Language="VBScript">  
Const adcExecSync = 1  
Const adcFetchAsynch = 3  
  
Sub ExecuteHow  
  ' set RDS properties at run-time  
  RDS1.ExecuteOptions = adcExecSync  
  RDS1.FetchOptions = adcFetchAsynch  
  RDS.Refresh  
End Sub  
</Script>  
</body>  
</html>  
<!-- EndExecuteOptionsRuntimeVBS -->  
```  
  
## See Also  
 [ExecuteOptions Property (RDS)](../../../ado/reference/rds-api/executeoptions-property-rds.md)   
 [FetchOptions Property (RDS)](../../../ado/reference/rds-api/fetchoptions-property-rds.md)



