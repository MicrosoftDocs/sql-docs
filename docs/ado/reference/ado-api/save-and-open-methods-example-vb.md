---
title: "Save and Open Methods Example (VB)"
description: "Save and Open Methods Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Save method [ADO], Visual Basic example"
  - "Open method [ADO]"
dev_langs:
  - "VB"
---
# Save and Open Methods Example (VB)
These three examples demonstrate how the [Save](./save-method.md) and [Open](./open-method-ado-recordset.md) methods can be used together.  
  
 Assume that you are going on a business trip and want to take along a table from a database. Before you go, you access the data as a [Recordset](./recordset-object-ado.md) and save it in a transportable form. When you arrive at your destination, you access the **Recordset** as a local, disconnected **Recordset**. You make changes to the **Recordset**, and then save it again. Finally, when you return home, you connect to the database again and update it with the changes you made on the road.  
  
 First, access and save the ***Authors*** table.  
  
```  
'BeginSaveVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset and connection variables  
    Dim rstAuthors As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strSQLAuthors As String  
  
    ' Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    Set rstAuthors = New ADODB.Recordset  
    strSQLAuthors = "SELECT au_id, au_lname, au_fname, city, phone FROM Authors"  
    rstAuthors.Open strSQLAuthors, Cnxn, adOpenDynamic, adLockOptimistic, adCmdText  
  
    'For sake of illustration, save the Recordset to a diskette in XML format  
    rstAuthors.Save "c:\Pubs.xml", adPersistXML  
  
    ' clean up  
    rstAuthors.Close  
    Cnxn.Close  
    Set rstAuthors = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    'clean up  
    If Not rstAuthors Is Nothing Then  
        If rstAuthors.State = adStateOpen Then rstAuthors.Close  
    End If  
    Set rstAuthors = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndSaveVB  
```  
  
 At this point, you have arrived at your destination. You will access the ***Authors*** table as a local, disconnected **Recordset**. You must have the **MSPersist** provider on the computer that you are using to access the saved file, a:\Pubs.xml.  
  
```  
Attribute VB_Name = "Save"  
```  
  
 Finally, you return home. Now update the database with your changes.  
  
```  
Attribute VB_Name = "Save"  
```  
  
## See Also  
 [Open Method (ADO Recordset)](./open-method-ado-recordset.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)   
 [More About Recordset Persistence](../../guide/data/more-about-recordset-persistence.md)   
 [Save Method](./save-method.md)