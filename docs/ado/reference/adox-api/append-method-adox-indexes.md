---
description: "Append Method (ADOX Indexes)"
title: "Append Method (ADOX Indexes) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Indexes::Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 6695769f-275b-4b70-81bd-1a5f7d74926c
author: rothja
ms.author: jroth
---
# Append Method (ADOX Indexes)
Adds a new [Index](./index-object-adox.md) object to the [Indexes](./indexes-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Indexes.Append Index [,Columns]  
```  
  
#### Parameters  
 *Index*  
 The **Index** object to append or the name of the index to create and append.  
  
 *Columns*  
 Optional. A **Variant** value that specifies the name(s) of the column(s) to be indexed. The *Columns* parameter corresponds to the value(s) of the [Name](./name-property-adox.md) property of a [Column](./column-object-adox.md) object or objects.  
  
## Remarks  
 The *Columns* parameter can take either the name of a column or an array of column names.  
  
 An error will occur if the provider does not support creating indexes.  
  
## Applies To  
 [Indexes Collection (ADOX)](./indexes-collection-adox.md)  
  
## See Also  
 [Indexes Append Method Example (VB)](./indexes-append-method-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)