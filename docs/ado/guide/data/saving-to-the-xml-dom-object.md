---
title: "Saving to the XML DOM Object | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "XML DOM object [ADO], saving to"
ms.assetid: 4d20fd28-aaf8-4232-83ce-f9d1e5f93dae
caps.latest.revision: 3
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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