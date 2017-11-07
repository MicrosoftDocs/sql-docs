---
title: "Connecting using Azure Active Directory Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.prod: "sql-non-specified"
ms.technology: 
  - "drivers"
ms.topic: "article"
ms.assetid: 9c9d97be-de1d-412f-901d-5d9860c3df8c
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Connecting using Azure Active Directory Authentication
This article provides information on how to develop Java applications to use the Azure Active Directory authentication feature with Microsoft JDBC Driver 6.0 (or higher) for SQL Server.

Beginning with Microsoft JDBC Driver 6.0 for SQL Server, you can use Azure Active Direcoty (AAD) authentication which is a mechanism of connecting to Azure SQL Database v12 using identities in Azure Active Directory. Use Azure Active Directory authentication to centrally manage identities of database users and as an alternative to SQL Server authentication. The JDBC Driver 6.0 (or higher) allows you to specify your Azure Active Directory credentials in the JDBC connection string to connect to Azure SQL DB. For information on how to configure Azure Active Directory authentication visit [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/). 

Two new connection properties have been added to support Azure Active Directory Authentication:
*	**authentication**:  Use this property to indicate which SQL authentication method to use for connection. Possible values are: **ActiveDirectoryIntegrated**, **ActiveDirectoryPassword**, **SqlPassword** and the default **NotSpecified**.
	* Use 'authentication=ActiveDirectoryIntegrated' to connect to a SQL Database using integrated Windows authentication. To use this authentication mode you need to federate the on-premise Active Directory Federation
Services (ADFS) with Azure AD in the cloud. Once this is setup, you can access Azure SQL DB without being prompted for ceredentials when you are logged in a domain joined machine. 
	* Use 'authentication=ActiveDirectoryPassword' to connect to a SQL Database using an Azure AD principal name and password.
	* Use 'authentication=SqlPassword' to connect to a SQL Server using userName/user and password properties.
	* Use 'authentication=NotSpecified' or leave it as default if none of these authentication methods is needed.

*	**accessToken**: Use this property to connect to a SQL database using an access token. accessToken can only be set using the Properties parameter of the getConnection() method in the DriverManager class. It cannot be used in the connection URL.  

For details see the authentication property on the [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md) page.  


## Client Setup Requirements
Please make sure that the following components are installed on the client machine:
* Java 7 or above
*	Microsoft JDBC Driver 6.2 (or higher) for SQL Server
*	If you are using the access token based authentication mode, you will need [azure-activedirectory-library-for-java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies to run the examples from this article. See **Connecting using Access Token** section for more details.
*	If you are using the ActiveDirectoryPassword authentication mode you will need [azure-activedirectory-library-for-java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies. See **Connecting using ActiveDirectoryPassword Authentication Mode** section for more details.
*	If you are using the ActiveDirectoryIntegrated mode, you will need to install the Active Directory Authentication Library for SQL Server (ADALSQL.DLL) and sqljdbc_auth.dll.
	* ADALSQL.DLL enables applications to authenticate to Microsoft Azure SQL Database using Azure Active Directory. Download the DLL from [Microsoft Active Directory Authentication Library for Microsoft SQL Server](http://www.microsoft.com/en-us/download/details.aspx?id=48742)
	* For ADALSQL.DLL two binary versions X86 and X64 are available to download. If the wrong binary version is installed or if the DLL is missing, the driver will raise the following error: "Unable to load adalsql.dll (Authentication=…….). Error code: 0x2.". In such case download the right version of ADALSQL.DLL. 
	* sqljdbc_auth.dll is available in the driver package. Copy the sqljdbc_auth.dll file to a directory on the Windows system path on the computer where the JDBC driver is installed. Alternatively you can set the java.libary.path system property to specify the directory of the sqljdbc_auth.dll. 
	* If you are running a 64-bit JVM on a x64 processor, use the sqljdbc_auth.dll file in the x64 folder. 
	* If you are running a 32-bit Java Virtual Machine (JVM), use the sqljdbc_auth.dll file in the x86 folder, even if the operating system is the x64 version. 
	* For example, if you are using the 32-bit JVM and the JDBC driver is installed in the default directory, you can specify the location of the DLL by using the following virtual machine (VM) argument when the Java application is started:  
		```
		-Djava.library.path=C:\Microsoft JDBC Driver <version> for SQL Server\sqljdbc_<version>\enu\auth\x86
		```
	
## Connecting using ActiveDirectoryIntegrated Authentication Mode
The following example shows how to use 'authentication=ActiveDirectoryIntegrated' mode. Run this example on a domain joined machine that is federated with Azure Active Directory. A contained database user representing your Azure AD principal, or one of the groups, you belong to, must exist in the database and must have the CONNECT permission. 
	
Replace the server/database name with your server/database name in the following lines before executing the example:

```
ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
ds.setDatabaseName("demo"); // replace with your database name
```

The example to use ActiveDirectoryIntegrated authentication mode:
```
import java.sql.Connection;
import java.sql.ResultSet;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class IntegratedExample {

	public static void main(String[] args) throws Exception {
		SQLServerDataSource ds = new SQLServerDataSource();

		ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
		ds.setDatabaseName("demo"); // Replace with your database name
		ds.setAuthentication("ActiveDirectoryIntegrated");
		ds.setHostNameInCertificate("*.database.windows.net");

		Connection connection = ds.getConnection();

		ResultSet rs = connection.createStatement().executeQuery("SELECT SUSER_SNAME()");
		if(rs.next()){
			System.out.println("You have successfully logged on as: " + rs.getString(1));
		}
	}
}
```
Running this example on a machine joined to a domain that is federated with Azure Active Directory will automatically use your Windows credentials and no password is required. If connection is established, you will see the following message:
```
You have successfully logged on as: <your domain user name>
```

## Connecting using ActiveDirectoryPassword Authentication Mode
The following example shows how to use 'authentication=ActiveDirectoryPassword' mode.

Before building and running the example:
1.	On the client machine (on which, you want to run the example), download the [azure-activedirectory-library-for-java library](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies, and include them in the Java build path
2.	Locate the following lines of code and replace the server/database name with your server/database name.
	```
	ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
	ds.setDatabaseName("demo"); // replace with your database name
	```
3.	Locate the following lines of code and replace user name, with the name of the Azure AD user you want to connect as.
	```
	ds.setUser("bob@cqclinic.onmicrosoft.com"); // replace with your user name
	ds.setPassword("password"); 	// replace with your password
	```

The example to use ActiveDirectoryPassword authentication mode:
```
import java.sql.Connection;
import java.sql.ResultSet;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class UserPasswordExample {
	
	public static void main(String[] args) throws Exception{
		SQLServerDataSource ds = new SQLServerDataSource();
		
		ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
		ds.setDatabaseName("demo"); // Replace with your database
		ds.setUser("bob@cqclinic.onmicrosoft.com"); // Replace with your user name
		ds.setPassword("password"); // Replace with your password
		ds.setAuthentication("ActiveDirectoryPassword");
		ds.setHostNameInCertificate("*.database.windows.net");
		
		Connection connection = ds.getConnection();
		
		ResultSet rs = connection.createStatement().executeQuery("SELECT SUSER_SNAME()");
		if(rs.next()){
			System.out.println("You have successfully logged on as: " + rs.getString(1));
		}
	}
}
```
If connection is established, you will see the following message as output:
```
You have successfully logged on as: <your user name>
```

> [!NOTE]  
> A contained user database must exist and a contained database user representing the specified Azure AD user or one of the groups, the specified Azure AD user belongs to, must exist in the database and must have the CONNECT permission (except for Azure Active Directory server admin or group)


## Connecting using Access Token
Applications/services can retrieve an access token from the Azure Active Directory and use that to connect to SQL Azure Database. Note that accessToken can only be set using the Properties parameter of the getConnection() method in the DriverManager class. It cannot be used in the connection string.
 
The example below contains a simple Java application that connects to Azure SQL Database using access token based authentication. Before building and running the example, perform the following steps:
1.	Create an application account in Azure Active Directory for your service.
	1. Sign in to the Azure management portal
	2. Click on Azure Active Directory in the left hand navigation
	3. Click the directory tenant where you wish to register the sample application. This must be the same directory that is associated with your database (the server hosting your database).
	4. Click the Applications tab.
	5. In the drawer, click Add.
	6. Click "Add an application my organization is developing".
	7. Enter mytokentest as a friendly name for the application, select "Web Application and/or Web API", and click next.
	8. Assuming this application is a daemon/service and not a web application, it doesn't have a sign-in URL or app ID URI. For these two fields, enter http://mytokentest
	9. While still in the Azure portal, click the Configure tab of your application
	10. Find the Client ID value and copy it aside, you will need this later when configuring your application ( i.e.  a4bbfe26-dbaa-4fec-8ef5-223d229f647d). See the snapshot below.
	11. Under section “Keys”, select the duration of the key, save the configuration, and copy the key for later use. This is the client Secret.
	12. On the bottom, click on “view endpoints”, and copy the URL under “OAUTH 2.0 AUTHORIZATION ENDPOINT” for later use. This is the STS URL.


![JDBC_AAD_Token](../../connect/jdbc/media/jdbc_aad_token.png)


2. Logon to your Azure SQL Server’s user database as an Azure Active Directory admin and using a T-SQL command
provision a contained database user for your application principal. See the [Connecting to SQL Database or SQL Data Warehouse By Using Azure Active Directory Authentication](https://azure.microsoft.com/en-us/documentation/articles/sql-database-aad-authentication/)
 for more details on how to create an Azure Active Directory admin and a contained database user.

	```
	CREATE USER [mytokentest] FROM EXTERNAL PROVIDER
	```

3.	On the client machine (on which, you want to run the example), download the [azure-activedirectory-library-for-java](https://github.com/AzureAD/azure-activedirectory-library-for-java) library and its dependencies, and include them in the Java build path. Note that the azure-activedirectory-library-for-java is only needed to run this specific example as it uses the APIs from this library to retrieve the access token from Azure AD. If you already have an access token, you can skip this step. Note that you will also need to remove the section in the example that retrieves access token.

In the example below, replace the STS URL, Client ID, Client Secret, server and database name with your values.

```
import java.sql.Connection;
import java.sql.ResultSet;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

// The azure-activedirectory-library-for-java is needed to retrieve the access token from the AD. 
import com.microsoft.aad.adal4j.AuthenticationContext;
import com.microsoft.aad.adal4j.AuthenticationResult;
import com.microsoft.aad.adal4j.ClientCredential;


public class TokenBasedExample {

	public static void main(String[] args) throws Exception{

		// Retrieve the access token from the AD.
		String spn = "https://database.windows.net/";
		String stsurl = "https://login.microsoftonline.com/..."; // Replace with your STS URL.
		String clientId = "a4bbfe26-dbaa-4fec-8ef5-223d229f647d"; // Replace with your client ID.
		String clientSecret = "..."; // Replace with your client secret.

		AuthenticationContext context = new AuthenticationContext(stsurl, false, Executors.newFixedThreadPool(1));
		ClientCredential cred = new ClientCredential(clientId, clientSecret);

		Future<AuthenticationResult> future = context.acquireToken(spn, cred, null);
		String accessToken = future.get().getAccessToken();

		System.out.println("Access Token: " + accessToken);		
		
		// Connect with the access token.
		SQLServerDataSource ds = new SQLServerDataSource();

		ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name.
		ds.setDatabaseName("demo"); // Replace with your database name.
		ds.setAccessToken(accessToken);
		ds.setHostNameInCertificate("*.database.windows.net");

		Connection connection = ds.getConnection();

		ResultSet rs = connection.createStatement().executeQuery("SELECT SUSER_SNAME()");
		if(rs.next()){
			System.out.println("You have successfully logged on as: " + rs.getString(1));
		}
	}
}
``` 

If the connection is successful, you will see the following message as output:
```
Access Token: <your access token>
You have successfully logged on as: <your client ID>	
``` 