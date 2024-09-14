---
title: "sp_invoke_external_rest_endpoint (Transact-SQL)"
description: The sp_invoke_external_rest_endpoint stored procedure invokes an HTTPS REST endpoint.
author: jettermctedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 09/10/2024
ms.service: sql
ms.topic: "reference"
f1_keywords:
  - "sp_invoke_external_rest_endpoint_TSQL"
  - "sys.sp_invoke_external_rest_endpoint"
  - "sys.sp_invoke_external_rest_endpoint_TSQL"
  - "sp_invoke_external_rest_endpoint"
helpviewer_keywords:
  - "sp_invoke_external_rest_endpoint"
dev_langs:
  - "TSQL"
monikerRange: "azuresqldb-current"
---
# sp_invoke_external_rest_endpoint (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The `sp_invoke_external_rest_endpoint` stored procedure invokes an HTTPS REST endpoint provided as an input argument to the procedure.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXEC @returnValue = sp_invoke_external_rest_endpoint
  [ @url = ] N'url'
  [ , [ @payload = ] N'request_payload' ]
  [ , [ @headers = ] N'http_headers_as_json_array' ]
  [ , [ @method = ] 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE' | 'HEAD' ]
  [ , [ @timeout = ] seconds ]
  [ , [ @credential = ] credential ]
  [ , @response OUTPUT ]
```

## Arguments

#### [ @url = ] N'*url*'

URL of the HTTPS REST endpoint to be called. *@url* is **nvarchar(4000)** with no default.

#### [ @payload = ] N'*request_payload*'

Unicode string in a JSON, XML, or TEXT format that contains the payload to send to the HTTPS REST endpoint. Payloads must be a valid JSON document, a well formed XML document, or text. *@payload* is **nvarchar(max)** with no default.

#### [ @headers = ] N'*headers*'

Headers that must be sent as part of the request to the HTTPS REST endpoint. Headers must be specified using a flat JSON (a JSON document without nested structures) format. Headers defined in the [Forbidden headers name](https://developer.mozilla.org/en-US/docs/Glossary/Forbidden_header_name) list will be ignored even if explicitly passed in the *@headers* parameter; their values will be discarded or replaced with system-supplied values when starting the HTTPS request.

The *@headers* parameter is **nvarchar(4000)** with no default.

#### [ @method = ] N'*method*'

HTTP method for calling the URL. Must be one of the following values: `GET`, `POST`, `PUT`, `PATCH`, `DELETE`, `HEAD`. *@method* is **nvarchar(6)** with `POST` as default value.

#### [ @timeout = ] *seconds*

Time in seconds allowed for the HTTPS call to run. If the full HTTP request and response can't be sent and received within the defined timeout in seconds, the stored procedure execution is halted, and an exception is raised. Timeout starts when the HTTP connection starts and ends when the response, and payload included if any, has been received. *@timeout* is a positive **smallint** with a default value 30. Accepted values: 1 to 230.

#### [ @credential = ] *credential*

Indicate which DATABASE SCOPED CREDENTIAL object is used to inject authentication info in the HTTPS request. *@credential* is **sysname** with no default value.

#### @response OUTPUT

Allow the response received from the called endpoint to be passed into the specified variable. *@response* is **nvarchar(max)**.

## Return value

Execution will return `0` if the HTTPS call was done and the HTTP status code received is a 2xx status code (`Success`). If the HTTP status code received isn't in the 2xx range, the return value will be the HTTP status code received. If the HTTPS call can't be done at all, an exception will be thrown.

## Permissions

Requires EXECUTE ANY EXTERNAL ENDPOINT database permission.

For example:

```sql
GRANT EXECUTE ANY EXTERNAL ENDPOINT TO [<PRINCIPAL>];
```

## Response format

Response of the HTTP call and the resulting data sent back by the invoked endpoint is available through the *@response* output parameter. *@response* might contain a JSON document with the following schema:

```json
{
  "response": {
    "status": {
      "http": {
        "code": "",
        "description": ""
      }
    },
    "headers": {}
  },
  "result": {}
}
```

Specifically:

- *response*: a JSON object that contains the HTTP result and other response metadata.
- *result*: the JSON payload returned by the HTTP call. Omitted if the received HTTP result is a 204 (`No Content`).

Or the *@response* might contain an XML document with the following schema:

```xml
<output>
    <response>
        <status>
            <http code="" description=" " />
        </status>
        <headers>
            <header key="" value="" />
            <header key="" value="" />
        </headers>
    </response>
    <result>
    </result>
</output>
```

Specifically:

- *response*: an XML object that contains the HTTP result and other response metadata.
- *result*: the XML payload returned by the HTTP call. Omitted if the received HTTP result is a 204 (`No Content`).

In the `response` section, aside from the HTTP status code and description, the entire set of received response headers will be provided in the `headers` object. The following example shows a `response` section in JSON (also the structure for text responses):

```json
"response": {
  "status": {
    "http": {
      "code": 200,
      "description": "OK"
    }
  },
  "headers": {
    "Date": "Thu, 08 Sep 2022 21:51:22 GMT",
    "Content-Length": "1345",
    "Content-Type": "application\/json; charset=utf-8",
    "Server": "Kestrel",
    "Strict-Transport-Security": "max-age=31536000; includeSubDomains"
    }
  }
```

And the following example shows a `response` section in XML:

```XML
<response>
    <status>
        <http code="200" description="OK" />
    </status>
    <headers>
        <header key="Date" value="Tue, 01 Apr 1976 21:12:04 GMT" />
        <header key="Content-Length" value="2112" />
        <header key="Content-Type" value="application/xml" />
        <header key="Server" value="Windows-Azure-Blob/1.0 Microsoft-HTTPAPI/2.0" />
        <header key="x-ms-request-id" value="31536000-64bi-64bi-64bi-31536000" />
        <header key="x-ms-version" value="2021-10-04" />
        <header key="x-ms-creation-time" value="Wed, 19 Apr 2023 22:17:33 GMT" />
        <header key="x-ms-server-encrypted" value="true" />
    </headers>
</response>
```

## Allowed endpoints

Only calls to endpoints in the following services are allowed:

| Azure Service | Domain |
| --- | --- |
| Azure Functions | *.azurewebsites.net |
| Azure Apps Service | *.azurewebsites.net |
| Azure App Service Environment | *.appserviceenvironment.net |
| Azure Static Web Apps | *.azurestaticapps.net |
| Azure Logic Apps | *.logic.azure.com |
| Azure Event Hubs | *.servicebus.windows.net |
| Azure Event Grid | *.eventgrid.azure.net |
| Azure Cognitive Services | *.cognitiveservices.azure.com |
| Azure OpenAI | *.openai.azure.com |
| PowerApps / Dataverse | *.api.crm.dynamics.com |
| Microsoft Dynamics | *.dynamics.com |
| Azure Container Instances | *.azurecontainer.io |
| Azure Container Apps | *.azurecontainerapps.io |
| Power BI | api.powerbi.com |
| Microsoft Graph | graph.microsoft.com |
| Analysis Services | *.asazure.windows.net |
| IoT Central | *.azureiotcentral.com |
| API Management | *.azure-api.net |
| Azure Blob Storage | *.blob.core.windows.net |
| Azure Files | *.file.core.windows.net |
| Azure Queue Storage | *.queue.core.windows.net |
| Azure Table Storage | *.table.core.windows.net |
| Azure Communication Services | *.communications.azure.com |
| Bing Search | api.bing.microsoft.com |
| Azure Key Vault | *.vault.azure.net |
| Azure AI Search | *.search.windows.net |
| Azure Maps | *.atlas.microsoft.com |
| Azure AI Translator | api.cognitive.microsofttranslator.com |

[Outbound firewall rules for Azure SQL Database and Azure Synapse Analytics](/azure/azure-sql/database/outbound-firewall-rule-overview) control mechanism can be used to further restrict outbound access to external endpoints.

> [!NOTE]  
> If you want to invoke a REST service that isn't within the allowed list, you can use API Management to securely expose the desired service and make it available to `sp_invoke_external_rest_endpoint`.

## Limits

#### Payload size

Payload, both when received and when sent, is UTF-8 encoded when sent over the wire. In that format, its size is limited to 100 MB.

#### URL length

The maximum URL length (generated after using the *@url* parameter and adding the specified credentials to the query string, if any) is 8 KB; the maximum query string length (query string + credential query string) is 4 KB.

#### Headers size

The maximum request and response header size (all header fields: headers passed via *@headers* parameter + credential header + system supplied headers) is 8 KB.

## Throttling

The number of concurrent connections to external endpoints done via `sp_invoke_external_rest_endpoint` are capped to 10% of worker threads, with a maximum of 150 workers. On an [single database](/azure/azure-sql/database/single-database-overview) throttling is enforced at the database level, while on an [elastic pool](/azure/azure-sql/database/elastic-pool-overview) throttling is enforced both at database and at pool level.

To check how many concurrent connections a database can sustain, run the following query:

```sql
SELECT
  [database_name],
  DATABASEPROPERTYEX(DB_NAME(), 'ServiceObjective') AS service_level_objective,
  [slo_name] as service_level_objective_long,
  [primary_group_max_outbound_connection_workers] AS max_database_outbound_connection,
  [primary_pool_max_outbound_connection_workers] AS max_pool_outbound_connection
FROM
  sys.dm_user_db_resource_governance
WHERE
  database_id = DB_ID();
```

If a new connection to an external endpoint using `sp_invoke_external_rest_endpoint` is tried when the maximum concurrent connections are already reached, error 10928 (or 10936 if you have reached elastic pools limits) will be raised. For example:

```output
Msg 10928, Level 16, State 4, Procedure sys.sp_invoke_external_rest_endpoint_internal, Line 1 [Batch Start Line 0]
Resource ID : 1. The outbound connections limit for the database is 20 and has been reached.
See 'https://docs.microsoft.com/azure/azure-sql/database/resource-limits-logical-server' for assistance.
```

## Credentials

Some REST endpoints require authentication in order to be properly invoked. Authentication can usually be done by passing some specific key-value pairs in the query string or in the HTTP headers set with the request.

It's possible to use DATABASE SCOPED CREDENTIALS to securely store authentication data (like a Bearer token for example) to be used by `sp_invoke_external_rest_endpoint` to call a protected endpoint. When creating the DATABASE SCOPED CREDENTIAL, use the IDENTITY parameter to specify what authentication data will be passed to the invoked endpoint and how. IDENTITY supports four options:

- `HTTPEndpointHeaders`: send specified authentication data using the **Request Headers**
- `HTTPEndpointQueryString`: send specified authentication data using the **Query String**
- `Managed Identity`: send the System Assigned **Managed Identity** using the request headers
- `Shared Access Signature`: provide limited delegated access to resources via a **signed URL** (Also referred to as SAS)

the created DATABASE SCOPED CREDENTIAL can be used via the *@credential* parameter:

```sql
EXEC sp_invoke_external_rest_endpoint
  @url = N'https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @credential = [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
```

### [Request Headers](#tab/request-headers)

With this IDENTITY value, the DATABASE SCOPED CREDENTIAL will be added to the request headers. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointHeaders', SECRET = '{"x-functions-key":"<your-function-key-here>"}';
```

### [Query String](#tab/query-string)

With this IDENTITY value, the DATABASE SCOPED CREDENTIAL will be added to the query string. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointQueryString', SECRET = '{"code":"<your-function-key-here>"}';
```

### [Managed Identity](#tab/managed-identity)

[!INCLUDE [entra-authentication-options](../../includes/entra-authentication-options.md)]

With this IDENTITY value, the authentication information for the DATABASE SCOPED CREDENTIAL is taken from the system-assigned managed identity of the logical server in which the database resides, and is passed in the request headers. The SECRET must be set to the APP_ID (or CLIENT_ID) used to configure Microsoft Entra authentication of the called endpoint. (For example: [Configure your App Service or Azure Functions app to use Microsoft Entra sign-in](/azure/app-service/configure-authentication-provider-aad))

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'Managed Identity', SECRET = '{"resourceid":"<APP_ID>"}';
```

Both system-assigned and user-assigned managed identities are supported:

- If there's at least one user-assigned managed identity, the defined primary identity is used for authentication when using a managed identity-based database scoped credential.

- If there's no user-assigned managed identity assigned then the system-assigned managed identity is used, if it's enabled, for authentication when using a managed identity-based database scoped credential.

- If both user-assigned and system-assigned managed identities are defined, the user-assigned managed identity is used.

- If there's more than one user-assigned managed identity assigned, only the primary identity is used.

---

### Credential name rules

The created DATABASE SCOPED CREDENTIAL must adhere to specific rules in order to be used with `sp_invoke_external_rest_endpoint`. The rules are the following:

- Must be a valid URL
- The URL domain must be one of those domains included in the allowlist
- The URL must not contain a query string
- Protocol + Fully Qualified Domain Name (FQDN) of the called URL must match Protocol + FQDN of the credential name
- Each part of the called URL path must match completely with the respective part of URL path in the credential name
- The credential must point to a path that is more generic than the request URL. For example, a credential created for path `https://northwind.azurewebsite.net/customers` can't be used for the URL `https://northwind.azurewebsite.net`

#### Collation and credential name rules

[RFC 3986 Section 6.2.2.1](https://www.rfc-editor.org/rfc/rfc3986#section-6.2.2.1) states that "When a URI uses components of the generic syntax, the component syntax equivalence rules always apply; namely, that the scheme and host are case-insensitive", and [RFC 7230 Section 2.7.3](https://www.rfc-editor.org/rfc/rfc7230#section-2.7.3) mentions that "all other are compared in a case-sensitive manner".

As there's a collation rule set at the database level, the following logic will be applied, to be coherent with the database collation rule and the RFC mentioned above. (The described rule could potentially be more restrictive than the RFC rules, for example if database is set to use a case-sensitive collation.):

1. Check if the URL and credential match using the RFC, which means:
   - Check the scheme and host using a case-insensitive collation (`Latin1_General_100_CI_AS_KS_WS_SC`)
   - Check all other segments of the URL are compared in a case-sensitive collation (`Latin1_General_100_BIN2`)
1. Check that the URL and credential match using the database collation rules (and without doing any URL encoding).

### Grant permissions to use credential

Database users who access a DATABASE SCOPED CREDENTIAL must have permission to use that credential.

To use the credential, a database user must have `REFERENCES` permission on a specific credential:

```sql
GRANT REFERENCES ON DATABASE SCOPED CREDENTIAL::[<CREDENTIAL_NAME>] TO [<PRINCIPAL>];
```

## Remarks

### Wait type

When `sp_invoke_external_rest_endpoint` is waiting for the call to the invoked service to complete, it will report an HTTP_EXTERNAL_CONNECTION wait type.

### HTTPS and TLS

Only endpoints that are configured to use HTTPS with at least TLS 1.2 encryption protocol are supported.

### HTTP redirects

`sp_invoke_external_rest_endpoint` won't automatically follow any HTTP redirect received as a response from the invoked endpoint.

### HTTP headers

`sp_invoke_external_rest_endpoint` will automatically inject the following headers in the HTTP request:

- *content-type*: set to `application/json; charset=utf-8`
- *accept*: set to `application/json`
- *user-agent*: set `<EDITION>/<PRODUCT VERSION>` for example: `SQL Azure/12.0.2000.8`

While *user-agent* will always be overwritten by the stored procedure, the *content-type* and *accept* header values can be user defined via the *@headers* parameter. Only the media type directive is allowed to be specified in the content-type and specifying the charset or boundary directives isn't possible.

#### Request and response payload supported [media types](https://developer.mozilla.org/en-US/docs/Glossary/MIME_type)

The following are accepted values for the header *content-type*.

- application/json
- application/vnd.microsoft.*.json
- application/xml
- application/vnd.microsoft.*.xml
- application/vnd.microsoft.*+xml
- application/x-www-form-urlencoded
- text/*

For the *accept* header, the following are the accepted values.

- application/json
- application/xml
- text/*

For more information on text header types, refer to the [text type registry at IANA](https://www.iana.org/assignments/media-types/media-types.xhtml#text).

> [!NOTE]  
> If you're testing invocation of the REST endpoint with other tools, like [cURL](https://curl.se/) or any modern REST client like [Insomnia](https://insomnia.rest/), make sure to include the same headers that are automatically injected by `sp_invoke_external_rest_endpoint` to have the same behavior and results.

## Best practices

### Use a batching technique

If you have to send a set of rows to a REST endpoint, for example to an Azure Function or to an event hub, it's recommended to batch the rows into a single JSON document, to avoid the HTTPS call overhead for each row sent. This can be done using the `FOR JSON` statement, for example:

```sql
-- create the payload
DECLARE @payload AS NVARCHAR(MAX);

SET @payload = (
        SELECT [object_id], [name], [column_id]
        FROM sys.columns
        FOR JSON AUTO
        );

-- invoke the REST endpoint
DECLARE @retcode INT,
    @response AS NVARCHAR(MAX);

EXEC @retcode = sp_invoke_external_rest_endpoint @url = '<REST_endpoint>',
    @payload = @payload,
    @response = @response OUTPUT;

-- return the result
SELECT @retcode, @response;
```

## Examples

Here you can find some examples on how to use `sp_invoke_external_rest_endpoint` to integrate with common Azure Services like Azure Functions or Azure Event Hubs. More samples to integrate with other services can be found on [GitHub](https://github.com/Azure-Samples/azure-sql-db-invoke-external-rest-endpoints).

### A. Call an Azure Function using an HTTP trigger binding without authentication

The following example calls an Azure Function using an HTTP trigger binding allowing anonymous access.

```sql
DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint
  @url = N'https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @headers = N'{"header1":"value_a", "header2":"value2", "header1":"value_b"}',
  @payload = N'{"some":{"data":"here"}}',
  @response = @response OUTPUT;

SELECT @ret AS ReturnCode, @response AS Response;
```

### B. Call an Azure Function using an HTTP trigger binding with an authorization key

The following example calls an Azure Function using an HTTP trigger binding configured to require an authorization key. The authorization key will be passed in the `x-function-key` header as required by Azure Functions. For more information, see [Azure Functions - API key authorization](/azure/azure-functions/functions-bindings-http-webhook-trigger#api-key-authorization).

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointHeaders', SECRET = '{"x-functions-key":"<your-function-key-here>"}';

DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint
  @url = N'https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @headers = N'{"header1":"value_a", "header2":"value2", "header1":"value_b"}',
  @credential = [https://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>],
  @payload = N'{"some":{"data":"here"}}',
  @response = @response OUTPUT;

SELECT @ret AS ReturnCode, @response AS Response;
```

### C. Read the contents of a file from Azure Blob Storage with a SAS token

This example reads a file from Azure Blob Storage using a SAS token for authentication. The results will be returned in XML, so using the header `"Accept":"application/xml"` will be needed.

```sql
DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint
  @url = N'https://blobby.blob.core.windows.net/datafiles/my_favorite_blobs.txt?sp=r&st=2023-07-28T19:56:07Z&se=2023-07-29T03:56:07Z&spr=https&sv=2022-11-02&sr=b&sig=XXXXXX1234XXXXXX6789XXXXX',
  @headers = N'{"Accept":"application/xml"}',
  @method = 'GET',
  @response = @response OUTPUT;

SELECT @ret AS ReturnCode, @response AS Response;
```

### D. Send a message to an event hub using the Azure SQL Database Managed Identity

This sample shows how you can send messages to Event Hubs using the Azure SQL Managed Identity. Make sure you have configured the [System Managed Identity](/azure/active-directory/managed-identities-azure-resources/overview) for the Azure SQL Database logical server hosting your database, for example:

```bash
az sql server update -g <resource-group> -n <azure-sql-server> --identity-type SystemAssigned
```

After that, configure Event Hubs to allow Azure SQL Server's Managed Identity to be able to send messages ("Azure Event Hubs Data Sender" role) to the desired event hub. For more information, see [Use Event Hubs with managed identities](/azure/event-hubs/authenticate-managed-identity?tabs=latest#use-event-hubs-with-managed-identities).

Once this is done, you can use the `Managed Identity` identity name when defining the database scoped credential that will be used by `sp_invoke_external_rest_endpoint`. As explained in [Authenticate an application with Microsoft Entra ID to access Event Hubs resources](/azure/event-hubs/authenticate-application), the resource name (or ID) to use when using Microsoft Entra authentication is `https://eventhubs.azure.net`:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://<EVENT-HUBS-NAME>.servicebus.windows.net]
    WITH IDENTITY = 'Managed Identity',
        SECRET = '{"resourceid": "https://eventhubs.azure.net"}';
GO

DECLARE @Id UNIQUEIDENTIFIER = NEWID();
DECLARE @payload NVARCHAR(MAX) = (
        SELECT *
        FROM (
            VALUES (@Id, 'John', 'Doe')
            ) AS UserTable(UserId, FirstName, LastName)
        FOR JSON AUTO,
            WITHOUT_ARRAY_WRAPPER
        )
DECLARE @url NVARCHAR(4000) = 'https://<EVENT-HUBS-NAME>.servicebus.windows.net/from-sql/messages';
DECLARE @headers NVARCHAR(4000) = N'{"BrokerProperties": "' + STRING_ESCAPE('{"PartitionKey": "' + CAST(@Id AS NVARCHAR(36)) + '"}', 'json') + '"}'
DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint @url = @url,
    @headers = @headers,
    @credential = [https://<EVENT-HUBS-NAME>.servicebus.windows.net],
    @payload = @payload,
    @response = @response OUTPUT;

SELECT @ret AS ReturnCode, @response AS Response;
```

### E. Read and write a file to Azure File Storage with an Azure SQL Database scoped credentials

This example writes a file to an Azure File Storage using an Azure SQL Database scoped credentials for authentication and then returns the contents. The results will be returned in XML, so using the header `"Accept":"application/xml"` will be needed.

Start by creating a master key for the Azure SQL Database

```sql
create master key encryption by password = '2112templesmlm2BTS21.qwqw!@0dvd'
go
```

Then, create the database scoped credentials using the SAS token provided by the Azure Blob Storage Account.

```sql
create database scoped credential [filestore]
with identity='SHARED ACCESS SIGNATURE',
secret='sv=2022-11-02&ss=bfqt&srt=sco&sp=seespotrun&se=2023-08-03T02:21:25Z&st=2023-08-02T18:21:25Z&spr=https&sig=WWwwWWwwWWYaKCheeseNXCCCCCCDDDDDSSSSSU%3D'
go
```

Next, create the file and add text to it with the following two statements:

```sql
declare @payload nvarchar(max) = (select * from (values('Hello from Azure SQL!', sysdatetime())) payload([message], [timestamp])for json auto, without_array_wrapper)
declare @response nvarchar(max), @url nvarchar(max), @headers nvarchar(1000);
declare @len int = len(@payload)

-- Create the File
set @url = 'https://myfiles.file.core.windows.net/myfiles/test-me-from-azure-sql.json'
set @headers = json_object(
        'x-ms-type': 'file',
        'x-ms-content-length': cast(@len as varchar(9)),
        'Accept': 'application/xml')
exec sp_invoke_external_rest_endpoint
    @url = @url,
    @method = 'PUT',
    @headers = @headers,
    @credential = [filestore],
    @response = @response output
select cast(@response as xml);

-- Add text to the File
set @headers = json_object(
        'x-ms-range': 'bytes=0-' + cast(@len-1 as varchar(9)),
        'x-ms-write': 'update',
        'Accept': 'application/xml');
set @url = 'https://myfiles.file.core.windows.net/myfiles/test-me-from-azure-sql.json'
set @url += '?comp=range'
exec sp_invoke_external_rest_endpoint
    @url = @url,
    @method = 'PUT',
    @headers = @headers,
    @payload = @payload,
    @credential = [filestore],
    @response = @response output
select cast(@response as xml)
go
```

Finally, use the following statement to read the file

```sql
declare @response nvarchar(max);
declare @url nvarchar(max) = 'https://myfiles.file.core.windows.net/myfiles/test-me-from-azure-sql.json'
exec sp_invoke_external_rest_endpoint
    @url = @url,
    @headers = '{"Accept":"application/xml"}',
    @credential = [filestore],
    @method = 'GET',
    @response = @response output
select cast(@response as xml)
go
```

## Related content

- [Resource management in Azure SQL Database](/azure/azure-sql/database/resource-limits-logical-server)
- [sys.dm_resource_governor_resource_pools_history_ex](../system-dynamic-management-views/sys-dm-resource-governor-resource-pools-history-ex-azure-sql-database.md)
- [sys.dm_resource_governor_workload_groups_history_ex](../system-dynamic-management-views/sys-dm-resource-governor-workload-groups-history-ex-azure-sql-database.md)
- [sys.dm_user_db_resource_governance](../system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database.md)
- [GRANT Database Permissions (Transact-SQL)](../../t-sql/statements/grant-database-permissions-transact-sql.md)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [API Management](/azure/api-management/)
- [sp_invoke_external_rest_endpoint usage samples](https://github.com/Azure-Samples/azure-sql-db-invoke-external-rest-endpoints)
