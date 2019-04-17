---
title: "Handler Property Example (VB) | Microsoft Docs"
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
  - "Handler property [ADO], Visual Basic example"
ms.assetid: 9664f9a6-65fc-4e7f-be3d-3e4b501b558a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Handler Property Example (VB)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 This example demonstrates the [RDS DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object [Handler](../../../ado/reference/rds-api/handler-property-rds.md) property. (See [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md) for more details.)  
  
 Assume that the following sections in the parameter file, Msdfmap.ini, are located on the server:  
  
```  
[connect AuthorDataBase]  
Access=ReadWrite  
Connect="DSN=Pubs"  
[sql AuthorById]  
SQL="SELECT * FROM Authors WHERE au_id = ?"  
```  
  
 Your code looks like the following. The command assigned to the [SQL](../../../ado/reference/rds-api/sql-property.md) property will match the ***AuthorById*** identifier and will retrieve a row for author Michael O'Leary. The **DataControl** object **Recordset** property is assigned to a disconnected [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object purely as a coding convenience.  
  
```  
'BeginHandlerVB  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    Dim dc As New DataControl  
    Dim rst As ADODB.Recordset  
  
    dc.Handler = "MSDFMAP.Handler"  
    dc.ExecuteOptions = 1  
    dc.FetchOptions = 1  
    dc.Server = "https://MyServer"  
    dc.Connect = "Data Source=AuthorDataBase"  
    dc.SQL = "AuthorById('267-41-2394')"  
    dc.Refresh                  'Retrieve the record  
    Set rst = dc.Recordset      'Use another Recordset as a convenience  
    Debug.Print "Author is '" & rst!au_fname & " " & rst!au_lname & "'"  
  
    ' clean up  
    If rst.State = adStateOpen Then rst.Close  
    Set rst = Nothing  
    Set dc = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rst Is Nothing Then  
        If rst.State = adStateOpen Then rst.Close  
    End If  
    Set rst = Nothing  
    Set dc = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndHandlerVB  
```  
  
## See Also  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)   
 [Handler Property (RDS)](../../../ado/reference/rds-api/handler-property-rds.md)


