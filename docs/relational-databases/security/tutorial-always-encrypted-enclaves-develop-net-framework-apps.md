---
title: "Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves"
description: "Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 02/15/2023
ms.service: sql
ms.subservice: security
ms.topic: tutorial
monikerRange: ">= sql-server-ver15"
---
# Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This tutorial teaches you how to develop an application that issues database queries that use a server-side secure enclave for [Always Encrypted with secure enclaves](encryption/always-encrypted-enclaves.md).

> [!NOTE]
> .NET Framework does not support using Always Encrypted with VBS enclaves without attestation. This tutorial is only applicable if you're using attestation with Microsoft Azure Attestation (with Intel SGX enclaves in Azure SQL Database) or Host Guardian Service (with VBS enclaves in SQL Server).

## Prerequisites

Make sure you've completed one of the [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started) tutorials before following the below steps in this tutorial.

You'll also need Visual Studio (version 2022 is recommended) - download it from [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com). Your application development machine must run .NET Framework 4.7.2 or later.

## Step 1: Set up your Visual Studio Project

To use Always Encrypted with secure enclaves in a .NET Framework application, you need to make sure your application is built against .NET Framework 4.7.2 and is integrated with the [Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders NuGet](https://www.nuget.org/packages/Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders). In addition, if you store your column master key in Azure Key Vault, you also need to integrate your application with the [Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider NuGet](https://www.nuget.org/packages/Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider), version 2.4.0 or later. 

1. Open Visual Studio.

2. Create a new C\# Console App (.NET Framework) project.

3. Make sure your project targets at least .NET Framework 4.7.2. Right-click on the project in Solution Explorer, select **Properties** and set **Target framework** to .NET Framework 4.7.2.

4. Install the following NuGet package by going to **Tools** (main menu) > **NuGet Package Manager** > **Package Manager Console**. Run the following code in the Package Manager Console.

   ```powershell
   Install-Package Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders -IncludePrerelease
   ```

5. If you use Azure Key Vault for storing your column master keys, install the following NuGet packages by going to **Tools** (main menu) > **NuGet Package Manager** > **Package Manager Console**. Run the following code in the Package Manager Console.

   ```powershell
   Install-Package Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider -IncludePrerelease -Version 2.4.0
   Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory
   ```

6. Open the App.config file for your project.

7. Locate the `<configuration>` section and add or update the `<configSections>` sections.

   1. If the `<configuration>` section does **not** contain the `<configSections>` section, add the following content immediately below `<configuration>`.

      ```xml
      <configSections>
        <section name="SqlColumnEncryptionEnclaveProviders" type="System.Data.SqlClient.SqlColumnEncryptionEnclaveProviderConfigurationSection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
      </configSections>
      ```

   2. If the `<configuration>` section already contains the `<configSections>` section, add the following line within the `<configSections>` section:

      ```xml
      <section name="SqlColumnEncryptionEnclaveProviders"  type="System.   Data.SqlClient.   SqlColumnEncryptionEnclaveProviderConfigurationSection, System.   Data,  Version=4.0.0.0, Culture=neutral,    PublicKeyToken=b77a5c561934e089" />
      ```

8. Inside the `<configuration>` section, below `</configSections>`, add a new section, which specifies an enclave provider to be used to attest and interact with your server-side secure enclave.

   1. If you're using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS) (you're using the database from [Tutorial: Getting started using Always Encrypted with secure enclaves in SQL Server](tutorial-getting-started-with-always-encrypted-enclaves.md)), add the below section.

      ```xml
      <SqlColumnEncryptionEnclaveProviders>
        <providers>
          <add name="VBS"  type="Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.HostGuardianServiceEnclaveProvider,  Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders,    Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"/>
        </providers>
      </SqlColumnEncryptionEnclaveProviders>
      ```

      Here's a complete example of an app.config file for a simple console application.

      ```xml
      <?xml version="1.0" encoding="utf-8" ?>
      <configuration>
        <configSections>
          <section name="SqlColumnEncryptionEnclaveProviders" type="System.Data.SqlClient.SqlColumnEncryptionEnclaveProviderConfigurationSection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
        </configSections>
        <SqlColumnEncryptionEnclaveProviders>
          <providers>
            <add name="VBS"  type="Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.HostGuardianServiceEnclaveProvider,  Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders,    Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"/>
          </providers>
        </SqlColumnEncryptionEnclaveProviders>
        <startup> 
         <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
        </startup>
      </configuration>
      ```

   1. If you're using [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] with SGX enclaves and Microsoft Azure Attestation (you're using the database from [Tutorial: Getting started with Always Encrypted with secure Intel SGX enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started-sgx)), add the below section.

      ```xml
      <SqlColumnEncryptionEnclaveProviders>
        <providers>
          <add name="SGX" type="Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.AzureAttestationEnclaveProvider, Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" />
        </providers>
      </SqlColumnEncryptionEnclaveProviders>
      ```

      Here's a complete example of an app.config file for a simple console application.

      ```xml
      <?xml version="1.0" encoding="utf-8" ?>
      <configuration>
        <configSections>
          <section name="SqlColumnEncryptionEnclaveProviders" type="System.Data.SqlClient.SqlColumnEncryptionEnclaveProviderConfigurationSection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
        </configSections>
        <SqlColumnEncryptionEnclaveProviders>
          <providers>
            <add name="SGX" type="Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders.AzureAttestationEnclaveProvider, Microsoft.SqlServer.Management.AlwaysEncrypted.EnclaveProviders, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" />
          </providers>
        </SqlColumnEncryptionEnclaveProviders>
        <startup> 
         <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
        </startup>
      </configuration>
      ```

## Step 2: Implement your application logic

Your application will connect to the **ContosoHR** database that was created in one of the tutorials, see [Prerequisites](#prerequisites) and it will run a query that contains the `LIKE` predicate on the **SSN** column and a range comparison on the **Salary** column.

1. Replace the content of the Program.cs file (generated by Visual Studio) with the below code. Update the database connection string with your server name, database authentication settings, and the enclave attestation URL for your environment.

    ```cs
    using System;
    using System.Data.SqlClient;
    using System.Data;

    namespace ConsoleApp1
    {
        class Program
        {
            static void Main(string[] args)
            {
    
                string connectionString = "Data Source = myserver; Initial Catalog = ContosoHR; Column Encryption Setting = Enabled;Enclave Attestation Url = http://hgs.bastion.local/Attestation; Integrated Security = true";

                //string connectionString = "Data Source = myserver.database.windows.net; Initial Catalog = ContosoHR; Column Encryption Setting = Enabled;Enclave Attestation Url = https://myattestationprovider.uks.attest.azure.net/attest/SgxEnclave; User ID=user; Password=password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = @"SELECT [SSN], [FirstName], [LastName], [Salary] FROM [HR].[Employees] WHERE [SSN] LIKE @SSNPattern AND [Salary] > @MinSalary;";

                    SqlParameter paramSSNPattern = cmd.CreateParameter();

                    paramSSNPattern.ParameterName = @"@SSNPattern";
                    paramSSNPattern.DbType = DbType.AnsiStringFixedLength;
                    paramSSNPattern.Direction = ParameterDirection.Input;
                    paramSSNPattern.Value = "%9838";
                    paramSSNPattern.Size = 11;

                    cmd.Parameters.Add(paramSSNPattern);

                    SqlParameter MinSalary = cmd.CreateParameter();

                    MinSalary.ParameterName = @"@MinSalary";
                    MinSalary.DbType = DbType.Currency;
                    MinSalary.Direction = ParameterDirection.Input;
                    MinSalary.Value = 20000;

                    cmd.Parameters.Add(MinSalary);
                    cmd.ExecuteNonQuery();
    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())

                    {
                        Console.WriteLine(reader[0] + ", " + reader[1] + ", " + reader[2] + ", " + reader[3]);
                    }   
                    Console.ReadKey();
                }
            }
        }
    }
    ```

2. Build and run the application.  

## See also

- [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)
