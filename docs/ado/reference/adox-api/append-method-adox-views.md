---
title: "Append Method (ADOX Views) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Views::raw_Append"
  - "Views::Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 6070fd58-3237-4c77-a966-5b39ce5d57e4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Views)
Creates a new [View](../../../ado/reference/adox-api/view-object-adox.md) object and appends it to the [Views](../../../ado/reference/adox-api/views-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Views.Append Name, Command  
```  
  
#### Parameters  
 *Name*  
 A **String** value that specifies the name of the view to create.  
  
 *Command*  
 An ADO [Command](../../../ado/reference/ado-api/command-object-ado.md) object that represents the view to create.  
  
## Remarks  
 Creates a new view in the data source with the name and attributes specified in the **Command** object.  
  
 If the command text that the user specifies represents a procedure rather than a view, the behavior is dependent upon the provider. **Append** will fail if the provider does not support persisting commands.  
  
> [!NOTE]
>  When using the OLE DB Provider for Microsoft Jet, the **Views** collection **Append** method will allow you to specify a **Procedure** rather than a **View** in the *Command* parameter. The **Procedure** will be added to the data source and will be added to the **Views** collection. After the **Append**, if the **Procedures** and **Views** collections are refreshed, the **Procedure** will no longer be in the **Views** collection and will appear in the **Procedures** collection.  
  
## Applies To  
 [Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)  
  
## See Also  
 [Views Append Method Example (VB)](../../../ado/reference/adox-api/views-append-method-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)
