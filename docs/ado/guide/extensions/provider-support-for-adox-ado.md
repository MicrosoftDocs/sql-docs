---
title: "Provider Support for ADOX (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "ADOX provider support [ADO]"
ms.assetid: 64234ce5-dc46-4c8a-a316-61956b6b9abb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Provider Support for ADOX (ADO)
Certain features of ADOX are unsupported, depending on your OLE DB data provider. ADOX is fully supported with the [OLE DB Provider for Microsoft Jet](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md). The unsupported features with the [Microsoft OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md), the [Microsoft OLE DB Provider for ODBC](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md), or the [Microsoft OLE DB Provider for Oracle](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-oracle.md) are listed in the following tables. ADOX is not supported by any other Microsoft OLE DB providers.  
  
## Microsoft OLE DB Provider for SQL Server  
  
|Object or Collection|Usage Restriction|  
|--------------------------|-----------------------|  
|**Tables** collection|Properties are read/write prior to object creation, and read-only when referencing an existing object.|  
|**Views** collection|**Views** is not supported.|  
|**Procedures** collection|The **Append** and **Delete** methods are not supported.|  
|**Procedure** object|The **Command** property is not supported.|  
|**Keys** collection|The **Append** and **Delete** methods are not supported.|  
|**Users** collection|**Users** is not supported.|  
|**Groups** collection|**Groups** is not supported.|  
  
## Microsoft OLE DB Provider for ODBC  
  
|Object or Collection|Usage Restriction|  
|--------------------------|-----------------------|  
|**Catalog** object|The **Create** method is not supported.|  
|**Tables** collection|The **Append** and **Delete** methods are not supported. Properties are read/write prior to object creation, and read-only when referencing an existing object.|  
|**Procedures** collection|The **Append** and **Delete** methods are not supported.|  
|**Procedure** object|The **Command** property is not supported.|  
|**Indexes** collection|The **Append** and **Delete** methods are not supported.|  
|**Keys** collection|The **Append** and **Delete** methods are not supported.|  
|**Users** collection|**Users** is not supported.|  
|**Groups** collection|**Groups** is not supported.|  
  
## Microsoft OLE DB Provider for Oracle  
  
|Object or Collection|Usage Restriction|  
|--------------------------|-----------------------|  
|**Catalog** object|The **Create** method is not supported.|  
|**Tables** collection|The **Append** and **Delete** methods are not supported. Properties are read/write prior to object creation, and read-only when referencing an existing object.|  
|**Views** collection|The **Append** and **Delete** methods are not supported.|  
|**View** object|The **Command** property is not supported.|  
|**Procedures** object|The **Append** and **Delete** methods are not supported.|  
|**Procedure** object|The **Command** property is not supported.|  
|**Indexes** collection|The **Append** and **Delete** methods are not supported.|  
|**Keys** collection|The **Append** and **Delete** methods are not supported.|  
|**Users** collection|**Users** is not supported.|  
|**Groups** collection|**Groups** is not supported.|
