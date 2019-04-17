---
title: "Count Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Collection::Count"
helpviewer_keywords: 
  - "Count property [ADO]"
ms.assetid: da9ccd1f-d402-41a2-940c-45556fc5340d
author: MightyPen
ms.author: genemi
manager: craigg
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
 [Count Property Example (VB)](../../../ado/reference/ado-api/count-property-example-vb.md)   
 [Count Property Example (VC++)](../../../ado/reference/ado-api/count-property-example-vc.md)   
 [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)
