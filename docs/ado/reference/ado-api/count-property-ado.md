---
title: "Count Property (ADO)"
description: "Count Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Collection::Count"
helpviewer_keywords:
  - "Count property [ADO]"
apitype: "COM"
---
# Count Property (ADO)
Indicates the number of objects in a collection.  
  
## Return Value  
 Returns a **Long** value.  
  
## Remarks  
 Use the **Count** property to determine how many objects are in a given collection.  
  
 Because numbering for members of a collection begins with zero, you should always code loops starting with the zero member and ending with the value of the **Count** property minus 1. If you are using Microsoft Visual Basic and want to loop through the members of a collection without checking the **Count** property, use the **For Each...Next** command.  
  
 If the **Count** property is zero, there are no objects in the collection.  
  
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
 [Count Property Example (VB)](./count-property-example-vb.md)   
 [Count Property Example (VC++)](./count-property-example-vc.md)   
 [Refresh Method (ADO)](./refresh-method-ado.md)