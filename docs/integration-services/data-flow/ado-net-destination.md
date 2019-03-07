---
title: "ADO NET Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.adonetdest.f1"
  - "sql13.dts.designer.adonetdest.connection.f1"
  - "sql13.dts.designer.adonetdest.mappings.f1"
  - "sql13.dts.designer.adonetdest.erroroutput.f1"
helpviewer_keywords: 
  - "destinations [Integration Services], ADO.NET"
  - "ADO.NET destination"
ms.assetid: cb883990-d875-4d8b-b868-45f9f15ebeae
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ADO NET Destination
  The ADO NET destination loads data into a variety of [!INCLUDE[vstecado](../../includes/vstecado-md.md)]-compliant databases that use a database table or view. You have the option of loading this data into an existing table or view, or you can create a new table and load the data into the new table.  
  
 You can use the ADO NET destination to connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)]. Connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using OLE DB is not supported. For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)], see [General Guidelines and Limitations (Windows Azure SQL Database)](https://go.microsoft.com/fwlink/?LinkId=248228).  
  
## Troubleshooting the ADO NET Destination  
 You can log the calls that the ADO NET destination makes to external data providers. You can use this logging capability to troubleshoot the saving of data to external data sources that the ADO NET destination performs. To log the calls that the ADO NET destination makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the ADO NET Destination  
 This destination uses an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to connect to a data source and the connection manager specifies the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] provider to use. For more information, see [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md).  
  
 An ADO NET destination includes mappings between input columns and columns in the destination data source. You do not have to map input columns to all destination columns. However, the properties of some destination columns can require the mapping of input columns. Otherwise, errors might occur. For example, if a destination column does not allow for null values, you must map an input column to that destination column. In addition, the data types of mapped columns must be compatible. For example, you cannot map an input column with a string data type to a destination column with a numeric data type if the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] provider does not support this mapping.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support inserting text into columns whose data type is set to image. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
> [!NOTE]  
>  The ADO NET destination does not support mapping an input column whose type is set to DT_DBTIME to a database column whose type is set to datetime. For more information about [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 The ADO NET destination has one regular input and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [ADO NET Custom Properties](../../integration-services/data-flow/ado-net-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## ADO NET Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **ADO NET Destination Editor** dialog box to select the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection for the destination. This page also lets you select a table or view from the database.  
  
 **To open the Connection Manager page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Connection Manager**.  
  
### Static Options  
 **Connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure ADO.NET Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new table by clicking **New**..  
  
 **New**  
 Create a new table or view by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
 **Use bulk insert when available**  
 Specify whether to use the <xref:System.Data.SqlClient.SqlBulkCopy> interface to improve the performance of bulk insert operations.  
  
 Only ADO.NET providers that return a <xref:System.Data.SqlClient.SqlConnection> object support the use of the <xref:System.Data.SqlClient.SqlBulkCopy> interface. The .NET Data Provider for SQL Server (SqlClient) returns a <xref:System.Data.SqlClient.SqlConnection> object, and a custom provider may return a <xref:System.Data.SqlClient.SqlConnection> object.  
  
 You can use the .NET Data Provider for SQL Server (SqlClient) to connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)].  
  
 If you select **Use bulk insert when available**, and set the **Error** option to **Redirect the row**, the batch of data that the destination redirects to the error output may include good rows.For more information about handling errors in bulk operations, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md). For more information about the **Error** option, see [ADO NET Destination Editor &#40;Error Output Page&#41;](../../integration-services/data-flow/ado-net-destination-editor-error-output-page.md).  
  
> [!NOTE]
>  If a SQL Server or Sybase source table includes an identity column, you must use Execute SQL tasks to enable IDENTITY_INSERT before the ADO NET destination and to disable it again afterward. (The identity column property specifies an incremental value for the column. The SET IDENTITY_INSERT statement lets explicit values from the source table be inserted into the identity column in the destination table.)  
> 
>   To run the SET IDENTITY_INSERT statements and the data loading successfully, you have to do the following things.  
>       1. Use the same ADO.NET connection manager for the Execute SQL tasks and for the ADO.NET destination.  
>       2. On the connection manager, set the **RetainSameConnection** property and the **MultipleActiveResultSets** property to True.  
>       3. On the ADO.NET destination, set the **UseBulkInsertWhenPossible** property to False.   
> 
>  For more information, see [SET IDENTITY_INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/set-identity-insert-transact-sql.md) and [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md).  
  
## External Resources  
 Technical article, [Loading data to Windows Azure SQL Database the fast way](https://go.microsoft.com/fwlink/?LinkId=244333), on sqlcat.com  
  
## ADO NET Destination Editor (Mappings Page)
  Use the **Mappings** page of the **ADO NET Destination Editor** dialog box to map input columns to destination columns.  
  
 **To open the Mappings page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Mappings**.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns in the table to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns in the table to input columns.  
  
 **Input Column**  
 View the input columns that you selected. You can remove mappings by selecting **\<ignore>** to exclude columns from the output.  
  
 **Destination Column**  
 View each available destination column, regardless of whether it is mapped or not.  
  
## ADO NET Destination Editor (Error Output Page)
  Use the **Error Output** page of the **ADO NET Destination Editor** dialog box to specify error handling options.  
  
 **To open the Error Output page**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the ADO NET destination.  
  
2.  On the **Data Flow** tab, double-click the ADO NET destination.  
  
3.  In the **ADO NET Destination Editor**, click **Error Output**.  
  
### Options  
 **Input or Output**  
 View the name of the input.  
  
 **Column**  
 Not used.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Not used.  
  
 **Description**  
 View the description of the operation.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
  
