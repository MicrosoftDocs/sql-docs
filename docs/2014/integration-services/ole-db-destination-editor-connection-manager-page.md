---
title: "OLE DB Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.oledbdestadapter.connection.f1"
helpviewer_keywords: 
  - "OLE DB Destination Editor"
ms.assetid: ae2200c6-8ba0-49b7-b01a-53425b84d2ed
author: janinezhang
ms.author: janinez
manager: craigg
---
# OLE DB Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **OLE DB Destination Editor** dialog box to select the OLE DB connection for the destination. This page also lets you select a table or view from the database.  
  
> [!NOTE]  
>  The `CommandTimeout` property of the OLE DB destination is not available in the **OLE DB Destination Editor**, but can be set by using the **Advanced Editor**. In addition, certain fast load options are available only in the **Advanced Editor**. For more information on these properties, see the OLE DB Destination section of [OLE DB Custom Properties](data-flow/ole-db-custom-properties.md).  
  
 To learn more about the OLE DB destination, see [OLE DB Destination](data-flow/ole-db-destination.md).  
  
## Static Options  
 **OLE DB connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for loading data into the destination. Loading double-byte character set (DBCS) data requires use of one of the fast load options. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](data-flow/ole-db-destination.md).  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Load data into a table or view in the OLE DB destination.|  
|Table or view - fast load|Load data into a table or view in the OLE DB destination and use the fast load option. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](data-flow/ole-db-destination.md).|  
|Table name or view name variable|Specify the table or view name in a variable.<br /><br /> **Related information**: [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md)|  
|Table name or view name variable - fast load|Specify the table or view name in a variable, and use the fast load option to load the data. For more information about the fast load data access modes, which are optimized for bulk inserts, see [OLE DB Destination](data-flow/ole-db-destination.md).|  
|SQL command|Load data into the OLE DB destination by using a SQL query.|  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
## Data Access Mode Dynamic Options  
 Each of the settings for **Data access mode** displays a dynamic set of options specific to that setting. The following sections describe each of the dynamic options available for each **Data access mode** setting.  
  
### Data access mode = Table or view  
 **Name of the table or the view**  
 Select the name of the table or view from a list of those available in the data source.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
### Data access mode = Table or view - fast load  
 **Name of the table or view**  
 Select a table or view from the database by using this list, or create a new table by clicking **New**.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Keep identity**  
 Specify whether to copy identity values when data is loaded. This property is available only with the fast load option. The default value of this property is `false`.  
  
 **Keep nulls**  
 Specify whether to copy null values when data is loaded. This property is available only with the fast load option. The default value of this property is `false`.  
  
 **Table lock**  
 Specify whether the table is locked during the load. The default value of this property is `true`.  
  
 **Check constraints**  
 Specify whether the destination checks constraints when it loads data. The default value of this property is `true`.  
  
 **Rows per batch**  
 Specify the number of rows in a batch. The default value of this property is **-1**, which indicates that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **OLE DB Destination Editor** to indicate that you do not want to assign a custom value for this property.  
  
 **Maximum insert commit size**  
 Specify the batch size that the OLE DB destination tries to commit during fast load operations. The value of **0** indicates that all data is committed in a single batch after all rows have been processed.  
  
> [!NOTE]  
>  A value of **0** might cause the running package to stop responding if the OLE DB destination and another data flow component are updating the same source table. To prevent the package from stopping, set the **Maximum insert commit size** option to **2147483647**.  
  
 If you provide a value for this property, the destination commits rows in batches that are the smaller of (a) the **Maximum insert commit size**, or (b) the remaining rows in the buffer that is currently being processed.  
  
> [!NOTE]  
>  Any constraint failure at the destination causes the entire batch of rows defined by **Maximum insert commit size** to fail.  
  
### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
### Data Access Mode = Table name or view name variable - fast load)  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Keep identity**  
 Specify whether to copy identity values when data is loaded. This property is available only with the fast load option. The default value of this property is `false`.  
  
 **Keep nulls**  
 Specify whether to copy null values when data is loaded. This property is available only with the fast load option. The default value of this property is `false`.  
  
 **Table lock**  
 Specify whether the table is locked during the load. The default value of this property is `false`.  
  
 **Check constraints**  
 Specify whether the task checks constraints. The default value of this property is `false`.  
  
 **Rows per batch**  
 Specify the number of rows in a batch. The default value of this property is **-1**, which indicates that no value has been assigned.  
  
> [!NOTE]  
>  Clear the text box in the **OLE DB Destination Editor** to indicate that you do not want to assign a custom value for this property.  
  
 **Maximum insert commit size**  
 Specify the batch size that the OLE DB destination tries to commit during fast load operations. The default value of **2147483647** indicates that all data is committed in a single batch after all rows have been processed.  
  
> [!NOTE]  
>  A value of **0** might cause the running package to stop responding if the OLE DB destination and another data flow component are updating the same source table. To prevent the package from stopping, set the **Maximum insert commit size** option to **2147483647**.  
  
### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of a SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
> [!NOTE]  
>  The OLE DB destination does not support parameters. If you need to execute a parameterized INSERT statement, consider the OLE DB Command transformation. For more information, see [OLE DB Command Transformation](data-flow/transformations/ole-db-command-transformation.md).  
  
 **Build query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse query**  
 Verify the syntax of the query text.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [OLE DB Destination Editor &#40;Mappings Page&#41;](../../2014/integration-services/ole-db-destination-editor-mappings-page.md)   
 [OLE DB Destination Editor &#40;Error Output Page&#41;](../../2014/integration-services/ole-db-destination-editor-error-output-page.md)   
 [Load Data by Using the OLE DB Destination](data-flow/load-data-by-using-the-ole-db-destination.md)  
  
  
