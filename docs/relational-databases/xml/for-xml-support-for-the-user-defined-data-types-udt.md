---
title: "FOR XML Support for the User-Defined Data Types (UDT) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "UDTs [SQL Server], XML"
  - "user-defined types [SQL Server], XML"
ms.assetid: 354e2150-fa2a-4583-b1aa-6b78ae4378b6
author: MightyPen
ms.author: genemi
manager: craigg
---
# FOR XML Support for the User-Defined Data Types (UDT)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  FOR XML does not support common language runtime (CLR) user-defined data types (UDTs).  
  
 To use FOR XML with CLR user-defined data types, make sure that the data type has an XML serialization, and use an explicit cast to XML in the FOR XML select clause.  
  
## See Also  
 [FOR XML Support for Various SQL Server Data Types](../../relational-databases/xml/for-xml-support-for-various-sql-server-data-types.md)  
  
  
