---
title: "Update OPENXML XPath expressions to remove unsupported functions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "OPENXML queries"
ms.assetid: b459abaf-8787-4b65-9231-ae30e5469fd0
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Update OPENXML XPath expressions to remove unsupported functions
  Upgrade Advisor detected the use of XPath functionality. You may be affected by changes in XPath functionality for OPENXML features after you upgrade.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 MSXML 3.0 is now the underlying engine that is used to process XPath expressions that are used within OPENXML queries. MSXML 3.0 has a stricter XPath 1.0 engine in which support for the following functions has been removed:  
  
-   format-number()  
  
-   formatNumber()  
  
-   current()  
  
-   element-available()  
  
-   function-available()  
  
-   system-property()  
  
## Corrective Action  
 In the case of format-number() and formatNumber(), you can use [!INCLUDE[tsql](../../includes/tsql-md.md)]. For the other unsupported functions listed earlier, there is no direct workaround.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
