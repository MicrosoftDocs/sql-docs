---
title: "Views Collection (ADOX)"
description: "Views Collection (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Catalog::Views"
  - "Views"
helpviewer_keywords:
  - "Views collection [ADOX]"
apitype: "COM"
---
# Views Collection (ADOX)
Contains all [View](./view-object-adox.md) objects of a catalog.  
  
## Remarks  
 The [Append](./append-method-adox-views.md) method for a **Views** collection is unique for ADOX. You can:  
  
-   Add a new view to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a view in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
-   Return the number of views contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Remove a view from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Views Collection Properties, Methods, and Events](./views-collection-properties-methods-and-events.md)  
  
## See Also  
 [Views and Fields Collections Example (VB)](./views-and-fields-collections-example-vb.md)   
 [Views Append Method Example (VB)](./views-append-method-example-vb.md)   
 [Views Collection, CommandText Property Example (VB)](./views-collection-commandtext-property-example-vb.md)   
 [Views Delete Method Example (VB)](./views-delete-method-example-vb.md)   
 [Views Refresh Method Example (VB)](./views-refresh-method-example-vb.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [View Object (ADOX)](./view-object-adox.md)