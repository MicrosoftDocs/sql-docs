---
description: "sp_invoke_external_rest_endpoint (Transact-SQL)"
title: "sp_invoke_external_rest_endpoint (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 8/26/2022
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_invoke_external_rest_endpoint_TSQL"
  - "sys.sp_invoke_external_rest_endpoint"
  - "sys.sp_invoke_external_rest_endpoint_TSQL"
  - "sp_invoke_external_rest_endpoint"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_invoke_external_rest_endpoint"
author: yorek
ms.author: damauri
monikerRange: "=azuresqldb-current"
---
# sp_invoke_external_rest_endpoint (Transact-SQL) ** PREVIEW **

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The **sp_invoke_external_rest_endpoint** stored procedure invokes an HTTPS REST endpoint provided as an input argument to the procedure. 

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
> [!NOTE]
> The feature is in public preview. Make sure you read the [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/) document before using it.


## Syntax  
  
```  
EXEC @returnValue = sp_invoke_external_rest_endpoint 
  @url
  [ , [ @payload = ] N'json_payload' ]
  [ , [ @headers = ] N'http_headers_as_json_array' ]
  [ , [ @method = ] 'GET'|'POST'|'PUT'|'PATCH'|'DELETE' ]
  [ , [ @timeout = ] seconds ]
  [ , [ @credential = ] credential]
  [ , @response OUTPUT]
```  
  
## Arguments  

#### `@url = N'url'`

URL of the HTTPS REST endpoint to be called. *url* is **nvarchar(4000)** with no default.

#### `[ @payload =  N'payload' ]`

Payload that must be sent to the HTTPS REST endpoint. *payload* is **nvarchar(max)** with no default. Optional.

#### `[ @headers =  N'headers' ]`

Headers that must be sent as part of the request to the HTTPS REST endpoint. Headers must be specified using a flat JSON (a JSON document without nested structures) format. *headers* parameter is **nvarchar(4000)** with no default. Optional.

#### `[ @method =  N'method' ]`

HTTP method to be used to call the URL. Can only be one of the following values: `GET`, `POST`, `PUT`, `PATCH`, `DELETE`. *method* is **nvarchar(6)** with `POST` as default value. Optional.

#### `[ @timeout = seconds ]`

Time in seconds allowed for the HTTPS call to run. If full HTTP request and response can't be sent and received within the defined timeout in seconds, the stored procedure execution will be halted, and an exception will be raised. Timeout starts when HTTP connection starts and ends when the response, payload included if any, has been received. *timeout* is a positive **smallint** with a default value 5. Optional. Accepted values: 1 to 230.

#### `[ @credential =  credential ]`

Indicate what DATABASE SCOPED CREDENTIAL object will be used to inject authentication info in the HTTPS request. *credential* is a **sysname** with no default value. Optional.

#### `[ @response = @variable OUTPUT ]`

Allow the response received from the called endpoint to be passed into the specified variable. *response* is a **nvarchar(max)**. Optional.

## Return Values  

Execution will return 0 if the HTTP call was done and the HTTP status code received is a 2xx status code (Success). If the HTTP Status code received isn't in the 2xx range, the return value will be the HTTP Status code received. If the HTTP can't be done at all, an exception will be thrown. 

## Permissions  
   
Requires **EXECUTE ANY EXTERNAL ENDPOINT** database permission.  

For example:

```sql
GRANT EXECUTE ANY EXTERNAL ENDPOINT TO [<PRINCIPAL>];
```

## Response format

Response of the HTTP call and the resulting data sent back by the invoked endpoint is available through the @response output parameter. @response contains a JSON document with the following schema: 

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
- *result*: the JSON payload returned by the HTTP call. Omitted if the received HTTP result is a 204 (No Content).

In the `response` section, aside the HTTP status code and description, the entire set of received response headers will be provided in the 'headers' object. The following example shows a `response` section:

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


## Allow-Listed Endpoints

Only calls to endpoints in the following services are allowed:

Azure Service | Domain
------ | ------
Azure Functions  | *.azurewebsites.net  
Azure Apps Service | *.azurewebsites.net  
Azure App Service Environment	| *.appserviceenvironment.net
Azure Static Web Apps	| *.azurestaticapps.net
Azure Logic Apps | *.logic.azure.com
Azure Event Hubs | *.servicebus.windows.net
Azure Event Grid | *.eventgrid.azure.net
Azure Cognitive Services | *.cognitiveservices.azure.com
PowerApps / Dataverse	| *.api.crm.dynamics.com
Azure Container Instances	| *.azurecontainer.io
Power BI | api.powerbi.com
Microsoft Graph	| graph.microsoft.com
Analysis Services	| *.asazure.windows.net
IoT Central	| *.azureiotcentral.com
API Management| *.azure-api.net

> [!NOTE]
> If you want to invoke a REST service that is not within the allowed list, you can use API Management to securely expose the desired service and make ti available to sp_invoke_external_rest_endpoint

## Limits

#### Payload size
Payload size, both received and sent, is limited to 100 MB (UTF-8 encoded) 

#### URL length
The maximum URL length (generated after using the @url parameter and adding all the query string provided as credentials, if any) is 8 KB; the maximum query string length (query string + credential query string) is 4 KB

#### Headers size
The maximum request and response header size (all header fields - headers parameter + credential secret + system defined) is 8 KB

## Throttling

Number of concurrent connections to external endpoints done via sp_invoke_external_rest_endpoint are capped to 10% of worker threads, with a hard cap of max 150 workers. To check how many concurrent connections a database can sustain, you can run the following query on the database:

```sql
SELECT 
  DATABASEPROPERTYEX(DB_NAME(), 'ServiceObjective') AS slo_namne, 
  primary_group_max_outbound_connection_workers AS max_outbound_connection  
FROM
  sys.dm_user_db_resource_governance
```

If a new connection to an external endpoint using sp_invoke_external_rest_endpoint is tried, but the number of maximum concurrent connections is already reached, error 10928 will be raised. For example:

```
Msg 10928, Level 16, State 4, Procedure sys.sp_invoke_external_rest_endpoint_internal, Line 1 [Batch Start Line 0]
Resource ID : 1. The outbound connections limit for the database is 20 and has been reached. 
See 'https://docs.microsoft.com/azure/azure-sql/database/resource-limits-logical-server' for assistance.
```

## Credentials

Some REST endpoints require authentication in order to be properly invoked. Authentication can usually be done by passing some specific key-value pairs in the querystring or in the HTTP headers set with the request.

It's possible to use DATABASE SCOPED CREDENTIALS to securely store authentication data (like a Bearer token for example) to be used by sp_invoke_external_rest_endpoint to call a protected endpoint. When creating the DATABASE SCOPED CREDENTIAL, you used the IDENTITY parameter to specify what authentication data will be passed to the invoked endpoint and how. IDENTITY supports three options:

- `HTTPEndpointHeaders`: send specified authentication data using the **Request Headers**
- `HTTPEndpointQueryString`: send specified authentication data using the **Query String**
- `Managed Identity`: send the System Assigned **Managed Identity** using the request headers

the created DATABASE SCOPED CREDENTIAL can be used via the @credential parameter:

```
EXEC sp_invoke_external_rest_endpoint 
  @url = N'http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @credential = [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
```

### [Request Headers](#tab/request-headers)

With this IDENTITY value, the DATABASE SCOPED CREDENTIAL will be added to the request headers. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointHeaders', SECRET = '{"x-functions-key":"<your-function-key-here>"}';
```

### [Query String](#tab/query-string)

With this IDENTITY value, the DATABASE SCOPED CREDENTIAL will be added to the query string. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointQueryString', SECRET = '{"code":"<your-function-key-here>"}';
```

### [Managed Identity](#tab/managed-identity)

With this IDENTITY value, the DATABASE SCOPED CREDENTIAL the authentication information will be taken from the System-Assigned Managed Identity of the Azure SQL server in which the Azure SQL database is in and it will be passed in the request headers. The SECRET must be set to the APP_ID (or CLIENT_ID) used to configure Azure AD Authentication of the called endpoint. (For example: [Configure your App Service or Azure Functions app to use Azure AD login](/azure/app-service/configure-authentication-provider-aad))

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'Managed Identity', SECRET = '{"resourceid":"<APP_ID>"}';
```

---

### Credential name rules

The created DATABASE SCOPED CREDENTIAL must adhere to specific rules in order to be used with sp_invoke_external_rest_endpoint. The rules are the following:

1. Must be a valid URL
1. The URL domain must be one of those domains included in the allow-list
1. The URL must not contain a query string
1. Protocol + Fully Qualified Domain Name (FQDN) of the called URL must match Protocol + FQDN of the credential name
1. Each part of the called URL path must match completely with the respective part of URL path in the credential name
1. The credential must point to a path that is more generic than the request URL. For example, a credential created for path `https://northwind.azurewebsite.net/customers` can't be used for the URL `https://northwind.azurewebsite.net`

**Notes on Collation and Credential name rules**

The [RFC 3986 Section 6.2.2.1](https://www.rfc-editor.org/rfc/rfc3986#section-6.2.2.1) states that "When a URI uses components of the generic syntax, the component syntax equivalence rules always apply; namely, that the scheme and host are case-insensitive" and [RFC 7230 Section 2.7.3](https://www.rfc-editor.org/rfc/rfc7230#section-2.7.3) mention that "all other are compared in a case-sensitive manner". 

As there's a collation rule set at the database level, the following logic will be applied, to be coherent with the database collation rule and the RFC mentioned above. (The described rule could potentially be more restrictive than the RFC rules, for example if database is set to use a case-sensitive collation):

1.	Check if URL and Credential match using the database collation rules (and without doing any URL encoding). If yes move to the next point.
1.	Check if URL and Credential match using the RFC, which means
    -	Check the scheme and host using a case-insensitive logic (Latin1_General_100_CI_AS_KS_WS_SC)
    -	Check all other segments of the URL are compared in a case-sensitive manner (Latin1_General_100_BIN2)

### Grant permissions to use credential

Database users who access a DATABASE SCOPED CREDENTIAL must have permission to use that credential.

To use the credential, a database user must have `REFERENCES` permission on a specific credential:

```sql
GRANT REFERENCES ON CREDENTIAL::[<CREDENTIAL_NAME>] TO [<PRINCIPAL>];
```

## Remarks  

### Wait Type

When sp_invoke_external_rest_endpoint is waiting for the call to the invoked service to complete, it will report a HTTP_EXTERNAL_CONNECTION wait type.

### HTTPS and TLS

Only endpoints that are configured to use HTTPS with at least TLS 1.2 encryption protocol are supported. 

### HTTP Redirects

sp_invoke_external_rest_endpoint won't automatically follow any HTTP redirect received as a response from the invoked endpoint.

### HTTP Headers

sp_invoke_external_rest_endpoint will automatically inject the following headers in the HTTP request

-	*content-type*: set to `application/json; charset=utf-8`
-	*accept*: set to `application/json`
-	*user-agent*: set `<EDITION>/<PRODUCT VERSION>` for example: `SQL Azure/12.0.2000.8`

If the same headers are also specified via the @headers parameter, the system-supplied values will take precedence and overwrite any user-specified values. 

> [!NOTE]
> If you are testing invocation of the REST endpoint also with other tools, like [curl](https://curl.se/) or any modern REST client, like [Postman](https://www.postman.com/) or [Innsomnia](http://insomnia.rest/), make sure to include the same headers that are automatically injected by sp_invoke_external_rest_endpoint to have the same behavior and results.

## Examples  
  
### A. Call an Azure Function using an HTTP trigger binding without authentication

The following example calls an Azure Function using an HTTP trigger binding allowing anonymous access

```sql
DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint 
  @url = N'http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @headers = N'{"header1":"value_a", "header2":"value2", "header1":"value_b"}',
  @payload = N'{"some":{"data":"here"}}',
  @response = @response OUTPUT;
	
SELECT @ret AS ReturnCode, @response AS Response;
```

### A. Call an Azure Function using an HTTP Trigger binding with an authorization key

The following example calls an Azure Function using an HTTP trigger binding configured to require an authorization key. The authorization key will be passed in the `x-function-key` header as required by Azure Functions. More info here: [Azure Functions - API key authorization](/azure/azure-functions/functions-bindings-http-webhook-trigger#api-key-authorization)

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointHeaders', SECRET = '{"x-functions-key":"<your-function-key-here>"}';

DECLARE @ret INT, @response NVARCHAR(MAX);

EXEC @ret = sp_invoke_external_rest_endpoint 
  @url = N'http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @headers = N'{"header1":"value_a", "header2":"value2", "header1":"value_b"}',
  @credential = [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>],
  @payload = N'{"some":{"data":"here"}}',  
  @response = @response OUTPUT;
	
SELECT @ret AS ReturnCode, @response AS Response;
```


## See Also  

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [API Management](/azure/api-management/)
- [Resource management in Azure SQL Database](../../../azure-sql/database/resource-limits-logical-server.md)