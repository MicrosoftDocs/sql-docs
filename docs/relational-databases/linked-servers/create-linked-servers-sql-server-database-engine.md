---
description: "Create linked servers (SQL Server Database Engine)"
title: "Create linked servers"
ms.date: "1/10/2022"
ms.service: sql
ms.subservice: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.linkedserver.properties.general.f1"
  - "sql13.swb.linkedserver.properties.security.f1"
  - "sql13.swb.linkedserver.properties.provider.f1"
  - "sql13.swb.linkedserver.properties.options.f1"
helpviewer_keywords: 
  - "linked servers [SQL Server], creating"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-dt-2019, FY22Q2Fresh
---
# Create linked servers (SQL Server Database Engine)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article shows how to create a linked server and access data from another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], an Azure SQL Managed Instance, or another data source by using [SQL Server Management Studio](../../ssms/menu-help/about-sql-server-management-studio.md) (SSMS) or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Linked servers enable the SQL Server database engine and Azure SQL Managed Instance to read data from the remote data sources and execute commands against the remote database servers (for example, OLE DB data sources) outside of the instance of SQL Server.
  
## <a name="Background"></a> Background


Linked servers are typically configured to enable the database engine to execute a Transact-SQL statement that includes tables in another instance of SQL Server, or another database product such as Oracle. Many types of data sources can be configured as linked servers, including third-party database providers and Azure Cosmos DB.


After a linked server is created, distributed queries can be run against this server, and queries can join tables from more than one data source. If the linked server is defined as an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or an Azure SQL Managed Instance, remote stored procedures can be executed.  
  
The capabilities and required arguments of the linked server can vary significantly. The examples in this article provide a typical example but all options are not described. For more information, see [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  
  
## Permissions

When using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, requires **ALTER ANY LINKED SERVER** permission on the server or membership in the **setupadmin** fixed server role. When using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] requires **CONTROL SERVER** permission or membership in the **sysadmin** fixed server role.  
  
## Create a linked server with SSMS

Create a linked server with SSMS using the following procedure:

### Open the New Linked Server dialog

In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS):

- Open Object Explorer.
- Expand **Server Objects**.
- Right-click **Linked Servers**.
- Select **New Linked Server**.  
  
### Edit the General page for the linked server properties

On the **General** page, in the **Linked server** box, type the name of the instance of **SQL Server** that you area linking to.

  > [!NOTE]  
  > If the instance of **SQL Server** is the default instance, enter the name of the computer that hosts the instance of **SQL Server**. If the **SQL Server** is a named instance, enter the name of the computer and the name of the instance, such as **Accounting\SQLExpress**.

Specify the **Server type** and related information if needed:

- **SQL Server**  
    Identify the linked server as an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or an Azure SQL Managed Instance. If you use this method of defining a linked server, the name specified in **Linked server** must be the network name of the server. Also, any tables retrieved from the server are from the default database defined for the login on the linked server.  

- **Other data source**  
    Specify an OLE DB server type other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clicking this option activates the options below it.  

  - **Provider**  
        Select an OLE DB data source from the list box. The OLE DB provider is registered with the given PROGID in the registry.  

  - **Product name**  
      Type the product name of the OLE DB data source to add as a linked server.  

  - **Data source**  
        Type the name of the data source as interpreted by the OLE DB provider. If you are connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], provide the instance name.  

  - **Provider string**  
        Type the unique programmatic identifier (PROGID) of the OLE DB provider that corresponds to the data source. For examples of valid provider strings, see [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  

  - **Location**  
        Type the location of the database as interpreted by the OLE DB provider.  

  - **Catalog**  
        Type the name of the catalog to use when making a connection to the OLE DB provider.  

### Edit the Security page for the linked server properties
  
On the **Security** page, specify the security context that will be used when the original instance connects to the linked server. There are two strategies to configure here that can be used alone or combined. The first is to map logins from the local server to the remote server, and the second is how the linked server should treat logins that are not mapped. 
#### Add login mappings

You can optionally specify how specific local server logins will authenticate using the linked server.

Under **Local server login to remote server login mappings**, repeat the following process for each login you would like to map:

- Select **Add**.
  
- Specify a **Local login**.

    Specify the local login that can connect to the linked server. The local login can be either a login using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication or a Windows Authentication login. Using a Windows group is not supported. Use this list to restrict the connection to specific logins, or to allow some logins to connect as a different login.  
    
> [!NOTE]
> Common issues with linked servers using Windows Authentication to a remote SQL Server instance arise from issues with service principal names (SPNs). For more information, see [Service Principal Name (SPN) Support in Client Connections](../../connect/oledb/features/service-principal-name-spn-support-in-client-connections.md). **Microsoft Kerberos Configuration Manager for SQL Server** is a diagnostic tool that helps troubleshoot Kerberos related connectivity issues with SQL Server. For more information, see [Microsoft Kerberos Configuration Manager for SQL Server](https://www.microsoft.com/download/details.aspx?id=39046).

- Select **Impersonate** (optional).

    Pass the username and password from the local login to the linked server. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, a login with the exact same name and password must exist on the remote server. For Windows logins, the login must be a valid login on the linked server.  

    To use impersonation, the configuration must meet the requirement for delegation.  

- Specify a **Remote User** if you aren't using impersonation.

    Use the remote user to map user defined in **Local login**. The **Remote User** must be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login on the remote server.  

- Specify a **Remote Password** if you aren't using impersonation.
  - Specify the password of the Remote User.  


Select **Remove** to remove an existing local login, if desired.

#### Specify the default security context for logins not present in the mapping list

In a domain environment where users are connecting by using their domain logins, selecting **Be made using the login's current security context** is often the best choice. When users connect to the original **SQL Server** by using a **SQL Server** login, the best choice is often to select **By using this security context**, and then providing the necessary credentials to authenticate at the linked server.

Select one of the following options:

- **Not be made**  
    A connection will not be made for logins not defined in the list.  

- **Be made without using a security context**  
    A connection will be made without using a security context for logins not defined in the list.  

- **Be made using the login's current security context**  
    A connection will be made using the current security context of the login for logins not defined in the list. If connected to the local server using Windows Authentication, your Windows credentials will be used to connect to the remote server. If connected to the local server using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, login name and password will be used to connect to the remote server. In this case, a login with the exact same name and password must exist on the remote server.  

- **Be made using this security context**  
    A connection will be made using the login and password specified in the **Remote login** and **With password** boxes for logins not defined in the list. The remote login must be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login on the remote server.  

### Edit the Server Options page in linked server properties (optional)
  
To view or specify server options, select the **Server Options** page. You can edit any of the following options:
  
- **Collation Compatible**  
     Affects Distributed Query execution against linked servers. If this option is set to true, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assumes that all characters in the linked server are compatible with the local server, with regard to character set and collation sequence (or sort order). This enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to send comparisons on character columns to the provider. If this option is not set, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always evaluates comparisons on character columns locally.  
  
     This option should be set only if it is certain that the data source corresponding to the linked server has the same character set and sort order as the local server.  
  
- **Data Access**  
     Enables and disables a linked server for distributed query access.  
  
- **RPC**  
     Enables remote procedure calls (RPC) from the specified server.  

  
- **RPC Out**  
     Enables RPC to the specified server.  
  
- **Use Remote Collation**  
     Determines whether the collation of a remote column or of a local server will be used.  
  
     If true, the collation of remote columns is used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources, and the collation specified in collation name is used for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources.  
  
     If false, distributed queries will always use the default collation of the local server, while collation name and the collation of remote columns are ignored. The default is false.  
  
- **Collation Name**  
     Specifies the name of the collation used by the remote data source if use remote collation is true and the data source is not a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. The name must be one of the collations supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     Use this option when accessing an OLE DB data source other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but whose collation matches one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations.  
  
     The linked server must support a single collation to be used for all columns in that server. Do not set this option if the linked server supports multiple collations within a single data source, or if the linked server's collation cannot be determined to match one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations.  
  
- **Connection Timeout**  
     Time-out value in seconds for connecting to a linked server.  
  
     If 0, use the `sp_configure` default [remote login timeout](../../database-engine/configure-windows/configure-the-remote-login-timeout-server-configuration-option.md) option value.  
  
- **Query Timeout**  
     Time-out value in seconds for queries against a linked server.  
  
     If 0, use the `sp_configure` default [remote query timeout](../../database-engine/configure-windows/configure-the-remote-query-timeout-server-configuration-option.md) option value.  
  
- **Enable Promotion of Distributed Transactions**  
     Use this option to protect the actions of a server-to-server procedure through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) transaction. When this option is TRUE, calling a remote stored procedure starts a distributed transaction and enlists the transaction with MS DTC. For more information, see [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md).  
  
### Save the linked server

Select **OK**.
  
## View or edit linked server provider options in SSMS
  
All providers do not have the same options available. For example, some types of data have indexes available and some might not. Use this dialog box to help [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] understand the capabilities of the provider. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installs some common data providers, however when the product providing the data changes, the provider installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might not support all the newest features. The best source of information about the capabilities of the product providing the data is the documentation for that product.  
To open the linked server **Providers Options** page in SSMS:
- Open Object Explorer.
- Expand **Server Objects**.
- Expand **Linked Servers**.
- Expand **Providers**.
- Right-click a provider and select **Properties**.

Provider options are defined as follows:
  
- **Dynamic parameter**  
     Indicates that the provider allows '?' parameter marker syntax for parameterized queries. Set this option only if the provider supports the **ICommandWithParameters** interface and supports a '?' as the parameter marker. Setting this option allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute parameterized queries against the provider. The ability to execute parameterized queries against the provider can result in better performance for certain queries.  
  
- **Nested queries**  
     Indicates that the provider allows nested `SELECT` statements in the FROM clause. Setting this option allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to delegate certain queries to the provider that require nesting SELECT statements in the FROM clause.  
  
- **Level zero only**  
     Only level 0 OLE DB interfaces are invoked against the provider.  
  
- **Allow inprocess**  

    [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows the provider to be instantiated as an in-process server. When this option is not set, the default behavior is to instantiate the provider outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Instantiating the provider outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process protects the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process from errors in the provider. When the provider is instantiated outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process, updates or inserts referencing long columns (**text**, **ntext**, or **image**) are not allowed.  
      
- **Non transacted updates**  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows updates, even if **ITransactionLocal** is not available. If this option is enabled, updates against the provider are not recoverable, because the provider does not support transactions.  
  
- **Index as access path**  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to use indexes of the provider to fetch data. By default, indexes are used only for metadata and are never opened  
  
- **Disallow ad hoc access**  
     [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow ad hoc access through the OPENROWSET and OPENDATASOURCE functions against the OLE DB provider. When this option is not set, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also does not allow ad hoc access.  
  
- **Supports 'Like' operator**  
     Indicates that the provider supports queries using the LIKE key word.  
  
## <a name="TsqlProcedure"></a> Create a linked server with Transact-SQL  

 To create a linked server by using [!INCLUDE[tsql](../../includes/tsql-md.md)], use the [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md), and [sp_addlinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md) statements.  
  
This example creates a linked server to another instance of SQL Server using Transact-SQL:
  
1. In Query Editor, enter the following [!INCLUDE[tsql](../../includes/tsql-md.md)] command to link to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] named `SRVR002\ACCTG`:  
  
    ```sql  
    USE [master]  
    GO  
    EXEC master.dbo.sp_addlinkedserver   
        @server = N'SRVR002\ACCTG',   
        @srvproduct=N'SQL Server';  
    GO  
  
    ```  
  
2. Execute the following code to configure the linked server to use the domain credentials of the login that is using the linked server.  
  
    ```sql  
    EXEC master.dbo.sp_addlinkedsrvlogin   
        @rmtsrvname = N'SRVR002\ACCTG',   
        @locallogin = NULL ,   
        @useself = N'True';  
    GO  
    ```  
  
## <a name="FollowUp"></a> Follow Up: Steps to take after you create a linked server

The following steps help you validate a linked server.
  
### Test the linked server  

 Considering either of the following two approaches to test a linked server's authentication in your current security context.
 
   - To test the ability to connect to a linked server in SSMS, browse to the linked server in Object Explorer, right-click the linked server and then select **Test Connection**.  


- To test the ability to connect to a linked server in T-SQL , execute a simple SELECT statement, for example, to retrieve basic database catalog information. This example returns the names of the databases on the linked server.  

  
    ```sql  
    SELECT name FROM [SRVR002\ACCTG].master.sys.databases;  
    GO  
  
    ```  
  
### Join tables from a linked server

Use four-part names to refer to an object on a linked server. Execute the following code to return a list of all logins on the local server and their matching logins on the linked server.
  
```sql
SELECT local.name AS LocalLogins, linked.name AS LinkedLogins  
FROM master.sys.server_principals AS local  
LEFT JOIN [SRVR002\ACCTG].master.sys.server_principals AS linked  
     ON local.name = linked.name;  
GO  
```

When `NULL` is returned for the linked server login, it indicates that the login does not exist on the linked server. These logins will not be able to use the linked server unless the linked server is configured to pass a different security context or the linked server accepts anonymous connections.  


## Linked servers with Azure SQL Managed Instance

If you're using Azure SQL Managed Instance, see the following examples from [sp_addlinkedserver (Transact-SQL)](../system-stored-procedures/sp-addlinkedserver-transact-sql.md):

- [Create SQL Managed Instance linked server with managed identity Azure AD authentication](../system-stored-procedures/sp-addlinkedserver-transact-sql.md#h-create-sql-managed-instance-linked-server-with-managed-identity-azure-ad-authentication)

- [Create SQL Managed Instance linked server with pass-through Azure AD authentication](../system-stored-procedures/sp-addlinkedserver-transact-sql.md#i-create-sql-managed-instance-linked-server-with-pass-through-azure-ad-authentication)

## Next steps

Learn more about managing linked servers in these articles:

- [Linked servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md)
- [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
