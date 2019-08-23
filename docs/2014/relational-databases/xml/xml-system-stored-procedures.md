---
title: "XML System Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "system stored procedures [SQL Server], XML"
  - "sp_xml_removedocument"
  - "OPENXML statement, system stored procedures"
  - "sp_xml_preparedocument"
  - "XML [SQL Server], system stored procedures"
ms.assetid: e60c7f85-6823-4d28-93d6-b053d08cc830
author: MightyPen
ms.author: genemi
manager: craigg
---
# XML System Stored Procedures
  SQL Server provides the following system stored procedures that are used together with OPENXML:  
  
-   [sp_xml_preparedocument &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-xml-preparedocument-transact-sql)  
  
-   [sp_xml_removedocument &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-xml-removedocument-transact-sql)  
  
 To write queries by using OPENXML, you must first create an internal representation of the XML document by calling **sp_xml_preparedocument**. The stored procedure returns a handle to the internal representation of the XML document. This handle is then passed to OPENXML. OPENXML provides rowset views of the document based on XPaths. Specifically, this is one row pattern and one or more column patterns.  
  
> [!NOTE]  
>  The document handle that is returned by **sp_xml_preparedocument** is valid for the duration of the session.  
  
 The internal representation of an XML document can be removed from memory by calling the **sp_xml_removedocument** system stored procedure.  
  
## See Also  
 [OPENXML &#40;Transact-SQL&#41;](/sql/t-sql/functions/openxml-transact-sql)   
 [OPENXML &#40;SQL Server&#41;](../xml/openxml-sql-server.md)  
  
  
