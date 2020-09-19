---
description: "Append Method (ADOX Tables)"
title: "Append Method (ADOX Tables) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Tables::Append"
  - "Tables::raw_Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: a362ed51-314c-4783-9598-538dbf755f3d
author: rothja
ms.author: jroth
---
# Append Method (ADOX Tables)
Adds a new [Table](./table-object-adox.md) object to the [Tables](./tables-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Tables.Append Table  
```  
  
#### Parameters  
 *Table*  
 A **Variant** value that contains a reference to the **Table** to append or the name of the table to create and append.  
  
## Remarks  
 An error will occur if the provider does not support creating tables.  
  
## Applies To  
 [Tables Collection (ADOX)](./tables-collection-adox.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)   
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)