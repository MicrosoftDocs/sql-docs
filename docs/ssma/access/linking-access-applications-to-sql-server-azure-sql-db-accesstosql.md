---
title: "Link Access Applications to SQL Server - Azure SQL Database | Microsoft Docs"
description: Learn how to link your Access tables to the migrated tables so that you can use your existing Access applications with SQL Server or Azure SQL Database.
ms.service: sql
ms.custom: ""
ms.date: "08/17/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Access databases, linking to SQL Azure"
  - "Access databases, linking to SQL Server"
  - "auto-increment columns"
  - "data types, unsupported"
  - "hyperlink columns"
  - "linking tables"
  - "migrating databases, post-migration issues"
  - "post-migration issues"
  - "reference, post-migration issues"
  - "refreshing linked tables"
  - "slow performance"
  - "unlinking tables"
ms.assetid: 82374ad2-7737-4164-a489-13261ba393d4
author: cpichuka 
ms.author: cpichuka 
---
# Linking Access applications to SQL Server - Azure SQL Database (AccessToSQL)
If you want to use your existing Access applications with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can link your original Access tables to the migrated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables. Linking modifies your Access database so that your queries, forms, reports, and data access pages use the data in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database instead of the data in your Access database.  
  
> [!NOTE]  
> Your Access tables remain in Access, but are not updated together with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure updates. After you link the tables and verify functionality, you might want to delete your Access tables.  
  
## Linking Access and SQL Server tables  
When you link an Access table to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table, the Jet database engine stores connection information and table metadata, but the data is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. This linking allows your Access applications operate against the Access tables even though the actual tables and data are in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.  
  
> [!NOTE]  
> If you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, your password is stored in clear text on the linked Access tables. We recommend using Windows Authentication.  
  
**To link tables**  
  
1.  In Access Metadata Explorer, select the tables that you want to link.  
  
2.  Right-click **Tables**, and then select **Link**.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Access backs up the original Access table and creates a linked table.  
  
After you link the tables, the tables in SSMA appear with a small link icon. In Access, the tables appear with a "linked" icon, which is a globe with an arrow pointing to it.  
  
When you open a table in Access, the data is retrieved using a keyset cursor. As a result, for large tables, all the data is not retrieved at one time. However, as you browse through the table, Access retrieves additional data as necessary.  
  
> [!IMPORTANT]  
> To link access tables with an Azure database, you need SQL Server Native Client(SNAC) version 10.5 or above.   
> You can obtain the latest version of SNAC from [Microsoft® SQL Server® 2008 R2 Feature Pack](https://www.microsoft.com/download/details.aspx?id=44272).  
  
## Unlinking Access tables  
When you unlink an Access table from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table, SSMA restores the original Access table and its data.  
  
**To unlink tables**  
  
1.  In Access Metadata Explorer, select the tables that you want to unlink.  
  
2.  Right-click **Tables**, and then select **Unlink**.  
  
## Linking tables to a different server  
If you have linked the Access tables to one SQL Server instance and you later want to change the links to another instance, you must relink the tables.  
  
**To link tables to a different server**  
  
1.  In Access Metadata Explorer, select the tables that you want to unlink.  
  
2.  Right-click **Tables** and then select **Unlink**.  
  
3.  Click the **Reconnect to SQL Server** button.  
  
4.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure to which you want to link the Access tables.  
  
5.  In Access Metadata Explorer, select the tables that you want to link.  
  
6.  Right-click **Tables**, and then select **Link**.  
  
## Updating linked tables  
If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table definitions are altered, you can unlink and then re-link the tables in SSMA by using the procedures shown previously in this topic. You can also update the tables by using Access.  
  
**To update linked tables by using Access**  
  
1.  Open the Access database.  
  
2.  In the **Objects** list, click **Tables**.  
  
3.  Right-click a linked table, and then select **Linked Table Manager**.  
  
4.  Select the check box next to each linked table that you want to update, and then click **OK**.  
  
## Possible post-migration issues  
The following sections list issues that might occur in existing Access applications after you migrate databases from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure and then link the tables, together with the causes and the resolutions.  
  
### Slow performance with linked tables  
**Cause:** Some queries might be slow after upsizing for the following reasons:  
  
-   The application depends on functions that do not exist in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, which causes Jet to pull down tables locally to run a SELECT query.  
  
-   Queries that update or delete many rows are sent by Jet as a parameterized query for each row.  
  
**Resolution:** Convert the slow-running queries to pass-through queries, stored procedures, or views. Converting to pass-through queries has the following issues:  
  
-   Pass-through queries cannot be modified. Modifying the query result or adding new records must be done in an alternative way, such as by having explicit **Modify** or **Add** buttons on your form that is bound to the query.  
  
-   Some queries require user input, but pass-through queries do not support user input. User input can be obtained by Visual Basic for Applications (VBA) code that prompts for parameters, or by a form that is used as an input control. In both cases, the VBA code submits the query with the user input to the server.  
  
### Auto-increment columns are not updated until the record is updated  
**Cause:** After calling RecordSet.AddNew in Jet, the auto increment column is available before the record is updated. This is not true in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. The new value of the identity column new value is available only after saving the new record.  
  
**Resolution:** Run the following Visual Basic for Applications (VBA) code before accessing the identity field:  
  
```  
Recordset.Update  
Recordset.Move 0,  
Recordset.LastModified  
```  
  
### New records are not available  
**Cause:** When you add a record to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table by using VBA, if the table's unique index field has a default value and you do not assign a value to that field, the new record does not appear until you reopen the table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. If you try to obtain a value from the new record, you receive the following error message:  
  
`Run-time error '3167' Record is deleted.`  
  
**Resolution:** When you open the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure table by using VBA code, include the `dbSeeChanges` option, as in the following example:  
  
`Set rs = db.OpenRecordset("TestTable", dbOpenDynaset, dbSeeChanges)`  
  
### After migration, some queries will not allow the user to add a new record  
**Cause:** If a query does not include all columns that are included in a unique index, you cannot add new values by using the query.  
  
**Resolution:** Ensure that all columns included in at least one unique index are part of the query.  
  
### You cannot modify a linked table schema with Access  
**Cause:** After migrating data and linking tables, the user cannot modify the schema of a table in Access.  
  
**Resolution:** Modify the table schema by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then update the link in Access.  
  
### Hyperlink functionality is lost after migrating data  
**Cause:** After migrating data, hyperlinks in columns lose their functionality and become simple **nvarchar(max)** columns.  
  
**Resolution:** None.  
  
### Some SQL Server data types are not supported by Access  
**Cause:** If you later update your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables to contain data types that are not supported by Access, you cannot open the table in Access.  
  
**Resolution:** You can define an Access query that returns only those rows with supported data types.  
  
## See also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
  
