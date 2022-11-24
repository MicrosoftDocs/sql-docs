---
title: "Item Property (ADO)"
description: "Item Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
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
apitype: "COM"
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

:::row:::
    :::column:::
        [Axes Collection (ADO MD)](../ado-md-api/axes-collection-ado-md.md)  
        [Columns Collection (ADOX)](../adox-api/columns-collection-adox.md)  
        [CubeDefs Collection (ADO MD)](../ado-md-api/cubedefs-collection-ado-md.md)  
        [Dimensions Collection (ADO MD)](../ado-md-api/dimensions-collection-ado-md.md)  
        [Errors Collection (ADO)](./errors-collection-ado.md)  
        [Fields Collection (ADO)](./fields-collection-ado.md)  
        [Groups Collection (ADOX)](../adox-api/groups-collection-adox.md)  
    :::column-end:::
    :::column:::
        [Hierarchies Collection (ADO MD)](../ado-md-api/hierarchies-collection-ado-md.md)  
        [Indexes Collection (ADOX)](../adox-api/indexes-collection-adox.md)  
        [Keys Collection (ADOX)](../adox-api/keys-collection-adox.md)  
        [Levels Collection (ADO MD)](../ado-md-api/levels-collection-ado-md.md)  
        [Members Collection (ADO MD)](../ado-md-api/members-collection-ado-md.md)  
        [Parameters Collection (ADO)](./parameters-collection-ado.md)  
    :::column-end:::
    :::column:::
        [Positions Collection (ADO MD)](../ado-md-api/positions-collection-ado-md.md)  
        [Procedures Collection (ADOX)](../adox-api/procedures-collection-adox.md)  
        [Properties Collection (ADO)](./properties-collection-ado.md)  
        [Tables Collection (ADOX)](../adox-api/tables-collection-adox.md)  
        [Users Collection (ADOX)](../adox-api/users-collection-adox.md)  
        [Views Collection (ADOX)](../adox-api/views-collection-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Item Property Example (VB)](./item-property-example-vb.md)   
 [Item Property Example (VC++)](./item-property-example-vc.md)