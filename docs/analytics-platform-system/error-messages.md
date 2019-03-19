---
title: Error messages - Parallel Data Warehouse | Microsoft Docs
description: Parallel Data Warehouse (PDW) error messages report errors and problems encountered by the PDW components and can also include SQL Server errors surfaced through PDW. These error messages use a consistent syntax for presenting information. Understanding this syntax will allow you to identify and correct problems.  
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---
# Error messages in Parallel Data Warehouse

Parallel Data Warehouse (PDW) error messages report errors and problems encountered by the PDW components and can also include SQL Server errors surfaced through PDW. These error messages use a consistent syntax for presenting information. Understanding this syntax will allow you to identify and correct problems on SQL Server PDW.  
  
## <a name="Basics"></a>Error Message Basics  
Error messages that are returned follow the same syntax.  
  
`Error_Indicator [SQL_State_Code] [Driver_Details] [QueryID] Message_String`  
  
These are the potential values for each field:  
  
|Field|Description|Example|  
|---------|---------------|-----------|  
|*Error_Indicator*|The word "ERROR" or other text alerting the user to a problem.|ERROR|  
|*SQL_State_Code*|The SQL state code, according to ODBC specification. The driver generates the appropriate SQL State code any time it returns a message to an application. The text "Microsoft" indicates the source of the error.|42000|  
|*Driver_Details*|Driver-dependent details, such as the type of driver used.|ODBC SQL Server 2008 R2 Parallel Data Warehouse driver|  
|*QueryID*|The unique identifier for the query. Use this value to find additional information related to processing of the query. For example, the query execution details can be found in the Admin Console using the query ID. For more information, see [Monitor the Appliance by Using the Admin Console](monitor-the-appliance-by-using-the-admin-console.md).<br /><br />If a QueryID is not applicable, the text "Internal" is returned to the user.|QID2377|  
|*Message_String*|A human-readable description of the error or problem. When returning SQL Server errors, this is the SQL Server message text.|Only equal assignment can appear in the set list of an UPDATE statement.|  
  
These example values would be presented to the user like this:  
  
`ERROR [42000] [Microsoft][ODBC SQL Server 2008 R2 Parallel Data Warehouse driver][QID2380]Only equal assignment can appear in the set list of an UPDATE statement.`  
  
## See Also  
<!-- MISSING LINKS 
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
[Understanding Admin Console Alerts](understanding-admin-console-alerts.md)  
  
