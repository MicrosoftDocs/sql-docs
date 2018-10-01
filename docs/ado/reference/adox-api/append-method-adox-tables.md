---
title: "Append Method (ADOX Tables) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Tables)
Adds a new [Table](../../../ado/reference/adox-api/table-object-adox.md) object to the [Tables](../../../ado/reference/adox-api/tables-collection-adox.md) collection.  
  
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
 [Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](../../../ado/reference/adox-api/columns-and-tables-append-methods-name-property-example-vb.md)   
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
