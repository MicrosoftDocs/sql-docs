---
title: "AI_GENERATE_EMBEDDINGS (Transact-SQL)"
description: The AI_GENERATE_EMBEDDINGS function creates vector arrays for data.
author: jettermctedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 01/06/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sql-ai
  - ignite-2025
f1_keywords:
  - "ai_generate_embeddings_TSQL"
  - "ai_generate_embeddings"
helpviewer_keywords:
  - "ai_generate_embeddings"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# AI_GENERATE_EMBEDDINGS (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

`AI_GENERATE_EMBEDDINGS` is a built-in function that creates embeddings (vector arrays) using a precreated AI model definition stored in the database.

> [!NOTE]  
> `AI_GENERATE_EMBEDDINGS` is available in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with SQL Server 2025 and the **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
AI_GENERATE_EMBEDDINGS ( source USE MODEL model_identifier [ PARAMETERS optional_json_request_body_parameters ] )
```

## Arguments

#### *source*

An [expression](../language-elements/expressions-transact-sql.md) of any character type (for example, **nvarchar**, **varchar**, **nchar**, or **char**).

#### *model_identifier*

The name of an [external model](../statements/create-external-model-transact-sql.md) defined as an `EMBEDDINGS` type that is used to create the embeddings vector array.

For more information, see [CREATE EXTERNAL MODEL](../statements/create-external-model-transact-sql.md).

#### *optional_json_request_body_parameters*

A valid JSON formatted list of additional parameters. These parameters are appended to the REST request message body before being sent to the external model's endpoint location. These parameters depend on what the external model's endpoint supports and accepts.

## Return types

`AI_GENERATE_EMBEDDINGS` returns a single-column table whose rows are the generated embedding vector arrays returned as JSON.

### Return format

The format of the returned JSON is as follows:

```json
[
    0.0023929428,
    0.00034713413,
    -0.0023142276,
    -0.025654867,
    -0.011492423,
    0.0010358924,
    -0.014836246,
    0.0035484824,
    0.000045630233,
    -0.027581815,
    0.023816079,
    0.005012586,
    -0.027732948,
    -0.010088143,
    ...
    -0.014571763
]
```

## Remarks

### Prerequisites

You must meet the following prerequisites to use `AI_GENERATE_EMBEDDINGS`:

- Enable `sp_invoke_external_endpoint` on the database, with the following command:

  ```sql
  EXECUTE sp_configure 'external rest endpoint enabled', 1;
  RECONFIGURE WITH OVERRIDE;
  ```

- Create an [external model](../statements/create-external-model-transact-sql.md) of the `EMBEDDINGS` type, accessible via the correct grants, roles, and/or permissions.

### Optional parameters

The parameter `optional_json_request_body_parameters` in `AI_GENERATE_EMBEDDINGS` is used when an endpoint parameter needs to be added to the body of the embeddings request message. Adding an optional parameter overrides the value at runtime if that parameter is defined in the model definition.

For example, if the [external model](../statements/create-external-model-transact-sql.md) contains the parameter for `dimensions` set to 1536, by passing that parameter in the `optional_json_request_body_parameters` at runtime with a new value.

In the following example, the `dimensions` parameter on the model is overridden:

```sql
json_object("dimensions":755)
```

The value passed into `optional_json_request_body_parameters` must be valid JSON.

### Create embedding endpoints

For more information on creating embedding endpoints, review the process for [Azure OpenAI in Azure AI Foundry Models](/azure/ai-services/openai/how-to/create-resource), [OpenAI](https://platform.openai.com/docs/guides/embeddings), or [Ollama](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings).

### Extended Events (XEvent)

`AI_GENERATE_EMBEDDINGS` has an extended event (`ai_generate_embeddings_summary`) that can be enabled for troubleshooting. It contains information about the REST request and response such as status code, any errors it encountered, and the model name used. The extended event `external_rest_endpoint_summary` contains additional information that can be for troubleshooting and debugging REST requests.

## Examples

### A. Create embeddings with a SELECT statement

The following example shows how to use the `AI_GENERATE_EMBEDDINGS` function with a select statement that returns results in a JSON array.

```sql
SELECT id,
       AI_GENERATE_EMBEDDINGS(large_text USE MODEL MyAzureOpenAIModel)
FROM myTable;
```

### B. Create embeddings with a SELECT statement using AI_GENERATE_CHUNKS

The following example shows how to use the `AI_GENERATE_EMBEDDINGS` function with the [AI_GENERATE_CHUNKS](ai-generate-chunks-transact-sql.md) function to pass text broken up in specified chunk sizes with a select statement that returns vector array results.

```sql
SELECT id,
       title,
       large_text,
       AI_GENERATE_EMBEDDINGS(c.chunk_text USE MODEL MyAzureOpenAIModel)
FROM myTable
CROSS APPLY AI_GENERATE_CHUNKS (
    SOURCE = large_text,
    CHUNK_TYPE = FIXED,
    CHUNK_SIZE = 10
) AS c;
```

### C. Create embeddings with a table update

The following example shows how to use the `AI_GENERATE_EMBEDDINGS` function with a table update statement to return the vector array results into a vector data type column.

```sql
UPDATE t
    SET myEmbeddings = AI_GENERATE_EMBEDDINGS(t.text USE MODEL MyAzureOpenAIModel)
FROM myTable AS t;
```

### D. Create embeddings with a SELECT statement and PARAMETERS

The following example shows how to use the `AI_GENERATE_EMBEDDINGS` function with a select statement and passing optional parameters to the endpoint, which returns vector array results.

```sql
DECLARE @params JSON = N'{"dimensions":768}'
SELECT id,
       AI_GENERATE_EMBEDDINGS(large_text USE MODEL MyAzureOpenAIModel PARAMETERS @params)
FROM myTable;
```

### E. Retry embeddings generation with `retry_count` PARAMETERS option

If the embeddings call encounters HTTP status codes indicating temporary issues, you can configure the request to automatically retry.

To specify the number of retries, add the following JSON to the `PARAMETERS` option. This value should be a positive integer between zero (`0`) and ten (`10`) inclusive, and can't be `NULL`.

> [!NOTE]  
> If a `retry_count` value is specified in the `AI_GENERATE_EMBEDDINGS` query, it overrides the `retry_count` (if defined) in the external model's configuration.

```sql
DECLARE @params JSON = N'{"sql_rest_options":{"retry_count":10}} '
SELECT id,
       AI_GENERATE_EMBEDDINGS(large_text USE MODEL MyAzureOpenAIModel PARAMETERS @params)
FROM myTable;
```

### F. A full example with model creation, chunking and embedding generation

### [Azure OpenAI API Key](#tab/request-headers)

The following example demonstrates an end-to-end process for making your data AI-ready using Azure OpenAI API Key:

1. Use [CREATE EXTERNAL MODEL](../statements/create-external-model-transact-sql.md) to register and make your embedding model accessible.

1. Split the dataset into smaller chunks with [AI_GENERATE_CHUNKS](ai-generate-chunks-transact-sql.md), so the data fits within the model's context window and improves retrieval accuracy.

1. Generate embeddings using `AI_GENERATE_EMBEDDINGS`.

1. Insert the results into a table with a [vector data type](../data-types/vector-data-type.md).

> [!NOTE]  
> Replace `<password>` with a valid password.

Enable the external REST endpoint invocation on the database server:

```sql
EXECUTE sp_configure 'external rest endpoint enabled', 1;
RECONFIGURE WITH OVERRIDE;
GO
```

Create a database master key:

```sql
IF NOT EXISTS (SELECT *
               FROM sys.symmetric_keys
               WHERE [name] = '##MS_DatabaseMasterKey##')
    BEGIN
        CREATE MASTER KEY ENCRYPTION BY PASSWORD = N'<password>';
    END
GO
```

Create access credentials to Azure OpenAI using a key:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
    WITH IDENTITY = 'HTTPEndpointHeaders', secret = '{"api-key":"YOUR_AZURE_OPENAI_KEY"}';
GO
```

Create an external model to call the Azure OpenAI embeddings REST endpoint:

```sql
CREATE EXTERNAL MODEL MyAzureOpenAIModel
WITH (
      LOCATION = 'https://my-azure-openai-endpoint.cognitiveservices.azure.com/openai/deployments/text-embedding-ada-002/embeddings?api-version=2023-05-15',
      API_FORMAT = 'Azure OpenAI',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'text-embedding-ada-002',
      CREDENTIAL = [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
);
```

Create a table with text to chunk and insert data:

```sql
CREATE TABLE textchunk
(
    text_id INT IDENTITY (1, 1) PRIMARY KEY,
    text_to_chunk NVARCHAR (MAX)
);
GO

INSERT INTO textchunk (text_to_chunk)
VALUES ('All day long we seemed to dawdle through a land which was full of beauty of every kind. Sometimes we saw little towns or castles on the top of steep hills such as we see in old missals; sometimes we ran by rivers and streams which seemed from the wide stony margin on each side of them to be subject to great floods.'),
       ('My Friend, Welcome to the Carpathians. I am anxiously expecting you. Sleep well to-night. At three to-morrow the diligence will start for Bukovina; a place on it is kept for you. At the Borgo Pass my carriage will await you and will bring you to me. I trust that your journey from London has been a happy one, and that you will enjoy your stay in my beautiful land. Your friend, DRACULA')
GO
```

Create a new table to hold the chunked text and vector embeddings:

```sql
CREATE TABLE text_embeddings
(
    embeddings_id INT IDENTITY (1, 1) PRIMARY KEY,
    chunked_text NVARCHAR (MAX),
    vector_embeddings VECTOR(1536)
);
```

Insert the chunked text and vector embeddings into the text_embeddings table using `AI_GENERATE_CHUNKS` and `AI_GENERATE_EMBEDDINGS`:

```sql
INSERT INTO text_embeddings (chunked_text, vector_embeddings)
SELECT c.chunk,
       AI_GENERATE_EMBEDDINGS(c.chunk USE MODEL MyAzureOpenAIModel)
FROM textchunk AS t
CROSS APPLY AI_GENERATE_CHUNKS (
    SOURCE = t.text_to_chunk,
    CHUNK_TYPE = FIXED,
    CHUNK_SIZE = 100
) AS c;
```

View the results

```sql
SELECT *
FROM text_embeddings;
```

### [Managed Identity](#tab/managed-identity)

The following example demonstrates an end-to-end process for making your data AI-ready using Managed Identity:

1. Enable [Managed Identity](../statements/create-external-model-transact-sql.md#managed-identity). This option applies to SQL Server Azure Arc / Azure Virtual Machine (VM) based deployments.

1. Use [CREATE EXTERNAL MODEL with Azure OpenAI using Managed Identity](../statements/create-external-model-transact-sql.md#create-an-external-model-with-azure-openai-using-managed-identity) to register and make your embedding model accessible using Managed Identity.

1. Split the dataset into smaller chunks with [AI_GENERATE_CHUNKS](ai-generate-chunks-transact-sql.md), so the data fits within the model's context window and improves retrieval accuracy.

1. Generate embeddings using `AI_GENERATE_EMBEDDINGS`.

1. Insert the results into a table with a [vector data type](../data-types/vector-data-type.md).

> [!NOTE]  
> Replace `<password>` with a [valid password](../statements/create-master-key-transact-sql.md#password-password).

Enable managed identity for authentication:

```sql
EXECUTE sp_configure 'allow server scoped db credentials', 1;
RECONFIGURE WITH OVERRIDE;
```

Enable the external REST endpoint invocation on the database server:

```sql
EXECUTE sp_configure 'external rest endpoint enabled', 1;
RECONFIGURE WITH OVERRIDE;
GO
```

Create a database master key:

```sql
IF NOT EXISTS (SELECT *
               FROM sys.symmetric_keys
               WHERE [name] = '##MS_DatabaseMasterKey##')
    BEGIN
        CREATE MASTER KEY ENCRYPTION BY PASSWORD = N'<password>';
    END
GO
```

Create access credentials to Azure OpenAI using a key:

```sql
CREATE DATABASE SCOPED CREDENTIAL [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
    WITH IDENTITY = 'Managed Identity', secret = '{"resourceid":"https://cognitiveservices.azure.com"}';
GO
```

Create an external model to call the Azure OpenAI embeddings REST endpoint:

```sql
CREATE EXTERNAL MODEL MyAzureOpenAIModel
WITH (
      LOCATION = 'https://my-azure-openai-endpoint.cognitiveservices.azure.com/openai/deployments/text-embedding-ada-002/embeddings?api-version=2023-05-15',
      API_FORMAT = 'Azure OpenAI',
      MODEL_TYPE = EMBEDDINGS,
      MODEL = 'text-embedding-ada-002',
      CREDENTIAL = [https://my-azure-openai-endpoint.cognitiveservices.azure.com/]
);
```

Create a table with text to chunk and insert data:

```sql
CREATE TABLE textchunk
(
    text_id INT IDENTITY (1, 1) PRIMARY KEY,
    text_to_chunk NVARCHAR (MAX)
);
GO

INSERT INTO textchunk (text_to_chunk)
VALUES ('All day long we seemed to dawdle through a land which was full of beauty of every kind. Sometimes we saw little towns or castles on the top of steep hills such as we see in old missals; sometimes we ran by rivers and streams which seemed from the wide stony margin on each side of them to be subject to great floods.'),
       ('My Friend, Welcome to the Carpathians. I am anxiously expecting you. Sleep well to-night. At three to-morrow the diligence will start for Bukovina; a place on it is kept for you. At the Borgo Pass my carriage will await you and will bring you to me. I trust that your journey from London has been a happy one, and that you will enjoy your stay in my beautiful land. Your friend, DRACULA')
GO
```

Create a new table to hold the chunked text and vector embeddings:

```sql
CREATE TABLE text_embeddings
(
    embeddings_id INT IDENTITY (1, 1) PRIMARY KEY,
    chunked_text NVARCHAR (MAX),
    vector_embeddings VECTOR(1536)
);
```

Insert the chunked text and vector embeddings into the text_embeddings table using `AI_GENERATE_CHUNKS` and `AI_GENERATE_EMBEDDINGS`:

```sql
INSERT INTO text_embeddings (chunked_text, vector_embeddings)
SELECT c.chunk,
       AI_GENERATE_EMBEDDINGS(c.chunk USE MODEL MyAzureOpenAIModel)
FROM textchunk AS t
CROSS APPLY AI_GENERATE_CHUNKS (
    SOURCE = t.text_to_chunk,
    CHUNK_TYPE = FIXED,
    CHUNK_SIZE = 100
) AS c;
```

View the results:

```sql
SELECT *
FROM text_embeddings;
```

---

## Related content

- [CREATE EXTERNAL MODEL (Transact-SQL)](../statements/create-external-model-transact-sql.md)
- [ALTER EXTERNAL MODEL (Transact-SQL)](../statements/alter-external-model-transact-sql.md)
- [DROP EXTERNAL MODEL (Transact-SQL)](../statements/drop-external-model-transact-sql.md)
- [AI_GENERATE_CHUNKS (Transact-SQL)](ai-generate-chunks-transact-sql.md)
- [sp_invoke_external_rest_endpoint](../../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md)
- [Create and deploy an Azure OpenAI in Azure AI Foundry Models resource](/azure/ai-services/openai/how-to/create-resource)
- [Connect your SQL Server to Azure Arc](../../sql-server/azure-arc/connect.md#connect-your-sql-server-to-azure-arc)
