---
title: "Procedure Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Procedure"
helpviewer_keywords: 
  - "Procedure object [ADOX]"
ms.assetid: 927bcf3e-32f5-4a80-98d3-600779f0732e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedure Object (ADOX)
Represents a stored procedure. When used in conjunction with the ADO [Command](../../../ado/reference/ado-api/command-object-ado.md) object, the **Procedure** object can be used for adding, deleting, or modifying stored procedures.  
  
## Remarks  
 The **Procedure** object allows you to create a stored procedure without having to know or use the provider's "CREATE PROCEDURE" syntax.  
  
 With the properties of a **Procedure** object, you can:  
  
-   Identify the procedure with the [Name](../../../ado/reference/adox-api/name-property-adox.md) property.  
  
-   Specify the ADO **Command** object that can be used to create or execute the procedure with the [Command](../../../ado/reference/adox-api/command-property-adox.md) property.  
  
-   Return date information with the [DateCreated](../../../ado/reference/adox-api/datecreated-property-adox.md) and [DateModified](../../../ado/reference/adox-api/datemodified-property-adox.md) properties.  
  
 This section contains the following topic.  
  
-   [Procedure Object Properties, Methods, and Events](../../../ado/reference/adox-api/procedure-object-properties-methods-and-events.md)  
  
## See Also  
 [Command and CommandText Properties Example (VB)](../../../ado/reference/adox-api/command-and-commandtext-properties-example-vb.md)   
 [Parameters Collection, Command Property Example (VB)](../../../ado/reference/adox-api/parameters-collection-command-property-example-vb.md)   
 [Procedures Append Method Example (VB)](../../../ado/reference/adox-api/procedures-append-method-example-vb.md)   
 [Procedures Delete Method Example (VB)](../../../ado/reference/adox-api/procedures-delete-method-example-vb.md)   
 [Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)
