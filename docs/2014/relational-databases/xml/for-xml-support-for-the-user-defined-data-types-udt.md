---
title: "FOR XML Support for the User-Defined Data Types (UDT) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-xml"
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "UDTs [SQL Server], XML"
  - "user-defined types [SQL Server], XML"
ms.assetid: 354e2150-fa2a-4583-b1aa-6b78ae4378b6
caps.latest.revision: 22
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# FOR XML Support for the User-Defined Data Types (UDT)
  FOR XML does not support common language runtime (CLR) user-defined data types (UDTs).  
  
 To use FOR XML with CLR user-defined data types, make sure that the data type has an XML serialization, and use an explicit cast to XML in the FOR XML select clause.  
  
## See Also  
 [FOR XML Support for Various SQL Server Data Types](for-xml-support-for-various-sql-server-data-types.md)  
  
  
