---
title: "OLE DB Providers (ADO)"
description: "OLE DB Providers (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB providers [ADO]"
  - "ADO, OLE DB providers"
---
# OLE DB Providers (ADO)
OLE DB defines a set of COM interfaces to provide applications with uniform access to data that is stored in diverse information sources. This approach allows a data source to share its data through the interfaces that support the amount of DBMS functionality appropriate to the data source. By design, the high-performance architecture of OLE DB is based on its use of a flexible, component-based services model. Rather than having a prescribed number of intermediary layers between the application and the data, OLE DB requires only as many components as are needed to accomplish a particular task.  
  
 For example, suppose a user wants to run a query. Consider the following scenarios:  
  
-   The data resides in a relational database for which there currently exists an ODBC driver but no native OLE DB provider: The application uses ADO to talk to the OLE DB Provider for ODBC, which then loads the appropriate ODBC driver. The driver passes the SQL statement to the DBMS, which retrieves the data.  
  
-   The data resides in Microsoft SQL Server for which there is a native OLE DB provider: The application uses ADO to talk directly to the OLE DB Provider for Microsoft SQL Server. No intermediaries are required.  
  
-   The data resides in Microsoft Exchange Server, for which there is an OLE DB provider but which does not expose an engine to process SQL queries: The application uses ADO to talk to the OLE DB Provider for Microsoft Exchange and calls upon an OLE DB query processor component to handle the querying.  
  
-   The data resides in the Microsoft NTFS file system in the form of documents: Data is accessed by using a native OLE DB provider over Microsoft Indexing Service, which indexes the content and properties of documents in the file system to enable efficient content searches.  
  
 In all the preceding examples, the application can query the data. The user's needs are met with a minimum number of components. In each case, additional components are used only if needed, and only the required components are invoked. This demand-loading of reusable and shareable components greatly contributes to high performance when OLE DB is used.  
  
 Providers fall into two categories: those providing data and those providing services. A data provider owns its own data and exposes it in tabular form to your application. A service provider encapsulates a service by producing and consuming data, augmenting features in your ADO applications. A service provider can also be further defined as a service component, which must work in conjunction with other service providers or components.  
  
 ADO provides a consistent, higher level interface to the various OLE DB providers.  
  
 This section contains the following topics.  
  
-   [Data Providers](./data-providers.md)  
  
-   [Service Providers and Components](./service-providers-and-components.md)