---
title: "Step 3: Populate the Fields List Box | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 315c32dc-aeb1-4629-b30e-87b44e8f84d1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 3: Populate the Fields List Box
To populate the Fields list box, insert the following code into the Click event handler of `lstMain`:  
  
```  
Private Sub lstMain_Click()  
    Dim rec As Record  
    Dim rs As Recordset  
    Set rec = New Record  
    Set rs = New Recordset  
    grs.MoveFirst  
    grs.Move lstMain.ListIndex  
    lstDetails.Clear  
    rec.Open grs  
    Select Case rec.RecordType  
        Case adCollectionRecord:  
            Set rs = rec.GetChildren  
            While Not rs.EOF  
                lstDetails.AddItem rs(0)  
                rs.MoveNext  
            Wend  
        Case adSimpleRecord:  
            recFields rec, lstDetails, txtDetails  
  
        Case adStructDoc:  
    End Select  
  
End Sub  
```  
  
 This code declares and instantiates local Record and Recordset objects, `rec` and `rs`, respectively.  
  
 The row corresponding to the resource selected in `lstMain` is made the current row of `grs`. Then the Details list box is cleared and `rec` is opened with the current row of `grs` as the source.  
  
 If the resource is a collection record, as specified by [RecordType](../../../ado/reference/ado-api/recordtype-property-ado.md), the local Recordset `rs` is opened on the children of rec. Then `lstDetails` is filled with the values from the rows of `rs`.  
  
 If the resource is a simple record, `recFields` is called. For more information about `recFields`, see the next step.  
  
 No code is implemented if the resource is a structured document.  
  
## See Also  
 [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md)   
 [Step 2: Initialize the Main List Box](../../../ado/guide/data/step-2-initialize-the-main-list-box.md)   
 [Step 4: Populate the Details Text Box](../../../ado/guide/data/step-4-populate-the-details-text-box.md)
