---
title: "Create Method (ADOX)"
description: "Create Method (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Catalog::raw_Create"
  - "_Catalog::Create"
helpviewer_keywords:
  - "Create method [ADOX]"
apitype: "COM"
---
# Create Method (ADOX)
Creates a new catalog.  
  
## Syntax  
  
```  
  
Catalog.Create ConnectString  
```  
  
#### Parameters  
 *ConnectString*  
 A **String** value used to connect to the data source.  
  
## Remarks  
 The **Create** method creates and opens a new ADO [Connection](../ado-api/connection-object-ado.md) to the data source specified in *ConnectString*. If successful, the new **Connection** object is assigned to the [ActiveConnection](./activeconnection-property-adox.md) property.  
  
 An error will occur if the provider does not support creating new catalogs.  
  
## Applies To  
 [Catalog Object (ADOX)](./catalog-object-adox.md)  
  
## See Also  
 [Create Method Example (VB)](./create-method-example-vb.md)   
 [ActiveConnection Property (ADOX)](./activeconnection-property-adox.md)