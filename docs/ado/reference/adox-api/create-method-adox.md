---
title: "Create Method (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Catalog::raw_Create"
  - "_Catalog::Create"
helpviewer_keywords: 
  - "Create method [ADOX]"
ms.assetid: 64f5c21c-b581-42d8-bdc7-c4f1bebaf105
author: MightyPen
ms.author: genemi
manager: craigg
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
 The **Create** method creates and opens a new ADO [Connection](../../../ado/reference/ado-api/connection-object-ado.md) to the data source specified in *ConnectString*. If successful, the new **Connection** object is assigned to the [ActiveConnection](../../../ado/reference/adox-api/activeconnection-property-adox.md) property.  
  
 An error will occur if the provider does not support creating new catalogs.  
  
## Applies To  
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)  
  
## See Also  
 [Create Method Example (VB)](../../../ado/reference/adox-api/create-method-example-vb.md)   
 [ActiveConnection Property (ADOX)](../../../ado/reference/adox-api/activeconnection-property-adox.md)
