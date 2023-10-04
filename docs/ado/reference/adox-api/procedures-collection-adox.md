---
title: "Procedures Collection (ADOX)"
description: "Procedures Collection (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Procedures"
  - "Catalog::Procedures"
helpviewer_keywords:
  - "Procedures collection [ADOX]"
apitype: "COM"
---
# Procedures Collection (ADOX)
Contains all [Procedure](./procedure-object-adox.md) objects of a catalog.  
  
## Remarks  
 The [Append](./append-method-adox-procedures.md) method for a **Procedures** collection is unique for ADOX. You can:  
  
-   Add a new procedure to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a procedure in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
-   Return the number of procedures contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Remove a procedure from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Indexes Collection Properties, Methods, and Events](./indexes-collection-properties-methods-and-events.md)  
  
## See Also  
 [Command and CommandText Properties Example (VB)](./command-and-commandtext-properties-example-vb.md)   
 [Parameters Collection, Command Property Example (VB)](./parameters-collection-command-property-example-vb.md)   
 [Procedures Append Method Example (VB)](./procedures-append-method-example-vb.md)   
 [Procedures Delete Method Example (VB)](./procedures-delete-method-example-vb.md)   
 [Procedures Refresh Method Example (VB)](./procedures-refresh-method-example-vb.md)   
 [Procedures Collection Properties, Methods, and Events](./procedures-collection-properties-methods-and-events.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Procedure Object (ADOX)](./procedure-object-adox.md)