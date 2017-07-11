---
title: "SQL Destination Editor (Connection Manager Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.sqlserverdestadapter.connection.f1"
helpviewer_keywords: 
  - "SQL Server Destination Editor"
ms.assetid: 423e1654-54af-47c6-ab6f-98670534557d
caps.latest.revision: 37
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# SQL Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **SQL Destination Editor** dialog box to specify data source information, and to preview the results. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination loads data into tables or views in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
 To learn more about the SQL Server destination, see [SQL Server Destination](../../integration-services/data-flow/sql-server-destination.md).  
  
## Options  
 **OLE DB connection manager**  
 Select an existing connection from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Use a table or view**  
 Select an existing table or view from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new table by using the **Create Table** dialog box.  
  
> [!NOTE]  
>  When you click **New**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] generates a default CREATE TABLE statement based on the connected data source. This default CREATE TABLE statement will not include the FILESTREAM attribute even if the source table includes a column with the FILESTREAM attribute declared. To run an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component with the FILESTREAM attribute, first implement FILESTREAM storage on the destination database. Then, add the FILESTREAM attribute to the CREATE TABLE statement in the **Create Table** dialog box. For more information, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 **Preview**  
 Preview results by using the **Preview Query Results** dialog box. Preview can display up to 200 rows.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [SQL Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/sql-destination-editor-mappings-page.md)   
 [SQL Destination Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/sql-destination-editor-advanced-page.md)   
 [Bulk Load Data by Using the SQL Server Destination](../../integration-services/data-flow/bulk-load-data-by-using-the-sql-server-destination.md)  
  
  