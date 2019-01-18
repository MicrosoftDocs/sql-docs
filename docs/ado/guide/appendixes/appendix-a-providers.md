---
title: "Appendix A: Providers | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "data providers [ADO]"
  - "providers [ADO]"
  - "ADO, providers"
  - "service providers [ADO]"
  - "service components [ADO]"
ms.assetid: e2581b47-b11e-4e1e-b96c-d39c77c5b48a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Appendix A: Data and Service Providers
This section addresses three kinds of providers: data providers, service providers, and service components. Providers fall into two categories: those providing data and those providing services. A *data provider* owns its own data and exposes it in tabular form to your application. A *service provider* encapsulates a service by producing and consuming data, augmenting features in your ADO applications. A service provider may also be further defined as a *service component*, which must work together with other service providers or components.

## Data Providers
 ADO is powerful and flexible because it can connect to any of several different data providers and still expose the same programming model, regardless of the specific features of any given provider.

 However, because each data provider is unique, how your application interacts with ADO will vary slightly by data provider. The differences usually fall into one of three categories:

-   Connection parameters in the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property.

-   [Command](../../../ado/reference/ado-api/command-object-ado.md) object usage.

-   Provider-specific [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) behavior.

 Details for each of the data providers currently available from Microsoft are listed as follows.

|Area|Topic|
|----------|-----------|
|ODBC databases|[Microsoft OLE DB Provider for ODBC](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md)|
|Microsoft Indexing Service|[Microsoft OLE DB Provider for Microsoft Indexing Service](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-indexing-service.md)|
|Active Directory Service|[Microsoft OLE DB Provider for Microsoft Active Directory Service](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-active-directory-service.md)|
|Microsoft Jet databases|[OLE DB Provider for Microsoft Jet](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md)|
|Microsoft SQL Server|[Microsoft OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md)|
|Oracle databases|[Microsoft OLE DB Provider for Oracle](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-oracle.md)|
|Internet Publishing|[Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md)|
|Simple data sources|[Microsoft OLE DB Simple Provider](../../../ado/guide/appendixes/microsoft-ole-db-simple-provider.md)|

## Provider-Specific Dynamic Properties
 The [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collections of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md), [Command](../../../ado/reference/ado-api/command-object-ado.md), and [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) objects include dynamic properties specific to the provider. These properties provide information about functionality specific to the provider beyond the built-in properties that ADO supports.

 After establishing the connection and creating these objects, use the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method on the **Properties** collection of the object to obtain the provider-specific properties. Refer to the provider documentation and the [OLE DB Programmer's Guide](https://msdn.microsoft.com/3c5e2dd5-35e5-4a93-ac3a-3818bb43bbf8) for detailed information about these dynamic properties.

## Service Providers
 To use a service provider, you must supply a keyword. You should also be aware of the provider-specific dynamic properties associated with each service provider. Provider-specific details are listed for each service provider that is currently available from Microsoft:

-   [Microsoft Data Shaping Service for OLE DB](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md)

-   [Microsoft OLE DB Persistence Provider](../../../ado/guide/appendixes/microsoft-ole-db-persistence-provider-ado-service-provider.md)

-   [Microsoft OLE DB Remoting Provider](../../../ado/guide/appendixes/microsoft-ole-db-remoting-provider-ado-service-provider.md)

## Service Components
 The [Cursor Service for OLE DB](../../../ado/guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) service component supplements the cursor support functions of data providers. It also requires a keyword and has dynamic properties.

 For more information about OLE DB Providers, see [Microsoft OLE DB](https://msdn.microsoft.com/library/windows/desktop/ms722784.aspx).

## Provider Commands
 For each provider listed here, if your applications allow users to enter SQL statements as the provider commands, you must always validate the user input and be vigilant of possible hacker attacks using potentially dangerous SQL statements, such as `DROP TABLE t1`, as part of the user input.

## See Also
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)
 [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md)
 [Microsoft OLE DB Provider for Microsoft Active Directory Service](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-active-directory-service.md)
 [Microsoft OLE DB Provider for Microsoft Indexing Service](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-indexing-service.md)
 [Microsoft OLE DB Provider for ODBC](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md)
 [Microsoft OLE DB Provider for Oracle](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-oracle.md)
 [Microsoft OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md)
 [Microsoft OLE DB Provider for Microsoft Jet](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md)
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
 [Refresh Method (RDS)](../../../ado/reference/rds-api/refresh-method-rds.md)
