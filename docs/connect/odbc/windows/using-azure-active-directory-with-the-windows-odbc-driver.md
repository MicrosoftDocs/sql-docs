---
title: "Using Azure Active Directory with the Windows ODBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 52205f03-ff29-4254-bfa8-07cced155c86
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Using Azure Active Directory with the Windows ODBC Driver
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

## Purpose

The ODBC Driver 13.1 for SQL Server - Windows allows ODBC applications to connect to an instance of SQL Azure using a federated identity in Azure Active Directory with username/password or Windows Integrated Authentication.
  
## Connecting
DSN Registry Entries, Connection String Keywords, and Connection Attributes are created/modified as follows:

|Connection Attribute|Equivalent Connection/DSN String Keyword|Comments|
|-|-|-|
`SQL_COPT_SS_AUTHENTICATION`|`SQLAuthentication`|Must be set before connecting. Connection string value overrides DSN and sets this attribute if also specified. <br><br> Valid values are the same as `SQLAuthentication` keyword.|
|`SQL_COPT_SS_ACCESS_TOKEN`|N/A|Accepts an AAD access token. If, `UID`, `PWD`, `Trusted_Connection`, or `SQLAuthentication` connection string keywords and their equivalent attributes are also specified, and error occurs.|
|`SQL_COPT_SS_ENCRYPT`|`Encrypt` (yes/no)||	
|`SQL_COPT_SS_TRUST_SERVER_CERTIFICATE`|`TrustServerCertificate` (yes/no)||	
|`SQL_COPT_SS_OLDPWD`|N/A|	Not supported with Azure Active Directory. <br><br>Password expiration for SQL Server Authentication was introduced in SQL Server 2005. The `SQL_COPT_SS_OLDPWD` attribute was added to allow the client to provide both the old and the new password for the connection. When this property is set, the provider will not use the connection pool for the first connection or for subsequent connections, since the connection string will contain the "old password" which has now changed.|
|`SQL_COPT_SS_INTEGRATED_SECURITY`|`Trusted_Connection` (yes/no)|Deprecated: Use `SQL_COPT_SS_AUTHENTICATION` set to Active Directory Integrated instead. <br><br>Forces use of Windows Authentication for access validation on server login. When Windows Authentication is used, the driver ignores user identifier and password values provided as part of `SQLConnect`, `SQLDriverConnect`, or `SQLBrowseConnect` processing.|


### Example connection strings
1.	SQL Server Authentication – legacy syntax. Server certificate is not validated, and encryption is used only if the server enforces it. The username/password is passed in the connection string.
`server=Server;database=Database;UID=UserName;PWD=Password;`
2.	SQL Authentication – new syntax. The client requests encryption (the default value of `Encrypt` is `true`) and the server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`). The username/password is passed in the connection string.
 `server=Server;database=Database;UID=UserName;PWD=Password;Authentication=SqlPassword;`
3.	Integrated Windows Authentication using SSPI (to SQL Server or SQL IaaS) – current syntax. Server certificate is not validated, unless encryption is used. 
`server=Server;database=Database;Trusted_Connection=yes;`
4.	Integrated Windows Authentication using SSPI (if the target database is in SQL Server or SQL IaaS) – new syntax. The client requests encryption (the default value of `Encrypt` is `true`) and the server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`). 
`server=Server;database=Database;Authentication=ActiveDirectoryIntegrated;`
5.	AAD Username/Password Authentication (if the target database is in Azure SQL DB). Server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`). The username/password is passed in the connection string. 
`server=Server;database=Database;UID=UserName;PWD=Password;Authentication=ActiveDirectoryPassword;`
6.	Integrated Windows Authentication using ADAL, which involves redeeming Windows account credentials for an AAD-issued access token, assuming the target database is in Azure SQL Database. Server certificate gets validated, regardless of the encryption setting (unless `TrustServerCertificate` is set to `true`). 
`server=Server;database=Database; Authentication=ActiveDirectoryIntegrated;`

> [!NOTE] 
>- When using the new Active Directory options, ensure that the [Active Directory Authentication Library for SQL Server](http://go.microsoft.com/fwlink/?LinkID=513072) has been installed.
>- To connect using a SQL Server account username and password, you may now use the new `SqlPassword` option, which is recommended especially for SQL Azure since this option enables more secure connection defaults.
>- To connect using an Azure Active Directory account username and password, specify Authentication=ActiveDirectoryPassword in the connection string and the `UID` and `PWD` keywords with the username and password, respectively.
>- To connect using Windows Integrated or Active Directory Integrated authentication, specify Authentication=ActiveDirectoryIntegrated in the connection string. The driver will choose the correct authentication mode automatically. `UID` and `PWD` must not be specified.

## Authenticating with an existing Access Token
The `SQL_COPT_SS_ACCESS_TOKEN` preconnection attribute allows the use of an access token for authentication instead of username and password, and also bypasses the negotiation and obtaining of an access token by the driver.

|Connection Attribute|Size/Type|Default Value|Description|
|-|-|-|-|
|`SQL_COPT_SS_ACCESS_TOKEN` (1256)|`SQL_IS_POINTER`, a pointer to a `ACCESSTOKEN` structure.|NULL|	When this connection attribute is not set or null, the driver authenticates to the server via username/password or an access token it has acquired. <br><br>When this connection attribute is not null, the driver interprets it as a pointer to an `ACCESSTOKEN` structure, which overrides all other authentication methods and causes the driver to present this token to the server for authentication.| 

~~~
typedef struct AccessToken
{
    DWORD dataSize;
    BYTE data[];
} ACCESSTOKEN;
~~~

The `ACCESSTOKEN` is a variable-length structure consisting of a 4-byte length followed by length bytes of opaque data that form the access token.

## Azure Active Directory Authentication Sample Code
The following samples show the code required to connect to SQL Server using Azure Active Directory using connection keywords.
~~~
    SQLRETURN rc = SQL_SUCCESS;
    SQLHENV hEnv = nullptr;
    SQLHDBC hDbc = nullptr;

    rc = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &hEnv);
    rc = SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3_80, SQL_IS_INTEGER);
    rc = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc);
  	
    SQLWCHAR *connString = L"Driver={ODBC Driver 13 for SQL Server};Server={server};UID=myuser;PWD=myPass;Authentication=SqlPassword";
    SQLDriverConnect(hDbc, nullptr, connString, SQL_NTS, nullptr, 0, nullptr, SQL_DRIVER_NOPROMPT);	
    return 0;
~~~
The following samples show the code required to connect to SQL Server using Azure Active Directory using access token authentication.
~~~
    SQLRETURN rc = SQL_SUCCESS;
    SQLHENV hEnv = nullptr;
    SQLHDBC hDbc = nullptr;

    rc = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &hEnv);
    rc = SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3_80, SQL_IS_INTEGER);
    rc = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc);
    
	char* pToken[100] = { 0 }; //access token.
    
	rc = SQLSetConnectAttr(hDbc, SQL_COPT_SS_ACCESS_TOKEN, pToken, SQL_IS_POINTER);
    SQLWCHAR *connString = L"Driver={ODBC Driver 13 for SQL Server};Server={server};";
    SQLDriverConnect(hDbc, nullptr, connString, SQL_NTS, nullptr, 0, nullptr, SQL_DRIVER_NOPROMPT);		
   
    delete[] pToken;

    return 0;
~~~

## See Also
[Token-based authentication support for Azure SQL DB using Azure AD auth](https://blogs.msdn.microsoft.com/sqlsecurity/2016/02/09/token-based-authentication-support-for-azure-sql-db-using-azure-ad-auth)
[!INCLUDE[appliesto-ss2016-asdb-xxxx-xxx_md](../../../includes/appliesto-ss2016-asdb-xxxx-xxx_md.md)]
