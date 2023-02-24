---
title: "Saving to the XML DOM Object"
description: "Saving to the XML DOM Object"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "XML DOM object [ADO], saving to"
---
# Saving to the XML DOM Object
You can save a Recordset in XML format to an instance of an MSXML DOM object, as shown in the following Visual Basic code:  
  
```  
Dim xDOM As New MSXML.DOMDocument  
Dim rsXML As New ADODB.Recordset  
Dim sSQL As String, sConn As String  
  
sSQL = "SELECT customerid, companyname, contactname FROM customers"  
sConn="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Program Files" & _  
        "\Common Files\System\msadc\samples\NWind.mdb"  
rsXML.Open sSQL, sConn  
rsXML.Save xDOM, adPersistADO   'Save Recordset directly into a DOM tree.  
...  
```  
  
## See Also  
 [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md)
