---
title: "Obtain a SqlClientFactory"
description: Learn how to obtain a SqlClientFactory from the DbProviderFactories class to work with specific data sources in .NET.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "12/22/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Obtain a SqlClientFactory

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The process of obtaining a <xref:System.Data.Common.DbProviderFactory> involves passing information about a data provider to the <xref:System.Data.Common.DbProviderFactories> class. Based on this information, the <xref:System.Data.Common.DbProviderFactories.GetFactory%2A> method creates a strongly typed provider factory. For example, to create a <xref:Microsoft.Data.SqlClient.SqlClientFactory>, you can pass `GetFactory` a string with the provider name specified as "**Microsoft.Data.SqlClient**".

The other overload of `GetFactory` takes a <xref:System.Data.DataRow>. Once you create the provider factory, you can then use its methods to create additional objects. Some of the methods of a `SqlClientFactory` include <xref:Microsoft.Data.SqlClient.SqlClientFactory.CreateConnection%2A>, <xref:Microsoft.Data.SqlClient.SqlClientFactory.CreateCommand%2A>, and <xref:Microsoft.Data.SqlClient.SqlClientFactory.CreateDataAdapter%2A>.

## Register SqlClientFactory

To retrieve the <xref:Microsoft.Data.SqlClient.SqlClientFactory> object by the <xref:System.Data.Common.DbProviderFactories> class in .NET Framework, it's necessary to register it in a **App.config** or **web.config** file. The following configuration file fragment shows the syntax and format for <xref:Microsoft.Data.SqlClient>.  

```xml  
<system.data>
  <DbProviderFactories>
    <add name="Microsoft SqlClient Data Provider"
      invariant="Microsoft.Data.SqlClient"
      description="Microsoft SqlClient Data Provider for SQL Server"
      type="Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient, Version=2.0.20168.4, Culture=neutral, PublicKeyToken=23ec7fc2d6eaa4a5"/>
  </DbProviderFactories>
</system.data>  
```  

The **invariant** attribute identifies the underlying data provider. This three-part naming syntax is also used when creating a new factory and for identifying the provider in an application configuration file so that the provider name, along with its associated connection string, can be retrieved at run time.  

> [!NOTE]  
> In .NET core, since there is no GAC or global configuration support, the <xref:Microsoft.Data.SqlClient.SqlClientFactory> object should be registered by calling <xref:System.Data.Common.DbProviderFactories.RegisterFactory%2A> method in the project.

The following sample shows how to use the <xref:Microsoft.Data.SqlClient.SqlClientFactory> in a .NET core application.

[!code-csharp[SqlClientFactory_Netcoreapp#1](~/../sqlclient/doc/samples/SqlClientFactory_Netcoreapp.cs#1)]

## See also

- [DbProviderFactories](dbproviderfactories.md)
- [Connection strings](connection-strings.md)
- [Using the configuration classes](/previous-versions/aspnet/ms228063(v=vs.100))
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
