---
title: "DateCreated Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Table::get_DateCreated"
  - "_Table::DateCreated"
  - "_Table::GetDateCreated"
helpviewer_keywords: 
  - "DateCreated property [ADOX]"
ms.assetid: 2bf4b00d-045c-444e-8af7-8af6297ed418
author: MightyPen
ms.author: genemi
manager: craigg
---
# DateCreated Property (ADOX)
Indicates the date the object was created.  
  
## Return Values  
 Returns a **Variant** value specifying the date created. The value is null if **DateCreated** is not supported by the provider.  
  
## Remarks  
 The **DateCreated** property is null for newly appended objects. After appending a new [View](../../../ado/reference/adox-api/view-object-adox.md) or [Procedure](../../../ado/reference/adox-api/procedure-object-adox.md), you must call the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method of the [Views](../../../ado/reference/adox-api/views-collection-adox.md) or [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) collection to obtain values for the **DateCreated** property.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Procedure Object (ADOX)](../../../ado/reference/adox-api/procedure-object-adox.md)|[Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)|[View Object (ADOX)](../../../ado/reference/adox-api/view-object-adox.md)|  
  
## See Also  
 [DateCreated and DateModified Properties Example (VB)](../../../ado/reference/adox-api/datecreated-and-datemodified-properties-example-vb.md)   
 [DateModified Property (ADOX)](../../../ado/reference/adox-api/datemodified-property-adox.md)
