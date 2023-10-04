---
title: "Error Object Properties Example (VB)"
description: "Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Number property [ADO], Visual Basic example"
  - "Source property [ADO], Visual Basic example"
  - "NativeError property [ADO], Visual Basic example"
  - "Description property [ADO], Visual Basic example"
  - "HelpFile property [ADO], Visual Basic example"
  - "SQLState property [ADO], Visual Basic example"
  - "HelpContext property [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)
This example triggers an error, traps it, and displays the [Description](../../../ado/reference/ado-api/description-property.md), [HelpContext](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md), [HelpFile](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md), [NativeError](../../../ado/reference/ado-api/nativeerror-property-ado.md), [Number](../../../ado/reference/ado-api/number-property-ado.md), [Source](../../../ado/reference/ado-api/source-property-ado-error.md), and [SQLState](../../../ado/reference/ado-api/sqlstate-property.md) properties of the resulting [Error](../../../ado/reference/ado-api/error-object.md) object.  
  
```  
'BeginDescriptionVB  
Public Sub Main()  
  
    Dim Cnxn As ADODB.Connection  
    Dim Err As ADODB.Error  
    Dim strError As String  
  
    On Error GoTo ErrorHandler  
  
    ' Intentionally trigger an error  
    Set Cnxn = New ADODB.Connection  
    Cnxn.Open "nothing"  
  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
  
    ' Enumerate Errors collection and display  
    ' properties of each Error object  
    For Each Err In Cnxn.Errors  
        strError = "Error #" & Err.Number & vbCr & _  
            "   " & Err.Description & vbCr & _  
            "   (Source: " & Err.Source & ")" & vbCr & _  
            "   (SQL State: " & Err.SQLState & ")" & vbCr & _  
            "   (NativeError: " & Err.NativeError & ")" & vbCr  
        If Err.HelpFile = "" Then  
            strError = strError & "   No Help file available"  
        Else  
            strError = strError & _  
               "   (HelpFile: " & Err.HelpFile & ")" & vbCr & _  
               "   (HelpContext: " & Err.HelpContext & ")" & _  
               vbCr & vbCr  
        End If  
  
        Debug.Print strError  
    Next  
  
    Resume Next  
End Sub  
'EndDescriptionVB  
```  
  
## See Also  
 [Description Property](../../../ado/reference/ado-api/description-property.md)   
 [Error Object](../../../ado/reference/ado-api/error-object.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [NativeError Property (ADO)](../../../ado/reference/ado-api/nativeerror-property-ado.md)   
 [Number Property (ADO)](../../../ado/reference/ado-api/number-property-ado.md)   
 [Source Property (ADO Error)](../../../ado/reference/ado-api/source-property-ado-error.md)   
 [SQLState Property](../../../ado/reference/ado-api/sqlstate-property.md)
