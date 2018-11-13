---
title: "CopyRecord, CopyTo, and SaveToFile Methods Example (VB) | Microsoft Docs"
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
  - "CopyRecord method [ADO], Visual Basic example"
  - "SaveToFile method [ADO], Visual Basic example"
  - "CopyTo method [ADO], Visual Basic example"
ms.assetid: 61a51b74-93cd-439c-877f-f3055499d39f
author: MightyPen
ms.author: genemi
manager: craigg
---
# CopyRecord, CopyTo, and SaveToFile Methods Example (VB)
This example demonstrates how to create copies of a file using [Stream](../../../ado/reference/ado-api/stream-object-ado.md) or [Record](../../../ado/reference/ado-api/record-object-ado.md) objects. One copy is made to a Web folder for Internet publishing. Other properties and methods shown include [Stream Type](../../../ado/reference/ado-api/type-property-ado-stream.md), **Open**, [LoadFromFile](../../../ado/reference/ado-api/loadfromfile-method-ado.md), and [Record Open](../../../ado/reference/ado-api/open-method-ado-record.md).  
  
```  
'BeginCopyRecordVB  
  
'Note:  
' This sample requires that "C:\checkmrk.wmf" and  
' "https://MyServer/mywmf.wmf" exist.  
  
Option Explicit  
  
Private Sub Form_Load()  
    On Error GoTo ErrorHandler  
  
    ' Declare variables  
    Dim strPicturePath, strStreamPath, strStream2Path, _  
        strRecordPath, strStreamURL, strRecordURL As String  
    Dim objStream, objStream2 As Stream  
    Dim objRecord As Record  
    Dim objField As Field  
  
    ' Instantiate objects  
    Set objStream = New Stream  
    Set objStream2 = New Stream  
    Set objRecord = New Record  
  
    ' Initialize path and URL strings  
    strPicturePath = "C:\checkmrk.wmf"  
    strStreamPath = "C:\mywmf.wmf"  
    strStreamURL = "URL=https://MyServer/mywmf.wmf"  
    strStream2Path = "C:\checkmrk2.wmf"  
    strRecordPath = "C:\mywmf.wmf"  
    strRecordURL = "https://MyServer/mywmf2.wmf"  
  
    ' Load the file into the stream  
    objStream.Open  
    objStream.Type = adTypeBinary  
    objStream.LoadFromFile (strPicturePath)  
  
    ' Save the stream to a new path and filename  
    objStream.SaveToFile strStreamPath, adSaveCreateOverWrite  
  
    ' Copy the contents of the first stream to a second stream  
    objStream2.Open  
    objStream2.Type = adTypeBinary  
    objStream.CopyTo objStream2  
  
    ' Save the second stream to a different path  
    objStream2.SaveToFile strStream2Path, adSaveCreateOverWrite  
  
    ' Because strStreamPath is a Web Folder, open a Record on the URL  
    objRecord.Open "", strStreamURL  
  
    ' Display the Fields of the record  
    For Each objField In objRecord.Fields  
        Debug.Print objField.Name & ": " & objField.Value  
    Next  
  
    ' Copy the record to a new URL  
    objRecord.CopyRecord "", strRecordURL, , , adCopyOverWrite  
  
    ' Load each copy of the graphic into Image controls for viewing  
    Image1.Picture = LoadPicture(strPicturePath)  
    Image2.Picture = LoadPicture(strStreamPath)  
    Image3.Picture = LoadPicture(strStream2Path)  
    Image4.Picture = LoadPicture(strRecordPath)  
  
    ' clean up  
    objStream.Close  
    objStream2.Close  
    objRecord.Close  
    Set objStream = Nothing  
    Set objStream2 = Nothing  
    Set objRecord = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not objStream Is Nothing Then  
        If objStream.State = adStateOpen Then objStream.Close  
    End If  
    Set objStream = Nothing  
  
    If Not objStream2 Is Nothing Then  
        If objStream2.State = adStateOpen Then objStream2.Close  
    End If  
    Set objStream2 = Nothing  
  
    If Not objRecord Is Nothing Then  
        If objRecord.State = adStateOpen Then objRecord.Close  
    End If  
    Set objRecord = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndCopyRecordVB  
```  
  
## See Also  
 [CopyRecord Method (ADO)](../../../ado/reference/ado-api/copyrecord-method-ado.md)   
 [CopyTo Method (ADO)](../../../ado/reference/ado-api/copyto-method-ado.md)   
 [LoadFromFile Method (ADO)](../../../ado/reference/ado-api/loadfromfile-method-ado.md)   
 [Open Method (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)   
 [Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)   
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)   
 [SaveToFile Method](../../../ado/reference/ado-api/savetofile-method.md)   
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)   
 [Type Property (ADO Stream)](../../../ado/reference/ado-api/type-property-ado-stream.md)
