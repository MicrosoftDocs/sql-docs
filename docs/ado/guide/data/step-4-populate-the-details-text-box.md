---
title: "Step 4: Populate the Details Text Box"
description: "Step 4: Populate the Details Text Box"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
---
# Step 4: Populate the Details Text Box
To populate the Details text box, create a new subroutine named **recFields** and insert the following code:  
  
```  
Sub recFields(r As Record, l As ListBox, t As TextBox)  
    Dim f As Field  
    Dim s As Stream  
    Set s = New Stream  
    Dim str As String  
  
    For Each f In r.Fields  
        l.AddItem f.Name & ": " & f.Value  
    Next  
    t.Text = ""  
    If r!RESOURCE_CONTENTCLASS = "text/plain" Then  
        s.Open r, adModeRead, adOpenStreamFromRecord  
        str = s.ReadText(1)  
        s.Position = 0  
        If Asc(Mid(str, 1, 1)) = 63 Then '//63 = "?"  
            s.Charset = "ascii"  
            s.Type = adTypeText  
        End If  
        t.Text = s.ReadText(adReadAll)  
    End If  
End Sub  
```  
  
 This code populates `lstDetails` with the fields and values of the simple record passed to `recFields`. If the resource is a text file, a text Stream is opened from the resource record. The code determines whether the character set is ASCII and copies the Stream contents into `txtDetails`.  
  
## See Also  
 [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md)   
 [Step 3: Populate the Fields List Box](../../../ado/guide/data/step-3-populate-the-fields-list-box.md)
