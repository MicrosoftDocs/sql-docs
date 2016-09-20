---
title: "Connection Strings for ADO.NET and .NET Framework Data Provider for SQL Server  (SQL Server PDW)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0b902b9e-35df-425d-8f4a-f95e0779c3b9
caps.latest.revision: 22
author: BarbKess
---
# Connection Strings for ADO.NET and .NET Framework Data Provider for SQL Server  (SQL Server PDW)
This topic contains connection string keywords, and other connection information that application developers need for connecting to SQL Server PDW with ADO.NET by using the .NET Framework Data Provider for SQL Server.  
  
## Contents  
  
-   [Basics](#Basics)  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Connection String Keywords](#ConnectionString)  
  
-   [Establish a Secure Connection](#SecureConnection)  
  
-   [Connection Example](#CreateConnect)  
  
## <a name="Basics"></a>Basics  
.NET Framework Data Provider for SQL Server is the Microsoft core data technology for accessing SQL Server with languages targeting the Common Language Runtime (CLR). This managed provider is installed with .NET Framework 3.5 SP1 or higher. To connect with ADO.NET by using .NET Framework Data Provider, use the System.Data.SqlClient namespace, and specify port 17001.  
  
This provider uses the  System.Data.SqlClient namespace.  
  
-   Application developers use these connection strings to programmatically connect to SQL Server PDW.  
  
-   Application users specify some of these connection string values, as required by the application, when connecting to SQL Server PDW.  
  
## <a name="BeforeBegin"></a>Before You Begin  
  
### Software Prerequisites  
To connect, you need .NET Framework 3.5 SP1 or higher.  
  
-   If you have Windows 7, you already have .NET Framework 4.0 and don’t need to install anything else.  
  
-   If you are running a version of Windows prior to Windows 7, you need .NET Framework 3.5 SP1 or higher. You might already have it installed. For installation details, see the [Microsoft .NET Framework 3.5 Service Pack 1](http://www.microsoft.com/downloads/en/confirmation.aspx?familyId=ab99342f-5d1a-413d-8319-81da479ab0d7&displayLang=en) page on the Microsoft Download Center.  
  
### Appliance Privileges  
To connect, you will need a login with privileges to connect and run queries on the appliance.  
  
### Learn More  
For more information about ADO.NET, see the [ADO.NET Managed Providers and DataSet](http://msdn.microsoft.com/en-us/sqlserver/aa937722) home page on MSDN.  
  
For more information about using System.Data.SqlClient, see [Using the .NET Framework Data Provider for SQL Server](http://msdn.microsoft.com/en-us/library/kb9s9ks0(v=VS.80).aspx) on MSDN. This link points to the 2.0 version of the data provider; this version ships with .NET Framework 3.5 SP1 and higher.  
  
## <a name="ConnectionString"></a>Connection String Keywords  
This table lists the supported connection string keywords for connecting to SQL Server PDW.  For full descriptions of each string keyword, connection string creation information, and usage of the APIs, see [SqlConnection.ConnectionString Property, .NET Framework 3.5](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnection.connectionstring(v=VS.90).aspx) for .NET Framework 3.5 or [SqlConnection.ConnectionString Property, .NET Framework 4](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnection.connectionstring(v=VS.100).aspx) for .NET Framework 4.  
  
The following table lists the keywords that can be used with an ADO.NET connection string:  
  
|Name|SQL Server PDW Support Notes|  
|--------|-------------------------------------------------------|  
|Application Name||  
|Connection Timeout||  
|Connection Reset||  
|Current Language|Supports default value only.|  
|Data Source||  
|Encrypt||  
|Initial Catalog||  
|Max Pool Size||  
|Min Pool Size||  
|Network Library|‘TCP’ only.|  
|Packet Size||  
|Password||  
|Persist Security Info||  
|Pooling||  
|TrustServerCertificate||  
|Type System Version||  
|User ID||  
|Workstation ID||  
  
## <a name="SecureConnection"></a>Establish a Secure Connection  
To establish a secure connection to SQL Server PDW via a managed ADO.NET connection, the connection string must contain the following:  
  
1.  The *ValidateServerCertificate* value set to **true**.  
  
2.  When the SSL certificate has a *dnsName* value in the *subjectAltName* value, the *HostNameInCertificate* value in the connection string must match the *dnsName* in the certificate.  
  
3.  When the certificate does not have a *dnsName* value in the *subjectAltName* field or no *subjectAltName* value is present, the *HostNameInCertificate* value in the connection string must match the *commonName* (CN) part in the *Subject name* of the certificate.  
  
    If multiple *commonName* parts exist in the *Subject name* of the certificate, the connection succeeds if the HNIC value matches any of the *Common Name* parts.  
  
## <a name="CreateConnect"></a>Connection Example  
To access SQL Server with ADO.NET:  
  
1.  Use the **System.Data.SqlClient** namespace.  
  
2.  Use the **DbProviderFactories** class to connect to SQL Server PDW. Note, this class is not exposed in the Solution Explorer of Visual Studio.  
  
3.  To create a .NET connection, add the line `using System.Data.Common` in the program header. To add the line in Visual Studio, right-click DBConnection, select **Resolve**, and select **using System.Data.Common**.  
  
The following sample code creates a connection and performs a query. Replace `<hostIP>`, `<dbname>`, `<login>`, and `<password>` with appropriate values for your environment.  
  
```  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Data.Common;  
namespace ConsoleApplication1  
{  
    class Program  
    {  
        static void Main(string[] args)  
        {  
            using (DbConnection connection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())  
            {  
                connection.ConnectionString = "SERVER=<hostIP>,17001;DATABASE=<dbname>;USER ID=<login>;PASSWORD=<password>";  
                connection.Open();  
                DbCommand command = connection.CreateCommand();  
                command.CommandText = "SELECT TOP 10 * FROM account";  
                DbDataReader reader = command.ExecuteReader();  
                while (reader.Read())  
                {  
                    System.Console.WriteLine(reader.GetValue(0));  
                }  
                reader.Close();  
                System.Console.ReadLine();  
            }  
        }  
    }  
}  
```  
  
## See Also  
[Connection Strings &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/connection-strings-sql-server-pdw.md)  
  
