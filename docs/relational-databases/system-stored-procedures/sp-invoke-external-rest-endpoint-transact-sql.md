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
# sp_invoke_external_rest_endpoint (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The **sp_invoke_external_rest_endpoint** stored procedure invokes a HTTPS REST endpoint provided as an input argument to the procedure. 

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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

#### `@url = N'url' `
URL of the HTTPS REST endpoint to be called. *url* is **nvarchar(4000)** with no default.

#### `[ @payload =  N'payload' ]`
Payload that must be sent to the HTTPS REST endpoint. *payload* is **nvarchar(max)** with no default. Optional.

#### `[ @headers =  N'headers' ]`
Headers that must be sent as part of the request to the HTTPS REST endpoint. Headers must be specified using a flat JSON (a JSON document without nested structures) format. *headers* is **nvarchar(4000)** with no default. Optional.

#### `[ @method =  N'method' ]`
HTTP method to be used to call the URL. Can only be one of the following values: `GET`, `POST`, `PUT`, `PATCH`, `DELETE`. *method* is **nvarchar(6)** with no `POST` as default value. Optional.

#### `[ @timeout = seconds ]`
Time in seconds allowed for the HTTPS call to run. If full HTTP request and response cannot be sent and received within the defined timeout in seconds, stored procedure execution will be halted, and an exception will raised. Timeout starts when HTTP connection starts and ends when the response, payload included if any, has been received. *timeout* is a positive **smallint** with a default value 5. Optional.Accepted values: 1 to 230.

#### `[ @credential =  credential ]`
Indicate what DATABASE SCOPED CREDENTIAL object will be used to inject authentication info in the HTTPS request. *credential* is a **sysname** with no default value. Optional.

#### `[ @response = @variable OUTPUT ]`
Allow the response to be passed into the specified variable. *response* is a **nvarchar(max)**. Optional.

## Return Values  

Execution will return 0 if the HTTP call was done and the HTTP status code received is a 2xx status code (Success). If the HTTP Status code received is not in the 2xx range, the return value will be the HTTP Status code received. If the HTTP cannot be done at all an exception will be raised. 

## Remarks  

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

- *response*: a JSON object that contains the HTTP result and additional response metadata.
- *result*: the JSON payload returned by the HTTP call. Omitted if the received HTTP result is a 204 (No Content).

### Limits

WIP

### Throttling

WIP

### HTTPS and TLS

Only endpoints that are configured to use HTTPS with at least TLS 1.2 encryption protocol are supported. 

### HTTP Redirects

sp_invoke_external_rest_endpoint will not automatically follow any HTTP redirect received as a response from the invoked endpoint.

### HTTP Headers

sp_invoke_external_rest_endpoint will automatically inject the following headers in the HTTP request

-	*content-type*: set to `application/json; charset=utf-8`
-	*accept*: set to `application/json`
-	*user-agent*: set `<EDITION>/<PRODUCT VERSION>` for example: `SQL Azure/12.0.2000.8`

If the same headers are also specied via the @headers parameter, the system-supplied values will take precedence and overwrite any user-specified values. 

> [!WARNING]
> If you are testing invocation of the REST endpoint also with other tools, like [curl](https://curl.se/) or any modern REST client, like [Postman](https://www.postman.com/) or [Innsomnia](http://insomnia.rest/), make sure to include the same headers that are automatically injected by sp_invoke_external_rest_endpoint to have the same behavior and results.

### Allow-Listed Endpoints

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

## Credentials

Some REST endpoints requires authentication in order to be properly invoked. Authentication can be done by passing some specific key-value pairs in the querystring or in the HTTP headers set with the request.

It is possible to use DATABASE SCOPED CREDENTIALS to securely store authentication data (like a Bearer token for example) to be used by sp_invoke_external_rest_endpoint to call a protected endpoint. When creating the DATABASE SCOPED CREDENTIAL you used the IDENTITY parameter to specify what authentication data will be passed to the invoked endpoint and how. IDENTITY supports three options:

- `HTTPEndpointHeaders`: send specified authentication data using the **Request Headers**
- `HTTPEndpointQueryString`: send specified authentication data using the **Query String**
- `Managed Identity`: send the System Assigned **Managed Identity** using the request headers

the created DATABASE SCOPED CREDENTIAL can ben used via the @credential parameter:

```
EXEC sp_invoke_external_rest_endpoint 
  @url = N'http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>?key1=value1',
  @credential = [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
```

### [Request Headers](#tab/request-headers)

With this IDENTITY value the DATABASE SCOPED CREDENTIAL will be added to the request headers. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointHeaders', SECRET = '{"x-functions-key":"<your-function-key-here>"}';
```

### [Query String](#tab/query-string)

With this IDENTITY value the DATABASE SCOPED CREDENTIAL will be added to the query string. The key-value pair containing the authentication information must be provided via the SECRET parameter using a flat JSON format. For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'HTTPEndpointQueryString', SECRET = '{"code":"<your-function-key-here>"}';
```

### [Managed Identity](#tab/managed-identity)

With this IDENTITY value the DATABASE SCOPED CREDENTIAL the authentication information will be taken from the System-Assigned Managed Identity of the Azure SQL server in which the Azure SQL database is in and it will be passed in the request headers. The SECRET must be set to the APP_ID (or CLIENT_ID) used to configure AAD Authentication of the called endpoint. (For example: [Configure your App Service or Azure Functions app to use Azure AD login](https://docs.microsoft.com/en-us/azure/app-service/configure-authentication-provider-aad))

```sql
CREATE DATABASE SCOPED CREDENTIAL [http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>]
WITH IDENTITY = 'Managed Identity', SECRET = '{"resourceid":"<APP_ID>"}';
```

---

### Credential name rules

WIP

### Grant permissions to use credential

Database users who access a DATABASE SCOPED CREDENTIAL must have permission to use that credential.

To use the credential, a database user must have `REFERENCES` permission on a specific credential:

```sql
GRANT REFERENCES ON CREDENTIAL::[<CREDENTIAL_NAME>] TO [<PRINCIPAL>];
```

## Permissions  
   
Requires **EXECUTE ANY EXTERNAL ENDPOINT** database permission.  

For example:

```sql
GRANT EXECUTE ANY EXTERNAL ENDPOINT TO [<PRINCIPAL>];
```

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

The following example calls an Azure Function using an HTTP trigger binding configured to require an authorization key. The authorization key will be passed in the `x-function-key` header as required by Azure Functions. More info here: [Azure Functions - API key authorization](https://docs.microsoft.com/azure/azure-functions/functions-bindings-http-webhook-trigger#api-key-authorization)

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
- [API Management](https://docs.microsoft.com/azure/api-management/)