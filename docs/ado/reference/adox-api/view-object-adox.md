---
title: "View Object (ADOX)"
description: "View Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "View object [ADOX]"
apitype: "COM"
---
# View Object (ADOX)
Represents a filtered set of records or a virtual table. When used in conjunction with the ADO [Command](../ado-api/command-object-ado.md) object, the **View** object can be used for adding, deleting, or modifying views.  
  
## Remarks  
 A view is a virtual table, created from other database tables or views. The **View** object allows you to create a view without having to know or use the provider's "CREATE VIEW" syntax.  
  
 With the properties of a **View** object, you can:  
  
-   Identify the view with the [Name](./name-property-adox.md) property.  
  
-   Specify the ADO **Command** object that can be used to add, delete, or modify views with the [Command](./command-property-adox.md) property.  
  
-   Return date information with the [DateCreated](./datecreated-property-adox.md) and [DateModified](./datemodified-property-adox.md) properties.  
  
 This section contains the following topic.  
  
-   [View Object Properties, Methods, and Events](./view-object-properties-methods-and-events.md)  
  
## See Also  
 [Views and Fields Collections Example (VB)](./views-and-fields-collections-example-vb.md)   
 [Views Append Method Example (VB)](./views-append-method-example-vb.md)   
 [Views Collection, CommandText Property Example (VB)](./views-collection-commandtext-property-example-vb.md)   
 [Views Delete Method Example (VB)](./views-delete-method-example-vb.md)   
 [Views Collection (ADOX)](./views-collection-adox.md)