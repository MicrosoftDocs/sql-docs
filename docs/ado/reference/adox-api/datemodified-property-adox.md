---
description: "DateModified Property (ADOX)"
title: "DateModified Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
---
# DateModified Property (ADOX)
Indicates the date the object was last modified.  
  
## Return Values  
 Returns a **Variant** value specifying the date modified. The value is null if **DateModified** is not supported by the provider.  
  
## Remarks  
 The **DateModified** property is null for newly appended objects. After appending a new [View](./view-object-adox.md) or [Procedure](./procedure-object-adox.md), you must call the [Refresh](../ado-api/refresh-method-ado.md) method of the [Views](./views-collection-adox.md) or [Procedures](./procedures-collection-adox.md) collection to obtain values for the **DateModified** property.  
  
## Applies To  

:::row:::
    :::column:::
        [Procedure Object (ADOX)](./procedure-object-adox.md)  
    :::column-end:::
    :::column:::
        [Table Object (ADOX)](./table-object-adox.md)  
    :::column-end:::
    :::column:::
        [View Object (ADOX)](./view-object-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [DateCreated and DateModified Properties Example (VB)](./datecreated-and-datemodified-properties-example-vb.md)   
 [DateCreated Property (ADOX)](./datecreated-property-adox.md)