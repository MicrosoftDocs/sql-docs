---
title: "More About Recordset Persistence"
description: "More About Recordset Persistence"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "persisting data [ADO]"
  - "data updates [ADO], persisting data"
  - "data persistence [ADO]"
  - "updating data [ADO], persisting data"
---
# More About Recordset Persistence
The ADO Recordset object supports storing the contents of a **Recordset** object in a file by using its [Save](../../reference/ado-api/save-method.md) method. The persistently stored file may exist on a local drive, server, or as a URL on a Web site. Later, the file can be restored with either the [Open](../../reference/ado-api/open-method-ado-recordset.md) method of the **Recordset** object or the [Execute](../../reference/ado-api/execute-method-ado-connection.md) method of the [Connection](../../reference/ado-api/connection-object-ado.md) object.  
  
 In addition, the [GetString](../../reference/ado-api/getstring-method-ado.md) method converts a **Recordset** object to a form in which the columns and rows are delimited with characters you specify.  
  
 To persist a **Recordset**, begin by converting it to a form that can be stored in a file. **Recordset** objects can be stored in the proprietary Advanced Data TableGram (ADTG) format or the open Extensible Markup Language (XML) format. ADTG examples are shown in the next section. For more information about XML persistence, see [Persisting Records in XML format](./persisting-records-in-xml-format.md).  
  
 Save any pending changes in the persisted file. Doing this allows you to issue a query that returns a **Recordset** object, edits the **Recordset**, saves it and the pending changes, later restores the **Recordset**, and then updates the data source with the saved pending changes.  
  
 For information about persistently storing **Stream** objects, see [Streams and Persistence](./streams-and-persistence.md).  
  
 For an example of **Recordset** persistence, see the XML Recordset Persistence Scenario.  
  
## Example  
  
### Save a Recordset:  
  
```  
Dim rs as New ADODB.Recordset  
rs.Save "c:\yourFile.adtg", adPersistADTG  
```  
  
### Open a persisted file with Recordset.Open:  
  
```  
Dim rs as New ADODB.Recordset  
rs.Open "c:\yourFile.adtg", "Provider=MSPersist",,,adCmdFile  
```  
  
 Optionally, if the **Recordset** does not have an active connection, you can accept all the defaults and code the following:  
  
```  
Dim rs as New ADODB.Recordset  
rs.Open "c:\yourFile.adtg"  
```  
  
### Open a persisted file with Connection.Execute:  
  
```  
Dim conn as New ADODB.Connection  
Dim rs as ADODB.Recordset  
conn.Open "Provider=MSPersist"  
Set rs = conn.execute("c:\yourFile.adtg")  
```  
  
### Open a persisted file with RDS.DataControl:  
 In this case, the **Server** property is not set.  
  
```  
Dim dc as New RDS.DataControl  
dc.Connection = "Provider=MSPersist"  
dc.SQL = "c:\yourFile.adtg"  
dc.Refresh  
```  
  
## See Also  
 [GetString Method (ADO)](../../reference/ado-api/getstring-method-ado.md)   
 [Microsoft OLE DB Persistence Provider (ADO Service Provider)](../appendixes/microsoft-ole-db-persistence-provider-ado-service-provider.md)   
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)   
 [Streams and Persistence](./streams-and-persistence.md)