---
title: "ADO NET Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.adonetdest.connection.f1"
ms.assetid: a3b11286-32c8-40e1-8ae7-090e2590345a
author: janinezhang
ms.author: janinez
manager: craigg
---
# ADO NET Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ADO NET Destination Editor** dialog box to select the [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection for the destination. This page also lets you select a table or view from the database.  
  
 To learn more about the ADO NET destination, see [ADO NET Destination](data-flow/ado-net-destination.md).  
  
 **To open the Connection Manager page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Connection Manager**.  
  
## Static Options  
 **Connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure ADO.NET Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new table by clicking **New**..  
  
 **New**  
 Create a new table or view by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
 **Use bulk insert when available**  
 Specify whether to use the <xref:System.Data.SqlClient.SqlBulkCopy> interface to improve the performance of bulk insert operations.  
  
 Only ADO.NET providers that return a <xref:System.Data.SqlClient.SqlConnection> object support the use of the <xref:System.Data.SqlClient.SqlBulkCopy> interface. The .NET Data Provider for SQL Server (SqlClient) returns a <xref:System.Data.SqlClient.SqlConnection> object, and a custom provider may return a <xref:System.Data.SqlClient.SqlConnection> object.  
  
 You can use the .NET Data Provider for SQL Server (SqlClient) to connect to [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)].  
  
 If you select **Use bulk insert when available**, and set the **Error** option to **Redirect the row**, the batch of data that the destination redirects to the error output may include good rows.For more information about handling errors in bulk operations, see [Error Handling in Data](data-flow/error-handling-in-data.md). For more information about the **Error** option, see [ADO NET Destination Editor &#40;Error Output Page&#41;](../../2014/integration-services/ado-net-destination-editor-error-output-page.md).  
  
> [!NOTE]  
>  If a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or Sybase source table includes an identity column, you must use Execute SQL tasks to run a SET IDENTITY_INSERT statement before and after the ADO NET destination. The identity column property specifies an incremental value for the column. The SET IDENTITY_INSERT statement enables explicit values to be inserted into the identity column. To run the CREATE TABLE and SET IDENTITY statements on the same database connection, set the `RetainSameConnection` property of the [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager to `True`. Also, use the same [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager for the Execute SQL tasks and the ADO NET destination.  
>   
>  For more information, see [SET IDENTITY_INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-identity-insert-transact-sql) and [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql-identity-property).  
  
## External Resources  
 Technical article, [Loading data to Windows Azure SQL Database the fast way](https://go.microsoft.com/fwlink/?LinkId=244333), on sqlcat.com  
  
## See Also  
 [ADO NET Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/ado-net-destination-editor-mappings-page.md)   
 [ADO NET Destination Editor &#40;Error Output Page&#41;](../../2014/integration-services/ado-net-destination-editor-error-output-page.md)   
 [ADO.NET Connection Manager](connection-manager/ado-net-connection-manager.md)   
 [Execute SQL Task](control-flow/execute-sql-task.md)  
  
  
