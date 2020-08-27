---
description: "DateCreated Property (ADOX)"
title: "DateCreated Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
---
# DateCreated Property (ADOX)
Indicates the date the object was created.  
  
## Return Values  
 Returns a **Variant** value specifying the date created. The value is null if **DateCreated** is not supported by the provider.  
  
## Remarks  
 The **DateCreated** property is null for newly appended objects. After appending a new [View](./view-object-adox.md) or [Procedure](./procedure-object-adox.md), you must call the [Refresh](../ado-api/refresh-method-ado.md) method of the [Views](./views-collection-adox.md) or [Procedures](./procedures-collection-adox.md) collection to obtain values for the **DateCreated** property.  
  
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
 [DateModified Property (ADOX)](./datemodified-property-adox.md)