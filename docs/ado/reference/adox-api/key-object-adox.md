---
title: "Key Object (ADOX)"
description: "Key Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Key"
helpviewer_keywords:
  - "Key object [ADOX]"
apitype: "COM"
---
# Key Object (ADOX)
Represents a primary, foreign, or unique key field from a database table.  
  
## Remarks  
 The following code creates a new **Key**:  
  
```  
Dim obj As New Key  
```  
  
 With the properties and collections of a **Key** object, you can:  
  
-   Identify the key with the [Name](./name-property-adox.md) property.  
  
-   Determine whether the key is primary, foreign, or unique with the [Type](./type-property-key-adox.md) property.  
  
-   Access the database columns of the key with the [Columns](./columns-collection-adox.md) collection.  
  
-   Specify the name of the related table with the [RelatedTable](./relatedtable-property-adox.md) property.  
  
-   Determine the action performed on deletion or update of a primary key with the [DeleteRule](./deleterule-property-adox.md) and [UpdateRule](./updaterule-property-adox.md) properties.  
  
 This section contains the following topic.  
  
-   [Key Object Properties, Methods, and Events](./key-object-properties-methods-and-events.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Keys Collection (ADOX)](./keys-collection-adox.md)