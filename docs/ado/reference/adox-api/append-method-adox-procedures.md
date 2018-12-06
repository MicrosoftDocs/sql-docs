---
title: "Append Method (ADOX Procedures) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Procedures::Append"
  - "Procedures::raw_Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 38e3492c-c1e1-42e3-a71a-befdc90204db
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Procedures)
Adds a new [Procedure](../../../ado/reference/adox-api/procedure-object-adox.md) object to the [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Procedures.Append Name, Command  
```  
  
#### Parameters  
 *Name*  
 A **String** value that specifies the name of the procedure to create and append.  
  
 *Command*  
 An ADO [Command](../../../ado/reference/ado-api/command-object-ado.md) object that represents the procedure to create and append.  
  
## Remarks  
 Creates a new procedure in the data source with the name and attributes specified in the **Command** object.  
  
 If the command text that the user specifies represents a view rather than a procedure, the behavior is dependent upon the provider being used. **Append** will fail if the provider does not support persisting commands.  
  
> [!NOTE]
>  When using the OLE DB Provider for Microsoft Jet, the **Procedures** collection **Append** method will allow you to specify a **View** rather than a **Procedure** in the *Command* parameter. The **View** will be added to the data source and will be added to the **Procedures** collection. After the **Append**, if the **Procedures** and **Views** collections are refreshed, the **View** will no longer be in the **Procedures** collection and will appear in the **Views** collection.  
  
## Applies To  
 [Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)  
  
## See Also  
 [Procedures Append Method Example (VB)](../../../ado/reference/adox-api/procedures-append-method-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
