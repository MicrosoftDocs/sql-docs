---
title: "Lookup Transformation Editor (Connection Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.lookuptransformation.referencetable.f1"
helpviewer_keywords: 
  - "Lookup Transformation Editor"
ms.assetid: e90d6b69-5a26-43c5-8433-5c3c9588e08c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lookup Transformation Editor (Connection Page)
  Use the **Connection** page of the **Lookup Transformation Editor** dialog box to select a connection manager. If you select an OLE DB connection manager, you also select a query, table, or view to generate the reference dataset.  
  
 To learn more about the Lookup transformation, see [Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 The following options are available when you select **Full cache** and **Cache connection manager** on the General page of the **Lookup Transformation Editor** dialog box.  
  
 **Cache connection manager**  
 Select an existing Cache connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Cache Connection Manager Editor** dialog box.  
  
 The following options are available when you select **Full cache**, **Partial cache**, or **No cache**, and **OLE DB connection manager**, on the General page of the **Lookup Transformation Editor** dialog box.  
  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new table by clicking **New**.  
  
> [!NOTE]  
>  If you specify a SQL statement on the **Advanced** page of the **Lookup Transformation Editor**, that SQL statement overrides and replaces the table name selected here. For more information, see [Lookup Transformation Editor &#40;Advanced Page&#41;](../../2014/integration-services/lookup-transformation-editor-advanced-page.md).  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
 **Use results of an SQL query**  
 Choose this option to browse to a preexisting query, build a new query, check query syntax, and preview query results.  
  
 **Build query**  
 Create the Transact-SQL statement to run by using **Query Builder**, a graphical tool that is used to create queries by browsing through data.  
  
 **Browse**  
 Use this option to browse to a preexisting query saved as a file.  
  
 **Parse Query**  
 Check the syntax of the query.  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. This option displays up to 200 rows.  
  
## External Resources  
 Blog entry, [Lookup cache modes](https://go.microsoft.com/fwlink/?LinkId=219518) on blogs.msdn.com  
  
## See Also  
 [Lookup Transformation Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Lookup Transformation Editor &#40;Columns Page&#41;](../../2014/integration-services/lookup-transformation-editor-columns-page.md)   
 [Lookup Transformation Editor &#40;Advanced Page&#41;](../../2014/integration-services/lookup-transformation-editor-advanced-page.md)   
 [Lookup Transformation Editor &#40;Error Output Page&#41;](../../2014/integration-services/lookup-transformation-editor-error-output-page.md)   
 [Fuzzy Lookup Transformation](data-flow/transformations/fuzzy-lookup-transformation.md)  
  
  
