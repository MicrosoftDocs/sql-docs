---
title: "ADO NET Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.adonetsource.f1"
helpviewer_keywords: 
  - "ADO.NET source"
  - "sources [Integration Services], ADO.NET"
  - "sources [Integration Services], DataReader"
  - ".NET Framework [Integration Services]"
  - "DataReader source"
ms.assetid: 2a2f1750-2cda-4dda-9dca-623a96a6b3c0
author: janinezhang
ms.author: janinez
manager: craigg
---
# ADO NET Source
  The ADO NET source consumes data from a .NET provider and makes the data available to the data flow.  
  
 You can use the ADO NET source to connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using OLE DB is not supported. For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)], see [General Guidelines and Limitations (Windows Azure SQL Database)](https://go.microsoft.com/fwlink/?LinkId=248228).  
  
## Data Type Support  
 The source converts any data type that does not map to a specific [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type to the DT_NTEXT [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type. This conversion occurs even if the data type is `System.Object`.  
  
 You can change the DT_NTEXT data type to the DT_WSTR data type, or the change DT_WSTR to DT_NTEXT. You change data types by setting the **DataType** property in the **Advanced Editor** dialog box of the ADO NET source. For more information, see [Common Properties](../common-properties.md).  
  
 The DT_NTEXT data type can also be converted to the DT_BYTES or DT_STR data type by using a Data Conversion transformation after the ADO NET source. For more information, see [Data Conversion Transformation](transformations/data-conversion-transformation.md).  
  
 In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the date data types, DT_DBDATE, DT_DBTIME2, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET, map to certain date data types in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can configure the ADO NET source to convert the date data types from those that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to those that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses. To configure the ADO NET source to convert these date data types, set the **Type System Version** property of the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to **Latest**. (The **Type System Version** property is on the **All** page of the **Connection Manager** dialog box. To open the **Connection Manager** dialog box, right-click the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager, and then click **Edit**.)  
  
> [!NOTE]  
>  If the **Type System Version** property for the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager is set to **SQL Server 2005**, the system converts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date data types to DT_WSTR.  
  
 The system converts user-defined data types (UDTs) to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] binary large objects (BLOB) when the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager specifies the provider as the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient). The system applies the following rules when it converts the UDT data type:  
  
-   If the data is a non-large UDT, the system converts the data to DT_BYTES.  
  
-   If the data is a non-large UDT, and the **Length** property of the column on the database is set to -1 or a value greater than 8,000 bytes, the system converts the data to DT_IMAGE.  
  
-   If the data is a large UDT, the system converts the data to DT_IMAGE.  
  
    > [!NOTE]  
    >  If the ADO NET source is not configured to use error output, the system streams the data to the DT_IMAGE column in chunks of 8,000 bytes. If the ADO NET source is configured to use error output, the system passes the whole array of bytes to the DT_IMAGE column. For more information about how to configure components to use error output, see [Error Handling in Data](error-handling-in-data.md).  
  
 For more information about the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, supported data type conversions, and data type mapping across certain databases including [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Integration Services Data Types](integration-services-data-types.md).  
  
 For information about mapping [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types to managed data types, see [Working with Data Types in the Data Flow](../extending-packages-custom-objects/data-flow/working-with-data-types-in-the-data-flow.md).  
  
## ADO NET Source Troubleshooting  
 You can log the calls that the ADO NET source makes to external data providers. You can use this logging capability to troubleshoot the loading of data from external data sources that the ADO NET source performs. To log the calls that the ADO NET source makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## ADO NET Source Configuration  
 You configure the ADO NET source by providing the SQL statement that defines the result set. For example, an ADO NET source that connects to the [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] database and uses the SQL statement `SELECT * FROM Production.Product` extracts all the rows from the **Production.Product** table and provides the dataset to a downstream component.  
  
> [!NOTE]  
>  When you use an SQL statement to invoke a stored procedure that returns results from a temporary table, use the WITH RESULT SETS option to define metadata for the result set.  
  
> [!NOTE]  
>  If you use an SQL statement to execute a stored procedure and the package fails with the following error, you may be able to resolve the error by adding the `SET FMTONLY OFF` statement before the exec statement.  
>   
>  **Column <column_name> cannot be found at the datasource.**  
  
 The ADO NET source uses an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to connect to a data source, and the connection manager specifies the .NET provider. For more information, see [ADO.NET Connection Manager](../connection-manager/ado-net-connection-manager.md).  
  
 The ADO NET source has one regular output and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [ADO NET Custom Properties](ado-net-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [DataReader Destination](datareader-destination.md)   
 [ADO NET Destination](ado-net-destination.md)   
 [Data Flow](data-flow.md)  
  
  
