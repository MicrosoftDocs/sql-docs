---
title: "Procedures Append Method Example (VB) | Microsoft Docs"
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
  - "Append method [ADOX], Visual Basic example"
ms.assetid: ce83b966-474b-4f57-8eb9-370996dfc5c0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedures Append Method Example (VB)
The following code demonstrates how to use a [Command](../../../ado/reference/ado-api/command-object-ado.md) object and the [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) collection [Append](../../../ado/reference/adox-api/append-method-adox-procedures.md) method to create a new procedure in the underlying data source.  
  
```  
' BeginCreateProcedureVB  
Sub Main()  
    On Error GoTo CreateProcedureError  
  
    Dim cnn As New ADODB.Connection  
    Dim cmd As New ADODB.Command  
    Dim prm As ADODB.Parameter  
    Dim cat As New ADOX.Catalog  
  
    ' Open the Connection  
    cnn.Open _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Create the parameterized command (Microsoft Jet specific)  
    Set cmd.ActiveConnection = cnn  
    cmd.CommandText = "PARAMETERS [CustId] Text;" & _  
    "Select * From Customers Where CustomerId = [CustId]"  
  
    ' Open the Catalog  
    Set cat.ActiveConnection = cnn  
  
    ' Create the new Procedure  
    cat.Procedures.Append "CustomerById", cmd  
  
    'Clean  
    cnn.Close  
    Set cat = Nothing  
    Set cmd = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
CreateProcedureError:  
    Set cat = Nothing  
    Set cmd = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndCreateProcedureVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](../../../ado/reference/adox-api/activeconnection-property-adox.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Procedure Object (ADOX)](../../../ado/reference/adox-api/procedure-object-adox.md)   
 [Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)
