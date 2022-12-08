---
title: "Append Method (ADOX Views)"
description: "Append Method (ADOX Views)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Views::raw_Append"
  - "Views::Append"
helpviewer_keywords:
  - "Append method [ADOX]"
apitype: "COM"
---
# Append Method (ADOX Views)
Creates a new [View](./view-object-adox.md) object and appends it to the [Views](./views-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Views.Append Name, Command  
```  
  
#### Parameters  
 *Name*  
 A **String** value that specifies the name of the view to create.  
  
 *Command*  
 An ADO [Command](../ado-api/command-object-ado.md) object that represents the view to create.  
  
## Remarks  
 Creates a new view in the data source with the name and attributes specified in the **Command** object.  
  
 If the command text that the user specifies represents a procedure rather than a view, the behavior is dependent upon the provider. **Append** will fail if the provider does not support persisting commands.  
  
> [!NOTE]
>  When using the OLE DB Provider for Microsoft Jet, the **Views** collection **Append** method will allow you to specify a **Procedure** rather than a **View** in the *Command* parameter. The **Procedure** will be added to the data source and will be added to the **Views** collection. After the **Append**, if the **Procedures** and **Views** collections are refreshed, the **Procedure** will no longer be in the **Views** collection and will appear in the **Procedures** collection.  
  
## Applies To  
 [Views Collection (ADOX)](./views-collection-adox.md)  
  
## See Also  
 [Views Append Method Example (VB)](./views-append-method-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)