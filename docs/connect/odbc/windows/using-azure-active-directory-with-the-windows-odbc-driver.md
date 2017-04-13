---
title: "Using Azure Active Directory with the Windows ODBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2017"
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

The ODBC Driver 13.1 for SQL Server - Windows allows ODBC applications to connect to an instance of SQL Azure using a federated identity in Azure Active Directory with username/password, an Azure Active Directory Access Token, or Windows Integrated Authentication. This is accomplished through the use of new DSN and connection string keywords, and connection attributes.

## New and/or Modified DSN and Connection String Keywords

The `Authentication` keyword can be used when connecting with a DSN or connection string to control the authentication mode. The value set in the connection string overrides that in the DSN, if provided. The _pre-attribute value_ of the `Authentication` setting is the value computed from the connection string and DSN values.

|Name|Values|Default|Description|
|-|-|-|-|
|`Authentication`|(not set), (empty string), `SqlPassword`, `ActiveDirectoryPassword`, `ActiveDirectoryIntegrated`|(not set)|Controls the authentication mode.<table><tr><th>Value<th>Description<tr><td>(not set)<td>Authentication mode determined by other keywords (existing legacy connection options.)<tr><td>(empty string)<td>(Connection string only.) Override and unset an `Authentication` value set in the DSN.<tr><td>SqlPassword<td>Directly authenticate to a SQL Server instance using a username and password.<tr><td>ActiveDirectoryPassword<td>Authenticate with an Azure Active Directory identity using a username and password.<tr><td>ActiveDirectoryIntegrated<td>Authenticate with an Azure Active Directory identity using integrated authentication.</table>|
|`Encrypt`|(not set), `Yes`, `No`|(see description)|Controls encryption for a connection. If the pre-attribute value of the `Authentication` setting is not _none_, the default is `Yes`. Otherwise, the default is `No`. The pre-attribute value of Encryption is `Yes` if the value is set to `Yes` in either the DSN or connection string.|

## New and/or Modified Connection Attributes

The following pre-connect connection attributes have either been introduced or modified to support Azure Active Directory authentication. When a connection attribute has a corresponding connection string or DSN keyword and is set, the connection attribute takes precedence.

|Attribute|Type|Values|Default|Description|
|-|-|-|-|-|
|`SQL_COPT_SS_AUTHENTICATION`|`SQL_IS_INTEGER`|`SQL_AU_NONE`, `SQL_AU_PASSWORD`, `SQL_AU_AD_INTEGRATED`, `SQL_AU_AD_PASSWORD`, `SQL_AU_RESET`|(not set)|See description of `Authentication` keyword above. `SQL_AU_NONE` is provided in order to explicitly override a set `Authentication` value in the DSN and/or connection string, while `SQL_AU_RESET` unsets the attribute if it was set, allowing the DSN or connection string value to take precedence.|
|`SQL_COPT_SS_ACCESS_TOKEN`|`SQL_IS_POINTER`|Pointer to `ACCESSTOKEN` or NULL|NULL|If non-null, specifies the AzureAD Access Token to use. It is an error to specify an access token and also `UID`, `PWD`, `Trusted_Connection`, or `Authentication` connection string keywords or their equivalent attributes.|
|`SQL_COPT_SS_ENCRYPT`|`SQL_IS_INTEGER`|`SQL_EN_OFF`, `SQL_EN_ON`|(see description)|Controls encryption for a connection. `SQL_EN_OFF` and `SQL_EN_ON` disable and enable encryption, respectively. If the pre-attribute value of the `Authentication` setting is not _none_, the default is `SQL_EN_ON`. Otherwise, the default is `SQL_EN_OFF`. ||`SQL_COPT_SS_OLDPWD`|\-|\-|\-|Not supported with Azure Active Directory, since password changes to AAD principals cannot be accomplished through an ODBC connection. <br><br>Password expiration for SQL Server Authentication was introduced in SQL Server 2005. The `SQL_COPT_SS_OLDPWD` attribute was added to allow the client to provide both the old and the new password for the connection. When this property is set, the provider will not use the connection pool for the first connection or for subsequent connections, since the connection string will contain the "old password" which has now changed.|
|`SQL_COPT_SS_INTEGRATED_SECURITY`|`SQL_IS_INTEGER`|`SQL_IS_OFF`,`SQL_IS_ON`|`SQL_IS_OFF`|_Deprecated_; use `SQL_COPT_SS_AUTHENTICATION` set to `SQL_AU_AD_INTEGRATED` instead. <br><br>Forces use of Windows Authentication for access validation on server login. When Windows Authentication is used, the driver ignores user identifier and password values provided as part of `SQLConnect`, `SQLDriverConnect`, or `SQLBrowseConnect` processing.|


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
  	
    SQLWCHAR *connString = L"Driver={ODBC Driver 13 for SQL Server};Server={server};UID=myuser;PWD=myPass;Authentication=ActiveDirectoryPassword";
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
