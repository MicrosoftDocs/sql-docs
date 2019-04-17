---
title: "DateModified Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Table::get_DateModified"
  - "_Table::DateModified"
  - "_Table::GetDateModified"
helpviewer_keywords: 
  - "DateModified property [ADOX]"
ms.assetid: fed09266-1547-4bda-9088-c254d81cc738
author: MightyPen
ms.author: genemi
manager: craigg
---
# DateModified Property (ADOX)
Indicates the date the object was last modified.  
  
## Return Values  
 Returns a **Variant** value specifying the date modified. The value is null if **DateModified** is not supported by the provider.  
  
## Remarks  
 The **DateModified** property is null for newly appended objects. After appending a new [View](../../../ado/reference/adox-api/view-object-adox.md) or [Procedure](../../../ado/reference/adox-api/procedure-object-adox.md), you must call the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method of the [Views](../../../ado/reference/adox-api/views-collection-adox.md) or [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) collection to obtain values for the **DateModified** property.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Procedure Object (ADOX)](../../../ado/reference/adox-api/procedure-object-adox.md)|[Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)|[View Object (ADOX)](../../../ado/reference/adox-api/view-object-adox.md)|  
  
## See Also  
 [DateCreated and DateModified Properties Example (VB)](../../../ado/reference/adox-api/datecreated-and-datemodified-properties-example-vb.md)   
 [DateCreated Property (ADOX)](../../../ado/reference/adox-api/datecreated-property-adox.md)
