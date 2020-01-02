---
title: "Introduction to Microsoft.Data.SqlClient namespace"
description: "Introduction page for the Microsoft.Data.SqlClient namespace."
ms.date: "09/30/2019"
ms.assetid: c18b1fb1-2af1-4de7-80a4-95e56fd976cb
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: rothja
ms.author: jroth
ms.reviewer: v-kaywon
---
# Introduction to Microsoft.Data.SqlClient namespace

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

## Release notes for Microsoft.Data.SqlClient 1.1.0

Release notes are also available in the GitHub Repository: [1.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.1).

### New Features

#### Always Encrypted with secure enclaves

Always Encrypted is available starting in Microsoft SQL Server 2016. Secure enclaves are available starting in Microsoft SQL Server 2019. In order to use the enclave feature, connection strings should include the required attestation protocol and attestation URL. Examples:

```
Attestation Protocol=HGS;Enclave Attestation Url=<attestation_url_for_HGS>
```

For more details, see:

- [SqlClient support for Always Encrypted](sql/sqlclient-support-always-encrypted.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)

## Release notes for Microsoft.Data.SqlClient 1.0

The initial release for the Microsoft.Data.SqlClient namespace offers additional functionality over the existing System.Data.SqlClient namespace.
Release notes are also available on the GitHub Repository: [1.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.0).

### New features

#### New features over .NET Framework 4.7.2 System.Data.SqlClient

- **Data Classification** - Available in Azure SQL Database and Microsoft SQL Server 2019.

- **UTF-8 support** - Available in Microsoft SQL Server 2019.

#### New features over .NET Core 2.2 System.Data.SqlClient

- **Data Classification** - Available in Azure SQL Database and Microsoft SQL Server 2019.

- **UTF-8 support** - Available in Microsoft SQL Server 2019.

- **Authentication** - Active Directory Password authentication mode.

### Data Classification

Data Classification brings a new set of APIs exposing read-only Data Sensitivity and Classification information about objects retrieved via SqlDataReader when the underlying source supports the feature and contains metadata about [data sensitivity and classification](../../relational-databases/security/sql-data-discovery-and-classification.md).

```csharp
public class SqlDataReader
{
    public Microsoft.Data.SqlClient.DataClassification.SensitivityClassification SensitivityClassification
}

namespace Microsoft.Data.SqlClient.DataClassification
{
    public class ColumnSensitivity
    {
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.SensitivityProperty> SensitivityProperties
    }
    public class InformationType
    {
        public string Id
        public string Name
    }
    public class Label
    {
        public string Id
        public string Name
    }
    public class SensitivityClassification
    {
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.ColumnSensitivity> ColumnSensitivities
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.InformationType> InformationTypes
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.Label> Labels
    }
    public class SensitivityProperty
    {
        public Microsoft.Data.SqlClient.DataClassification.InformationType InformationType
        public Microsoft.Data.SqlClient.DataClassification.Label Label
    }
}
```

### UTF-8 support

UTF-8 support does not require any application code changes. These SqlClient changes simply optimize the communication between the client and server when the server supports UTF-8 and the underlying column collation is UTF-8. See the UTF-8 section under [What's new in SQL Server 2019 preview](../../sql-server/what-s-new-in-sql-server-ver15.md).

### Always encrypted with enclaves

In general, existing documentation which uses System.Data.SqlClient on .NET Framework **and built-in column master key store providers** should now work with .NET Core, too.

 [Develop using Always Encrypted with .NET Framework Data Provider](../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)

 [Always Encrypted: Protect sensitive data and store encryption keys in the Windows certificate store](https://docs.microsoft.com/azure/sql-database/sql-database-always-encrypted)

### Authentication

Different authentication modes can be specified by using the _Authentication_ connection string option. For more information, see the [documentation for SqlAuthenticationMethod](https://docs.microsoft.com/dotnet/api/system.data.sqlclient.sqlauthenticationmethod?view=netframework-4.7.2).

> [!NOTE]
> Custom key store providers, like the Azure Key Vault provider, will need to be updated to support Microsoft.Data.SqlClient. Similarly, enclave providers will also need to be updated to support Microsoft.Data.SqlClient.
> Always Encrypted is only supported against .NET Framework and .NET Core targets. It is not supported against .NET Standard since .NET Standard is missing certain encryption dependencies.
