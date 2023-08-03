---
title: "Provider and DefaultDatabase Properties Example (VB)"
description: "Provider and DefaultDatabase Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "DefaultDatabase property [ADO], Visual Basic example"
  - "provider property [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# Provider and DefaultDatabase Properties Example (VB)
This example demonstrates the [Provider](./provider-property-ado.md) property by opening three [Connection](./connection-object-ado.md) objects using different providers. It also uses the [DefaultDatabase](./defaultdatabase-property.md) property to set the default database for the Microsoft ODBC Provider.  
  
> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.  
  
```  
'BeginProviderVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection strings  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    Dim Cnxn1 As ADODB.Connection  
    Dim Cnxn2 As ADODB.Connection  
    Dim Cnxn3 As ADODB.Connection  
    Dim strCnxn As String  
  
    ' Open a connection using the Microsoft ODBC provider  
    Set Cnxn1 = New ADODB.Connection  
    Cnxn1.ConnectionString = "driver={SQL Server};server='MySqlServer';" & _  
        "user id='MyUserID';password='MyPassword';"  
    Cnxn1.Open strCnxn  
    Cnxn1.DefaultDatabase = "Pubs"  
  
    ' Display the provider  
    MsgBox "Cnxn1 provider: " & Cnxn1.Provider  
  
    ' Open a connection using the Microsoft Jet provider  
    Set Cnxn2 = New ADODB.Connection  
    Cnxn2.Provider = "Microsoft.Jet.OLEDB.4.0"  
    Cnxn2.Open "Northwind.mdb", _  
        "MyUserID", "MyPassword"  
  
    ' Display the provider.  
    MsgBox "Cnxn2 provider: " & Cnxn2.Provider  
  
    ' Open a connection using the Microsoft SQL Server provider  
    Set Cnxn3 = New ADODB.Connection  
    Cnxn3.Provider = "sqloledb"  
    Cnxn3.Open "Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
  
    ' Display the provider  
    MsgBox "Cnxn3 provider: " & Cnxn3.Provider  
  
    ' clean up  
    Cnxn1.Close  
    Cnxn2.Close  
    Cnxn3.Close  
    Set Cnxn1 = Nothing  
    Set Cnxn2 = Nothing  
    Set Cnxn3 = Nothing  
    Exit Sub  
  
ErrorHandler:  
    If Not Cnxn1 Is Nothing Then  
        If Cnxn1.State = adStateOpen Then Cnxn1.Close  
    End If  
    Set Cnxn1 = Nothing  
  
    If Not Cnxn2 Is Nothing Then  
        If Cnxn2.State = adStateOpen Then Cnxn2.Close  
    End If  
    Set Cnxn2 = Nothing  
  
    If Not Cnxn3 Is Nothing Then  
        If Cnxn3.State = adStateOpen Then Cnxn3.Close  
    End If  
    Set Cnxn3 = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
  
End Sub  
'EndProviderVB  
```  
  
## See Also  
 [Connection Object (ADO)](./connection-object-ado.md)   
 [DefaultDatabase Property](./defaultdatabase-property.md)   
 [Provider Property (ADO)](./provider-property-ado.md)