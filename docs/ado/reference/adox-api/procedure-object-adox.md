---
title: "Procedure Object (ADOX)"
description: "Procedure Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Procedure object [ADOX]"
apitype: "COM"
---
# Procedure Object (ADOX)
Represents a stored procedure. When used in conjunction with the ADO [Command](../ado-api/command-object-ado.md) object, the **Procedure** object can be used for adding, deleting, or modifying stored procedures.  
  
## Remarks  
 The **Procedure** object allows you to create a stored procedure without having to know or use the provider's "CREATE PROCEDURE" syntax.  
  
 With the properties of a **Procedure** object, you can:  
  
-   Identify the procedure with the [Name](./name-property-adox.md) property.  
  
-   Specify the ADO **Command** object that can be used to create or execute the procedure with the [Command](./command-property-adox.md) property.  
  
-   Return date information with the [DateCreated](./datecreated-property-adox.md) and [DateModified](./datemodified-property-adox.md) properties.  
  
 This section contains the following topic.  
  
-   [Procedure Object Properties, Methods, and Events](./procedure-object-properties-methods-and-events.md)  
  
## See Also  
 [Command and CommandText Properties Example (VB)](./command-and-commandtext-properties-example-vb.md)   
 [Parameters Collection, Command Property Example (VB)](./parameters-collection-command-property-example-vb.md)   
 [Procedures Append Method Example (VB)](./procedures-append-method-example-vb.md)   
 [Procedures Delete Method Example (VB)](./procedures-delete-method-example-vb.md)   
 [Procedures Collection (ADOX)](./procedures-collection-adox.md)