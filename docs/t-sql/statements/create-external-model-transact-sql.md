---
title: "CREATE EXTERNAL MODEL (Transact-SQL)"
description: CREATE EXTERNAL MODEL (Transact-SQL) for creating an external model object that contains the location, authentication method, and purpose of an AI model inference endpoint.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: bspendolini
ms.date: 01/06/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sql-ai
  - ignite-2025
f1_keywords:
  - "CREATE_EXTERNAL_MODEL"
  - "EXTERNAL MODEL"
  - "ai_generate_embeddings"
helpviewer_keywords:
  - "External"
  - "External model"
  - "ai_generate_embeddings, external model"
dev_langs:
  - TSQL
ai-usage: ai-assisted
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# CREATE EXTERNAL MODEL (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Creates an external model object that contains the location, authentication method, and purpose of an AI model inference endpoint.

> [!NOTE]  
> `CREATE EXTERNAL MODEL` is available in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
CREATE EXTERNAL MODEL external_model_object_name
[ AUTHORIZATION owner_name ]
WITH
  ( LOCATION = '<prefix>://<path>[:<port>]'
    , API_FORMAT = '<OpenAI, Azure OpenAI, etc>'
    , MODEL_TYPE = EMBEDDINGS
    , MODEL = 'text-embedding-model-name'
    [ , CREDENTIAL = <credential_name> ]
    [ , PARAMETERS = '{"valid":"JSON"}' ]
    [ , LOCAL_RUNTIME_PATH = 'path to the ONNX Runtime files' ]
  );
```

## Arguments

### *external_model_object_name*

Specifies the user-defined name for the external model. The name must be unique within the database.

### *owner_name*

Specifies the name of the user or role that owns the external model. If you don't specify this argument, the current user becomes the owner. Depending on permissions and roles, you might need to grant explicit permission to users to use specific external models.

### LOCATION

Provides the connectivity protocol and path to the AI model inference endpoint.

### API_FORMAT

The API message format for the AI model inference endpoint provider.

Accepted values are:

- `Azure OpenAI`
- `OpenAI`
- `Ollama`
- `ONNX Runtime`

### MODEL_TYPE

The type of model accessed from the AI model inference endpoint location.

Accepted values are:

- `EMBEDDINGS`

### MODEL

The specific model hosted by the AI provider. For example, `text-embedding-ada-002`, `text-embedding-3-large`, or `o3-mini`.

### CREDENTIAL

Specifies the `DATABASE SCOPED CREDENTIAL` object used with the AI model inference endpoint. For more information about accepted credential types and naming rules, see [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md) or the [Remarks](#remarks) section of this article.

### PARAMETERS

A valid JSON string that contains runtime parameters to append to the AI model inference endpoint request message. For example:

```text
'{ "dimensions": 1536 }'
```

### LOCAL_RUNTIME_PATH

`LOCAL_RUNTIME_PATH` specifies the directory on the local SQL Server instance where the ONNX Runtime executables are located.

## Permissions

### External model creation and altering

Requires `ALTER ANY EXTERNAL MODEL` or `CREATE EXTERNAL MODEL` database permission.

For example:

```sql
GRANT CREATE EXTERNAL MODEL TO [<PRINCIPAL>];
```

Or:

```sql
GRANT ALTER ANY EXTERNAL MODEL TO [<PRINCIPAL>];
```

### External model grants

To use an external model in an AI function, a principal must be granted the ability to `EXECUTE` it.

For example:

```sql
GRANT EXECUTE ON EXTERNAL MODEL::MODEL_NAME TO [<PRINCIPAL>];
GO
```

## Retry count

If the embeddings call encounters HTTP status codes indicating temporary issues, you can configure the request to automatically retry. To specify the number of retries, add the following JSON to the `PARAMETERS` on the `EXTERNAL MODEL`. The `<number_of_retries>` should be a whole number between zero (`0`) and ten (`10`), inclusive, and can't be `NULL` or negative.

```text
{ "sql_rest_options": { "retry_count": <number_of_retries> } }
```

For example, to set the `retry_count` to 3, use the following JSON string:

```json
{ "sql_rest_options": { "retry_count": 3 } }
```

### Retry count with other parameters

You can combine retry count with other parameters as long as the JSON string is valid.

```json
{ "dimensions": 725, "sql_rest_options": { "retry_count": 5 } }
```

## Remarks

### HTTPS and TLS

For the `LOCATION` parameter, only AI model inference endpoints configured to use HTTPS with the TLS encryption protocol are supported.

### Accepted API formats and model types

The following sections outline accepted API formats for each `MODEL_TYPE`.

#### API_FORMAT for EMBEDDINGS

This table outlines the API formats and URL endpoint structures for the `EMBEDDINGS` model type. To view specific payload structures, use the link in the API Format column.

| API format | Location path format |
| --- | --- | --- |
| [Azure OpenAI](/azure/ai-services/openai/reference#embeddings) | https://{endpoint}/openai/deployments/{deployment-id}/embeddings?api-version={date} |
| [OpenAI](https://platform.openai.com/docs/api-reference/embeddings/create) | https:<span>//</span>{server_name}/v1/embeddings |
| [Ollama](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings) | https:<span>//</span>localhost:{port}/api/embed |

#### Create embedding endpoints

For more information on creating embedding endpoints, use these links for the appropriate AI model inference endpoint provider:

- [Azure OpenAI in Azure AI Foundry Models](/azure/ai-services/openai/how-to/create-resource)
- [OpenAI](https://platform.openai.com/docs/guides/embeddings)
- [Ollama](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings)

### Credential name rules for external model

The created `DATABASE SCOPED CREDENTIAL` used by an external model must follow these rules:

- Must be a valid URL

- The URL domain must be one of those domains included in the allow list.

- The URL must not contain a query string

- Protocol + Fully Qualified Domain Name (FQDN) of the called URL must match Protocol + FQDN of the credential name

- Each part of the called URL path must completely match the respective part of the URL path in the credential name.

- The credential must point to a path that's more generic than the request URL. For example, a credential created for path `https://northwind.azurewebsite.net/customers` can't be used for the URL `https://northwind.azurewebsite.net`.

#### Collation and credential name rules

[RFC 3986 Section 6.2.2.1](https://www.rfc-editor.org/rfc/rfc3986#section-6.2.2.1) states that "When a URI uses components of the generic syntax, the component syntax equivalence rules always apply; namely, that the scheme and host are case-insensitive." [RFC 7230 Section 2.7.3](https://www.rfc-editor.org/rfc/rfc7230#section-2.7.3) mentions that "all other are compared in a case-sensitive manner."

Because a collation rule is set at the database level, the following logic applies to keep the database collation rule and the RFC rules consistent. (The described rule could potentially be more restrictive than the RFC rules, for example if the database is set to use a case-sensitive collation.)

1. Check if the URL and credential match using the RFC, which means:
   - Check the scheme and host using a case-insensitive collation (`Latin1_General_100_CI_AS_KS_WS_SC`)
   - Check all other segments of the URL are compared in a case-sensitive collation (`Latin1_General_100_BIN2`)

1. Check that the URL and credential match using the database collation rules (and without doing any URL encoding).

### Managed identity

To use the [managed identity](/entra/identity/managed-identities-azure-resources/overview) of the Arc/VM host as a database level credential in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you must enable the option by using `sp_configure` with a user that is [granted the ALTER SETTINGS server-level permission](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md#permissions).

```sql
EXECUTE sp_configure 'allow server scoped db credentials', 1;
RECONFIGURE WITH OVERRIDE;
```

### SCHEMABINDING

Views created with `SCHEMABINDING` that reference an external model (such as a `SELECT` statement using `AI_GENERATE_EMBEDDINGS`) can't be dropped, and the Database Engine raises an error. To remove dependencies referencing an external model, you must first modify or drop the view definition.

## Catalog view

You can view external model metadata by querying the [sys.external_models](../../relational-databases/system-catalog-views/sys-external-models-transact-sql.md) catalog view. You must have access to a model to view its metadata.

```sql
SELECT *
FROM sys.external_models;
```

## Examples with remote endpoints

### Create an EXTERNAL MODEL with Azure OpenAI using Managed Identity

This example creates an external model of the `EMBEDDINGS` type using Azure OpenAI and uses [Managed Identity](/entra/identity/managed-identities-azure-resources/overview) for authentication.

In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, you must [connect your SQL Server to Azure Arc](../../sql-server/azure-arc/connect.md#connect-your-sql-server-to-azure-arc) and [enable the primary managed identity](../../sql-server/azure-arc/microsoft-entra-authentication-with-managed-identity.md#enable-the-primary-managed-identity).

> [!IMPORTANT]  
> If you use Managed Identity with Azure OpenAI and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], the [Cognitive Services OpenAI Contributor](/azure/role-based-access-control/built-in-roles#ai--machine-learning) role must be granted to [SQL Server's system-assigned managed identity enabled by Azure Arc](../../sql-server/azure-arc/managed-identity.md). For more information, see [Role-based access control for Azure OpenAI in Azure AI Foundry Models](/azure/ai-services/openai/how-to/role-based-access-control).

Create access credentials to Azure OpenAI using a managed identity:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
    WITH IDENTITY = 'Managed Identity', secret = '{"resourceid":"https://cognitiveservices.azure.com"}';
GO
```

Create the external model:

```sql
CREATE EXTERNAL MODEL MyAzureOpenAIModel
AUTHORIZATION CRM_User
WITH (
      LOCATION = 'https://my-azure-openai-endpoint.cognitiveservices.azure.com/openai/deployments/text-embedding-ada-002/embeddings?api-version=2024-02-01',
      API_FORMAT = 'Azure OpenAI',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'text-embedding-ada-002',
      CREDENTIAL = [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
);
```

### Create an external model with Azure OpenAI using API keys and parameters

This example creates an external model of the `EMBEDDINGS` type using Azure OpenAI and uses API Keys for authentication. The example also uses `PARAMETERS` to set the dimensions parameter at the endpoint to 725.

Create access credentials to Azure OpenAI using a key:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
    WITH IDENTITY = 'HTTPEndpointHeaders', secret = '{"api-key":"YOUR_AZURE_OPENAI_KEY"}';
GO
```

Create the external model:

```sql
CREATE EXTERNAL MODEL MyAzureOpenAIModel
AUTHORIZATION CRM_User
WITH (
      LOCATION = 'https://my-azure-openai-endpoint.cognitiveservices.azure.com/openai/deployments/text-embedding-3-small/embeddings?api-version=2024-02-01',
      API_FORMAT = 'Azure OpenAI',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'text-embedding-3-small',
      CREDENTIAL = [https://my-azure-openai-endpoint.cognitiveservices.azure.com/],
      PARAMETERS = '{"dimensions":725}'
);
```

### Create an EXTERNAL MODEL with Ollama and an explicit owner

This example creates an external model of the `EMBEDDINGS` type using Ollama hosted locally for development purposes.

```sql
CREATE EXTERNAL MODEL MyOllamaModel
AUTHORIZATION AI_User
WITH (
      LOCATION = 'https://localhost:11435/api/embed',
      API_FORMAT = 'Ollama',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'all-minilm'
);
```

### Create an EXTERNAL MODEL with OpenAI

This example creates an external model of the `EMBEDDINGS` type using the OpenAI `API_FORMAT` and HTTP header based credentials for authentication.

```sql
-- Create access credentials
CREATE DATABASE SCOPED CREDENTIAL [https://openai.com]
WITH IDENTITY = 'HTTPEndpointHeaders', secret = '{"Bearer":"YOUR_OPENAI_KEY"}';
GO

-- Create the external model
CREATE EXTERNAL MODEL MyAzureOpenAIModel
AUTHORIZATION CRM_User
WITH (
      LOCATION = 'https://api.openai.com/v1/embeddings',
      API_FORMAT = 'OpenAI',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'text-embedding-ada-002',
      CREDENTIAL = [https://openai.com]
);
```

## Example with ONNX Runtime running locally

[ONNX Runtime](https://onnxruntime.ai/) is an open-source inference engine that allows you to run machine learning models locally, making it ideal for integrating AI capabilities into SQL Server environments.

This example guides you through setting up [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] with ONNX Runtime to enable local AI-powered text embedding generation. It only applies on Windows.

> [!IMPORTANT]  
> This feature requires that [SQL Server Machine Learning Services](../../machine-learning/install/sql-machine-learning-services-windows-install.md) is installed.

### Security considerations

You can use the AI Runtime Host feature to configure and use your own LLMs and ONNX libraries with SQL Server. Because Microsoft does not validate or monitor third-party models and libraries, you are responsible for selecting appropriate models and libraries, filtering content, securing the runtime, and ensuring compliance with any applicable policies and regulations.

> [!CAUTION]
> A malicious or compromised ONNX model could exfiltrate data or execute unauthorized code. Only use models from trusted, verified sources.

To mitigate these risks, consider the following security best practices:

- **Implement strong access controls**: Ensure that only authorized users have access to sensitive data and ONNX Runtime models. Validate all models before loading them into SQL Server. Use the [principle of least privilege](/entra/identity-platform/secure-least-privileged-access), as well as database roles and privileges.
- **Monitor and audit access**: Regularly monitor and audit access to the database and `AI_GENERATE_EMBEDDINGS` function calls to detect suspicious activity.
- **Conduct regular security assessments**: Perform vulnerability scans and security reviews to identify and mitigate potential risks.

### Step 1: Enable developer preview features on SQL Server 2025

Run the following Transact-SQL (T-SQL) command to enable [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] preview features in the database you would like use for this example:

```sql
ALTER DATABASE SCOPED CONFIGURATION
SET PREVIEW_FEATURES = ON;
```

### Step 2: Enable the local AI runtime on SQL Server 2025

Enable external AI runtimes by running the following T-SQL query:

```sql
EXECUTE sp_configure 'external AI runtimes enabled', 1;
RECONFIGURE WITH OVERRIDE;
```

### Step 3: Set up the ONNX Runtime library

Create a directory on the SQL Server instance to hold the ONNX Runtime library files. In this example, `C:\onnx_runtime` is used.

You can use the following commands to create the directory:

```powershell
cd C:\
mkdir onnx_runtime
```

Next, download a version of [ONNX Runtime](https://github.com/microsoft/onnxruntime/releases) (1.19 or greater) that's appropriate for your operating system. After unzipping the download, copy the `onnxruntime.dll` (located in the lib directory) to the `C:\onnx_runtime` directory that was created.

### Step 4: Set up the tokenization library

Download and build [the `tokenizers-cpp` library](https://github.com/mlc-ai/tokenizers-cpp/tree/main) from GitHub. Once the dll is created, place the tokenizer in the `C:\onnx_runtime` directory.

The tokenizer must be compiled as a shared dynamic link library using MSVC, and must export a specific entry point:

```cpp
#include "tokenizers_cpp.h"     // for example: `tokenizers-cpp\include\tokenizers_cpp.h`
#include <string>
#include <vector>
 
extern "C" __declspec(dllexport)
void LoadBlobJsonAndEncode(
  const std::string& json_blob, // contents of `tokenizer.json`
  const std::string& text,      // input text to tokenize
  std::vector<int>& out_ids     // output token IDs (the embeddings)
) {
  // ~~ Implement according to current API of `tokenizers-cpp` ~~
  // auto tok = tokenizers::Tokenizer::FromBlobJSON(json_blob);
  // out_ids = tok->Encode(text);
}
```

> [!NOTE]  
> The exact signature of this export may change. Ensure the created dll is named **tokenizers_cpp.dll**

### Step 5: Download the ONNX model

Start by creating the `model` directory in `C:\onnx_runtime\`.

```powershell
cd C:\onnx_runtime
mkdir model
```

This example uses the `all-MiniLM-L6-v2-onnx` model, which can be downloaded from [Hugging Face](https://huggingface.co/nsense/all-MiniLM-L6-v2-onnx).

Clone the repository into the `C:\onnx_runtime\model` directory with the following [git](https://git-scm.com/downloads) command:

*If not installed, you can download git from the following [download link](https://git-scm.com/downloads) or via winget (winget install Microsoft.Git)*

```console
cd C:\onnx_runtime\model
git clone https://huggingface.co/nsense/all-MiniLM-L6-v2-onnx
```

### Step 6: Set directory permissions

Use the following PowerShell script to provide the MSSQLLaunchpad user access to the ONNX Runtime directory:

```powershell
$AIExtPath = "C:\onnx_runtime";
$Acl = Get-Acl -Path $AIExtPath
$AccessRule = New-Object System.Security.AccessControl.FileSystemAccessRule("MSSQLLaunchpad", "FullControl", "ContainerInherit,ObjectInherit", "None", "Allow")
$Acl.AddAccessRule($AccessRule)
Set-Acl -Path $AIExtPath -AclObject $Acl
```

### Step 7: Create the external model

Run the following query to register your ONNX model as an external model object:

***The 'PARAMETERS' value used here is a placeholder needed for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].***

```sql
CREATE EXTERNAL MODEL myLocalOnnxModel
WITH (
    LOCATION = 'C:\onnx_runtime\model\all-MiniLM-L6-v2-onnx',
    API_FORMAT = 'ONNX Runtime',
    MODEL_TYPE = EMBEDDINGS,
    MODEL = 'allMiniLM',
    PARAMETERS = '{"valid":"JSON"}',
    LOCAL_RUNTIME_PATH = 'C:\onnx_runtime\'
);
```

- `LOCATION` should point to the directory containing `model.onnx` and `tokenizer.json` files.
- `LOCAL_RUNTIME_PATH` should point to directory containing `onnxruntime.dll` and `tokenizer_cpp.dll` files.

### Step 8: Generate embeddings

Use the `ai_generate_embeddings` function to test the model by running the following query:

```sql
SELECT AI_GENERATE_EMBEDDINGS(N'Test Text' USE MODEL myLocalOnnxModel);
```

This command launches the `AIRuntimeHost`, load the required DLLs, and processes the input text.

The result from the previous query is an array of embeddings:

```output
[0.320098,0.568766,0.154386,0.205526,-0.027379,-0.149689,-0.022946,-0.385856,-0.039183...]
```

### Enable XEvent system logging

Run the following query to enable system logging for troubleshooting.

```sql
CREATE EVENT SESSION newevt
ON SERVER
ADD EVENT ai_generate_embeddings_airuntime_trace
(
    ACTION (sqlserver.sql_text, sqlserver.session_id)
)
ADD TARGET package0.ring_buffer
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    TRACK_CAUSALITY = ON,
    STARTUP_STATE = OFF
);
GO

ALTER EVENT SESSION newevt ON SERVER STATE = START;
GO
```

Next, use this query see the captured system logs:

```sql
SELECT event_data.value('(@name)[1]', 'varchar(100)') AS event_name,
       event_data.value('(@timestamp)[1]', 'datetime2') AS [timestamp],
       event_data.value('(data[@name = "model_name"]/value)[1]', 'nvarchar(200)') AS model_name,
       event_data.value('(data[@name = "phase_name"]/value)[1]', 'nvarchar(100)') AS phase,
       event_data.value('(data[@name = "message"]/value)[1]', 'nvarchar(max)') AS message,
       event_data.value('(data[@name = "request_id"]/value)[1]', 'nvarchar(max)') AS session_id,
       event_data.value('(data[@name = "error_code"]/value)[1]', 'bigint') AS error_code
FROM (SELECT CAST (target_data AS XML) AS target_data
      FROM sys.dm_xe_sessions AS s
           INNER JOIN sys.dm_xe_session_targets AS t
               ON s.address = t.event_session_address
      WHERE s.name = 'newevt'
            AND t.target_name = 'ring_buffer') AS data
CROSS APPLY target_data.nodes('//RingBufferTarget/event') AS XEvent(event_data);
```

### Clean up

To remove the external model object, run the following T-SQL statement:

```sql
DROP EXTERNAL MODEL myLocalOnnxModel;
```

To remove the directory permissions, run the following PowerShell commands:

```powershell
$Acl.RemoveAccessRule($AccessRule)
Set-Acl -Path $AIExtPath -AclObject $Acl
```

Finally, delete the `C:/onnx_runtime` directory.

## Related content

- [ALTER EXTERNAL MODEL (Transact-SQL)](alter-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](drop-external-model-transact-sql.md)
- [AI_GENERATE_EMBEDDINGS (Transact-SQL)](../functions/ai-generate-embeddings-transact-sql.md)
- [AI_GENERATE_CHUNKS (Transact-SQL)](../functions/ai-generate-chunks-transact-sql.md)
- [sys.external_models (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-models-transact-sql.md)
- [Create and deploy an Azure OpenAI in Azure AI Foundry Models resource](/azure/ai-services/openai/how-to/create-resource)
- [Server configuration options](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Role-based access control for Azure OpenAI in Azure AI Foundry Models](/azure/ai-services/openai/how-to/role-based-access-control)
