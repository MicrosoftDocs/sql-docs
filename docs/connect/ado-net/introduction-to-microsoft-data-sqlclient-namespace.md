---
title: "Introduction to Microsoft.Data.SqlClient Namespace"
ms.date: "08/15/2019"
ms.assetid: c18b1fb1-2af1-4de7-80a4-95e56fd976cb
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# Introduction to Microsoft.Data.SqlClient Namespace

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

This page describes how the Microsoft.Data.SqlClient namespace offers additional functionality over the existing System.Data.SqlClient namespace.
  
## Release notes
All release notes are available on the [GitHub Repository](https://github.com/dotnet/SqlClient/tree/master/release-notes).

## New features

### New features over .NET Framework 4.7.2 System.Data.SqlClient

Data Classification - Available in Azure SQL Database and Microsoft SQL Server 2019 since CTP 2.0.

UTF-8 support - Available in Microsoft SQL Server SQL Server 2019 since CTP 2.3.

### New features over .NET Core 2.2 System.Data.SqlClient

Data Classification - Available in Azure SQL Database and Microsoft SQL Server 2019 since CTP 2.0.

UTF-8 support - Available in Microsoft SQL Server SQL Server 2019 since CTP 2.3.

Always Encrypted with Enclaves - Always Encrypted is available in Microsoft SQL Server 2016 and higher. Enclave support was introduced in Microsoft Sql Server 2019 CTP 2.0.

Authentication - Active Directory Password authentication mode.

### Data Classification

Data Classification brings a new set of APIs exposing read-only Data Sensitivity and Classification information about objects retrieved via SqlDataReader when the underlying source supports the feature and contains metadata about [data sensitivity and classification](https://docs.microsoft.com/sql/relational-databases/security/sql-data-discovery-and-classification?view=sql-server-2017).

```C#
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

UTF-8 support does not require any application code changes. These SqlClient changes simply optimize the communication between the client and server when the server supports UTF-8 and the underlying column collation is UTF-8. See the UTF-8 section under [What's new in SQL Server 2019 preview](https://docs.microsoft.com/sql/sql-server/what-s-new-in-sql-server-ver15?view=sqlallproducts-allversions#utf-8-support-ctp-23).

### Always Encrypted with Enclaves

In general, existing documentation which uses System.Data.SqlClient on .NET Framework **and built-in column master key store providers** should now work with .NET Core, too.

 [Develop using Always Encrypted with .NET Framework Data Provider](https://docs.microsoft.com/sql/relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider?view=sql-server-2017)

 [Always Encrypted: Protect sensitive data and store encryption keys in the Windows certificate store](https://docs.microsoft.com/azure/sql-database/sql-database-always-encrypted)

### Authentication

Different authentication modes can be specified by using the _Authentication_ connnection string option. For more information, see the [documentation for SqlAuthenticationMethod](https://docs.microsoft.com/dotnet/api/system.data.sqlclient.sqlauthenticationmethod?view=netframework-4.7.2).

> [!NOTE]
> Custom key store providers, like the Azure Key Vault provider, will need to be updated to support Microsoft.Data.SqlClient. Similarly, enclave providers will also need to be updated to support Microsoft.Data.SqlClient.
> Always Encrypted is only supported against .NET Framework and .NET Core targets. It is not supported against .NET Standard since .NET Standard is missing certain encryption dependencies.
