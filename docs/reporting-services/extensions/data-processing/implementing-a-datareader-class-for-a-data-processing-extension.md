---
title: "Implement a DataReader class for a data processing extension"
description: Increase application performance and reduce system overhead by implementing a DataReader class for a data processing extension.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "data processing extensions [Reporting Services], data readers"
  - "data readers [Reporting Services]"
  - "DataReader class"
  - "read-only data"
---
# Implement a DataReader class for a data processing extension
  The **DataReader** object enables a client to retrieve a read-only, forward-only stream of data from a data source. Results are returned as the query executes and are stored in the network buffer on the client until you request them using the **Read** method of the **DataReader** class. To create a **DataReader** class, implement <xref:Microsoft.ReportingServices.DataProcessing.IDataReader> and optionally implement <xref:Microsoft.ReportingServices.DataProcessing.IDataReaderExtension>. Using a **DataReader** object increases application performance both by retrieving data as soon as it's available, rather than waiting for the entire results of the query to be returned, and (by default) storing only one row at a time in memory, reducing system overhead.  
  
 After creating an instance of your **Command** class, you create a **DataReader** object by calling **Command.ExecuteReader** to retrieve rows from the data source. The **DataReader** implementation must provide two basic capabilities: forward-only access over the result sets obtained by executing a command and access to the column types, names, and values within each row. Clients use the **Read** method of the **DataReader** object to obtain a row from the results of the query.  
  
 In Report Designer, your **DataReader** object is used to retrieve a list of fields and schema information about the result set. This retrieval is accomplished by implementing the **GetName**, **GetValue**, **GetFieldType,** and **GetOrdinal** methods of the <xref:Microsoft.ReportingServices.DataProcessing.IDataReader> interface.  
  
 The <xref:Microsoft.ReportingServices.DataProcessing.IDataReaderExtension> interface allows you to supply specific aggregation information about your result set. For a sample **DataReader** class implementation, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Related content

- [Reporting Services extensions](../../../reporting-services/extensions/reporting-services-extensions.md)
- [Implement a data processing extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
