---
title: "Item Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Parameters::GetItem"
  - "Indexes::GetItem"
  - "Parameters::Item"
  - "Tables::Item"
  - "Procedures::Item"
  - "Users::GetItem"
  - "Tables::GetItem"
  - "Procedures::GetItem"
  - "Users::get_Item"
  - "Users::Item"
  - "Views::GetItem"
  - "Groups::Item"
  - "Groups::get_Item"
  - "Columns::Item"
  - "Indexes::Item"
  - "Fields15::GetItem"
  - "Columns::GetItem"
  - "Fields::Item"
  - "Indexes::get_Item"
  - "Columns::get_Item"
  - "Fields15::Item"
  - "Views::get_Item"
  - "Groups::GetItem"
  - "Errors::get_Item"
  - "Fields15::get_Item"
  - "Tables::get_Item"
  - "Views::Item"
  - "Errors::GetItem"
  - "Parameters::get_Item"
  - "Errors::Item"
  - "Procedures::get_Item"
helpviewer_keywords: 
  - "Item property [ADO]"
ms.assetid: e11484bb-c5c7-42d8-9bb8-21572125d727
author: MightyPen
ms.author: genemi
manager: craigg
---
# Item Property (ADO)
Indicates a specific member of a collection, by name or ordinal number.  
  
## Syntax  
  
```  
Set object = collection.Item ( Index )  
```  
  
## Return Value  
 Returns an object reference.  
  
## Parameters  
 *Index*  
 A **Variant** expression that evaluates either to the name or to the ordinal number of an object in a collection.  
  
## Remarks  
 Use the **Item** property to return a specific object in a collection. If **Item** cannot find an object in the collection corresponding to the *Index* argument, an error occurs. Also, some collections don't support named objects; for these collections, you must use ordinal number references.  
  
 The **Item** property is the default property for all collections; therefore, the following syntax forms are interchangeable:  
  
```  
collection.Item (Index)  
collection (Index)  
```  
  
## Applies To  
  
||||  
|-|-|-|  
|[Axes Collection (ADO MD)](../../../ado/reference/ado-md-api/axes-collection-ado-md.md)|[Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)|[CubeDefs Collection (ADO MD)](../../../ado/reference/ado-md-api/cubedefs-collection-ado-md.md)|  
|[Dimensions Collection (ADO MD)](../../../ado/reference/ado-md-api/dimensions-collection-ado-md.md)|[Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)|[Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)|  
|[Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)|[Hierarchies Collection (ADO MD)](../../../ado/reference/ado-md-api/hierarchies-collection-ado-md.md)|[Indexes Collection (ADOX)](../../../ado/reference/adox-api/indexes-collection-adox.md)|  
|[Keys Collection (ADOX)](../../../ado/reference/adox-api/keys-collection-adox.md)|[Levels Collection (ADO MD)](../../../ado/reference/ado-md-api/levels-collection-ado-md.md)|[Members Collection (ADO MD)](../../../ado/reference/ado-md-api/members-collection-ado-md.md)|  
|[Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)|[Positions Collection (ADO MD)](../../../ado/reference/ado-md-api/positions-collection-ado-md.md)|[Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)|  
|[Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)|[Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)|[Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)|  
|[Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)|||  
  
## See Also  
 [Item Property Example (VB)](../../../ado/reference/ado-api/item-property-example-vb.md)   
 [Item Property Example (VC++)](../../../ado/reference/ado-api/item-property-example-vc.md)   
