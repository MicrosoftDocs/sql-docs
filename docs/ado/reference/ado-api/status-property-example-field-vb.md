---
title: "Status Property Example (Field) (VB) | Microsoft Docs"
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
  - "Status property [ADO Field], Visual Basic example"
ms.assetid: fdd09b60-39c7-44be-8008-e891a031f80e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Status Property Example (Field) (VB)
The following example opens a document from a read/write folder using the [Internet Publishing Provider](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). The [Status](../../../ado/reference/ado-api/status-property-ado-field.md) property of a [Field](../../../ado/reference/ado-api/field-object.md) object of the [Record](../../../ado/reference/ado-api/record-object-ado.md) will first be set to **adFieldPendingInsert**, then be updated to **adFieldOk**.  
  
```  
'BeginStatusFieldVB  
  
 ' to integrate this code replace the values in the source string  
  
Sub Main()  
  
   Dim File As ADODB.Record  
   Dim strFile As String  
   Dim Cnxn As ADODB.Connection  
   Dim strCnxn As String  
  
   Set Cnxn = New ADODB.Connection  
   strCnxn = "url=https://MyServer/"  
   Cnxn.Open strCnxn  
  
   Set File = New ADODB.Record  
   strFile = "Folder/FileName"  
   ' Open a read/write document  
   File.Source = strFile  
   File.ActiveConnection = Cnxn  
   File.Mode = adModeReadWrite  
   File.Open  
  
   Debug.Print "Append a couple of fields"  
  
   File.Fields.Append "chektest:fld1", adWChar, 42, adFldUpdatable, "fld1"  
   File.Fields.Append "chektest:fld2", adWChar, 42, adFldUpdatable, "fld2"  
  
   Debug.Print "status for the fields"  
   Debug.Print File.Fields("chektest:fld1").Status 'adfldpendinginsert  
   Debug.Print File.Fields("chektest:fld2").Status 'adfldpendinginsert  
  
    'turn off error-handling to verify field status  
   On Error Resume Next  
  
   File.Fields.Update  
  
   Debug.Print "Update succeeds"  
   Debug.Print File.Fields("chektest:fld1").Status 'adfldpendinginsert + adFieldUnavailable  
   Debug.Print File.Fields("chektest:fld2").Status 'adfldpendinginsert + adFieldUnavailable  
  
    ' resume default error-handling  
   On Error GoTo 0  
  
     ' clean up  
    File.Close  
    Cnxn.Close  
    Set File = Nothing  
    Set Cnxn = Nothing  
  
End Sub  
'EndStatusFieldVB  
```  
  
 The following example deletes a known **Field** from a **Record** opened from a document. The **Status** property will first be set to **adFieldOK**, then **adFieldPendingUnknown**.  
  
```  
Attribute VB_Name = "StatusField"  
```  
  
 The following code deletes a **Field** from a **Record** opened on a read-only document. **Status** will be set to **adFieldPendingDelete**. At [Update](../../../ado/reference/ado-api/update-method.md), the delete will fail and **Status** will be **adFieldPendingDelete** plus **adFieldPermissionDenied**. [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) clears the pending **Status** setting.  
  
```  
Attribute VB_Name = "StatusField"  
```  
  
## See Also  
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)   
 [Status Property (ADO Field)](../../../ado/reference/ado-api/status-property-ado-field.md)
