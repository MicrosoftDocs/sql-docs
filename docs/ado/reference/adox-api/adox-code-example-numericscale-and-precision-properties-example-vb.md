---
title: "NumericScale and Precision properties ADOX code example (VB)"
description: "ADOX Code Example: NumericScale and Precision Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Precision property [ADOX], Visual Basic example"
  - "NumericScale property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# ADOX Code Example: NumericScale and Precision Properties Example (VB)
This example demonstrates the [NumericScale](./numericscale-property-adox.md) and [Precision](./precision-property-adox.md) properties of the [Column](./column-object-adox.md) object. This code displays their value for the **Order Details** table of the *Northwind* database.  
  
```  
' BeginNumericScalePrecVB  
Sub Main()  
    On Error GoTo NumericScalePrecXError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim tblOD As ADOX.Table  
    Dim colLoop As ADOX.Column  
  
    ' Connect the catalog.  
    cnn.Open "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "data source='Northwind.mdb';"  
    Set cat.ActiveConnection = cnn  
  
    ' Retrieve the Order Details table  
    Set tblOD = cat.Tables("Order Details")  
  
    ' Display numeric scale and precision of  
    ' small integer fields.  
    For Each colLoop In tblOD.Columns  
        If colLoop.Type = adSmallInt Then  
            MsgBox "Column: " & colLoop.Name & vbCr & _  
                "Numeric scale: " & _  
                colLoop.NumericScale & vbCr & _  
                "Precision: " & colLoop.Precision  
        End If  
    Next colLoop  
  
    'Clean up  
    cnn.Close  
    Set cat = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
NumericScalePrecXError:  
    Set cat = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndNumericScalePrecVB  
```  
  
## See Also  
 [Column Object (ADOX)](./column-object-adox.md)   
 [NumericScale Property (ADOX)](./numericscale-property-adox.md)   
 [Precision Property (ADOX)](./precision-property-adox.md)