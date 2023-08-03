---
title: "Read, ReadText, Write, and WriteText Methods Example (VB)"
description: "Read, ReadText, Write, and WriteText Methods Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ReadText method [ADO], Visual Basic example"
  - "Write method [ADO], Visual Basic example"
  - "Read method [ADO], Visual Basic example"
  - "WriteText method [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# Read, ReadText, Write, and WriteText Methods Example (VB)
This example demonstrates how to read the contents of a text box into both a text [Stream](./stream-object-ado.md) and a binary **Stream**. Other properties and methods shown include [Position](./position-property-ado.md), [Size](./size-property-ado-parameter.md), [Charset](./charset-property-ado.md), and [SetEOS](./seteos-method.md).  
  
```  
'BeginReadVB  
Private Sub cmdRead_Click()  
    On Error GoTo ErrorHandler  
  
    'Declare variables  
    Dim objStream As Stream  
    Dim varA As Variant  
    Dim bytA() As Byte  
    Dim i As Integer  
    Dim strBytes As String  
  
    'Instantiate and Open Stream  
    Set objStream = New Stream  
    objStream.Open  
  
    'Write the text content of a textbox to the stream  
    If Text1.Text = "" Then  
        Err.Raise 1, , "The text field is blank."  
    End If  
    objStream.WriteText Text1.Text  
  
    'Display the text contents and size of the stream  
    objStream.Position = 0  
    Debug.Print "Default text:"  
    Debug.Print objStream.ReadText  
    Debug.Print objStream.Size  
  
    'Switch character set and display  
    objStream.Position = 0  
    objStream.Charset = "Windows-1252"  
    Debug.Print "New Charset text:"  
    Debug.Print objStream.ReadText  
    Debug.Print objStream.Size  
  
    'Switch to a binary stream and display  
    objStream.Position = 0  
    objStream.Type = adTypeBinary  
    Debug.Print "Binary:"  
    Debug.Print objStream.Read  
    Debug.Print objStream.Size  
  
    'Load an array of bytes with the text box text  
    ReDim bytA(Len(Text1.Text))  
    For i = 1 To Len(Text1.Text)  
        bytA(i - 1) = CByte(Asc(Mid(Text1.Text, i, 1)))  
    Next  
  
    'Write the buffer to the binary stream and display  
    objStream.Position = 0  
    objStream.Write bytA()  
    objStream.SetEOS  
    objStream.Position = 0  
    Debug.Print "Binary after Write:"  
    Debug.Print objStream.Read  
    Debug.Print objStream.Size  
  
    'Switch back to a text stream and display  
    Debug.Print "Translated back:"  
    objStream.Position = 0  
    objStream.Type = adTypeText  
    Debug.Print objStream.ReadText  
    Debug.Print objStream.Size  
  
    ' clean up  
    objStream.Close  
    Set objStream = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not objStream Is Nothing Then  
        If objStream.State = adStateOpen Then objStream.Close  
    End If  
    Set objStream = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndReadVB  
```  
  
## See Also  
 [Charset Property (ADO)](./charset-property-ado.md)   
 [Position Property (ADO)](./position-property-ado.md)   
 [Read Method](./read-method.md)   
 [ReadText Method](./readtext-method.md)   
 [SetEOS Method](./seteos-method.md)   
 [Size Property (ADO Stream)](./size-property-ado-stream.md)   
 [Stream Object (ADO)](./stream-object-ado.md)   
 [Write Method](./write-method.md)   
 [WriteText Method](./writetext-method.md)