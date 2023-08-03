---
title: "Provider Support for ADOX (ADO)"
description: "Provider Support for ADOX (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADOX provider support [ADO]"
---
# Provider Support for ADOX (ADO)
Certain features of ADOX are unsupported, depending on your OLE DB data provider. ADOX is fully supported with the [OLE DB Provider for Microsoft Jet](../appendixes/microsoft-ole-db-provider-for-microsoft-jet.md). The unsupported features with the [Microsoft OLE DB Provider for SQL Server](../appendixes/microsoft-ole-db-provider-for-sql-server.md), the [Microsoft OLE DB Provider for ODBC](../appendixes/microsoft-ole-db-provider-for-odbc.md), or the [Microsoft OLE DB Provider for Oracle](../appendixes/microsoft-ole-db-provider-for-oracle.md) are listed in the following tables. ADOX is not supported by any other Microsoft OLE DB providers.  
  
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