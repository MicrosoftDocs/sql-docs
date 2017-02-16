---
title: "RDS Tutorial (VBScript) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "02/14/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "RDS tutorial [ADO], VBScript"
ms.assetid: e2a48c4d-88b1-43ff-a202-9cdec54997d2
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# RDS Tutorial (VBScript)
This is the RDS Tutorial, written in Microsoft Visual Basic Scripting Edition. For a description of the purpose of this tutorial, see the [RDS Tutorial](../../../ado/guide/remote-data-service/rds-tutorial.md).  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 In this tutorial, [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) and [RDS.DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md) are created at design time — that is, they are defined with object tags, like this: `<OBJECT>...</OBJECT>`. Alternatively, they could be created at run time with the [CreateObject Method (RDS)](../../../ado/reference/rds-api/createobject-method-rds.md) method. For example, the **RDS.DataControl** object could be created like this:  
  
```  
Set DC = Server.CreateObject("RDS.DataControl")  
   <!-- RDS.DataControl -->  
   <OBJECT   
      ID="DC1" CLASSID="CLSID:BD96C556-65A3-11D0-983A-00C04FC29E33">  
   </OBJECT>  
  
   <!-- RDS.DataSpace -->  
   <OBJECT   
      ID="DS1" WIDTH=1 HEIGHT=1  
      CLASSID="CLSID:BD96C556-65A3-11D0-983A-00C04FC29E36">  
   </OBJECT>  
  
   <SCRIPT LANGUAGE="VBScript">  
  
   Sub RDSTutorial()  
   Dim DF1   
```  
  
## Step 1 — Specify a server program  
 VBScript can discover the name of the IIS Web server it is running on by accessing the VBScript **Request.ServerVariables** method available to Active Server Pages:  
  
```  
"http://<%=Request.ServerVariables("SERVER_NAME")%>"  
```  
  
 However, for this tutorial, use the imaginary server, "yourServer".  
  
> [!NOTE]
>  Pay attention to the data type of **ByRef** arguments. VBScript does not let you specify the variable type, so you must always pass a **Variant**. When using HTTP, RDS will allow you to pass a Variant to a method that expects a non-Variant if you invoke it with the **RDS.DataSpace** object [CreateObject](../../../ado/reference/rds-api/createobject-method-rds.md) method. When using DCOM or an in-process server, you must match the parameter types on the client and server sides or you will receive a "Type Mismatch" error.  
  
```  
Set DF1 = DS1.CreateObject("RDSServer.DataFactory", "http://yourServer")  
```  
  
## Step 2a — Invoke the server program with RDS.DataControl  
 This example is merely a comment demonstrating that the default behavior of the **RDS.DataControl** is to perform the specified query.  
  
```  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID="DC1">  
   <PARAM NAME="SQL" VALUE="SELECT * FROM Authors">  
   <PARAM NAME="Connect" VALUE="DSN=Pubs;">  
   <PARAM NAME="Server" VALUE="http://yourServer/">  
</OBJECT>  
...  
<SCRIPT LANGUAGE="VBScript">  
  
Sub RDSTutorial2A()  
   Dim RS  
   DC1.Refresh  
   Set RS = DC1.Recordset  
...  
```  
  
## Step 2b — Invoke the server program with RDSServer.DataFactory  
  
## Step 3 — Server obtains a Recordset  
  
## Step 4 — Server returns the Recordset  
  
```  
Set RS = DF1.Query("DSN=Pubs;", "SELECT * FROM Authors")  
```  
  
## Step 5 — DataControl is made usable by visual controls  
  
```  
' Assign the returned recordset to the DataControl.  
  
DC1.SourceRecordset = RS  
```  
  
## Step 6a — Changes are sent to the server with RDS.DataControl  
 This example is merely a comment demonstrating how the **RDS.DataControl** performs updates.  
  
```  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID="DC1">  
   <PARAM NAME="SQL" VALUE="SELECT * FROM Authors">  
   <PARAM NAME="Connect" VALUE="DSN=Pubs;">  
   <PARAM NAME="Server" VALUE="http://yourServer/">  
</OBJECT>  
...  
<SCRIPT LANGUAGE="VBScript">  
  
Sub RDSTutorial6A()  
Dim RS  
DC1.Refresh  
...  
Set RS = DC1.Recordset  
' Edit the Recordset object...  
' The SERVER and CONNECT properties are already set from Step 2A.  
Set DC1.SourceRecordset = RS  
...  
DC1.SubmitChanges  
```  
  
## Step 6b — Changes are sent to the server with RDSServer.DataFactory  
  
```  
DF.SubmitChanges "DSN=Pubs", RS  
  
End Sub  
</SCRIPT>  
</BODY>  
</HTML>  
```  
  
 **This is the end of the tutorial.**  
  
## See Also  
 [RDS Tutorial](../../../ado/guide/remote-data-service/rds-tutorial.md)   
